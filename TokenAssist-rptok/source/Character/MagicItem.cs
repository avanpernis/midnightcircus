using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TokenAssist
{
    public class MagicItem
    {
        public enum PowerUsageType
        {
            AtWill,
            Encounter,
            Daily,
            HealingSurge,
            Consumable,
            Undefined
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public int LootCount
        {
            get { return mLootCount; }
            set { mLootCount = value; }
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

        public PowerUsageType PowerUsage
        {
            get { return mPowerUsage; }
            set { mPowerUsage = value; }
        }

        public CharacterPower.ActionType PowerAction
        {
            get { return mPowerAction; }
            set { mPowerAction = value; }
        }

        public bool HasPower
        {
            get { return (PowerUsage != PowerUsageType.Undefined) && (PowerAction != CharacterPower.ActionType.Undefined); }
        }

        private string mName = string.Empty;
        private int mLootCount = 0;
        private string mUrl = null;
        private string mCompendiumEntry = null;
        private PowerUsageType mPowerUsage = PowerUsageType.Undefined;
        private CharacterPower.ActionType mPowerAction = CharacterPower.ActionType.Undefined;
    }
}
