using System;
using System.ComponentModel.DataAnnotations;
using Bump.Resources.Localization.Errors;

namespace Bump.Localization.Attributes {

    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter )]
    public class LRequired : RequiredAttribute {

        public LRequired() {
            ErrorMessageResourceType = typeof( CommonErrors );
            ErrorMessageResourceName = "FieldRequired";
        }

    }

}