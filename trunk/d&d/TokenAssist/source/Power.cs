using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TokenAssist
{
    public class Power
    {
        public enum UsageType
        {
            AtWill,
            Encounter,
            Daily,
            Undefined
        }

        public enum ActionType
        {
            Free,
            Minor,
            Move,
            Standard,
            ImmediateInterrupt,
            ImmediateReaction,
            Undefined
        }

        public enum AttackStatType
        {
            Strength,
            Dexterity,
            Constitution,
            Intelligence,
            Wisdom,
            Charisma,
            Undefined
        }

        public enum DefenseType
        {
            AC,
            Fortitude,
            Reflex,
            Will,
            Undefined
        }

        public const int DefaultAttackBonus = int.MinValue;
        public static readonly string DefaultDamage;

        static Power()
        {
            DefaultDamage = int.MinValue.ToString();
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public UsageType Usage
        {
            get { return mUsage; }
            set { mUsage = value; }
        }

        public ActionType Action
        {
            get { return mAction; }
            set { mAction = value; }
        }

        public int AttackBonus
        {
            get { return mAttackBonus; }
            set { mAttackBonus = value; }
        }

        public string Damage
        {
            get { return mDamage; }
            set { mDamage = value; }
        }

        public string MaxDamage
        {
            get
            {
                return Regex.Replace(Damage, @"(\d*)d(\d+)", delegate(Match match)
                {
                    int value1 = int.Parse(match.Groups[1].Value);
                    int value2 = int.Parse(match.Groups[2].Value);
                    return match.Result((value1 * value2).ToString());
                });
            }
        }

        public AttackStatType AttackStat
        {
            get { return mAttackStat; }
            set { mAttackStat = value; }
        }

        public DefenseType Defense
        {
            get { return mDefense; }
            set { mDefense = value; }
        }

        public string Url
        {
            get { return mUrl; }
            set { mUrl = value; }
        }

        public string CompendiumEntry
        {
            get { return mCompendiumEntry; }
            set { mCompendiumEntry = value; }
        }

        private string mName = string.Empty;
        private UsageType mUsage = UsageType.Undefined;
        private ActionType mAction = ActionType.Undefined;
        private int mAttackBonus = DefaultAttackBonus;
        private string mDamage = DefaultDamage;
        private AttackStatType mAttackStat = AttackStatType.Undefined;
        private DefenseType mDefense = DefenseType.Undefined;
        private string mUrl = null;
        private string mCompendiumEntry = null;
    }
}
