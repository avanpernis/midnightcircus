using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;

namespace TokenAssist
{
    public static class MonsterLoader
    {
        public static Monster Load(string filename)
        {
            Monster m = new Monster();

            RollUtilities.EvaluateMaximum("1d6 + 5 + 2");

            try
            {
                XElement root = XElement.Load(filename);

                LoadMisc(m, root);
                LoadAbilities(m, root);
                LoadDefenses(m, root);
                LoadSkills(m, root);
                LoadPowers(m, root);
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
            GuardedExec(
                () => m.ActionPoints = int.Parse(docRoot.Element("ActionPoints").Attribute("FinalValue").Value),
                "Action Points"
            );
            GuardedExec(
                () => m.SavingThrow = int.Parse(docRoot.XPathSelectElement("//SavingThrows//MonsterSavingThrow").Attribute("FinalValue").Value),
                "Saving Throw"
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
                MessageSystem.Warning("Warning: error loading abilities, " + e.Message);
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
                MessageSystem.Warning("Warning: error loading defenses, " + e.Message);
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
                MessageSystem.Warning("error loading skills, " + e.Message);
            }
        }

        /// <summary>
        /// Read the powers and append them to the Monster
        /// </summary>
        /// <param name="m">The monster we are going to fill in</param>
        /// <param name="docRoot">The root of the xml to examine</param>
        private static void LoadPowers(Monster m, XElement docRoot)
        {
            XElement powersRoot = docRoot.Element("Powers");
            if (powersRoot == null)
            {
                MessageSystem.Warning("error loading powers.  Aborting power processing.");
                return;
            }

            foreach (XElement powerElement in powersRoot.Elements("MonsterPower"))
            {
                try
                {
                    MonsterPower newPower = new MonsterPower();
                    newPower.Name = powerElement.Element("Name").Value;

                    XElement actionElement = powerElement.Element("Action");
                    if (actionElement != null)
                        newPower.Action = actionElement.Value;

                    newPower.Category = powerElement.Element("Usage").Value;

                    // add keywords
                    XElement keywordNode = powerElement.Element("Keywords");
                    if (keywordNode != null)
                    {
                        foreach (XElement keyword in keywordNode.Elements("Name"))
                        {
                            newPower.Keywords.Add(keyword.Value);
                        }
                    }

                    LoadAttackFromPower(powerElement, ref newPower);

                    m.Powers.AddLast(newPower);
                }
                catch (Exception e)
                {
                    MessageSystem.Warning("error loading powers, " + e.Message);
                }
            }
        }

        /// <summary>
        /// Attempt to read the attack attributes for this power, and fill the passed power with them.
        /// </summary>
        /// <param name="powerElement">The root XML node to read from</param>
        /// <param name="newPower">The power to fill</param>
        private static void LoadAttackFromPower(XElement powerElement, ref MonsterPower newPower)
        {
            XElement attackNode = null;
            attackNode = powerElement.XPathSelectElement(".//Attacks//MonsterAttack");
            if (attackNode == null)
                return;

            XElement exp;

            // find the range value
            exp = attackNode.Element("Range");
            if (exp != null)
                newPower.RangeText = HtmlUtilities.ScrubString(exp.Value);

            // attempt to find the damage done by the attack
            exp = attackNode.XPathSelectElement("./Hit//Expression");
            if (exp != null)
                newPower.Damage = exp.Value;

            // attempt to find the "on hit" information of the power
            exp = attackNode.XPathSelectElement("./Hit//Description");
            if (exp != null)
                newPower.OnHitText = HtmlUtilities.ScrubString(exp.Value);

            // attempt to find information on the effect of the power
            XElement effectNode = attackNode.XPathSelectElement("./Effect//Description");
            if (effectNode != null)
            {
                newPower.EffectText = HtmlUtilities.ScrubString(effectNode.Value);
            }

            // pull out information on the attack roll and defenses
            XElement attackBonuses = attackNode.Element("AttackBonuses");
            if (attackBonuses != null)
            {
                exp = attackBonuses.Element("MonsterPowerAttackNumber");
                if (exp != null)
                {
                    XAttribute attr = exp.Attribute("FinalValue");
                    if (exp != null)
                        newPower.AttackBonus = int.Parse(attr.Value);
                }

                exp = attackBonuses.XPathSelectElement(".//DefenseName");
                if (exp != null)
                {
                    newPower.Defense = exp.Value;
                }
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
                MessageSystem.Warning("could not load field " + fieldName);
            }
        }
    }
}
