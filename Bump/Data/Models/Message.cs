using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bump.Data.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public BumpUser Author { get; set; }

        [Required]
        [MinLength(1)]
        public string Content { get; set; }
        
        [Required]
        public Theme Theme { get; set; }
        
    }
}