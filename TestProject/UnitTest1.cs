using System.ComponentModel;
using Entities.Enums;
using Infrastructure.KudaGo;
using Infrastructure.KudaGo.Configurations;
using Infrastructure.KudaGo.Services;
using Microsoft.Extensions.Options;
using UseCase.KudaGo;
using TypeConverter = Infrastructure.KudaGo.Services.TypeConverter;

namespace TestProject
{
    public class Tests
    {
        private IKudaGoService _kudaGoService = null!;

        [SetUp]
        public void Setup()
        {
            var settings = new KudaGoSettings()
            {
                UriAndVersion = "https://kudago.com/public-api/v1.4"
            };
            var options = Options.Create(settings);

            _kudaGoService = new KudaGoService(options, new HttpClient(), new TypeConverter());
        }

        [Test]
        public async Task Test1()
        {
            var request = new KudaGoRequest{ 
                Count = 3, 
                Categories = new Category[] { Category.BusinessEvents, Category.Concert, Category.Cinema }, 
                Date = DateTime.Now };

        var events = await _kudaGoService.GetEventsAsync(request);
            Assert.That(events.Count, Is.EqualTo(3));
        }
    }
}