using System.ComponentModel.DataAnnotations;
using Bump.Resources.Localization.Errors;

namespace Bump.Localization.Attributes {

    public class LStringLength : StringLengthAttribute {

        public LStringLength( int maximumLength ) : base( maximumLength ) {
            ErrorMessageResourceType = typeof( CommonErrors );
            ErrorMessageResourceName = "StringLength";
        }

    }

}