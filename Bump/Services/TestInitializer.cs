using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bump.Auth;
using Data.Api.Local;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Bump.Services {

    public static class TestInitializer {

        public static void Initialize( ILocalApi local , UserManager< BumpUser > userManager ,
            IWebHostEnvironment environment ) {
            local.ResetDatabase();
            if( Directory.Exists( environment.WebRootPath + "/files" ) ) {
                Directory.Delete( environment.WebRootPath + "/files" , true );
            }

            InitDbContent( local , userManager , environment );
        }

        private static void InitDbContent( ILocalApi local , UserManager< BumpUser > userManager ,
            IWebHostEnvironment environment ) {
            var users = userManager.Users
                .Select( it => new User( it.Id ) )
                .ToList();

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
                    var dir = environment.WebRootPath + FileManager.GetFolder( media );
                    if( !Directory.Exists( dir ) ) {
                        Directory.CreateDirectory( dir );
                    }

                    File.Copy( environment.WebRootPath + "/doc.png" ,
                        environment.WebRootPath + FileManager.GetPath( media ) );

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