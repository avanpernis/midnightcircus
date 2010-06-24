using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        /// <summary>
        /// Show the login dialog to the user and attempt to login with this information
        /// </summary>
        /// <returns>True if we have a connection to the compendium afterwards.  False if the user gave up</returns>
        public static bool PromptForLogin()
        {
            bool done = false;
            while (!done)
            {
                // if the user pressed ok, validate the login
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        CompendiumAccess.Instance.Login(loginForm.Email, loginForm.Password);
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }

                    if (CompendiumAccess.Instance.Connected)
                    {
                        done = true;
                    }
                    else
                    {
                        MessageBox.Show("Unable to authenticate with D&D Compendium", loginForm.Email, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // otherwise user didn't want to login, bail!
                else
                {
                    done = true;
                    mCache.DisableSource(CompendiumCache.SOURCE_NET);
                }
            }

            return CompendiumAccess.Instance.Connected;
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

        public static string GetUrl(string url)
        {
            try
            {
                string results = CompendiumAccess.Instance.GetUrl(url);
                results = FixCompendiumOutput(results);
                return results;
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetUrl Exception: " + e.Message);

                return null;
            }
        }

        public static string FixCompendiumOutput(string raw)
        {
            string results = raw;
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

            // sometimes there are weird non-hyphen characters used for negative modifiers/penalties
            results = results.Replace("\u2013", @"-");

            // sometimes there are weird quotation mark characters that do not play nicely
            results = results.Replace("\u201c", @"'");
            results = results.Replace("\u201d", @"'");

            // some magic items use a unicode circle to separate things like "(Consumable • Healing)"
            // it is also used as the bullet for an unordered list in some powers
            // replace it with the equivalent HTML code
            results = results.Replace("\u2022", @"&middot;");

            // using the compendium link to their diamond image can be slow -- use an HTML character instead
            results = results.Replace(@"<img src=""images/bullet.gif"" alt=""""/>", @"&diams;");

            // maptool tries to do funky things with things in brackets [ ], so replace things in brackets
            // NOTE: no longer needed since we embed the final result in a variable, but keeping around the code in case we change our minds...
            //results = Regex.Replace(results, @"(\[\w*\])", delegate(Match match)
            //    {
            //        return match.Result(@"{""$1""}");
            //    });

            // some urls have &'s in them, and that is bad for when we try and use the xml loader to autoformat the results
            // assume all urls are useless for now, and strip them out
            results = Regex.Replace(results, @"(<a target[^>]+>)([^<]+)(<[^>]+>)", delegate(Match match)
                {
                    return match.Result("$2");
                });

            // maptool does not handle the 'float' CSS specification so we need to manually adjust the name and level of the power
            results = Regex.Replace(results, @"\<span[\s\w=""]*>([\w'\s]+)</span\s*>([\w'\s]+)<", delegate(Match match)
                {
                    return match.Result(@"$2: $1<");
                });

            // maptool cannot handle the external style sheet, so we need to flatten out the style elements
            results = ApplyStyleSheet(results);

            // do some final pretty formatting
            results = ApplyFormatting(results);

            return results;
        }

        /// <summary>
        /// Replace the style sheet definitions with their flattened values as MapTool cannot read and use
        /// external style sheets.
        /// 
        /// TODO: Change this from hardcoded values to actually use the DDI Compendium stylesheet.
        /// </summary>
        /// <param name="input">The input string containing HTML from a compendium entry.</param>
        /// <returns>A string with the style sheet values flattened.</returns>
        public static string ApplyStyleSheet(string input)
        {
            string results = input.Replace(@"<div id=""detail"">", @"<div style=""width: 600;"">");
            results = results.Replace(@"<h1 class=""atwillpower""", @"<h1 style=""font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #619869;""");
            results = results.Replace(@"<h1 class=""encounterpower""", @"<h1 style=""font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #961334;""");
            results = results.Replace(@"<h1 class=""dailypower""", @"<h1 style=""font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #4d4d4f;""");
            results = results.Replace(@"<h1 class=""player""", @"<h1 style=""font-size: 1.35em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #1d3d5e;""");
            results = results.Replace(@"<h1 class=""magicitem""", @"<h1 style=""font-size: 1.09em; line-height: 2; padding-left: 15px; margin: 0; color: #ffffff; background: #d8941d;""");
            results = results.Replace(@"<p class=""flavor""", @"<p style=""padding-left: color: #3e141e; display: block; padding: 2px 15px; margin: 0; background: #d6d6c2;""");
            results = results.Replace(@"<p class=""powerstat""", @"<p style=""padding-left: color: #3e141e; padding: 0px 0px 0px 15px; margin: 0; background: #ffffff;""");

            return results;
        }

        public static string ApplyFormatting(string input)
        {
            // cannot load the xml into an xml document with the &XXX; style HTML codes so temporarily convert while processing in xml
            string results = input.Replace("&nbsp;", "nbsp");
            results = results.Replace("&middot;", "middot");
            results = results.Replace("&diams;", "diams");

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
            results = results.Replace("diams", "&diams;");

            // any quotation marks that still exist need to be converted to single quotes
            results = results.Replace("\"", @"'");

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
