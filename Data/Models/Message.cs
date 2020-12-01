using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] public BumpUser Author { get; set; }

        [Required] [MinLength(1)] public string Content { get; set; }

        [Required] public Theme Theme { get; set; }

        [Required] public DateTime CreationTime { get; set; }

        public List<Media> Media { get; set; }

        public List<Vote> Votes { get; set; }
    }
}