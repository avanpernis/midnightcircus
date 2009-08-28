using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public class Character
    {
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public Race Race
        {
            get { return mRace; }
            set { mRace = value; }
        }

        public Class Class
        {
            get { return mClass; }
            set { mClass = value; }
        }

        public List<Power> Powers
        {
            get { return mPowers; }
            set { mPowers = value; }
        }

        public List<Feat> Feats
        {
            get { return mFeats; }
            set { mFeats = value; }
        }

        public List<MagicItem> MagicItems
        {
            get { return mMagicItems; }
            set { mMagicItems = value; }
        }

        public Dictionary<string, Stat> Stats
        {
            get { return mStats; }
            set { mStats = value; }
        }

        /// <summary>
        /// Get the value of the specified stat if it exists. If the specified stat does not exist,
        /// it is assumed that the value of the stat is effectively zero.
        /// </summary>
        /// <param name="statName">the name of the stat to retrieve</param>
        /// <returns>the value of the specified stat if it exists, otherwise zero</returns>
        public int GetStatValue(string statName)
        {
            Stat stat;
            return mStats.TryGetValue(statName, out stat) ? stat.Value : 0;
        }

        private string mName = string.Empty;
        private Race mRace;
        private Class mClass;
        private List<Power> mPowers = new List<Power>();
        private List<Feat> mFeats = new List<Feat>();
        private List<MagicItem> mMagicItems = new List<MagicItem>();
        private Dictionary<string, Stat> mStats = new Dictionary<string, Stat>();
    }
}
