using System;
using System.Collections.Generic;
using System.Text;

namespace TokenAssist
{
    public class Character : Actor
    {
        public Character()
            : base()
        {
        }

        public Race Race { get; set; }
        public Class Class { get; set; }
        public List<Power> Powers { get; set; }
        public List<Feat> Feats { get; set; }
        public List<MagicItem> MagicItems { get; set; }
        public Dictionary<string, Stat> Stats { get; set; }

        /// <summary>
        /// Get the value of the specified stat if it exists. If the specified stat does not exist,
        /// it is assumed that the value of the stat is effectively zero.
        /// </summary>
        /// <param name="statName">the name of the stat to retrieve</param>
        /// <returns>the value of the specified stat if it exists, otherwise zero</returns>
        public int GetStatValue(string statName)
        {
            Stat stat;
            return Stats.TryGetValue(statName, out stat) ? stat.Value : 0;
        }
    }
}
