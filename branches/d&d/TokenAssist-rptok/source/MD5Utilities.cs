using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TokenAssist
{
    public static class MD5Utilities
    {
        private static MD5 sMD5 = new MD5CryptoServiceProvider();

        /// <summary>
        /// Compute an MD5 checksum for the specified file
        /// </summary>
        /// <param name="filename">The filename whose checksum will be computed</param>
        /// <returns>the checksum of the specified file</returns>
        public static string ComputeChecksum(string filename)
        {           
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in sMD5.ComputeHash(stream))
                {
                    builder.Append(b.ToString("x"));
                }

                return builder.ToString();
            }
        }   
    }
}
