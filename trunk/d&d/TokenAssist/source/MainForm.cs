using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokenAssist
{
    public partial class MainForm : Form
    {
        private const string Dnd4eFileFilter = @"dnd4e files (*.dnd4e)|*.dnd4e|All files (*.*)|*.*";

        public MainForm()
            : this(string.Empty)
        {
        }

        public MainForm(string source)
        {
            InitializeComponent();

            mTextBoxSource.Text = source;

            // defaulting the destination folder to the desktop seems useful
            mTextBoxDestination.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // authenticate with the D&D Compendium if needed
            if (!CompendiumUtilities.Authenticate())
            {
                MessageBox.Show("Unable to authenticate with D&D Compendium", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void mButtonBrowseSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Dnd4eFileFilter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mTextBoxSource.Text = dialog.FileName;
            }
        }

        private void mButtonBrowseDropbox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Dnd4eFileFilter;
            try
            {
                dialog.InitialDirectory = Path.Combine(Dropbox.Folder, @"D&D\Characters");
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    mTextBoxSource.Text = dialog.FileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to find dropbox folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mBrowseDestination_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mTextBoxDestination.Text = dialog.SelectedPath;
            }
        }

        private void mButtonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mTextBoxSource.Text))
            {
                MessageBox.Show(mLabelSource.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(mTextBoxDestination.Text))
            {
                MessageBox.Show(mLabelDestination.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string destination = Path.Combine(mTextBoxDestination.Text, Path.ChangeExtension(Path.GetFileName(mTextBoxSource.Text), ".txt"));

            if (File.Exists(destination))
            {
                DialogResult result = MessageBox.Show("Specified destination file already exists. Would you like to overwrite?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                mPanelMain.Enabled = false;

                Character character = CharacterLoader.Load(mTextBoxSource.Text);

                TokenGenerator.Dump(character, destination);
            }
            finally
            {
                mPanelMain.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
