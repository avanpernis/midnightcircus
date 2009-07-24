using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TokenAssist
{
    public class MagicItem
    {
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        private string mName = string.Empty;
    }
}
