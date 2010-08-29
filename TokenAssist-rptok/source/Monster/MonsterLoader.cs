using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace TokenAssist
{
    public static class MonsterLoader
    {
        public static Monster Load(string filename)
        {
            Monster m = new Monster();
            try
            {
                XElement root = XElement.Load(filename);

                LoadMisc(m, root);
                LoadAbilities(m, root);
                LoadDefenses(m, root);
                LoadSkills(m, root);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not parse monster file: " + filename + "\n\n" +
                    "Reason: " + ex.Message
                    );
            }

            return m;
        }        
        
        private static void LoadMisc(Monster m, XElement docRoot)
        {
            GuardedExec(
                () => m.Name = docRoot.Element("Name").Value,
                "Name"
            );
            GuardedExec(
                () => m.Level = int.Parse(docRoot.Element("Level").Value),
                "Level"
            );
            GuardedExec(
                () => m.HP = int.Parse(docRoot.Element("HitPoints").Attribute("FinalValue").Value),
                "HP"
            );
            GuardedExec(
                () => m.Speed = int.Parse(docRoot.Element("LandSpeed").Element("Speed").Attribute("FinalValue").Value),
                "Level"
            );
            GuardedExec(
                () => m.Initiative = int.Parse(docRoot.Element("Initiative").Attribute("FinalValue").Value),
                "Initiative"
            );
        }

        private static void LoadAbilities(Monster m, XElement docRoot)
        {
            try
            {
                XElement attributeRoot = docRoot.Element("AbilityScores").Element("Values");

                foreach (XElement ab in attributeRoot.Elements("AbilityScoreNumber"))
                {
                    string abilityName = ab.Element("Name").Value;
                    int abilityValue = int.Parse(ab.Attribute("FinalValue").Value);

                    m.Abilities[abilityName].Value = abilityValue;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Warning: error loading abilities, " + e.Message);
            }
        }

        private static void LoadDefenses(Monster m, XElement docRoot)
        {
            try
            {
                XElement defenseRoot = docRoot.Element("Defenses").Element("Values");
                foreach (XElement e in defenseRoot.Elements("SimpleAdjustableNumber"))
                {
                    string name = e.Element("Name").Value;
                    int value = int.Parse(e.Attribute("FinalValue").Value);

                    m.Defenses[name] = value;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Warning: error loading defenses, " + e.Message);
            }
        }
        
        private static void LoadSkills(Monster m, XElement docRoot)
        {
            try
            {
                XElement skillRoot = docRoot.Element("Skills").Element("Values");
                foreach (XElement ab in skillRoot.Elements("SkillNumber"))
                {
                    string skillName = ab.Element("Name").Value;
                    int skillValue = int.Parse(ab.Attribute("FinalValue").Value);

                    m.Skills[skillName] = skillValue;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Warning: error loading skills, " + e.Message);
            }
        }

        private static void GuardedExec(System.Action act, string fieldName)
        {
            try
            {
                act();
            }
            catch (Exception)
            {
            }
        }
    }
}
