using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bump.Auth;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bump.VM {

    public static class ThemeVmMapper {

        public static async Task< ThemeVm > ToVm( this Theme entity , UserManager< BumpUser > userManager ) {
            List< MessageVm > messages;
            if( entity.Messages == null ) {
                messages = new List< MessageVm >();
            }
            else {
                messages = new List< MessageVm >( entity.Messages.Length );
                foreach( var message in entity.Messages ) {
                    messages.Add( await message.ToVm( userManager , null ) );
                }
            }

            return new ThemeVm {
                Id = entity.Id ,
                Author = await userManager.FindByIdAsync( entity.Author.Id ) ,
                Content = entity.Content ,
                Title = entity.Name ,
                Messages = messages ,
                Subcategory = entity.Subcategory ,
                StartTime = entity.CreationTime.ToLocalTime() ,
                Media = entity.Media.Select( it => it ).ToList()
            };
        }

    }

}