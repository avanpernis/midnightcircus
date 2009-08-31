using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace TokenAssist
{
    public static class CompendiumUtilities
    {
        private static readonly CompendiumLoginForm loginForm = new CompendiumLoginForm();
        private static readonly CompendiumCache mCache = new CompendiumCache();

        public static bool Authenticate()
        {
            if (!CompendiumAccess.Connected)
            {
                loginForm.ShowDialog();
            }
            
            return CompendiumAccess.Connected;
        }
        
        public static string GetStyleSheet()
        {
            return GetUrl(@"http://www.wizards.com/dndinsider/compendium/styles/detail.css");
        }

        public static string GetPower(string url)
        {
            return mCache.Get(EntryType.TYPE_POWER, url);
        }


        public static string GetItem(string url)
        {
            return mCache.Get(EntryType.TYPE_ITEM, url);
        }


        public static string GetFeat(string url)
        {
            return  mCache.Get(EntryType.TYPE_FEAT, url);
        }

        public static string GetRawUrl(string url)
        {
            return CompendiumAccess.Instance.GetUrl(url);
        }

        public static string GetUrl(string url)
        {
            try
            {
                string results = CompendiumAccess.Instance.GetUrl(url);

                // first we strip off all the surrounding crap that makes this an html document
                int start = results.IndexOf(@"<div id=""detail"">");
                int end = results.IndexOf(@"</div>", start);

                results = results.Substring(start, end - start + 6).Trim(); // + 6 to get past '</div>'

                // for some reason, compendium uses 3 ?'s for an apostrophe
                results = results.Replace(@"???", @"'");

                // sometimes there are funky unicode apostrophes as well
                results = results.Replace("\u2019", @"'");

                // sometimes there are strange unicode spaces
                results = results.Replace("\uF020", @" ");

                // some magic items use a unicode circle to separate things like "(Consumable • Healing)"
                // it is also used as the bullet for an unordered list in some powers
                // replace it with the equivalent HTML code
                results = results.Replace("\u2022", @"&middot;");

                // we need to fully qualify the urls for images
                results = results.Replace(@"<img src=""", @"<img src=""http://www.wizards.com/dndinsider/compendium/");

                // maptool tries to do funky things with things in brackets [ ], so replace things in brackets
                // NOTE: no longer needed since we embed the final result in a variable, but keeping around the code in case we change our minds...
                //results = Regex.Replace(results, @"(\[\w*\])", delegate(Match match)
                //    {
                //        return match.Result(@"{""$1""}");
                //    });

                // maptool does not handle the 'float' CSS specification so we need to manually adjust the name and level of the power               
                results = Regex.Replace(results, @"\<span[\s\w=""]*>([\w'\s]+)</span\s*>([\w'\s]+)<", delegate(Match match)
                    {
                        return match.Result(@"$2: $1<");
                    });

                // maptool cannot handle the external style sheet, so we need to flatten out the style elements
                results = results.Replace(@"<div id=""detail"">", @"<div style=""width: 600;"">");
                results = results.Replace(@"<h1 class=""atwillpower""", @"<h1 style=""font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #619869;""");
                results = results.Replace(@"<h1 class=""encounterpower""", @"<h1 style=""font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #961334;""");
                results = results.Replace(@"<h1 class=""dailypower""", @"<h1 style=""font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #4d4d4f;""");
                results = results.Replace(@"<h1 class=""player""", @"<h1 style=""font-size: 1.35em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #1d3d5e;""");
                results = results.Replace(@"<h1 class=""magicitem""", @"<h1 style=""font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #d8941d;""");
                results = results.Replace(@"<p class=""flavor""", @"<p style=""padding-left: color: #3e141e; display: block; padding: 2px 15px; margin: 0; background: #d6d6c2;""");
                results = results.Replace(@"<p class=""powerstat""", @"<p style=""padding-left: color: #3e141e; padding: 0px 0px 0px 15px; margin: 0; background: #ffffff;""");               

                // do some final pretty formatting
                results = ApplyFormatting(results);
                
                return results;
            }
            catch
            {
                return null;
            }
        }

        public static string ApplyFormatting(string input)
        {
            // cannot load the xml into an xml document with the &XXX; style HTML codes so temporarily convert while processing in xml
            string results = input.Replace("&nbsp;", "nbsp");
            results = results.Replace("&middot;", "middot");

            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(results);
            xmlWriter.Formatting = Formatting.None; // maptool does not like newlines in input dialogs
            xmlWriter.QuoteChar = '\''; // this makes it easier to use string attributes inside of the html string
            xmlDocument.WriteContentTo(xmlWriter);

            xmlWriter.Flush();
            memoryStream.Seek(0L, SeekOrigin.Begin);

            StreamReader streamReader = new StreamReader(memoryStream);
            results = streamReader.ReadToEnd();

            // restore the &XXX; HTML elements
            results = results.Replace("nbsp", "&nbsp;");
            results = results.Replace("middot", "&middot;");

            return results;
        }

        public static Character ActiveCharacter
        {
            set
            {
                mCache.ActiveCharacter = value;
            }
            get
            {
                return mCache.ActiveCharacter;
            }
        }
    }
}
