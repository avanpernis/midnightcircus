using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace WindowsFormsApplication1
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

        public void Load(string filename)
        {
            try
            {
                XElement root = XElement.Load(filename);
                LoadMisc(root);
                LoadAbilities(root);
                LoadDefenses(root);
                LoadSkills(root);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not parse monster file: " + filename + "\n\n" +
                    "Reason: " + ex.Message
                    );
            }
        }

        public void WriteToken(string filename)
        {
            Token t = new Token();

            foreach (KeyValuePair<string, AbilityScore> pair in Abilities)
            {
                TokenProperty p = new TokenProperty();
                p.Name = pair.Key;
                p.Value = pair.Value.Value.ToString();
                t.Properties.Add(p);
            }

            foreach (KeyValuePair<string, int?> pair in Defenses)
            {
                TokenProperty p = new TokenProperty();
                p.Name = pair.Key;
                p.Value = pair.Value.ToString();
                t.Properties.Add(p);
            }

            foreach (KeyValuePair<string, int> pair in Skills)
            {
                TokenProperty p = new TokenProperty();
                p.Name = pair.Key;
                p.Value = pair.Value.ToString();
                t.Properties.Add(p);
            }

            t.Write(filename);
        }

        protected void LoadMisc(XElement docRoot)
        {
            GuardedExec( 
                () => Name = docRoot.Element("Name").Value, 
                "Name"
            );
            GuardedExec(
                () => Level = int.Parse(docRoot.Element("Level").Value),
                "Level"
            );
            GuardedExec(
                () => HP = int.Parse(docRoot.Element("HitPoints").Attribute("FinalValue").Value),
                "HP"
            );
            GuardedExec(
                () => Speed = int.Parse(docRoot.Element("LandSpeed").Element("Speed").Attribute("FinalValue").Value),
                "Level"
            );
            GuardedExec(
                () => Initiative = int.Parse(docRoot.Element("Initiative").Attribute("FinalValue").Value),
                "Initiative"
            );
        }
        
        protected void LoadAbilities(XElement docRoot)
        {
            try
            {
                XElement attributeRoot = docRoot.Element("AbilityScores").Element("Values");

                foreach (XElement ab in attributeRoot.Elements("AbilityScoreNumber"))
                {
                    string abilityName = ab.Element("Name").Value;
                    int abilityValue = int.Parse(ab.Attribute("FinalValue").Value);

                    Abilities[abilityName].Value = abilityValue;
                }

                LogAdd(Abilities.Validate());
            }
            catch (Exception e)
            {
                LogAdd("Warning: error loading abilities, " + e.Message);
            }
        }

        protected void LoadDefenses(XElement docRoot)
        {
            try
            {
                XElement defenseRoot = docRoot.Element("Defenses").Element("Values");
                foreach (XElement e in defenseRoot.Elements("SimpleAdjustableNumber"))
                {
                    string name = e.Element("Name").Value;
                    int value = int.Parse(e.Attribute("FinalValue").Value);

                    Defenses[name] = value;
                }

                LogAdd(Defenses.Validate());
            }
            catch (Exception e)
            {
                LogAdd("Warning: error loading defenses, " + e.Message);
            }
        }

        protected void LoadSkills(XElement docRoot)
        {
            try
            {
                XElement skillRoot = docRoot.Element("Skills").Element("Values");
                foreach (XElement ab in skillRoot.Elements("SkillNumber"))
                {
                    string skillName = ab.Element("Name").Value;
                    int skillValue = int.Parse(ab.Attribute("FinalValue").Value);

                    Skills[skillName] = skillValue;
                }
            }
            catch (Exception e)
            {
                LogAdd("Warning: error loading skills, " + e.Message);
            }
        }

        protected void GuardedExec(System.Action act, string fieldName)
        {
            try
            {
                act();
            }
            catch (Exception)
            {
                LogAdd("Warning: error loading \""+fieldName+"\"");
            }
        }

        protected void LogAdd(string newMsg)
        {
            mLog.AppendLine(newMsg);
        }
    }
}
