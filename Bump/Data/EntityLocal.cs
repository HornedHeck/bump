using System;
using System.Collections.Generic;
using System.Linq;
using Bump.Data.Mappers;
using Bump.Data.Models;
using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using LMessage = Bump.Data.Models.Message;
using LTheme = Bump.Data.Models.Theme;
using Message = Entities.Message;
using Theme = Entities.Theme;
using ThemeCategory = Entities.ThemeCategory;
using LThemeCategory = Bump.Data.Models.ThemeCategory;
using LThemeSubcategory = Bump.Data.Models.ThemeSubcategory;
using Media = Entities.Media;
using LMedia = Bump.Data.Models.Media;
using ThemeSubcategory = Entities.ThemeSubcategory;

namespace Bump.Data
{
    public class EntityLocal : DbContext, ILocalApi
    {
        private readonly string _filename = "Bump.db";
        private readonly bool _isTest;

        public EntityLocal()
        {
            Database.EnsureCreated();
            _isTest = true;
        }

        public EntityLocal(string filename, bool isTest = true)
        {
            _filename = filename;
            _isTest = isTest;
        }

        public void ResetDatabase()
        {
            // if (!_isTest) return;
            Messages.RemoveRange(Messages);
            Themes.RemoveRange(Themes);
            Users.RemoveRange(Users);
            Subcategories.RemoveRange(Subcategories);
            Categories.RemoveRange(Categories);
            Media.RemoveRange(Media);
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_filename}");
        }

        public DbSet<LTheme> Themes { get; set; }
        public DbSet<LThemeCategory> Categories { get; set; }
        public DbSet<LThemeSubcategory> Subcategories { get; set; }
        public DbSet<LMessage> Messages { get; set; }
        public DbSet<LMedia> Media { get; set; }
        public DbSet<BumpUser> Users { get; set; }

        // public DbSet<> 

        public void AddUser(User user)
        {
            Users.Add(user.Map());
        }

        public Media GetMedia(long id)
        {
            return Media.Find(id).Map();
        }

        public void AddMedia(Media media)
        {
            var entry = Media.Add(media.Map());
            SaveChanges();
            media.Id = entry.Entity.Id;
        }

        public Theme GetTheme(int id)
        {
            return Themes.Find(id)?.Map();
        }

        public void CreateTheme(Theme theme)
        {
            var subcategory = Subcategories.Find(theme.Subcategory.Id);
            var res = Themes.Add(theme.Map(subcategory, Media));
            SaveChanges();
            theme.Id = res.Entity.Id;
        }

        public void CreateMessage(Message message)
        {
            var theme = Themes.Find(message.Theme);
            var entry = Messages.Add(message.Map(theme, Media));
            SaveChanges();
            message.Id = entry.Entity.Id;
        }

        public void UpdateMessage(Message message)
        {
            Messages.Find(message.Id)?.Also(it =>
            {
                it.Content = message.Content;
                it.Media = message.Media.Select(media => Media.Find(media)).ToList();
                SaveChanges();
            });
        }

        public void DeleteMessage(int id)
        {
            Messages.Find(id)?.Also(entry =>
            {
                Messages.Remove(entry);
                SaveChanges();
            });
        }

        public Message GetMessage(int id)
        {
            return Messages.Find(id)?.Map();
        }

        public List<ThemeCategory> GetCategories() => Categories.Map().ToList();

        public List<ThemeSubcategory> GetSubcategories(long category) => Subcategories
            .Where(it => it.Category.Id == category)
            .AsEnumerable()
            .Map()
            .ToList();

        public List<Theme> GetThemes(long subcategory) => Themes
            .Where(it => it.Subcategory.Id == subcategory)
            .AsEnumerable()
            .Map()
            .ToList();

        public void AddCategory(ThemeCategory category)
        {
            var entry = Categories.Add(category.Map());
            SaveChanges();
            category.Id = entry.Entity.Id;
        }

        public void AddSubcategory(ThemeSubcategory subcategory)
        {
            var category = Categories.Find(subcategory.Category.Id);
            var entry = Subcategories.Add(subcategory.Map(category));
            SaveChanges();
            subcategory.Id = entry.Entity.Id;
        }
    }
}