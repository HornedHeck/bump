using System.Collections.Generic;
using Entities;

namespace Bump.Models
{
    public class SubcategoryVM
    {
        public long Id { get; set; }

        public List<Theme> Themes { get; set; }
    }
}