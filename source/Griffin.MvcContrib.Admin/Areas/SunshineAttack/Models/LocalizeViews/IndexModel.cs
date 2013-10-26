﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace SunshineAttack.Localization.Areas.SunshineAttack.Models.LocalizeViews
{
    public class IndexModel
    {
        public IEnumerable<ViewPrompt> Prompts { get; set; }
        public IEnumerable<CultureInfo> Cultures { private get; set; }

        public IEnumerable<SelectListItem> CultureItems
        {
            get
            {
                return Cultures.Select(p => new SelectListItem
                                                {
                                                    Text = p.DisplayName,
                                                    Value = p.Name
                                                });
            }
        }

        public string TableFilter { get; set; }
        public bool OnlyNotTranslated { get; set; }
    }
}