using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace TokenAssist
{
    [XmlRoot("UserSettings")]
    public class UserSettings
    {
        private const int HistorySize = 10;

        static UserSettings mInstance = new UserSettings();

        public static UserSettings Instance
        {
            get { return mInstance; }
            set { mInstance = value; }
        }

        [XmlElement("Username")]
        public string Username
        {
            get { return mUsername; }
            set { mUsername = value; }
        }

        [XmlElement("FilenameHistory")]
        public List<string> FilenameHistory
        {
            get { return mFilenameHistory; }
            set { mFilenameHistory = value; }
        }

        [XmlElement("DestinationHistory")]
        public List<string> DestinationHistory
        {
            get { return mDestinationHistory; }
            set { mDestinationHistory = value; }
        }

        public void OpenedFile(string filename)
        {
            AdjustHistory(mFilenameHistory, filename);
        }

        public void UsedDestination(string destination)
        {
            AdjustHistory(mDestinationHistory, destination);
        }

        private static void AdjustHistory(List<string> history, string item)
        {
            int index = history.IndexOf(item);

            if (index != -1)
            {
                history.RemoveAt(index);
            }

            // insert the recently used item at the front
            history.Insert(0, item);

            // keep only the most recently used items
            while (history.Count > HistorySize)
            {
                history.RemoveAt(history.Count - 1);
            }
        }

        private string mUsername = string.Empty;
        private List<string> mFilenameHistory = new List<string>();
        private List<string> mDestinationHistory = new List<string>();
    }
}
