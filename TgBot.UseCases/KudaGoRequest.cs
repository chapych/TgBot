using KudaGo.Models.Enums;

namespace UseCase.KudaGo;

public class KudaGoRequest
{
    public int Count { get; set; }
    public Category[] CatCategories { get; set; }
    public DateTime Date { get; set; }
}