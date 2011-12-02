using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace TokenAssist
{
    public static class Dropbox
    {
        private static string sFolder = null;

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

                using (StreamReader tmpFile = new StreamReader(DatabasePath))
                {
                    string line = null;
                    string nextLine = null;
                    while ((nextLine = tmpFile.ReadLine()) != null) // until end of file
                        line = tmpFile.ReadLine(); // write current line to var

                    if (line != null)
                    {
                        sFolder = DecodeFrom64(line);
                        return sFolder;
                    }
                    else
                        return null;

                }
            }
        }

        private static string DatabasePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Dropbox\host.db");
            }
        }

        /// <summary>
        /// The method to Decode your Base64 strings.
        /// </summary>
        /// <param name="encodedData">The String containing the characters to decode.</param>
        /// <returns>A String containing the results of decoding the specified sequence of bytes.</returns>
        private static string DecodeFrom64(string encodedData)
        {

            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);

            string returnValue = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);

            return returnValue;

        }
    }
}
