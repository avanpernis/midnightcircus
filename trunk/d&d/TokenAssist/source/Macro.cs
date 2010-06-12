using System;
using System.Collections.Generic;
using System.Text;

namespace TokenAssist
{
    public class Macro
    {
        public string Label
        {
            get { return mLabel; }
            set { mLabel = value; }
        }

        public string Group
        {
            get { return mGroup; }
            set { mGroup = value; }
        }

        public string BackgroundColor
        {
            get { return mBackgroundColor; }
            set { mBackgroundColor = value; }
        }

        public string ForegroundColor
        {
            get { return mForegroundColor; }
            set { mForegroundColor = value; }
        }

        public string Contents
        {
            get { return mContents; }
            set { mContents = value; }
        }

        private string mLabel = string.Empty;
        private string mGroup = string.Empty;
        private string mBackgroundColor = string.Empty;
        private string mForegroundColor = string.Empty;
        private string mContents = string.Empty;
    }
}
