using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bump.Auth;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bump.Models
{
    public class MessageVM
    {
        public int Id { get; set; }

        public int Theme { get; set; }

        public BumpUser Author { get; set; }

        public string Content { get; set; }

        public List<long> Media { get; set; } = new List<long>();

        public string Method { get; set; }

        public List<Vote> Votes { get; set; }
    }

    public static class MessageVmMapper
    {
        public static async Task<MessageVM> ToVm(this Message entity, UserManager<BumpUser> userManager, string method)
        {
            return new MessageVM
            {
                Id = entity.Id,
                Author = await userManager.FindByIdAsync(entity.Author.Id),
                Content = entity.Content,
                Method = method,
                Theme = entity.Theme,
                Votes = entity.Votes,
                Media = entity.Media.ToList()
            };
        }
    }
}