using System;
using System.IO;
using System.IO.Packaging;

namespace TokenAssist
{
    public static class ZipUtilities
    {
        /// <summary>
        /// Zip the specified directory, recursively including subdirectories, into a zip file with
        /// the specified name.
        /// </summary>
        /// <param name="directory">The directory to compress, recursively.</param>
        /// <param name="filename">The path and filename of the resulting zipfile.</param>
        public static void ZipDirectory(string directory, string filename)
        {
            using (ZipPackage zipPackage = ZipPackage.Open(filename, FileMode.Create, FileAccess.ReadWrite) as ZipPackage)
            {
                AddDirectory(zipPackage, new DirectoryInfo(directory), @".\");
            }
        }

        private static void AddDirectory(ZipPackage zipPackage, DirectoryInfo dirInfo, string relativePath)
        {
            foreach (DirectoryInfo subDirInfo in dirInfo.GetDirectories())
            {
                AddDirectory(zipPackage, subDirInfo, Path.Combine(relativePath, subDirInfo.Name));
            }

            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                AddFile(zipPackage, fileInfo, relativePath);
            }
        }

        private static void AddFile(ZipPackage zipPackage, FileInfo fileInfo, string relativePath)
        {
            string destination = Path.Combine(relativePath, Path.GetFileName(fileInfo.FullName));

            Uri uri = PackUriHelper.CreatePartUri(new Uri(destination, UriKind.Relative));

            PackagePart part = zipPackage.CreatePart(uri, System.Net.Mime.MediaTypeNames.Text.Plain, CompressionOption.Maximum);

            using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            using (Stream partStream = part.GetStream())
            {
                fileStream.CopyTo(partStream);
            }
        }       
    }
}
