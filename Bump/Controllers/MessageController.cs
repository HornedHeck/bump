using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Data;
using Bump.Models;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepo _messageRepo;
        private readonly UserManager<BumpUser> _userManager;
        private readonly FileManager _fileManager;

        public MessageController(IMessageRepo messageRepo, UserManager<BumpUser> userManager, FileManager fileManager)
        {
            _messageRepo = messageRepo;
            _userManager = userManager;
            _fileManager = fileManager;
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
        [HttpPost]
        public void VoteMessage(int id)
        {
            var vote = new Vote {UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)};
            _messageRepo.VoteUp(id, vote);
        }

        [Authorize]
        public async Task<IActionResult> UpdateMessage(int id)
        {
            var entity = _messageRepo.GetMessage(id);
            var messageVm = await entity.ToVm(_userManager, "UpdateMessage");
            return View("Message", messageVm);
        }

        [ActionName("UpdateMessage")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePost(MessageVM messageVm, IFormFile uploadingMedia, string action = null)
        {
            var entity = _messageRepo.GetMessage(messageVm.Id);
            messageVm.Media = entity.Media.ToList();

            if (uploadingMedia != null)
            {
                var mediaId = await _fileManager.SaveFile(uploadingMedia);
                messageVm.Media.Add(mediaId);
            }

            _messageRepo.UpdateMessage(entity.Id, entity.Content, messageVm.Media.ToArray());

            if (action == "UpdateMessage")
            {
                return View("Message", messageVm);
            }

            return RedirectToAction(
                actionName: "Theme",
                controllerName: "Home",
                routeValues: new {themeId = messageVm.Theme}
            );
        }

        [Authorize]
        public IActionResult CreateMessage(long themeId)
        {
            var message = new MessageVM
            {
                Method = "CreateMessage",
                Content = "",
                Theme = themeId
            };
            return View("Message", message);
        }

        [Authorize]
        [ActionName("CreateMessage")]
        [HttpPost]
        public IActionResult CreatePost(MessageVM messageVm)
        {
            if (ModelState.IsValid)
            {
                var entity = new Message(
                    id: 0,
                    author: new User(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    content: messageVm.Content,
                    media: new long[0],
                    theme: messageVm.Theme,
                    creationTime: DateTime.Now,
                    votes: new List<Vote>()
                );
                _messageRepo.CreateMessage(entity);
                return RedirectToAction(
                    actionName: "Theme",
                    controllerName: "Home",
                    routeValues: new {themeId = messageVm.Theme}
                );
            }
            else
            {
                return View("Message", messageVm);
            }
        }

        public async Task<IActionResult> UploadMedia(MessageVM messageVm, IFormFile file)
        {
            await _fileManager.SaveFile(file);
            var entity = _messageRepo.GetMessage(messageVm.Id);
            messageVm.Media = messageVm.Media.Concat(entity.Media).Distinct().ToList();
            return RedirectToAction(
                messageVm.Method,
                new {messageVm = messageVm}
            );
        }
    }
}