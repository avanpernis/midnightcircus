﻿using System;
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

        public string Url
        {
            get { return mUrl; }
            set { mUrl = value; }
        }

        public string CompendiumEntry
        {
            get { return mCompendiumEntry; }
            set { mCompendiumEntry = value; }
        }

        private string mName = string.Empty;
        private string mUrl = null;
        private string mCompendiumEntry = null;
    }
}
