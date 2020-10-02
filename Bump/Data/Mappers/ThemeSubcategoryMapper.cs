using System.Collections.Generic;
using System.Linq;
using Entities;
using LThemeSubcategory = Bump.Data.Models.ThemeSubcategory;
using ThemeCategory = Bump.Data.Models.ThemeCategory;

namespace Bump.Data.Mappers
{
    public static class ThemeSubcategoryMapper
    {
        public static ThemeSubcategory Map(this LThemeSubcategory item) => new ThemeSubcategory
        {
            Id = item.Id,
            Name = item.Name,
            Category = item.Category.Map()
        };

        public static IEnumerable<ThemeSubcategory> Map(this IEnumerable<LThemeSubcategory> items) =>
            items.Select(Map);

        public static LThemeSubcategory Map(this ThemeSubcategory entity, ThemeCategory category) =>
            new LThemeSubcategory
            {
                Name = entity.Name,
                Category = category
            };
    }
}