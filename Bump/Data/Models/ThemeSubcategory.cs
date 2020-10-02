using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bump.Data.Models
{
    public class ThemeSubcategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public ThemeCategory Category { get; set; }
    }
}