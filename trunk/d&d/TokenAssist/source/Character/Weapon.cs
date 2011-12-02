using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TokenAssist
{
    public class Weapon
    {
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public int AttackBonus
        {
            get { return mAttackBonus; }
            set { mAttackBonus = value; }
        }

        public string Damage
        {
            get { return mDamage; }
            set
            {
                mDamage = value;

                // also calculate the maximum damage that this weapon is capable of
                mMaxDamage = RollUtilities.EvaluateMaximum(Damage).ToString();
            }
        }

        public string MaxDamage
        {
            get
            {
                return mMaxDamage;
            }
        }

        public string CriticalDamage
        {
            get { return mCriticalDamage; }
            set { mCriticalDamage = value; }
        }

        public string AttackStat
        {
            get { return mAttackStat; }
            set { mAttackStat = value; }
        }

        public string Defense
        {
            get { return mDefense; }
            set { mDefense = value; }
        }

        private string mName = string.Empty;
        private int mAttackBonus = int.MinValue;
        private string mDamage = null;
        private string mMaxDamage = null;
        private string mCriticalDamage = null;
        private string mAttackStat = null;
        private string mDefense = null;
    }
}
