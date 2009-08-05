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

        private string mUsername = string.Empty;
    }
}
