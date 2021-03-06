﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;

namespace TokenAssist
{
    /// <summary>
    /// This class creates a Monster instance containing all the relevant
    /// data found in the file
    /// </summary>
    public class MonsterLoader : Loader
    {
        static public Monster Load(string filename)
        {
            MonsterLoader loader = new MonsterLoader(filename);
            return loader.Monster;
        }
        

        public MonsterLoader(string filename)
        {
            try
            {
                XElement root = XElement.Load(filename);

                LoadMisc(root);
                LoadAbilities(root);
                LoadDefenses(root);
                LoadSkills(root);
                LoadImmunities(root);
                LoadResistances(root);
                LoadVulnerabilities(root);
                LoadTraits(root);
                LoadPowers(root);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not parse monster file: " + filename + "\n\n" +
                    "Reason: " + ex.Message
                    );
            }
        }

        
        private void LoadMisc(XElement docRoot)
        {
            GuardedExec(
                () => mMonster.Name = docRoot.Element("Name").Value,
                "Name"
            );
            GuardedExec(
                () => mMonster.Level = int.Parse(docRoot.Element("Level").Value),
                "Level"
            );
            GuardedExec(
                () => mMonster.HP = int.Parse(docRoot.Element("HitPoints").Attribute("FinalValue").Value),
                "HP"
            );
            GuardedExec(
                () => mMonster.Speed = int.Parse(docRoot.Element("LandSpeed").Element("Speed").Attribute("FinalValue").Value),
                "Level"
            );
            GuardedExec(
                () => mMonster.Initiative = int.Parse(docRoot.Element("Initiative").Attribute("FinalValue").Value),
                "Initiative"
            );
            GuardedExec(
                () => mMonster.ActionPoints = int.Parse(docRoot.Element("ActionPoints").Attribute("FinalValue").Value),
                "Action Points"
            );
            GuardedExec(
                () => mMonster.SavingThrow = int.Parse(docRoot.XPathSelectElement("//SavingThrows//MonsterSavingThrow").Attribute("FinalValue").Value),
                "Saving Throw"
            );
        }

        private void LoadAbilities(XElement docRoot)
        {
            try
            {
                XElement attributeRoot = docRoot.Element("AbilityScores").Element("Values");

                foreach (XElement ab in attributeRoot.Elements("AbilityScoreNumber"))
                {
                    string abilityName = ab.Element("Name").Value;
                    int abilityValue = int.Parse(ab.Attribute("FinalValue").Value);

                    mMonster.Abilities[abilityName].Value = abilityValue;
                }
            }
            catch (Exception e)
            {
                MessageSystem.Warning("Warning: error loading abilities, " + e.Message);
            }
        }

        private void LoadDefenses(XElement docRoot)
        {
            try
            {
                XElement defenseRoot = docRoot.Element("Defenses").Element("Values");
                foreach (XElement e in defenseRoot.Elements("SimpleAdjustableNumber"))
                {
                    string name = e.Element("Name").Value;
                    int value = int.Parse(e.Attribute("FinalValue").Value);

                    mMonster.Defenses[name] = value;
                }
            }
            catch (Exception e)
            {
                MessageSystem.Warning("Warning: error loading defenses, " + e.Message);
            }
        }
        
        private void LoadSkills(XElement docRoot)
        {
            try
            {
                XElement skillRoot = docRoot.Element("Skills").Element("Values");
                foreach (XElement ab in skillRoot.Elements("SkillNumber"))
                {
                    string skillName = ab.Element("Name").Value;
                    int skillValue = int.Parse(ab.Attribute("FinalValue").Value);

                    mMonster.Skills[skillName] = skillValue;
                }
            }
            catch (Exception e)
            {
                MessageSystem.Warning("error loading skills, " + e.Message);
            }
        }

        private void LoadImmunities(XElement docRoot)
        {
            try
            {
                XElement immunityRoot = docRoot.Element("Immunities");
                foreach (XElement e in immunityRoot.Elements("ObjectReference"))
                {
                    string immunityName = e.Element("ReferencedObject").Element("Name").Value;

                    mMonster.Immunities.Add(new DamageDetails(immunityName));
                }
            }
            catch (Exception e)
            {
                MessageSystem.Warning("error loading immunities, " + e.Message);
            }
        }

        private void LoadResistances(XElement docRoot)
        {
            try
            {
                XElement resistanceRoot = docRoot.Element("Resistances");
                foreach (XElement e in resistanceRoot.Elements("CreatureSusceptibility"))
                {
                    string resistanceName = e.Element("ReferencedObject").Element("Name").Value;
                    int resistanceAmount = int.Parse(e.Element("Amount").Attribute("FinalValue").Value);
                    string resistanceDetails = e.Element("Details").Value;

                    mMonster.Resistances.Add(new DamageDetails(resistanceName, resistanceAmount, resistanceDetails));
                }
            }
            catch (Exception e)
            {
                MessageSystem.Warning("error loading immunities, " + e.Message);
            }
        }

