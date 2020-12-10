using System;
using System.Collections.Generic;
using Bump.Localization.Messages;
using Bump.Services.Email;
using Common;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Tests.Utils {

    internal static class TestObjectsFactory {

        internal static Message MessageEntity => new Message {
            Id = 1 ,
            Content = "Content" ,
            CreationTime = DateTime.Now.AddDays( 1 ) ,
            Media = new[] {1L , 2L , 3L} ,
            Theme = 1L ,
            Votes = new List< Vote > {
                new Vote {UserId = "1"} ,
                new Vote {UserId = "2"} ,
                new Vote {UserId = "3"} ,
            } ,
            Author = new User( "1" )
        };

        internal static ThemeCategory CategoryEntity => new ThemeCategory {
            Id = 2 ,
            Name = "Category"
        };

        internal static ThemeSubcategory SubcategoryEntity => new ThemeSubcategory {
            Id = 2 ,
            Name = "Subcategory" ,
            Category = CategoryEntity
        };


        internal static Theme ThemeEntity => new Theme {
            Id = 1 ,
            Name = "Title" ,
            Content = "Content" ,
            CreationTime = DateTime.Now.AddDays( 1 ) ,
            Media = new[] {1L , 2L , 3L} ,
            Messages = new[] {
                MessageEntity.Also( it => it.Id = 1 ) ,
                MessageEntity.Also( it => it.Id = 2 ) ,
                MessageEntity.Also( it => it.Id = 3 )
            } ,
            Author = new User( "1" ) ,
            Subcategory = SubcategoryEntity
        };


    }

}