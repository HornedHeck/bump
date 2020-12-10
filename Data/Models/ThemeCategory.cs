using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Data.Models
{
    public class ThemeCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}