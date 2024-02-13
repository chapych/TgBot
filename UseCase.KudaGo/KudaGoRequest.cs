using Entities.Enums;

namespace UseCase.KudaGo;

public class KudaGoRequest
{
    public int Count { get; set; }
    public Category[] Categories { get; set; } = null!;
    public DateTime Date { get; set; }
}