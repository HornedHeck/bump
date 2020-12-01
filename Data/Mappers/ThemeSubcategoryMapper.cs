using System.Collections.Generic;
using System.Linq;
using Entities;
using LThemeSubcategory = Data.Models.ThemeSubcategory;
using ThemeCategory = Data.Models.ThemeCategory;

namespace Data.Mappers
{
    internal static class ThemeSubcategoryMapper
    {
        internal static ThemeSubcategory Map(this LThemeSubcategory item) => new ThemeSubcategory
        {
            Id = item.Id,
            Name = item.Name,
            Category = item.Category.Map()
        };

        internal static IEnumerable<ThemeSubcategory> Map(this IEnumerable<LThemeSubcategory> items) =>
            items.Select(Map);

        internal static LThemeSubcategory Map(this ThemeSubcategory entity, ThemeCategory category) =>
            new LThemeSubcategory
            {
                Name = entity.Name,
                Category = category
            };
    }
}