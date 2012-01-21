using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TokenAssist
{
    public class CharacterPower : Power
    {
        public string AttackTypeAndRange
        {
            get { return mAttackTypeAndRange; }
            set { mAttackTypeAndRange = value; }
        }

        public bool AllowsForMultipleAttacks
        {
            get { return mAllowsForMultipleAttacks; }
            set { mAllowsForMultipleAttacks = value; }
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

        private string mAttackTypeAndRange = string.Empty;
        private bool mAllowsForMultipleAttacks = false;
        private List<Weapon> mWeapons = new List<Weapon>();
        private string mUrl = null;
        private string mCompendiumEntry = null;
    }
}
