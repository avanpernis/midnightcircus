using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public class AbilityScore
    {
        public int Value
        {
            get
            {
                if (mValue != null)
                    return (int)mValue;
                else
                    throw new NullReferenceException("attempt to read a value that was never initialized");
            }

            set 
            { 
                mValue = value; 
            }
        }

        public int Modifier
        {
            get { return ((int)mValue - 10) / 2; }
            set { mValue = 10 + value * 2; }
        }

        private int? mValue = null;
    }
}
