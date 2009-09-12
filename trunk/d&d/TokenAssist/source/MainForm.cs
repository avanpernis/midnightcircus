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

            UpdateSourceFiles();
            UpdateDestinations();

            ChosenSourceFile = source;

            // defaulting the destination folder to the desktop seems useful
            ChosenOutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // attempt to load our local cookie cache
            CompendiumAccess.Instance.ReadCookies();
        }

        private string ChosenSourceFile
        {
            get
            {
                return mComboBoxSource.Text;
            }
            set
            {
                mComboBoxSource.Text = value;
            }
        }

        private string ChosenOutputFolder
        {
            get
            {
                return mComboBoxDestination.Text;
            }
            set
            {
                mComboBoxDestination.Text = value;
            }
        }

        private void UpdateSourceFiles()
        {
            UpdateHistory(mComboBoxSource, UserSettings.Instance.FilenameHistory);
        }

        private void UpdateDestinations()
        {
            UpdateHistory(mComboBoxDestination, UserSettings.Instance.DestinationHistory);
        }

        private static void UpdateHistory(ComboBox comboBox, List<string> history)
        {
            comboBox.Items.Clear();

            foreach (string item in history)
            {
                comboBox.Items.Add(item);
            }
        }

        private void BrowseForSourceFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Dnd4eFileFilter;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ChosenSourceFile = dialog.FileName;
            }
        }

        private void mMenuItemOpen_Click(object sender, EventArgs e)
        {
            BrowseForSourceFile();
        }

        private void mMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mMenuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"TokenAssist (Midnight Circus)", this.Text);
        }

        private void mButtonBrowseSource_Click(object sender, EventArgs e)
        {
            BrowseForSourceFile();
        }

        private void mButtonBrowseDropbox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Dnd4eFileFilter;
            dialog.RestoreDirectory = true;
            try
            {
                dialog.InitialDirectory = Path.Combine(Dropbox.Folder, @"D&D\Characters");
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ChosenSourceFile = dialog.FileName;
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
                ChosenOutputFolder = dialog.SelectedPath;
            }
        }

        private void mButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mButtonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ChosenSourceFile))
            {
                MessageBox.Show(mLabelSource.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(ChosenOutputFolder))
            {
                MessageBox.Show(mLabelDestination.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string destination = Path.Combine(ChosenOutputFolder, Path.ChangeExtension(Path.GetFileName(ChosenSourceFile), ".txt"));

            if (File.Exists(destination))
            {
                DialogResult result = MessageBox.Show("Specified destination file already exists. Would you like to overwrite?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            // save the user's choices
            UserSettings.Instance.OpenedFile(ChosenSourceFile);
            UpdateSourceFiles();

            UserSettings.Instance.UsedDestination(ChosenOutputFolder);
            UpdateDestinations();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                mPanelMain.Enabled = false;

                Character character = CharacterLoader.Load(ChosenSourceFile);

                TokenGenerator.Dump(character, destination);
            }
            finally
            {
                mPanelMain.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void mMenuItemRecentFiles_DropDownOpening(object sender, EventArgs e)
        {
            mMenuItemRecentFiles.DropDownItems.Clear();

            int count = 0;

            foreach (string filename in UserSettings.Instance.FilenameHistory)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();

                // for ampersands to show up, we need to have two of them
                menuItem.Text = string.Format("&{0} {1}", (++count % 10), filename.Replace("&", "&&"));
                menuItem.Tag = filename;

                menuItem.Click += new EventHandler(menuItemRecentFile_Click);

                mMenuItemRecentFiles.DropDownItems.Add(menuItem);
            }
        }

        void menuItemRecentFile_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            ChosenSourceFile = menuItem.Tag as string;
        }
    }
}
