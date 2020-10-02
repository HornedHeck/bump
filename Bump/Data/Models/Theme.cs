using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bump.Data.Models
{
    public class Theme
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MinLength(5)]
        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public BumpUser Author { get; set; }

        public List<Message> Messages { get; set; }
        
        public ThemeSubcategory Subcategory { get; set; }
    }
}