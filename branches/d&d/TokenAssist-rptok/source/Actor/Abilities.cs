using System;
using System.Collections.Generic;
using System.Text;

namespace TokenAssist
{
    ////////////////////////////////////////////////////////////////////////////
    // Hold all the ability scores for an actor
    ////////////////////////////////////////////////////////////////////////////
    public class Abilities : Dictionary<string, AbilityScore>
    {
        public Abilities()
        {
            Add("Strength", new AbilityScore());
            Add("Dexterity", new AbilityScore());
            Add("Constitution", new AbilityScore());
            Add("Intelligence", new AbilityScore());
            Add("Wisdom", new AbilityScore());
            Add("Charisma", new AbilityScore());
        }
    }
}