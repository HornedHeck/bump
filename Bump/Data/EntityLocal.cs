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

namespace Bump.Data
{
    public class EntityLocal : DbContext, ILocalApi
    {
        private string _filename = "Bump.db";
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
            if (!_isTest) return;
            Messages.RemoveRange(Messages);
            Themes.RemoveRange(Themes);
            Users.RemoveRange(Users);
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_filename}");
        }

        public DbSet<LTheme> Themes { get; set; }
        public DbSet<LMessage> Messages { get; set; }
        public DbSet<BumpUser> Users { get; set; }

        public void AddUser(User user)
        {
            Users.Add(user.Map());
        }

        public Media LoadMedia(int id)
        {
            throw new System.NotImplementedException();
        }

        public Theme GetTheme(int id)
        {
            return Themes.Find(id)?.Map();
        }

        public void CreateTheme(Theme theme)
        {
            var res = Themes.Add(theme.Map());
            SaveChanges();
            theme.Id = res.Entity.Id;
        }

        public List<ThemeHeader> GetThemeHeaders()
        {
            return Themes.Map().ToList();
        }

        public void CreateMessage(Message message)
        {
            var theme = Themes.Find(message.Theme);
            var entry = Messages.Add(message.Map(theme));
            SaveChanges();
            message.Id = entry.Entity.Id;
        }

        public void UpdateMessage(Message message)
        {
            Messages.Find(message.Id)?.Also(it =>
            {
                it.Content = message.Content;
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
    }
}