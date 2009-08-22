using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public class Stat
    {
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public int Value
        {
            get { return mValue; }
            set { mValue = value; }
        }

        private string mName = string.Empty;
        private int mValue = int.MinValue;
    }
}
