using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bump.TagHelpers {

    public class CenteredVertical : TagHelper {

        private const string Style = "display: flex; align-items: center; height: inherit;";

        public override void Process( TagHelperContext context , TagHelperOutput output ) {
            output.TagName = "div";
            var old = "";
            if( output.Attributes.ContainsName( "style" ) ) {
                old = " " + output.Attributes [ "style" ].Value;
            }

            output.Attributes.SetAttribute( "style" , Style + old );
        }

    }

}