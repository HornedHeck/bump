using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable UnusedMember.Global

namespace Data.Models
{
    public class Vote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string UserId { get; set; }
    }
}