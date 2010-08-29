using System;
using System.Collections.Generic;
using System.Text;


namespace TokenAssist
{
    public class Monster
    {
        ///////////////////////////////////////////////////////////////////////
        // Hold all the ability scores of this monster
        ///////////////////////////////////////////////////////////////////////
        public class MonsterAbilities : Dictionary<string, AbilityScore>
        {
            public MonsterAbilities()
            {
                Add("Strength", new AbilityScore());
                Add("Intelligence", new AbilityScore());
                Add("Wisdom", new AbilityScore());
                Add("Dexterity", new AbilityScore());
                Add("Constitution", new AbilityScore());
                Add("Charisma", new AbilityScore());
            }

            public string Validate()
            {
                StringBuilder result = new StringBuilder();

                foreach (KeyValuePair<string, AbilityScore> pair in this)
                {
                    if (pair.Value == null)
                        result.Append("Warning: Ability \"" + pair.Key + "\" was not initialized]n");
                }
                return result.ToString();
            }
        }

        ///////////////////////////////////////////////////////////////////////
        // Hold all the defense information for this monster
        ///////////////////////////////////////////////////////////////////////
        public class MonsterDefenses : Dictionary<string, int?>
        {
            public MonsterDefenses()
            {
                Add("AC", null);
                Add("Fortitude", null);
                Add("Reflex", null);
                Add("Will", null);
            }

            public string Validate()
            {
                StringBuilder result = new StringBuilder();

                foreach (KeyValuePair<string, int?> pair in this)
                {
                    if (pair.Value == null)
                        result.Append("Warning: Ability \"" + pair.Key + "\" was not initialized]n");
                }

                return result.ToString();
            }
        }

        ///////////////////////////////////////////////////////////////////////
        // Hold all the skill scores for this monster
        ///////////////////////////////////////////////////////////////////////
        public class MonsterSkills : Dictionary<string, int>
        {
            public MonsterSkills()
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

        public string Name = "Unknown";
        public int Level = 0;
        public int HP = 0;
        public int Speed = 0;
        public int Initiative = 0;

        public MonsterAbilities Abilities = new MonsterAbilities();
        public MonsterDefenses Defenses = new MonsterDefenses();
        public MonsterSkills Skills = new MonsterSkills();

        protected StringBuilder mLog = new StringBuilder();
        public string Log
        {
            get 
            { 
                string temp = mLog.ToString();
                mLog.Clear();
                return temp;
            }
        }


        public Monster()
        {
            
        }
        

        protected void LogAdd(string newMsg)
        {
            mLog.AppendLine(newMsg);
        }
    }
}
