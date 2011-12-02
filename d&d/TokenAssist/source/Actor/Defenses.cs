using System;
using System.Collections.Generic;
using System.Text;

namespace TokenAssist
{
    ////////////////////////////////////////////////////////////////////////////
    // Hold all the defense information for an actor
    ////////////////////////////////////////////////////////////////////////////
    public class Defenses : Dictionary<string, int?>
    {
        public Defenses()
        {
            Add("AC", null);
            Add("Fortitude", null);
            Add("Reflex", null);
            Add("Will", null);
        }
    }
}