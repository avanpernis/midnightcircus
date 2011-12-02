using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public class MonsterPower
    {
        public string Name;

        /// <summary>
        /// The category this power should appear in
        /// </summary>
        public string Category;

        /// <summary>
        /// The expression that represents how much damage should be dealt
        /// </summary>
        public string Damage;

        /// <summary>
        /// Description of the range for this attack
        /// </summary>
        public string RangeText;

        /// <summary>
        /// Explanation of what happens should the power hit
        /// </summary>
        public string OnHitText;

        /// <summary>
        /// Explanation of what happens regardless of power outcome
        /// </summary>
        public string EffectText;

        /// <summary>
        /// The bonus to attack roll that will be applied for this power
        /// </summary>
        public int? AttackBonus;

        /// <summary>
        /// The attribute that you must beat with your attack roll
        /// </summary>
        public string Defense;

        /// <summary>
        /// Does this power involve multiple attack rolls?
        /// </summary>
        public bool MultiTarget;

        public SortedSet<string> Keywords = new SortedSet<string>();

        
    };
}
