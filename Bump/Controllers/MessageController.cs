using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Bump.Auth;
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
        private readonly MessageRepo _messageRepo;
        private readonly UserManager<BumpUser> _userManager;
        private readonly FileManager _fileManager;

        public MessageController(MessageRepo messageRepo, UserManager<BumpUser> userManager, FileManager fileManager)
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
        [HttpPatch]
        public void VoteMessage(int id)
        {
            var vote = new Vote {UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)};
            _messageRepo.VoteUp(id, vote);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateMessage(int id)
        {
            var entity = _messageRepo.GetMessage(id);
            var vm = await entity.ToVm(_userManager, "UpdateMessage");
            return View("Message", vm);
        }

        [ActionName("UpdateMessage")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePost(MessageVM vm, IFormFile uploadingMedia, long? deleteMedia)
        {
            return await UpdateMessage(
                vm,
                uploadingMedia,
                deleteMedia,
                message => _messageRepo.UpdateMessage(vm.Id, vm.Content, vm.Media.ToArray())
            );
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateMessage(long themeId)
        {
            var vm = new MessageVM
            {
                Method = "CreateMessage",
                Content = "",
                Theme = themeId,
                Media = new List<long>()
            };
            return View("Message", vm);
        }

        [Authorize]
        [ActionName("CreateMessage")]
        [HttpPost]
        public async Task<IActionResult> CreatePost(
            MessageVM vm,
            IFormFile uploadingMedia = null,
            long? deleteMedia = null
        )
        {
            return await UpdateMessage(vm, uploadingMedia, deleteMedia, message =>
            {
                var entity = new Message(
                    id: 0,
                    author: new User(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    content: message.Content,
                    media: message.Media.ToArray(),
                    theme: message.Theme,
                    creationTime: DateTime.Now,
                    votes: new List<Vote>()
                );
                _messageRepo.CreateMessage(entity);
            });
        }

        private async Task<IActionResult> UpdateMessage(
            MessageVM vm,
            IFormFile uploadingMedia,
            long? deleteMedia,
            Action<MessageVM> consumer
        )
        {
            vm.Media ??= new List<long>();

            if (uploadingMedia != null)
            {
                var mediaId = await _fileManager.SaveFile(uploadingMedia);
                vm.Media.Add(mediaId);
                return View("Message", vm);
            }

            if (deleteMedia != null)
            {
                _fileManager.RemoveMedia((long) deleteMedia);
                vm.Media.Remove((long) deleteMedia);
                return View("Message", vm);
            }

            if (ModelState.IsValid)
            {
                consumer(vm);
                return RedirectToAction(
                    actionName: "Theme",
                    controllerName: "Home",
                    routeValues: new {themeId = vm.Theme}
                );
            }

            return View("Message", vm);
        }
    }
}