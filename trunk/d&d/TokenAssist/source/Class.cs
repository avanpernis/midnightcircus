using System;
using System.Collections.Generic;
using System.Text;

namespace TokenAssist
{
    public class Class
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

        private string mName = string.Empty;
        private string mUrl = null;
    }
}
