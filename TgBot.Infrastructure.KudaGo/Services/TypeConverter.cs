using Entities.Enums;

namespace Infrastructure.KudaGo.Services
{
    public class TypeConverter : ITypeConverter
    {
        private readonly Dictionary<Category, string> _categoriesToString = new()
        {
            [Category.BusinessEvents] = "business-events",
            [Category.Cinema] = "cinema",
            [Category.Concert] = "concert"
        };

        public string ConvertToString(Category category) => _categoriesToString[category];
    }
}
