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

        private void mLoginButton_Click(object sender, EventArgs e)
        {
            CompendiumAccess.Instance.Login(mEmailText.Text, mPasswordText.Text);
            Close();
        }
    }
}
