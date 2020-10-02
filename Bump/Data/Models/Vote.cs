using System.ComponentModel.DataAnnotations.Schema;

namespace Bump.Data.Models
{
    public class Vote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string UserId { get; set; }
    }
}