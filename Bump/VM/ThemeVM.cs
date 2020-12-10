using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bump.Auth;
using Bump.Localization.Attributes;
using Bump.Resources.Localization.Strings;
using Entities;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Bump.VM {

    public class ThemeVm {

        public long Id { get; set; }

        public BumpUser Author { get; set; }

        [LRequired]
        [Display( ResourceType = typeof( CommonStrings ) , Name = "Content" )]
        public string Content { get; set; }


        [LRequired]
        [Display( ResourceType = typeof( CommonStrings ) , Name = "Title" )]
        public string Title { get; set; }

        public List< MessageVm > Messages { get; set; }

        public ThemeSubcategory Subcategory { get; set; }

        public DateTime StartTime { get; set; }

        public List< long > Media { get; set; }

        public string Method { get; set; }

    }

}