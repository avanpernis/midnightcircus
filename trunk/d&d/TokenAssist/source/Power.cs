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

        public List<Weapon> Weapons
        {
            get { return mWeapons; }
            set { mWeapons = value; }
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
        private List<Weapon> mWeapons = new List<Weapon>();       
        private string mUrl = null;
        private string mCompendiumEntry = null;
    }
}
