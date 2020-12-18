using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.Services;
using Bump.VM;
using Data.Repo;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Controllers {

    public class MessageController : Controller {

        private readonly MessageRepo _messageRepo;
        private readonly UserManager< BumpUser > _userManager;
        private readonly FileManager _fileManager;

        public MessageController( MessageRepo messageRepo , UserManager< BumpUser > userManager , FileManager fileManager ) {
            _messageRepo = messageRepo;
            _userManager = userManager;
            _fileManager = fileManager;
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteMessage( int id ) {
            var message = _messageRepo.GetMessage( id );
            _messageRepo.DeleteMessage( id );

            return RedirectToAction(
                "Theme" ,
                "Home" ,
                new {themeId = message.Theme}
            );
        }

        [Authorize]
        [HttpPatch]
        public void VoteMessage( int id ) {
            var vote = new Vote {UserId = HttpContext.User.FindFirstValue( ClaimTypes.NameIdentifier )};
            _messageRepo.VoteUp( id , vote );
        }

        [Authorize]
        [HttpGet]
        public async Task< IActionResult > UpdateMessage( int id ) {
            var entity = _messageRepo.GetMessage( id );
            var vm = await entity.ToVm( _userManager , "UpdateMessage" );

            return View( "Message" , vm );
        }

        [ActionName( "UpdateMessage" )]
        [Authorize]
        [HttpPost]
        public async Task< IActionResult > UpdatePost( MessageVm vm , IFormFile uploadingMedia , long? deleteMedia ) {
            return await UpdateMessage(
                vm ,
                uploadingMedia ,
                deleteMedia ,
                message => _messageRepo.UpdateMessage( vm.Id , vm.Content , vm.Media.ToArray() )
            );
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateMessage( long themeId ) {
            var vm = new MessageVm {
                Method = "CreateMessage" ,
                Content = "" ,
                Theme = themeId ,
                Media = new List< long >()
            };

            return View( "Message" , vm );
        }

        [Authorize]
        [ActionName( "CreateMessage" )]
        [HttpPost]
        public async Task< IActionResult > CreatePost(
            MessageVm vm ,
            IFormFile uploadingMedia = null ,
            long? deleteMedia = null
        ) {
            return await UpdateMessage( vm , uploadingMedia , deleteMedia , message => {
                var entity = new Message(
                    0 ,
                    new User( HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value ) ,
                    message.Content ,
                    message.Media.ToArray() ,
                    message.Theme ,
                    DateTime.Now ,
                    new List< Vote >()
                );
                _messageRepo.CreateMessage( entity );
            } );
        }

        private async Task< IActionResult > UpdateMessage(
            MessageVm vm ,
            IFormFile uploadingMedia ,
            long? deleteMedia ,
            Action< MessageVm > consumer
        ) {
            vm.Media ??= new List< long >();

            if( uploadingMedia != null ) {
                var mediaId = await _fileManager.SaveFile( uploadingMedia );
                vm.Media.Add( mediaId );

                return View( "Message" , vm );
            }

            if( deleteMedia != null ) {
                _fileManager.RemoveMedia( (long) deleteMedia );
                vm.Media.Remove( (long) deleteMedia );

                return View( "Message" , vm );
            }

            if( !ModelState.IsValid ) return View( "Message" , vm );

            consumer( vm );

            return RedirectToAction(
                "Theme" ,
                "Home" ,
                new {themeId = vm.Theme}
            );

        }

    }

}