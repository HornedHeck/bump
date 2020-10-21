using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Resources.Strings;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Bump.Models
{
    public class MessageVM
    {
        public int Id { get; set; }

        public long Theme { get; set; }

        public BumpUser Author { get; set; }

        [LRequired]
        [Display(ResourceType = typeof(CommonStrings), Name = "Content")]
        [LStringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }

        public List<long> Media { get; set; } = new List<long>();

        public string Method { get; set; }

        public List<Vote> Votes { get; set; }

        public DateTime CreationTime { get; set; }
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
                Votes = entity.Votes ?? new List<Vote>(),
                Media = entity.Media?.ToList() ?? new List<long>(),
                CreationTime = entity.CreationTime.ToLocalTime()
            };
        }
    }
}