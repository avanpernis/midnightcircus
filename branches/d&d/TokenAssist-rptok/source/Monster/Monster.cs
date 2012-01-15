using System;
using System.Collections.Generic;
using System.Text;

namespace TokenAssist
{
    public class Monster : Actor
    {
        public LinkedList<MonsterPower> Powers = new LinkedList<MonsterPower>();
        public List<DamageDetails> Immunities = new List<DamageDetails>();
        public List<DamageDetails> Resistances = new List<DamageDetails>();
        public List<DamageDetails> Vulnerabilities = new List<DamageDetails>();
        public List<Trait> Traits = new List<Trait>();

        public Monster()
            : base()
        {
        }

        public override int HealingSurges
        {
            get
            {
                // heroic tier (levels 1 through 10) = 1 healing surge
                // paragon tier (levels 11 through 20) = 2 healing surges
                // epic tier (levels 21 through 30) = 3 healing surges
                return ((Level - 1) / 10) + 1;
            }
        }
    }
}