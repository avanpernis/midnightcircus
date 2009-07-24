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
        private const string validateText = @"First published in";

        private static CookieContainer mSessionCookies = null;

        private static bool mConnected = false;
        public static bool Connected
        {
            get
            {
                return mConnected;
            }
        }


        public bool Login(string user, string password)
        {
            System.Net.ServicePointManager.Expect100Continue = false;

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
                mConnected = true;

            return mConnected;
        }

        public string GetUrl(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.CookieContainer = mSessionCookies;
            req.AllowAutoRedirect = false;
            req.SendChunked = false;
            req.CookieContainer = mSessionCookies;

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
                        Debug.WriteLine("read " + result);
                        reader.Close();
                        dataStream.Close();
                    }
                }
                response.Close();

                ++hops;

            } while ((hops <= maxRedirects) && (!foundIt));

            return result;
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