        private void LoadVulnerabilities(XElement docRoot)
        {
            try
            {
                XElement vulnerabilityRoot = docRoot.Element("Weaknesses");
                foreach (XElement e in vulnerabilityRoot.Elements("CreatureSusceptibility"))
                {
                    string vulnerabilityName = e.Element("ReferencedObject").Element("Name").Value;
                    int vulnerabilityAmount = int.Parse(e.Element("Amount").Attribute("FinalValue").Value);
                    string vulnerabilityDetails = e.Element("Details").Value;

                    mMonster.Vulnerabilities.Add(new DamageDetails(vulnerabilityName, vulnerabilityAmount, vulnerabilityDetails));
                }
            }
            catch (Exception e)
            {
                MessageSystem.Warning("error loading immunities, " + e.Message);
            }
        }

        private void LoadTraits(XElement docRoot)
        {
            XElement powersRoot = docRoot.Element("Powers");
            if (powersRoot == null)
            {
                MessageSystem.Warning("error loading traits.  Aborting trait processing.");
                return;
            }

            foreach (XElement traitElement in powersRoot.Elements("MonsterTrait"))
            {
                try
                {
                    Trait trait = new Trait();
                    trait.Name = traitElement.Element("Name").Value;
                    trait.Range = int.Parse(traitElement.Element("Range").Attribute("FinalValue").Value);
                    trait.Description = traitElement.Element("Details").Value;

                    foreach (XElement e in traitElement.Element("Keywords").Elements("ObjectReference"))
                    {
                        trait.Keywords.Add(e.Element("ReferencedObject").Element("Name").Value.Trim().ToLower());
                    }

                    mMonster.Traits.Add(trait);
                }
                catch (Exception e)
                {
                    MessageSystem.Warning("error loading traits, " + e.Message);
                }
            }
        }


        /// <summary>
        /// Read the powers and append them to the Monster
        /// </summary>
        /// <param name="docRoot">The root of the xml to examine</param>
        private void LoadPowers(XElement docRoot)
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
                        newPower.Action = ActionFromStr(actionElement.Value);

                    XElement usageElement = powerElement.Element("Usage");
                    if (usageElement != null)
                        newPower.Usage = UsageFromStr(usageElement.Value);
                    else
                        newPower.Usage = Power.UsageType.AtWill;

                    XElement node = powerElement.Element("UsageDetails");
                    if (node != null)
                        newPower.UsageDetails = node.Value;

                    // add keywords
                    XElement keywordNode = powerElement.Element("Keywords");
                    if (keywordNode != null)
                    {
                        foreach (XElement keyword in keywordNode.Elements("Name"))
                        {
                            newPower.Keywords.Add(keyword.Value);
                        }
                    }

                    // power trigger
                    XElement triggerNode = powerElement.Element("Trigger");
                    if (triggerNode != null)
                        newPower.TriggerText = triggerNode.Value;

                    LoadAttackFromPower(powerElement, ref newPower);

                    mMonster.Powers.AddLast(newPower);
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
        private void LoadAttackFromPower(XElement powerElement, ref MonsterPower newPower)
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

            // find the target information
            exp = attackNode.Element("Targets");
            if (exp != null)
                newPower.Targets = HtmlUtilities.ScrubString(exp.Value);

            newPower.MultiTarget = false;
            if (!string.IsNullOrWhiteSpace(newPower.Targets))
            {
                newPower.MultiTarget |= newPower.Targets.Contains("enemies");
                newPower.MultiTarget |= newPower.Targets.Contains("creatures");
            }

            // attempt to find the damage done by the attack
            exp = attackNode.XPathSelectElement("./Hit//Expression");
            if ((exp != null) && (exp.Value != ""))
                newPower.HitDamageExp = exp.Value;

            // attempt to find the "on hit" information of the power
            exp = attackNode.XPathSelectElement("./Hit//Description");
            if ((exp != null) && (exp.Value != ""))
            {
                newPower.OnHitText = HtmlUtilities.ScrubString(exp.Value);
            }
            
            exp = attackNode.XPathSelectElement("./Miss//Expression");
            if ((exp != null) && (exp.Value != ""))
            {
                newPower.MissDamageExp = HtmlUtilities.ScrubString(exp.Value);
            }

            exp = attackNode.XPathSelectElement("./Miss//Description");
            if ((exp != null) && (exp.Value != ""))
            {
                newPower.OnMissText = HtmlUtilities.ScrubString(exp.Value);
            }

            // attempt to find information on the effect of the power
            XElement effectNode = attackNode.XPathSelectElement("./Effect//Description");
            if ((effectNode != null) && (effectNode.Value != ""))
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
        
        
        /// <summary>
        ///  The Monster object that this instance will act on
        /// </summary>
        public Monster Monster
        {
            get { return mMonster; }
        }
        private Monster mMonster = new Monster();
    }
}
