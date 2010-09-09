﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace TokenAssist
{
    public class Token
    {
        public Token()
        {
            Name = "Untitled Token";
            TokenType = "4ePlayer";
        }

        public string Name { get; set; }
        public string TokenType { get; set; }
        public string TokenPortrait { get; set; }
        public string TokenImage { get; set; }

        public List<TokenMacro> Macros = new List<TokenMacro>();
        private List<TokenProperty> Properties = new List<TokenProperty>();

        private string mTokenImageMD5 = null;
        private string mTokenPortraitMD5 = null;
        private string mAssetPath = null;

        public void AddProperty(string key, object value)
        {
            Properties.Add(new TokenProperty(key, value));
        }

        /// <summary>
        /// Write the given file to a rptok at the given file
        /// </summary>
        /// <param name="filename">The full path the rptok file should be created as</param>
        public void Write(string filename)
        {
            // build a temporary directory we can start creating our files in
            string tempPath = System.IO.Path.GetTempPath();
            string tokenPath = System.IO.Path.Combine(tempPath, "TokenAssist");

            if (System.IO.Directory.Exists(tokenPath))
                System.IO.Directory.Delete(tokenPath, true);

            System.IO.Directory.CreateDirectory(tokenPath);


            mAssetPath = System.IO.Path.Combine(tokenPath, "assets");
            System.IO.Directory.CreateDirectory(mAssetPath);

            // no token image specified, create a default as it would not be exportable otherwise
            if (TokenImage == null)
            {
                MessageSystem.Warning("No token image specified, using default instead");

                mTokenImageMD5 = AddDefaultTokenImage();
            }
            else
            {
                try
                {
                    mTokenImageMD5 = AddAsset(TokenImage, false);
                }
                catch (Exception e)
                {
                    MessageSystem.Warning("Problem loading token image" + e.Message);
                    MessageSystem.Warning("Attempting default");
                    try
                    {
                        mTokenImageMD5 = AddDefaultTokenImage();
                    }
                    catch (Exception e2)
                    {
                        MessageSystem.Error("Problem loading token image" + e2.Message);
                        throw;
                    }
                }
            }

            // handle the portrait image
            if (TokenPortrait == null)
            {
                MessageSystem.Warning("No token portrait image specified");
            }
            else
            {
                try
                {
                    mTokenPortraitMD5 = AddAsset(TokenPortrait, false);
                }
                catch (Exception e)
                {
                    MessageSystem.Warning("Problem loading token image" + e.Message);
                }
            }



            // create the content.xml file
            string contentPath = System.IO.Path.Combine(tokenPath, "content.xml");
            try
            {
                WriteContentFile(contentPath);
            }
            catch (Exception e)
            {
                MessageSystem.Error("Error creating content.xml file" + e.Message);
                throw;
            }

            // create the properties.xml file
            string propertiesFilePath = System.IO.Path.Combine(tokenPath, "properties.xml");
            try
            {
                WritePropertiesFile(propertiesFilePath);
            }
            catch (Exception e)
            {
                MessageSystem.Error("Error creating properties.xml file" + e.Message);
                throw;
            }

            ZipUtilities.ZipDirectory(tokenPath, filename);
        }

        protected string AddDefaultTokenImage()
        {
            string imageFilename = System.IO.Path.Combine(mAssetPath, "default.png");
        
            using (Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TokenAssist.Resources.defaultTokenImage.png"))
            using (FileStream tempFile = new FileStream(imageFilename, FileMode.Create))
            {
                imageStream.CopyTo(tempFile);
            }

            return AddAsset(imageFilename, true);
        }

        /// <summary>
        /// Add the given file to the assets of this token.
        /// </summary>
        /// <param name="filename">The full path of the file to add to this token</param>
        /// <param name="rename">true if we should move the file, false if we make a duplicate</param>
        protected string AddAsset(string filename, bool rename = false)
        {
            string md5 = MD5Utilities.ComputeChecksum(filename);

            string destFile = System.IO.Path.Combine(mAssetPath, md5);

            string extension = System.IO.Path.GetExtension(filename).TrimStart('.');
            if (extension.Equals("jpg", StringComparison.CurrentCultureIgnoreCase))
                extension = "jpeg";

            // write the xml reference file for this asset
            string assetEntry = TokenAssist.Properties.Resources.TokenAssetTemplate;
            assetEntry = assetEntry.Replace(@"###MD5_SUM###", md5);
            assetEntry = assetEntry.Replace(@"###NAME###", System.IO.Path.GetFileNameWithoutExtension(filename));
            assetEntry = assetEntry.Replace(@"###EXTENSION###", extension);

            using (StreamWriter file = new StreamWriter(destFile))
            {
                file.Write(assetEntry);
            }

            // place the actual asset under its appropriate filename
            if (rename)
                System.IO.File.Move(filename, destFile + '.' + extension);
            else
                System.IO.File.Copy(filename, destFile + '.' + extension);

            return md5;
        }


        /// <summary>
        /// Create the content.xml file that is part of the token with the given full path filename
        /// </summary>
        /// <param name="contentFilename">The full path of the filename to write the new token file to</param>
        protected void WriteContentFile(string contentFilename)
        {
            using (StreamWriter file = new StreamWriter(contentFilename))
            {
                StringBuilder builder = new StringBuilder();

                string result = global::TokenAssist.Properties.Resources.ContentTemplate;

                result = result.Replace(@"###TOKEN_NAME###", Name);
                result = result.Replace(@"###TOKEN_TYPE###", TokenType);

                result = result.Replace(@"###IMAGE_MD5_SUM###", mTokenImageMD5);

                if (mTokenPortraitMD5 == null)
                {
                    result = result.Replace(@"###PORTRAIT_SECTION###", "");
                }
                else
                {
                    string portraitSection = "<portraitImage><id>###PORTRAIT_MD5###</id></portraitImage>";
                    portraitSection = portraitSection.Replace("###PORTRAIT_MD5###", mTokenPortraitMD5);
                    result = result.Replace(@"###PORTRAIT_SECTION###", portraitSection);
                }

                // build the property block
                builder.Clear();
                foreach (TokenProperty p in Properties)
                {
                    builder.Append(p.ToString());
                }
                result = result.Replace(@"###PROPERTY_SECTION###", builder.ToString());

                // build the macro block
                builder.Clear();
                for (int i = 0; i < Macros.Count; ++i)
                {
                    Macros[i].Index = i + 1;
                    builder.Append(Macros.ToString());
                }
                result = result.Replace(@"###MACRO_SECTION###", builder.ToString());

                file.Write(result);
            }
        }

        /// <summary>
        /// Write the properties.xml to the given path
        /// </summary>
        /// <param name="filepath">The full path of the filename to write to</param>
        protected void WritePropertiesFile(string filepath)
        {
            using (StreamWriter file = new StreamWriter(filepath))
            {
                file.Write(global::TokenAssist.Properties.Resources.TokenPropertiesFileTemplate);
            }
        }
    }
}
