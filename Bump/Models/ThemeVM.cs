using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Resources.Strings;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bump.Models
{
    public class ThemeVm
    {
        public long Id { get; set; }

        public BumpUser Author { get; set; }

        [LRequired]
        [Display(ResourceType = typeof(CommonStrings), Name = "Content")]
        public string Content { get; set; }


        [LRequired]
        [Display(ResourceType = typeof(CommonStrings), Name = "Title")]
        public string Title { get; set; }

        public List<MessageVM> Messages { get; set; }

        public ThemeSubcategory Subcategory { get; set; }

        public DateTime StartTime { get; set; }

        public List<long> Media { get; set; }
    }

    public static class ThemeVmMapper
    {
        public static async Task<ThemeVm> ToVm(this Theme entity, UserManager<BumpUser> userManager)
        {
            List<MessageVM> messages;
            if (entity.Messages == null)
            {
                messages = new List<MessageVM>();
            }
            else
            {
                messages = new List<MessageVM>(entity.Messages.Length);
                foreach (var message in entity.Messages)
                {
                    messages.Add(await message.ToVm(userManager, null));
                }
            }

            return new ThemeVm
            {
                Id = entity.Id,
                Author = await userManager.FindByIdAsync(entity.Author.Id),
                Content = entity.Content,
                Title = entity.Name,
                Messages = messages,
                Subcategory = entity.Subcategory,
                StartTime = entity.CreationTime.ToLocalTime(),
                Media = entity.Media.Select(it => (long) it).ToList()
            };
        }
    }
}