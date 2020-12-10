using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bump.Resources.Localization.Errors;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace Bump.Localization {

    public class LocalizedAttributeProvider : IValidationAttributeAdapterProvider {

        private readonly ISet< string > _codes;
        private readonly IStringLocalizer< CommonErrors > _localizer;

        private readonly ValidationAttributeAdapterProvider _provider = new ValidationAttributeAdapterProvider();

        public LocalizedAttributeProvider( IStringLocalizer< CommonErrors > localizer ) {
            _localizer = localizer;
            _codes = _localizer.GetAllStrings().Select( it => it.Name ).ToHashSet();
        }

        public IAttributeAdapter GetAttributeAdapter( ValidationAttribute attribute , IStringLocalizer stringLocalizer ) {
            return _provider.GetAttributeAdapter(
                attribute ,
                _codes.Contains( attribute.ErrorMessage ) ? _localizer : stringLocalizer
            );
        }

    }

}