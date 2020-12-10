using System.Diagnostics;
using System.Threading.Tasks;
using Bump.Auth;
using Bump.VM;
using Microsoft.AspNetCore.Mvc;
using Data.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Bump.Controllers {

    public class HomeController : Controller {

        private readonly ThemeRepo _themeRepo;
        private readonly UserManager< BumpUser > _userManager;

        public HomeController( ThemeRepo themeRepo , UserManager< BumpUser > userManager ) {
            _themeRepo = themeRepo;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index() {
            return View( _themeRepo.GetCategories() );
        }

        public IActionResult Category( long category ) {
            return View( _themeRepo.GetSubcategories( category ) );
        }

        public IActionResult Subcategory( long subcategory ) {
            return View( new SubcategoryVm {
                Id = subcategory ,
                Themes = _themeRepo.GetThemes( subcategory )
            } );
        }

        [Authorize]
        public async Task< IActionResult > Theme( long themeId ) {
            var entity = _themeRepo.GetTheme( themeId );

            return View( await entity.ToVm( _userManager ) );
        }

        [ResponseCache( Duration = 0 , Location = ResponseCacheLocation.None , NoStore = true )]
        public IActionResult Error() {
            return View( new ErrorVM {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier} );
        }

    }

}