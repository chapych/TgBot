using System.Net.Http.Json;
using Entities.Enums;
using Infrastructure.KudaGo.Configurations;
using Infrastructure.KudaGo.Services;
using Microsoft.Extensions.Options;
using UseCase.KudaGo;
using Event = Entities.Entities.Event;

namespace Infrastructure.KudaGo;

public class KudaGoService : IKudaGoService
{
    private readonly HttpClient _httpClient;
    private readonly ITypeConverter _typeConverter;
    private readonly string _apiUriAndVersion;

    private readonly int _daysBefore = 1;
    private readonly int _daysAfter = 7;

    private const string ControllerName = "events";
    private const string Fields = "fields=id,place,location,dates,description,coords,address,title,subway,price";
    private const string Expand = "expand=place";
    private const string Categories = "categories=";
    private const string Location = "location=msk";
    private const string PageSize = "page_size=";
    private const string ActualSince = "actual_since=";
    private const string ActualUntil = "actual_until=";
    private const string TextFormat = "text_format=text";


    public KudaGoService(IOptions<KudaGoSettings> settings, HttpClient httpClient, ITypeConverter typeConverter)
    {
        _httpClient = httpClient;
        _typeConverter = typeConverter;

        _apiUriAndVersion = settings.Value.UriAndVersion;
    }

    public async Task<List<Event>> GetEventsAsync(KudaGoRequest request)
    {
        try
        {
            var pageSize = PageSize + request.Count;
            var categoriesField = GetCategoriesField(request.Categories);
            var dataSince = request.Date.AddDays(-_daysBefore);
            var dataUntil = request.Date.AddDays(_daysAfter);
            var actualSince = ActualSince + ((DateTimeOffset)dataSince).ToUnixTimeSeconds();
            var actualUntil = ActualUntil + ((DateTimeOffset)dataUntil).ToUnixTimeSeconds();

            var endpoint = GetEndpoint(pageSize, actualSince, actualUntil, categoriesField);
            var responseData = await GetResponseData(endpoint);

            if (responseData != null)
            {
                var events = await GetEvents(responseData, request.Count);
                return events;
            }
            throw new InvalidOperationException();
        }
        catch
        {
            //
        }

        return Enumerable.Empty<Event>().ToList();
    }

    private async Task<ResponseData?> GetResponseData(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        var responseData = await response.Content.ReadFromJsonAsync<ResponseData>();
        return responseData;
    }

    private async Task<List<Event>> GetEvents(ResponseData responseData, int count)
    {
        var events = new List<Event>();

        var currentResponseData = responseData;
        while (true)
        {
            var converted = responseData.Events.Select(x => new Event(x.Name, x.Description));
            events.AddRange(converted);
            var eventsCount = events.Count;
            if (eventsCount == count)
                break;

            var request = currentResponseData?.Next;
            if (request == null)
                break;

            var nextResponse = await _httpClient.GetAsync(request);
            currentResponseData = await nextResponse.Content.ReadFromJsonAsync<ResponseData>();
        }

        return events;
    }

    private string GetEndpoint(string pageSize, string actualSince, string actualUntil, string categoriesField)
    {
        return _apiUriAndVersion +
               "/" +
               ControllerName +
               "/?" +
               Location +
               "&" +
               pageSize +
               //"&" +
               //Fields +
               //"&" +
               //Expand + 
               "&" +
               actualSince +
               "&" +
               actualUntil +
               "&" +
               categoriesField +
               "&" + 
               TextFormat;
    }

    private string GetCategoriesField(Category[]? categories)
    {
        if (categories == null) 
            return Categories;

        var categoriesField = Categories;
        var length = categories.Length;
        for (var index = 0; index < length; index++)
        {
            var category = categories[index];
            var convertToString = _typeConverter.ConvertToString(category);

            categoriesField += convertToString;
            if(NotLastIndex(index))
                categoriesField += ",";
        }

        return categoriesField;

        bool NotLastIndex(int index)
        {
            return index != length - 1;
        }
    }
}