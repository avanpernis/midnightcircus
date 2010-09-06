using System;
using System.Collections.Generic;
using System.Text;

namespace TokenAssist
{
    ////////////////////////////////////////////////////////////////////////////
    // Base class for information that is common among characters, monsters, etc
    ////////////////////////////////////////////////////////////////////////////
    public abstract class Actor
    {
        public string Name = "Unknown";
        public int Level = 0;
        public int HP = 0;
        public int Speed = 0;
        public int Initiative = 0;

        public Abilities Abilities = new Abilities();
        public Defenses Defenses = new Defenses();
        public Skills Skills = new Skills();       
    }
}
