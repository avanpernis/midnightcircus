using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TokenAssist
{
    public partial class CompendiumLoginForm : Form
    {
        public CompendiumLoginForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (UserSettings.Instance != null)
            {
                // load previous user information
                mEmailText.Text = UserSettings.Instance.Username;
            }

            // select all the text for easy overwrite if desired
            mEmailText.SelectAll();
        }

        private void mLoginButton_Click(object sender, EventArgs e)
        {
            // save the user information
            UserSettings.Instance.Username = mEmailText.Text;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CompendiumAccess.Instance.Login(mEmailText.Text, mPasswordText.Text);
                Close();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public string Email
        {
            get
            {
                return mEmailText.Text;
            }
        }

        public string Password
        {
            get
            {
                return mPasswordText.Text;
            }
        }
    }
}
