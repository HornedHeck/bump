using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bump.Areas.Identity.Pages {

    [AllowAnonymous]
    [ResponseCache( Duration = 0 , Location = ResponseCacheLocation.None , NoStore = true )]
    [SuppressMessage( "ReSharper" , "MemberCanBePrivate.Global" )]
    public class ErrorModel : PageModel {

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty( RequestId );

        public void OnGet() {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

    }

}