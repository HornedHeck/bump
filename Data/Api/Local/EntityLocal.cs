using System.Collections.Generic;
using System.Linq;
using Common;
using Data.Mappers;
using Data.Models;
using Entities;
using Microsoft.EntityFrameworkCore;
using LMessage = Data.Models.Message;
using LTheme = Data.Models.Theme;
using LThemeCategory = Data.Models.ThemeCategory;
using LThemeSubcategory = Data.Models.ThemeSubcategory;
using LMedia = Data.Models.Media;
using LVote = Data.Models.Vote;
using Media = Entities.Media;
using Message = Entities.Message;
using Theme = Entities.Theme;
using ThemeCategory = Entities.ThemeCategory;
using ThemeSubcategory = Entities.ThemeSubcategory;
using Vote = Entities.Vote;

namespace Data.Api.Local {

    public sealed class EntityLocal : DbContext , ILocalApi {

        private readonly string _filename = "Bump.db";

        public EntityLocal() {
            Database.EnsureCreated();
        }

        public EntityLocal( string filename , bool isTest = true ) {
            _filename = filename;
            Database.EnsureCreated();
        }

        public void AddUser( User user ) {
            Users.Add( user.Map() );
        }

        public Media GetMedia( long id ) {
            return Media.Find( id )?.Map();
        }

        public void AddMedia( Media media ) {
            var entry = Media.Add( media.Map() );
            SaveChanges();
            media.Id = entry.Entity.Id;
        }

        public void RemoveMedia( long id ) {
            Media.Find( id )?.Run( it => Media.Remove( it ) );
            SaveChanges();
        }

        public Theme GetTheme( long id ) {
            return Themes.Find( id )?.Map();
        }

        public void CreateTheme( Theme theme ) {
            var subcategory = Subcategories.Find( theme.Subcategory.Id );
            var res = Themes.Add( theme.Map( subcategory , Media ) );
            SaveChanges();
            theme.Id = res.Entity.Id;
        }

        public void CreateMessage( Message message ) {
            var theme = Themes.Find( message.Theme );
            var entry = Messages.Add( message.Map( theme , Media ) );
            SaveChanges();
            message.Id = entry.Entity.Id;
        }

        public void UpdateMessage( Message message ) {
            Messages.Find( message.Id )?.Also( it => {
                it.Content = message.Content;
                it.Media = message.Media.Select( media => Media.Find( media ) ).ToList();
                SaveChanges();
            } );
        }

        public void DeleteMessage( int id ) {
            Messages.Find( id )?.Also( entry => {
                Messages.Remove( entry );
                SaveChanges();
            } );
        }

        public Message GetMessage( int id ) {
            return Messages.Find( id )?.Map();
        }

        public List< ThemeCategory > GetCategories() {
            return Categories.Map().ToList();
        }

        public List< ThemeSubcategory > GetSubcategories( long category ) {
            return Subcategories
                .Where( it => it.Category.Id == category )
                .AsEnumerable()
                .Map()
                .ToList();
        }

        public List< Theme > GetThemes( long subcategory ) {
            return Themes
                .Where( it => it.Subcategory.Id == subcategory )
                .AsEnumerable()
                .Map()
                .ToList();
        }

        public void UpdateTheme( long theme , string title , string content , IEnumerable< long > media ) {
            Themes.Find( theme )?.Also( it => {
                it.Title = title;
                it.Content = content;
                it.Media = media.Select( id => Media.Find( id ) ).ToList();
                SaveChanges();
            } );
        }

        public void AddCategory( ThemeCategory category ) {
            var entry = Categories.Add( category.Map() );
            SaveChanges();
            category.Id = entry.Entity.Id;
        }

        public void AddSubcategory( ThemeSubcategory subcategory ) {
            var category = Categories.Find( subcategory.Category.Id );
            var entry = Subcategories.Add( subcategory.Map( category ) );
            SaveChanges();
            subcategory.Id = entry.Entity.Id;
        }

        public void VoteUp( int message , Vote vote ) {
            Messages.Find( message )?.Also( item => {
                if( item.Votes == null )
                    item.Votes = new List< LVote > {vote.Map()};
                else if( item.Votes.All( v => v.UserId != vote.UserId ) ) {
                    item.Votes.Add( vote.Map() );
                }

            } );
            SaveChanges();
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
            optionsBuilder.UseSqlite( $"Filename={_filename}" );
        }

        // ReSharper disable UnusedAutoPropertyAccessor.Global
        // ReSharper disable MemberCanBePrivate.Global
        public DbSet< LTheme > Themes { get; set; }
        public DbSet< LThemeCategory > Categories { get; set; }
        public DbSet< LThemeSubcategory > Subcategories { get; set; }
        public DbSet< LMessage > Messages { get; set; }
        public DbSet< LMedia > Media { get; set; }
        public DbSet< BumpUser > Users { get; set; }

        // ReSharper disable once UnusedMember.Global
        public DbSet< LVote > Votes { get; set; }

        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper restore UnusedAutoPropertyAccessor.Global

    }

}