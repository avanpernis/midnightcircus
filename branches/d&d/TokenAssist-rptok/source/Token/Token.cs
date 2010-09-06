using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TokenAssist
{
    class Token
    {
        public string Name = "";
        public string PropertyType = "4ePlayer";
        public string TokenPortrait = null;
        public string TokenImage = null;

        public List<TokenMacro> Macros = new List<TokenMacro>();
        public List<TokenProperty> Properties = new List<TokenProperty>();
        
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

            string assetpath = System.IO.Path.Combine(tokenPath, "assets");
            System.IO.Directory.CreateDirectory(assetpath);

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
                result = result.Replace(@"###PROP_TYPE###", PropertyType);

                // put in the md5 calculated for the token image
                string portraitMD5 = "";
                if (TokenPortrait == null)
                {
                    MessageSystem.Warning("No token portrait image specified");
                }
                else
                {
                    try
                    {
                        portraitMD5 = MD5Utilities.ComputeChecksum(TokenPortrait);
                    }
                    catch (Exception e)
                    {
                        MessageSystem.Warning("Problem loading token image" + e.Message);
                    }
                }
                result = result.Replace(@"###IMAGE_MD5_SUM###", TokenImage);

                // put in the md5 calculated for the portrait image
                string tokenImageMD5 = "";
                if (TokenImage == null)
                {
                    MessageSystem.Warning("No token image specified");
                }
                else
                {
                    try
                    {
                        tokenImageMD5 = MD5Utilities.ComputeChecksum(TokenImage);
                    }
                    catch (Exception e)
                    {
                        MessageSystem.Warning("Problem loading portrait image" + e.Message);
                    }
                }
                result = result.Replace(@"###PORTRAIT_MD5###", TokenPortrait);

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
