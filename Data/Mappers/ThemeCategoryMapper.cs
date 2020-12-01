using System.Collections.Generic;
using System.Linq;
using Entities;
using LThemeCategory = Data.Models.ThemeCategory;

namespace Data.Mappers
{
    internal static class ThemeCategoryMapper
    {
        internal static ThemeCategory Map(this LThemeCategory item) =>
            new ThemeCategory
            {
                Id = item.Id,
                Name = item.Name
            };

        internal static IEnumerable<ThemeCategory> Map(this IEnumerable<LThemeCategory> items) => items.Select(Map);

        internal static LThemeCategory Map(this ThemeCategory entity) => new LThemeCategory
        {
            Name = entity.Name
        };
    }
}