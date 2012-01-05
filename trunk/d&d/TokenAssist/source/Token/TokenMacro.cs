﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

namespace TokenAssist
{
    public enum ColorType { white, green, red, blue, grey, black, orange };

    public class TokenMacro
    {
        public string Name = "";
        public string Command = "";
        public string Group = "";
        public string SortBy = "";
        public string ToolTip = "";
        public int Index = 1;

        public ColorType ButtonColor = ColorType.white;
        public ColorType FontColor = ColorType.black;

        public override string ToString()
        {
            string result = Properties.Resources.MacroTemplate;

            result = result.Replace(@"###ENTRY_NUMBER###", Index.ToString());

            result = result.Replace(@"###BUTTON_COLOR###", ButtonColor.ToString());
            result = result.Replace(@"###FONT_COLOR###", FontColor.ToString());

            result = result.Replace(@"###MACRO_NAME###", SecurityElement.Escape(Name));
            result = result.Replace(@"###TOOLTIP###", SecurityElement.Escape(ToolTip));
            result = result.Replace(@"###MACRO_GROUP###", SecurityElement.Escape(Group));
            result = result.Replace(@"###MACRO_COMMAND###", SecurityElement.Escape(Command));
            result = result.Replace(@"###SORTBY###", SecurityElement.Escape(SortBy));

            return result;
        }
    }
}