using System.Threading.Tasks;
using Bump.Auth;
using Bump.Models;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepo _messageRepo;
        private readonly UserManager<BumpUser> _userManager;

        public MessageController(IMessageRepo messageRepo, UserManager<BumpUser> userManager)
        {
            _messageRepo = messageRepo;
            _userManager = userManager;
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteMessage(int id)
        {
            var message = _messageRepo.GetMessage(id);
            _messageRepo.DeleteMessage(id);
            return RedirectToAction(
                actionName: "Theme",
                controllerName: "Home",
                routeValues: new {themeId = message.Theme}
            );
        }

        [Authorize]
        public async Task<IActionResult> UpdateMessage(int id)
        {
            var entity = _messageRepo.GetMessage(id);
            var message = new MessageVM
            {
                Id = entity.Id,
                Method = "UpdateMessage",
                Author = await _userManager.FindByIdAsync(entity.Author.Id),
                Content = entity.Content,
                Theme = entity.Theme
            };
            return View("Message", message);
        }

        [ActionName("UpdateMessage")]
        [Authorize]
        [HttpPost]
        public IActionResult UpdatePost(MessageVM messageVm)
        {
            _messageRepo.UpdateMessage(messageVm.Id, messageVm.Content, new int[0]);
            return RedirectToAction(
                actionName: "Theme",
                controllerName: "Home",
                routeValues: new {themeId = messageVm.Theme}
            );
        }

        [Authorize]
        public async Task<IActionResult> CreateMessage(int id)
        {
            var entity = _messageRepo.GetMessage(id);
            var message = new MessageVM
            {
                Id = entity.Id,
                Method = "CreateMessage",
                Author = await _userManager.FindByIdAsync(entity.Author.Id),
                Content = entity.Content,
                Theme = entity.Theme
            };
            return View("Message", message);
        }

        [Authorize]
        [ActionName("CreateMessage")]
        [HttpPost]
        public IActionResult CreatePost(MessageVM messageVm)
        {
            var entity = new Message(
                id: messageVm.Id,
                author: new User(messageVm.Author.Id),
                content: messageVm.Content,
                media: new int[0],
                theme: messageVm.Theme
            );
            _messageRepo.CreateMessage(entity);
            return RedirectToAction(
                actionName: "Theme",
                controllerName: "Home",
                routeValues: new {themeId = messageVm.Theme}
            );
        }
    }
}