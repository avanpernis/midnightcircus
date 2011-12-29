using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TokenAssist
{
    public class ClassFeature
    {
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }

        private string mName = string.Empty;       
        private string mDescription = null;
    }
}
