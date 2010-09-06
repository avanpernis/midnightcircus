using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TokenAssist
{
    public partial class MainForm : Form
    {
        private const string SourceFileFilter = @"Supported files (*.dnd4e, *.monster)|*.dnd4e;*.monster|Character Builder files (*.dnd4e)|*.dnd4e|Monster files (*.monster)|*.monster|All files (*.*)|*.*";
        private const string DestinationFileFilter = @"Maptool Token files (*.rptok)|*.rptok|All files (*.*)|*.*";

        public MainForm()
            : this(string.Empty)
        {
        }

        public MainForm(string source)
        {
            InitializeComponent();

            MessageSystem.OnMessage += new EventHandler<MessageEventArgs>(MessageSystem_OnMessage);

            UpdateSourceFiles();
            UpdateDestinations();

            ChosenSourceFile = source;

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
                if (!string.IsNullOrEmpty(value))
                {
                    // load the specified file, populate some of the other fields with educated guesswork
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        mPanelMain.Enabled = false;

                        if (value.EndsWith(".monster"))
                        {
                            mMonster = MonsterLoader.Load(value);
                            mCharacter = null;

                            ChosenDestinationFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), string.Format("{0}.rptok", mMonster.Name));
                        }
                        else
                        {
                            mCharacter = CharacterLoader.Load(value);
                            mMonster = null;

                            ChosenDestinationFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), string.Format("{0}.rptok", mCharacter.Name));
                        }

                        mComboBoxSource.Text = value;

                        MessageSystem.Success(ChosenSourceFile + " loaded.");
                    }
                    catch (Exception exception)
                    {
                        mComboBoxSource.Text = string.Empty;

                        MessageSystem.Error(exception.Message);
                    }
                    finally
                    {
                        mPanelMain.Enabled = true;
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

        private string ChosenDestinationFile
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
            dialog.Filter = SourceFileFilter;
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

        private void mComboBoxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenSourceFile = mComboBoxSource.Text;
        }

        private void mButtonBrowseSource_Click(object sender, EventArgs e)
        {
            BrowseForSourceFile();
        }

        private void mButtonBrowseDropbox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = SourceFileFilter;
            dialog.RestoreDirectory = true;
            
            try
            {
                dialog.InitialDirectory = Path.Combine(Dropbox.Folder, @"D&D");
            }
            catch (Exception exception)
            {
                MessageSystem.Error(string.Format("Unable to find dropbox folder: {0}", exception.Message));
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ChosenSourceFile = dialog.FileName;
            }
        }

        private void mBrowseDestination_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = DestinationFileFilter;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ChosenDestinationFile = dialog.FileName;
            }
        }

        private void mButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // user has confirmed setup, write out the token
        private void mButtonOK_Click(object sender, EventArgs e)
        {
            if ((mCharacter == null) && (mMonster == null))
            {
                MessageSystem.Error(mLabelSource.Text);
                return;
            }

            if (string.IsNullOrEmpty(ChosenDestinationFile))
            {
                MessageSystem.Error(mLabelDestination.Text);
                return;
            }

            if (File.Exists(ChosenDestinationFile))
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

            UserSettings.Instance.UsedDestination(ChosenDestinationFile);
            UpdateDestinations();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                mPanelMain.Enabled = false;

                if (mCharacter != null)
                    TokenGenerator.Dump(mCharacter, ChosenDestinationFile);
                else if (mMonster != null)
                    MonsterTokenBuilder.WriteToken(mMonster, ChosenDestinationFile, mImageBrowserPortrait.ImageFile, mImageBrowserToken.ImageFile);

                MessageSystem.Success(ChosenDestinationFile + " created.");
            }
            catch (Exception exception)
            {
                MessageSystem.Error(exception.Message);
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

        void MessageSystem_OnMessage(object sender, MessageEventArgs e)
        {
            mListBoxOutput.Items.Add(e);
        }

        private void mListBoxOutput_DrawItem(object sender, DrawItemEventArgs e)
        {
            MessageEventArgs args = mListBoxOutput.Items[e.Index] as MessageEventArgs;

            e.DrawBackground();

            e.Graphics.DrawString(args.Message, e.Font, GetBrush(args.Type), e.Bounds, StringFormat.GenericDefault);

            e.DrawFocusRectangle();
        }  

        private static Brush GetBrush(MessageType type)
        {
            switch(type)
            {
                case MessageType.Info:
                default:
                    return Brushes.Black;
                case MessageType.Warning:
                    return Brushes.Orange;
                case MessageType.Error:
                    return Brushes.Red;
                case MessageType.Success:
                    return Brushes.Green;
            }
        }

        private Character mCharacter;
        private Monster mMonster;
    }
}
