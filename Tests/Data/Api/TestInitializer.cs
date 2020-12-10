using System;
using System.Collections.Generic;
using Data.Api.Local;
using Entities;

namespace Tests.Data.Api {

    public static class TestInitializer {

        internal static void InitDbContent( ILocalApi local ) {

            var users = new List< User > {
                new User( "1" ) ,
                new User( "2" ) ,
                new User( "3" )
            };

            users.ForEach( local.AddUser );
            var random = new Random();
            var category = new ThemeCategory {
                Id = 0 ,
                Name = "Category"
            };
            local.AddCategory( category );
            var subcategory = new ThemeSubcategory {
                Id = 0 ,
                Name = "Subcategory" ,
                Category = category
            };
            local.AddSubcategory( subcategory );
            for( var i = 0 ; i < 5 ; i++ ) {
                var theme = new Theme(
                    i + 1 ,
                    users [ random.Next( users.Count ) ] ,
                    $"Theme {i + 1}" ,
                    $"Content of theme {i + 1}" ,
                    new Message[0] ,
                    new long[0] ,
                    DateTime.Today ,
                    subcategory
                ) {
                    Subcategory = subcategory
                };
                local.CreateTheme( theme );
                for( var j = 0 ; j < 10 ; j++ ) {
                    var media = new Media {
                        Type = MediaType.Image ,
                        Name = "doc.png"
                    };
                    local.AddMedia( media );

                    var message = new Message(
                        0 ,
                        users [ random.Next( users.Count ) ] ,
                        $"Content of message {j + 1}" ,
                        new[] {media.Id} ,
                        theme.Id ,
                        DateTime.Now.AddHours( random.NextDouble() * 3 - 1.5 ) ,
                        new List< Vote >()
                    );
                    local.CreateMessage( message );
                }
            }
        }

    }

}