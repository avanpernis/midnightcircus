using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace TokenAssist
{
    static class Program
    {
        private const string UserSettingsFilename = "UserSettings.xml";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);

            LoadUserSettings();

            string source = string.Empty;

            // assume that if there is at least one command-line argument (other than the name of the executable),
            // that it is the path to the dnd4e file that the user wants to operate on
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                source = Environment.GetCommandLineArgs()[1];
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(source));
        }

        public static void OnApplicationExit(object sender, EventArgs e)
        {
            SaveUserSettings();
        }

        private static void LoadUserSettings()
        {
            if (!File.Exists(UserSettingsFilename))
            {
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));

            using (TextReader reader = new StreamReader(UserSettingsFilename))
            {
                UserSettings.Instance = (UserSettings)serializer.Deserialize(reader);
            }
        }

        private static void SaveUserSettings()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));
            
            using (TextWriter writer = new StreamWriter(UserSettingsFilename))
            {
                serializer.Serialize(writer, UserSettings.Instance);
            }
        }
    }
}
