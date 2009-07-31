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

        private string mName = string.Empty;
        private List<Power> mPowers = new List<Power>();
        private List<Feat> mFeats = new List<Feat>();
        private List<MagicItem> mMagicItems = new List<MagicItem>();
    }
}
