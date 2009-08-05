using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    // predefined characteristics that *every* character would have
    public class Stat
    {
        public enum StatType
        {
            Ability,
            Skill,
            SavingThrow
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public string Value
        {
            get { return mValue; }
            set { mValue = value; }
        }


        private string mName = string.Empty;
        private string mValue = string.Empty;


    }

    // a stat with a conditional modifier, derived from the regular statistic
    public class condStat : Stat
    {
        public string Condition
        {
            get { return mCondition; }
            set { mCondition = value; }
        }

        public string Modifier
        {
            get { return mModifier; }
            set { mModifier = value; }
        }

        private string mCondition = string.Empty;
        private string mModifier = string.Empty;

    }
}
