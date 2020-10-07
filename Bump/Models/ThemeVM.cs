using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bump.Auth;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bump.Models
{
    public class ThemeVM
    {
        public BumpUser Author { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public List<MessageVM> Messages { get; set; }

        public ThemeSubcategory Subcategory { get; set; }
        
        public DateTime StartTime { get; set; }
    }

    public static class ThemeVmMapper
    {
        public static async Task<ThemeVM> ToVm(this Theme entity, UserManager<BumpUser> userManager)
        {
            var messages = new List<MessageVM>(entity.Messages.Length);
            foreach (var message in entity.Messages)
            {
                messages.Add(await message.ToVm(userManager, null));
            }

            return new ThemeVM
            {
                Author = await userManager.FindByIdAsync(entity.Author.Id),
                Content = entity.Content,
                Title = entity.Name,
                Messages = messages,
                Subcategory = entity.Subcategory,
                StartTime = entity.CreationTime.ToLocalTime()
            };
        }
    }
}