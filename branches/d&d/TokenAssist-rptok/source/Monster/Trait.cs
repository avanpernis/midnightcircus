using System;
using System.Collections.Generic;
using System.Text;

namespace TokenAssist
{
    public class Trait
    {
        public Trait()
        {
            Keywords = new List<string>();
        }

        public string Name { get; set; }
        public int Range { get; set; } // we assume that non-zero means aura
        public string Description { get; set; }
        public List<string> Keywords { get; set; }
    }
}