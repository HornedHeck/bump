using System;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepo _messageRepo;

        public MessageController(IMessageRepo messageRepo)
        {
            _messageRepo = messageRepo;
        }

        public string DeleteMessage(int id)
        {
            _messageRepo.DeleteMessage(id);
            return "Deleted";
        }

        public string UpdateMessage(Message message)
        {
            _messageRepo.UpdateMessage(message);
            return "Updated";
        }

        public string CreateMessage(Message message)
        {
            _messageRepo.CreateMessage(message);
            return "Created";
        }
    }
}