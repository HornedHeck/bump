using Bump.Resources.Localization.Errors;
using Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Bump.Localization {

    public class ErrorsLocalizer {

        private readonly IStringLocalizer< CommonErrors > _commons;

        public ErrorsLocalizer( IStringLocalizer< CommonErrors > commons ) {
            _commons = commons;
        }

        public string this[ IdentityError error ] => _commons [ error.Code ]
            .Run( localized => localized.ResourceNotFound ? error.Description : localized.Value );

    }

}