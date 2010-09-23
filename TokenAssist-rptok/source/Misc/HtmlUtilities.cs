using System;

namespace TokenAssist
{
    public static class HtmlUtilities
    {
        public static string Tag(string input, string tag)
        {
            return string.Format("<{0}>{1}</{0}>", tag, input);
        }

        public static string Bold(string input)
        {
            return Tag(input, "b");
        }

        public static string ScrubString(string input)
        {
            string result = input;
            // for some reason, compendium uses 3 ?'s for an apostrophe
            result = result.Replace(@"???", @"'");

            // sometimes there are funky unicode apostrophes as well
            result = result.Replace("\u2019", @"'");

            // sometimes there are strange unicode spaces
            result = result.Replace("\uF020", @" ");

            // sometimes there are weird non-hyphen characters used for negative modifiers/penalties
            result = result.Replace("\u2013", @"-");

            // sometimes there are weird quotation mark characters that do not play nicely
            result = result.Replace("\u201c", @"'");
            result = result.Replace("\u201d", @"'");

            return result;
        }
    }
}
