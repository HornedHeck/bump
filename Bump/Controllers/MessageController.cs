using Bump.Models;
using Data.Repo;
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

        [HttpDelete]
        public IActionResult DeleteMessage(int id)
        {
            var message = _messageRepo.GetMessage(id);
            _messageRepo.DeleteMessage(id);
            return Redirect(Url.Action("Theme", "Home", new {themeId = message.Theme}));
        }


        public IActionResult UpdateMessage(int id)
        {
            var message = new Message(_messageRepo.GetMessage(id)) {Method = "UpdateMessage"};
            return View("Message", message);
        }

        [ActionName("UpdateMessage")]
        [HttpPost]
        public IActionResult UpdatePost(Message message)
        {
            _messageRepo.UpdateMessage(message.Id, message.Content, new int[0]);
            return Redirect(Url.Action(
                "Theme",
                "Home",
                new {themeId = message.Theme}
            ));
        }


        public IActionResult CreateMessage(int id)
        {
            var message = new Message(_messageRepo.GetMessage(id)) {Method = "CreateMessage"};
            return View("Message", message);
        }

        [ActionName("CreateMessage")]
        [HttpPost]
        public IActionResult CreatePost(Message message)
        {
            _messageRepo.CreateMessage(message.Convert());
            return Redirect(Url.Action(
                "Theme",
                "Home",
                new {themeId = message.Theme}
            ));
        }
    }
}