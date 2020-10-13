using System.ComponentModel.DataAnnotations;
using Bump.Resources.Localization.Auth;

namespace Bump.Localization.Attributes
{
    public class LConfirmation : CompareAttribute
    {
        public LConfirmation(string otherProperty) : base(otherProperty)
        {
            ErrorMessageResourceType = typeof(CommonErrors);
            ErrorMessageResourceName = "PasswordConfirmationMatch";
        }
    }
}