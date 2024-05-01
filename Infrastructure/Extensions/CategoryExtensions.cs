using KudGo.Entities.Enums;

namespace TgBot.Infrastructure.Extensions
{
    internal static class CategoryExtensions
    {
        public static Category ToKudaGoCategory(this Entities.Enums.Category category)
        {
            return category switch
            {
                Entities.Enums.Category.BusinessEvents => Category.BusinessEvents,
                Entities.Enums.Category.Cinema => Category.Cinema,
                Entities.Enums.Category.Concert => Category.Concert,
                _ => Category.None
            };
        }

        public static Entities.Enums.Category ToDomainCategory(this Category category)
        {
            return category switch
            {
                Category.BusinessEvents => Entities.Enums.Category.BusinessEvents,
                Category.Cinema => Entities.Enums.Category.Cinema,
                Category.Concert => Entities.Enums.Category.Concert,
                Category.None => Entities.Enums.Category.None,
                _ => Entities.Enums.Category.None
            };
        }
    }
}
