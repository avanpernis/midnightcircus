using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    enum ColorType { green, red, blue, grey, black };

    class TokenMacro
    {
        public string Name = "";
        public string Command = "";
        public string Group = "";
        public string SortBy = "";
        public string ToolTip = "";
        public int Index = 1;

        public ColorType ButtonColor = ColorType.green;
        public ColorType FontColor = ColorType.black;

        public override string ToString()
        {
            string result = Properties.Resources.MacroTemplate;

            result = result.Replace(@"###ENTRY_NUMBER###", Index.ToString());

            result = result.Replace(@"###BUTTON_COLOR###", ButtonColor.ToString());
            result = result.Replace(@"###FONT_COLOR###", FontColor.ToString());

            result = result.Replace(@"###MACRO_NAME###", Name);
            result = result.Replace(@"###TOOLTIP###", ToolTip);
            result = result.Replace(@"###MACRO_GROUP###", Group);
            result = result.Replace(@"###MACRO_COMMAND###", Command);
            result = result.Replace(@"###SORTBY###", SortBy);

            return result;
        }
    }
}
