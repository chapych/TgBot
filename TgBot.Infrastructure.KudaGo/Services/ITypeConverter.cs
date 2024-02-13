using Entities.Enums;

namespace Infrastructure.KudaGo.Services;

public interface ITypeConverter
{
    string ConvertToString(Category category);
}