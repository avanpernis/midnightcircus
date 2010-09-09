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
        public Actor()
        {
            Name = "Undefined";

            Abilities = new Abilities();
            Defenses = new Defenses();
            Skills = new Skills();
        }

        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int Speed { get; set; }
        public int Initiative { get; set; }
        public int ActionPoints { get; set; }
        public virtual int HealingSurges { get; set; }

        public Abilities Abilities { get; set; }
        public Defenses Defenses { get; set; }
        public Skills Skills { get; set; }

        public int HalfLevel { get { return Level / 2; } }
    }
}
