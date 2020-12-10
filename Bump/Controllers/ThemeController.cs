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

    public class ThemeController : Controller {

        private readonly FileManager _fileManager;

        private readonly ThemeRepo _repo;
        private readonly UserManager< BumpUser > _userManager;

        public ThemeController( ThemeRepo repo , FileManager fileManager , UserManager< BumpUser > userManager ) {
            _repo = repo;
            _fileManager = fileManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateTheme( long subcategory ) {
            var model = new ThemeVm {
                Subcategory = new ThemeSubcategory {Id = subcategory} ,
                Media = new List< long >() ,
                Method = "CreateTheme"
            };

            return View( "Theme" , model );
        }

        [Authorize]
        [HttpPost]
        public async Task< IActionResult > CreateTheme( ThemeVm vm , IFormFile uploadingMedia , long? deleteMedia = null ) {
            return await UpdateTheme( vm , uploadingMedia , deleteMedia , theme => {
                var author = new User( HttpContext.User.FindFirstValue( ClaimTypes.NameIdentifier ) );
                var entity = new Theme(
                    0 ,
                    author ,
                    vm.Title ,
                    vm.Content ,
                    new Message[0] ,
                    vm.Media.ToArray() ,
                    DateTime.Now ,
                    vm.Subcategory
                );
                _repo.CreateTheme( entity );
                vm.Id = entity.Id;
            } );
        }

        [Authorize]
        [HttpGet]
        public async Task< IActionResult > EditTheme( long themeId ) {
            var entity = _repo.GetTheme( themeId );
            var vm = await entity.ToVm( _userManager );
            vm.Method = "EditTheme";

            return View( "Theme" , vm );
        }

        [Authorize]
        [HttpPost]
        public async Task< IActionResult > EditTheme( ThemeVm vm , IFormFile uploadingMedia , long? deleteMedia = null ) {
            return await UpdateTheme( vm , uploadingMedia , deleteMedia ,
                theme => { _repo.UpdateTheme( vm.Id , vm.Title , vm.Content , vm.Media ); } );
        }

        private async Task< IActionResult > UpdateTheme( ThemeVm vm , IFormFile uploadingMedia , long? deleteMedia ,
            Action< ThemeVm > consumer ) {
            vm.Media ??= new List< long >();

            if( uploadingMedia != null ) {
                var mediaId = await _fileManager.SaveFile( uploadingMedia );
                vm.Media.Add( mediaId );

                return View( "Theme" , vm );
            }

            if( deleteMedia != null ) {
                _fileManager.RemoveMedia( (long) deleteMedia );
                vm.Media.Remove( (long) deleteMedia );

                return View( "Theme" , vm );
            }

            if( !ModelState.IsValid ) return View( "Theme" , vm );

            consumer( vm );

            return RedirectToAction( "Theme" , "Home" , new {themeId = vm.Id} );

        }

    }

}