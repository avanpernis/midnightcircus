using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TokenAssist
{
    public partial class CompendiumAuthenticator : Form
    {
        private const string LoginUrl = @"http://www.wizards.com/default.asp?x=dnd/insider/compendium";

        // unique search string that should only appear in the rendered HTML document when logged in
        // NOTE: 'Launch' changes to 'Demo' when not logged in.
        private const string LoggedInSearchString = "Click here to Launch the Compendium";

        public CompendiumAuthenticator()
        {
            InitializeComponent();

            mWebBrowser.Navigate(LoginUrl);

            // a "navigated" event occurs after logging in -- detect when that happens and close the window for the user
            this.mWebBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.mWebBrowser_Navigated);
        }

        public bool LoggedIn
        {
            get
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Cookie", Cookie);
                string results = ASCIIEncoding.ASCII.GetString(webClient.DownloadData(LoginUrl));
                return (results.IndexOf(LoggedInSearchString) != -1);
            }
        }

        public string Cookie
        {
            get
            {
                return mWebBrowser.Document.Cookie;
            }
        }

        private void mWebBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // are we authenticated yet?
            if (LoggedIn)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            if (e.Url.AbsoluteUri != LoginUrl)
            {
                // the user navigated away from the login page -- don't allow that.
                mWebBrowser.Navigate(LoginUrl);
            }
        }

        private void mWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            // if in the process of navigating, we are likely attempting to authenticate
            this.Text = "Authenticating with D&D Compendium...";
            mProgressBar.Show();
        }

        private void mWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // if the document finished loading, they are likely not authenticated yet
            this.Text = "Please log into the D&D Compendium.";
            mProgressBar.Hide();
        }
    }
}
