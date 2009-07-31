﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TokenAssist
{
    public class Weapon
    {
        public enum AttackStatType
        {
            Strength,
            Dexterity,
            Constitution,
            Wisdom,
            Intelligence,
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
                mMaxDamage = Regex.Replace(Damage, @"(\d*)d(\d+)", delegate(Match match)
                {
                    int value1 = int.Parse(match.Groups[1].Value);
                    int value2 = int.Parse(match.Groups[2].Value);
                    return match.Result((value1 * value2).ToString());
                });
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

        private string mName = string.Empty;
        private int mAttackBonus = int.MinValue;
        private string mDamage = null;
        private string mMaxDamage = null;
        private string mCriticalDamage = null;
        private AttackStatType mAttackStat = AttackStatType.Undefined;
        private DefenseType mDefense = DefenseType.Undefined;
    }
}
