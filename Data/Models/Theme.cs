using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Theme
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MinLength(5)]
        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        
        public virtual BumpUser Author { get; set; }
        
        public virtual List<Message> Messages { get; set; }
        
        public virtual List<Media> Media { get; set; }
        
        [Required]
        public virtual ThemeSubcategory Subcategory { get; set; }
        
        [Required]
        public DateTime CreationTime { get; set; }
        
    }
}