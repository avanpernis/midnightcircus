using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace TokenAssist
{
    public sealed class CompendiumAccess
    {
        private const string loginUrl = @"http://www.wizards.com/dndinsider/compendium/login.aspx?page=power&id=5093";
        private const string checkUrl = @"http://www.wizards.com/dndinsider/compendium/power.aspx?id=5093";
        private const string validateText = @"First published in";

        private const string cookieFilename = "cookies.dat";
        private static readonly Uri sCompendiumUri = new UriBuilder("http", "www.wizards.com").Uri;
        
        private static CookieContainer mSessionCookies = null;

        private static bool? mConnected = null;
        public static bool Connected
        {
            get
            {
                // if we are undecided, go ahead and try
                if (mConnected == null)
                {
                    CompendiumAccess.Instance.CheckAccess();
                }

                return (mConnected == true) ? true : false;
            }
        }

        public CompendiumAccess()
        {
            System.Net.ServicePointManager.Expect100Continue = false;
        }


        /// <summary>
        /// Check if we have compendium access using the currently stored cookies.
        /// Updates the connected property with the result as well
        /// </summary>
        public bool CheckAccess()
        {
            mConnected = false;

            // if we have no cookies, we can't very well be logged in
            if (mSessionCookies == null)
            {
                return false;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(checkUrl);
            request.CookieContainer = mSessionCookies;

            string responseFromServer = GetUrl(request);
            if (responseFromServer.Contains(validateText))
            {
                mConnected = true;
            }
            
            return (mConnected == true) ? true : false;;
        }


        /// <summary>
        /// Attempt to login to the compendium
        /// *NOTE* This will throw out any existing authentication cookies you had
        /// </summary>
        public bool Login(string user, string password)
        {
            mSessionCookies = new CookieContainer();
            mConnected = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(loginUrl);
            request.CookieContainer = mSessionCookies;

            // Get the initial login page important to get the cookie they assign to us
            string responseFromServer = GetUrl(request);

            // pull out the event ids from the returned server response
            string viewState = null;
            string eventValidation = null;

            Regex inputTagPattern = new Regex(@"<\s*(input[^>]*)>");
            Match inputTags = inputTagPattern.Match(responseFromServer);

            // for each input tag in the response
            while (inputTags.Success)
            {
                String tag = inputTags.Groups[1].Value;
                Regex parseTagPattern = new Regex(@"id\s*=\s*""([^""]+)""\s+value\s*=\s*""([^""]+)""");
                Match tagMatches = parseTagPattern.Match(tag);

                if (tagMatches.Success)
                {
                    string id = tagMatches.Groups[1].Value;
                    string val = tagMatches.Groups[2].Value;

                    if (id == "__EVENTVALIDATION")
                    {
                        eventValidation = val;
                    }
                    else if (id == "__VIEWSTATE")
                    {
                        viewState = val;
                    }
                }

                inputTags = inputTags.NextMatch();
            }

            // hmm we didn't find what we needed, bail!
            if ((viewState == null) || (eventValidation == null))
                return false;

            viewState = System.Web.HttpUtility.UrlEncode(viewState);
            eventValidation = System.Web.HttpUtility.UrlEncode(eventValidation);

            // now set the method and send the login information to the server
            request = (HttpWebRequest)WebRequest.Create(loginUrl);
            request.Method = "POST";
            request.KeepAlive = true;
            request.Headers.Add("Keep-Alive", "300");
            request.CookieContainer = mSessionCookies;

            string userInfo = @"email=" + System.Web.HttpUtility.UrlEncode(user) + @"&password=" + System.Web.HttpUtility.UrlEncode(password) + @"&InsiderSignin=Sign+In";
            string postData = "__VIEWSTATE=" + viewState + "&" + "__EVENTVALIDATION=" + eventValidation + "&" + userInfo;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // read the response we get back
            responseFromServer = GetUrl(request);

            if (responseFromServer.Contains(validateText))
            {
                mConnected = true;
                SaveCookies();
            }

            return (mConnected == true) ? true : false;
        }
        
        public string GetUrl(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.CookieContainer = mSessionCookies;
            req.AllowAutoRedirect = false;
            req.SendChunked = false;

            return GetUrl(req);
        }

        public string GetUrl(HttpWebRequest req)
        {
            int hops = 1;
            const int maxRedirects = 5;

            string result = null;

            bool foundIt = false;
            do
            {
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                // redirect! follow that url
                if (response.StatusCode == HttpStatusCode.Found)
                {
                    string newUrl = response.Headers["Location"].ToString();
                    Debug.WriteLine("Found redirecting->" + newUrl);

                    req = (HttpWebRequest)WebRequest.Create(newUrl);
                    req.CookieContainer = mSessionCookies;
                    req.AllowAutoRedirect = false;
                    req.SendChunked = false;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        foundIt = true;
                        Stream dataStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(dataStream);
                        result = reader.ReadToEnd();
                        reader.Close();
                        dataStream.Close();
                    }
                }
                response.Close();

                ++hops;

            } while ((hops <= maxRedirects) && (!foundIt));

            return result;
        }


        /// <summary>
        /// Save the current cookies to a local file
        /// </summary>
        public void SaveCookies()
        {
            using (StreamWriter writer = new StreamWriter(cookieFilename))
            {
                foreach (Cookie c in mSessionCookies.GetCookies(sCompendiumUri))
                {
                    writer.WriteLine(c.ToString());
                }
            }
        }


        /// <summary>
        /// Load the current cookies from the local store
        /// </summary>
        public void ReadCookies()
        {
            mSessionCookies = new CookieContainer();

            try
            {
                using (StreamReader reader = new StreamReader(cookieFilename))
                {
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] items = line.Split('=');
                        if (items.Length == 2)
                        {
                            Cookie newCookie = new Cookie(items[0], items[1]);
                            mSessionCookies.Add(sCompendiumUri, newCookie);
                        }
                    }
                }
            }
            catch
            {
                mSessionCookies = null;
            }
        }

        // singleton handling
        static readonly CompendiumAccess instance = new CompendiumAccess();

        static CompendiumAccess()
        {
        }
        public static CompendiumAccess Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
