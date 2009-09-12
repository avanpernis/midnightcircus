using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace TokenAssist
{
    public static class ExtraMacroLoader
    {
        public static Macro Load(string filename)
        {
            Macro macro = new Macro();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line.Contains("__LABEL__"))
                    {
                        macro.Label = reader.ReadLine();
                    }
                    else if (line.Contains("__GROUP__"))
                    {
                        macro.Group = reader.ReadLine();
                    }
                    else if (line.Contains("__BACKGROUND_COLOR__"))
                    {
                        macro.BackgroundColor = reader.ReadLine();
                    }
                    else if (line.Contains("__FOREGROUND_COLOR__"))
                    {
                        macro.ForegroundColor = reader.ReadLine();
                    }
                    else if (line.Contains("__CONTENTS__"))
                    {
                        macro.Contents = reader.ReadToEnd();
                    }
                }
            }

            return macro;
        }
    }
}
