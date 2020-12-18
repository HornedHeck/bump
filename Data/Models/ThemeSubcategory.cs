using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Data.Models
{
    public class ThemeSubcategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } = 0;

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public virtual ThemeCategory Category { get; set; }
    }
}