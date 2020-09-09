using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Marketplace.Domain
{
    public class ClassifiedAdTitle : Value<ClassifiedAdTitle>
    {
        private readonly string _value;
        private ClassifiedAdTitle(string value)
        {
            if (value.Length > 100)
                throw new ArgumentOutOfRangeException("Title cannot be longer that 100 characters", nameof(value));
            _value = value;
        }

        public static ClassifiedAdTitle FromString(string title) => new ClassifiedAdTitle(title);

        public static ClassifiedAdTitle FromHtml(string htmlTitle)
        {
            var supportedTagsReplaced = htmlTitle
            .Replace("<i>", "*")
            .Replace("</i>", "*")
            .Replace("<b>", "**")
            .Replace("</b>", "**");
            return new ClassifiedAdTitle(Regex.Replace(
            supportedTagsReplaced, "<.*?>", string.Empty));
        }

    }
}
