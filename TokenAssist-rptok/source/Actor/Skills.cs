using System;
using System.Collections.Generic;
using System.Text;

namespace TokenAssist
{
    ////////////////////////////////////////////////////////////////////////////
    // Hold all the skill scores for an actor
    ////////////////////////////////////////////////////////////////////////////
    public class Skills : Dictionary<string, int>
    {
        public Skills()
        {
            Add("Perception", 0);
            Add("Athletics", 0);
            Add("Stealth", 0);
            Add("Acrobatics", 0);
            Add("Arcana", 0);
            Add("Bluff", 0);
            Add("Diplomacy", 0);
            Add("Dungeoneering", 0);
            Add("Endurance", 0);
            Add("Heal", 0);
            Add("History", 0);
            Add("Insight", 0);
            Add("Intimidate", 0);
            Add("Nature", 0);
            Add("Religion", 0);
            Add("Streetwise", 0);
            Add("Thievery", 0);
        }
    }
}