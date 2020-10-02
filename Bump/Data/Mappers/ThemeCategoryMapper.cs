using System.Collections.Generic;
using System.Linq;
using Entities;
using LThemeCategory = Bump.Data.Models.ThemeCategory;

namespace Bump.Data.Mappers
{
    public static class ThemeCategoryMapper
    {
        public static ThemeCategory Map(this LThemeCategory item) =>
            new ThemeCategory
            {
                Id = item.Id,
                Name = item.Name
            };

        public static IEnumerable<ThemeCategory> Map(this IEnumerable<LThemeCategory> items) => items.Select(Map);

        public static LThemeCategory Map(this ThemeCategory entity) => new LThemeCategory
        {
            Name = entity.Name
        };
    }
}