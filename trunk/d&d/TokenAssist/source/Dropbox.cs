using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace TokenAssist
{
    public static class Dropbox
    {
        private static readonly string sFolder = null;

        public static bool IsInstalled
        {
            get
            {
                return File.Exists(DatabasePath);
            }
        }

        public static string Folder
        {
            get
            {
                if (!IsInstalled)
                {
                    return null;
                }

                // have we already calculated the dropbox folder?
                if (sFolder != null)
                {
                    return sFolder;
                }

                string connectionString = string.Format(@"Data Source={0};Version=3;New=False;Compress=True;", DatabasePath);

                using (SQLiteConnection sqlConnection = new SQLiteConnection(connectionString))
                {
                    sqlConnection.Open();

                    SQLiteCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandText = @"SELECT value FROM config WHERE key=""dropbox_path""";

                    SQLiteDataReader sqlReader = sqlCommand.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(sqlReader);

                    string value = null;

                    if (dataTable.Rows.Count > 0)
                    {
                        value = dataTable.Rows[0]["value"] as string;
                        value = Encoding.Default.GetString(System.Convert.FromBase64String(value));

                        // the dropbox path has a python pickle -- we need to work around this.
                        // The 'V' in the start specifies it is an unicode string object.
                        // The \u005C is a backslash in unicode.
                        // After that, there is '\npX\n.' which specifies the protocol it uses (p1) and the end of the object (lone dot).
                        value = value.Substring(1, value.IndexOf("\n") - 1).Replace(@"\u005C", @"\");
                    }
                    else
                    {
                        value = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"My Dropbox");
                    }

                    if (value == null)
                    {
                        throw new Exception("Couldn't find dropbox");
                    }
                    else
                        return value;
                }
            }
        }

        private static string DatabasePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Dropbox\dropbox.db");
            }
        }
    }
}
