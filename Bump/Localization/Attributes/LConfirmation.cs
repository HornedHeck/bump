using System.ComponentModel.DataAnnotations;
using Bump.Resources.Localization.Errors;

namespace Bump.Localization.Attributes {

    public class LConfirmation : CompareAttribute {

        public LConfirmation( string otherProperty ) : base( otherProperty ) {
            ErrorMessageResourceType = typeof( CommonErrors );
            ErrorMessageResourceName = "PasswordConfirmationMatch";
        }

    }

}