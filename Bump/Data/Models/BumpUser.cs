using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;

namespace Bump.Data.Models
{
    public class BumpUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(7)]
        [MaxLength(20)]
        public string Login { get; set; }

        [Required]
        [MinLength(7)]
        public string Name { get; set; }

        public User Convert()
        {
            return new User(Id, Name, Login);
        }
    }
}