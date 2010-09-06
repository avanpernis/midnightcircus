using System;
using System.IO;
using System.IO.Packaging;

namespace TokenAssist
{
    public class MemoryZip : IDisposable
    {
        public MemoryZip(string filename)
        {
            mZipPackage = ZipPackage.Open(filename, FileMode.Create, FileAccess.ReadWrite) as ZipPackage;
        }

        ~MemoryZip()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                mZipPackage.Close();
            }
        }

        public void AddDirectory(string directory)
        {
            AddDirectory(new DirectoryInfo(directory), @".\");
        }

        private void AddDirectory(DirectoryInfo dirInfo, string relativePath)
        {
            foreach (DirectoryInfo subDirInfo in dirInfo.GetDirectories())
            {
                AddDirectory(subDirInfo, Path.Combine(relativePath, subDirInfo.Name));
            }

            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                AddFile(fileInfo, relativePath);
            }
        }

        private void AddFile(FileInfo fileInfo, string relativePath)
        {
            string destination = Path.Combine(relativePath, Path.GetFileName(fileInfo.FullName));

            Uri uri = PackUriHelper.CreatePartUri(new Uri(destination, UriKind.Relative));

            PackagePart part = mZipPackage.CreatePart(uri, System.Net.Mime.MediaTypeNames.Text.Plain, CompressionOption.Maximum);

            using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            using (Stream partStream = part.GetStream())
            {
                fileStream.CopyTo(partStream);
            }
        }

        private ZipPackage mZipPackage;
    }
}
