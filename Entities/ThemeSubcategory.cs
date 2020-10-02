using System.Collections.Generic;

namespace Entities
{
    public class ThemeSubcategory
    {
        public long Id { get; set; }
        public ThemeCategory Category { get; set; }

        public string Name { get; set; }
    }
}