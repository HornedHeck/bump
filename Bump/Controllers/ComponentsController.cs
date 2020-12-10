using System.Threading.Tasks;
using Bump.Auth;
using Bump.Components;
using Bump.VM;
using Data.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bump.Controllers {

    public class ComponentsController : Controller {

        private readonly MessageRepo _repo;
        private readonly UserManager< BumpUser > _um;

        public ComponentsController( MessageRepo repo , UserManager< BumpUser > um ) {
            _repo = repo;
            _um = um;
        }

        [HttpPost]
        [Authorize]
        public async Task< ViewComponentResult > Message( int messageId ) {
            var vm = await _repo.GetMessage( messageId ).ToVm( _um , null );

            return ViewComponent( typeof( Message ) , vm );
        }

    }

}