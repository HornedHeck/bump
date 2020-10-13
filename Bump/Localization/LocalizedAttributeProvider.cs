using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bump.Resources.Localization.Auth;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace Bump.Localization
{
    public class LocalizedAttributeProvider : IValidationAttributeAdapterProvider
    {
        private readonly ValidationAttributeAdapterProvider _provider = new ValidationAttributeAdapterProvider();
        private readonly IStringLocalizer<CommonErrors> _localizer;
        private readonly ISet<string> _codes;

        public LocalizedAttributeProvider(IStringLocalizer<CommonErrors> localizer)
        {
            _localizer = localizer;
            _codes = _localizer.GetAllStrings().Select(it => it.Name).ToHashSet();
        }

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (_codes.Contains(attribute.ErrorMessage))
            {
                return _provider.GetAttributeAdapter(attribute, _localizer);
            }

            return _provider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}