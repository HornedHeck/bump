using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bump.Data.Models
{
    public class Media
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 2)]
        public int Type { get; set; }
    }
}