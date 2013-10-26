﻿using System.Globalization;

namespace SunshineAttack.Localization.Areas.SunshineAttack.Models.LocalizeViews
{
    public class ViewPrompt
    {
        private readonly global::SunshineAttack.Localization.Localization.Views.ViewPrompt _prompt;

        public ViewPrompt(global::SunshineAttack.Localization.Localization.Views.ViewPrompt prompt)
        {
            _prompt = prompt;
        }

        public string TextKey
        {
            get { return _prompt.Key.ToString(); }
        }

        /// <summary>
        /// Gets or sets locale id (refer to MSDN)
        /// </summary>
        public int LocaleId
        {
            get { return _prompt.LocaleId; }
        }

        /// <summary>
        /// Gets or sets controller that the text is for
        /// </summary>
        public string ViewPath
        {
            get { return _prompt.ViewPath; }
        }

        /// <summary>
        /// Gets or sets the text to translate
        /// </summary>
        public string TextName
        {
            get { return _prompt.TextName; }
        }

        /// <summary>
        /// Gets or sets translated text
        /// </summary>
        /// <value>Empty string if not translated</value>
        public string TranslatedText
        {
            get { return _prompt.TranslatedText; }
        }

        public CultureInfo Culture
        {
            get { return new CultureInfo(_prompt.LocaleId); }
        }
    }
}