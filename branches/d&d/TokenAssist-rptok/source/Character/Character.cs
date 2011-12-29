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
            ClassFeatures = new List<ClassFeature>();
            Powers = new List<Power>();
            Feats = new List<Feat>();
            MagicItems = new List<MagicItem>();
        }

        public int DailyItemuses
        {
            // heroic tier (levels 1 through 10) = 1 daily item use
            // paragon tier (levels 11 through 20) = 2 daily item uses
            // epic tier (levels 21 through 30) = 3 daily item uses
            get { return ((Level - 1) / 10) + 1; }
        }

        public string Portrait { get; set; }
        public Race Race { get; set; }
        public Class Class { get; set; }
        public List<ClassFeature> ClassFeatures { get; set; }
        public List<Power> Powers { get; set; }
        public List<Feat> Feats { get; set; }
        public List<MagicItem> MagicItems { get; set; }
    }
}
