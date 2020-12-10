using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bump.Auth;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bump.VM {

    public static class MessageVmMapper {

        public static async Task< MessageVm > ToVm( this Message entity , UserManager< BumpUser > userManager , string method ) {
            return new MessageVm {
                Id = entity.Id ,
                Author = await userManager.FindByIdAsync( entity.Author.Id ) ,
                Content = entity.Content ,
                Method = method ,
                Theme = entity.Theme ,
                Votes = entity.Votes ?? new List< Vote >() ,
                Media = entity.Media?.ToList() ?? new List< long >() ,
                CreationTime = entity.CreationTime.ToLocalTime()
            };
        }

    }

}