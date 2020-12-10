using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Resources.Localization.Strings;
using Entities;

namespace Bump.VM {

    public class MessageVM {

        public int Id { get; set; }

        public long Theme { get; set; }

        public BumpUser Author { get; set; }

        [LRequired]
        [Display( ResourceType = typeof( CommonStrings ) , Name = "Content" )]
        [LStringLength( 1000 , MinimumLength = 1 )]
        public string Content { get; set; }

        public List< long > Media { get; set; } = new List< long >();

        public string Method { get; set; }

        public List< Vote > Votes { get; set; }

        public DateTime CreationTime { get; set; }

    }

}