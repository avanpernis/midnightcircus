using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public class Power
    {
        public enum UsageType
        {
            AtWill,
            Encounter,
            Daily,
            Recharge,
            Undefined
        }

        public enum ActionType
        {
            No,
            Free,
            Minor,
            Move,
            Opportunity,
            Standard,
            ImmediateInterrupt,
            ImmediateReaction,
            Undefined
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

        private string mName = string.Empty;
        private UsageType mUsage = UsageType.Undefined;
        private ActionType mAction = ActionType.Undefined;
    }
}
