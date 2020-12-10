using Bump.VM;
using Microsoft.AspNetCore.Mvc;
using static Bump.Auth.AuthConstants;

namespace Bump.Components {

    public class Message : ViewComponent {

        public IViewComponentResult Invoke( MessageVM message ) {
            if( User?.Identity?.Name == message.Author.UserName ) {
                return View( "Owner" , message );
            }

            if( User?.IsInRole( Moderator ) == true || User?.IsInRole( Admin ) == true ) {
                return View( "Moderator" , message );
            }

            return View( message );
        }

    }

}