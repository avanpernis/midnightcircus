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
    }
}
