namespace TokenAssist
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mButtonOK = new System.Windows.Forms.Button();
            this.mBrowseDestination = new System.Windows.Forms.Button();
            this.mLabelDestination = new System.Windows.Forms.Label();
            this.mButtonBrowseSource = new System.Windows.Forms.Button();
            this.mLabelSource = new System.Windows.Forms.Label();
            this.mButtonBrowseDropbox = new System.Windows.Forms.Button();
            this.mButtonCancel = new System.Windows.Forms.Button();
            this.mComboBoxSource = new System.Windows.Forms.ComboBox();
            this.mComboBoxDestination = new System.Windows.Forms.ComboBox();
            this.mMenu = new System.Windows.Forms.MenuStrip();
            this.mMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenuItemRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mPanelMain = new System.Windows.Forms.Panel();
            this.mImageBrowserPortrait = new TokenAssist.ImageBrowser();
            this.mImageBrowserToken = new TokenAssist.ImageBrowser();
            this.mMenu.SuspendLayout();
            this.mPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mStatusStrip
            // 
            this.mStatusStrip.Location = new System.Drawing.Point(0, 377);
            this.mStatusStrip.Name = "mStatusStrip";
            this.mStatusStrip.Size = new System.Drawing.Size(478, 22);
            this.mStatusStrip.TabIndex = 0;
            this.mStatusStrip.Text = "statusStrip1";
            // 
            // mButtonOK
            // 
            this.mButtonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mButtonOK.Location = new System.Drawing.Point(262, 315);
            this.mButtonOK.Name = "mButtonOK";
            this.mButtonOK.Size = new System.Drawing.Size(75, 23);
            this.mButtonOK.TabIndex = 6;
            this.mButtonOK.Text = "OK";
            this.mButtonOK.UseVisualStyleBackColor = true;
            this.mButtonOK.Click += new System.EventHandler(this.mButtonOK_Click);
            // 
            // mBrowseDestination
            // 
            this.mBrowseDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mBrowseDestination.Location = new System.Drawing.Point(436, 278);
            this.mBrowseDestination.Name = "mBrowseDestination";
            this.mBrowseDestination.Size = new System.Drawing.Size(30, 20);
            this.mBrowseDestination.TabIndex = 5;
            this.mBrowseDestination.Text = "...";
            this.mBrowseDestination.UseVisualStyleBackColor = true;
            this.mBrowseDestination.Click += new System.EventHandler(this.mBrowseDestination_Click);
            // 
            // mLabelDestination
            // 
            this.mLabelDestination.AutoSize = true;
            this.mLabelDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mLabelDestination.Location = new System.Drawing.Point(17, 262);
            this.mLabelDestination.Name = "mLabelDestination";
            this.mLabelDestination.Size = new System.Drawing.Size(284, 13);
            this.mLabelDestination.TabIndex = 3;
            this.mLabelDestination.Text = "Please specify the token destination file (*.rptok)";
            // 
            // mButtonBrowseSource
            // 
            this.mButtonBrowseSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mButtonBrowseSource.Location = new System.Drawing.Point(436, 25);
            this.mButtonBrowseSource.Name = "mButtonBrowseSource";
            this.mButtonBrowseSource.Size = new System.Drawing.Size(30, 20);
            this.mButtonBrowseSource.TabIndex = 2;
            this.mButtonBrowseSource.Text = "...";
            this.mButtonBrowseSource.UseVisualStyleBackColor = true;
            this.mButtonBrowseSource.Click += new System.EventHandler(this.mButtonBrowseSource_Click);
            // 
            // mLabelSource
            // 
            this.mLabelSource.AutoSize = true;
            this.mLabelSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mLabelSource.Location = new System.Drawing.Point(17, 18);
            this.mLabelSource.Name = "mLabelSource";
            this.mLabelSource.Size = new System.Drawing.Size(353, 13);
            this.mLabelSource.TabIndex = 0;
            this.mLabelSource.Text = "Please specify the source file to convert (*.dnd4e, *.monster)";
            // 
            // mButtonBrowseDropbox
            // 
            this.mButtonBrowseDropbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mButtonBrowseDropbox.BackgroundImage = global::TokenAssist.Properties.Resources.dropbox;
            this.mButtonBrowseDropbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mButtonBrowseDropbox.Location = new System.Drawing.Point(436, 45);
            this.mButtonBrowseDropbox.Name = "mButtonBrowseDropbox";
            this.mButtonBrowseDropbox.Size = new System.Drawing.Size(30, 30);
            this.mButtonBrowseDropbox.TabIndex = 7;
            this.mButtonBrowseDropbox.UseVisualStyleBackColor = true;
            this.mButtonBrowseDropbox.Click += new System.EventHandler(this.mButtonBrowseDropbox_Click);
            // 
            // mButtonCancel
            // 
            this.mButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mButtonCancel.Location = new System.Drawing.Point(371, 315);
            this.mButtonCancel.Name = "mButtonCancel";
            this.mButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.mButtonCancel.TabIndex = 8;
            this.mButtonCancel.Text = "Cancel";
            this.mButtonCancel.UseVisualStyleBackColor = true;
            this.mButtonCancel.Click += new System.EventHandler(this.mButtonCancel_Click);
            // 
            // mComboBoxSource
            // 
            this.mComboBoxSource.FormattingEnabled = true;
            this.mComboBoxSource.Location = new System.Drawing.Point(15, 34);
            this.mComboBoxSource.Name = "mComboBoxSource";
            this.mComboBoxSource.Size = new System.Drawing.Size(415, 21);
            this.mComboBoxSource.TabIndex = 10;
            this.mComboBoxSource.SelectedIndexChanged += new System.EventHandler(this.mComboBoxSource_SelectedIndexChanged);
            // 
            // mComboBoxDestination
            // 
            this.mComboBoxDestination.FormattingEnabled = true;
            this.mComboBoxDestination.Location = new System.Drawing.Point(15, 278);
            this.mComboBoxDestination.Name = "mComboBoxDestination";
            this.mComboBoxDestination.Size = new System.Drawing.Size(415, 21);
            this.mComboBoxDestination.TabIndex = 11;
            // 
            // mMenu
            // 
            this.mMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenuItemFile,
            this.mMenuItemHelp});
            this.mMenu.Location = new System.Drawing.Point(0, 0);
            this.mMenu.Name = "mMenu";
            this.mMenu.Size = new System.Drawing.Size(478, 24);
            this.mMenu.TabIndex = 9;
            this.mMenu.Text = "mMenu";
            // 
            // mMenuItemFile
            // 
            this.mMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenuItemOpen,
            this.mSeparator1,
            this.mMenuItemRecentFiles,
            this.mSeparator2,
            this.mMenuItemExit});
            this.mMenuItemFile.Name = "mMenuItemFile";
            this.mMenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.mMenuItemFile.Text = "&File";
            // 
            // mMenuItemOpen
            // 
            this.mMenuItemOpen.Name = "mMenuItemOpen";
            this.mMenuItemOpen.Size = new System.Drawing.Size(152, 22);
            this.mMenuItemOpen.Text = "&Open";
            this.mMenuItemOpen.Click += new System.EventHandler(this.mMenuItemOpen_Click);
            // 
            // mSeparator1
            // 
            this.mSeparator1.Name = "mSeparator1";
            this.mSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // mMenuItemRecentFiles
            // 
            this.mMenuItemRecentFiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSeparator3});
            this.mMenuItemRecentFiles.Name = "mMenuItemRecentFiles";
            this.mMenuItemRecentFiles.Size = new System.Drawing.Size(152, 22);
            this.mMenuItemRecentFiles.Text = "Recent &Files";
            this.mMenuItemRecentFiles.DropDownOpening += new System.EventHandler(this.mMenuItemRecentFiles_DropDownOpening);
            // 
            // mSeparator3
            // 
            this.mSeparator3.Name = "mSeparator3";
            this.mSeparator3.Size = new System.Drawing.Size(57, 6);
            // 
            // mSeparator2
            // 
            this.mSeparator2.Name = "mSeparator2";
            this.mSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // mMenuItemExit
            // 
            this.mMenuItemExit.Name = "mMenuItemExit";
            this.mMenuItemExit.Size = new System.Drawing.Size(152, 22);
            this.mMenuItemExit.Text = "E&xit";
            this.mMenuItemExit.Click += new System.EventHandler(this.mMenuItemExit_Click);
            // 
            // mMenuItemHelp
            // 
            this.mMenuItemHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenuItemAbout});
            this.mMenuItemHelp.Name = "mMenuItemHelp";
            this.mMenuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.mMenuItemHelp.Text = "&Help";
            // 
            // mMenuItemAbout
            // 
            this.mMenuItemAbout.Name = "mMenuItemAbout";
            this.mMenuItemAbout.Size = new System.Drawing.Size(152, 22);
            this.mMenuItemAbout.Text = "&About";
            this.mMenuItemAbout.Click += new System.EventHandler(this.mMenuItemAbout_Click);
            // 
            // mPanelMain
            // 
            this.mPanelMain.Controls.Add(this.mImageBrowserPortrait);
            this.mPanelMain.Controls.Add(this.mImageBrowserToken);
            this.mPanelMain.Controls.Add(this.mComboBoxDestination);
            this.mPanelMain.Controls.Add(this.mComboBoxSource);
            this.mPanelMain.Controls.Add(this.mButtonCancel);
            this.mPanelMain.Controls.Add(this.mButtonBrowseDropbox);
            this.mPanelMain.Controls.Add(this.mLabelSource);
            this.mPanelMain.Controls.Add(this.mButtonBrowseSource);
            this.mPanelMain.Controls.Add(this.mLabelDestination);
            this.mPanelMain.Controls.Add(this.mBrowseDestination);
            this.mPanelMain.Controls.Add(this.mButtonOK);
            this.mPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPanelMain.Location = new System.Drawing.Point(0, 24);
            this.mPanelMain.Name = "mPanelMain";
            this.mPanelMain.Size = new System.Drawing.Size(478, 353);
            this.mPanelMain.TabIndex = 7;
            // 
            // mImageBrowserPortrait
            // 
            this.mImageBrowserPortrait.ImageFile = null;
            this.mImageBrowserPortrait.Label = "Portrait";
            this.mImageBrowserPortrait.Location = new System.Drawing.Point(171, 94);
            this.mImageBrowserPortrait.Name = "mImageBrowserPortrait";
            this.mImageBrowserPortrait.Size = new System.Drawing.Size(128, 141);
            this.mImageBrowserPortrait.TabIndex = 13;
            // 
            // mImageBrowserToken
            // 
            this.mImageBrowserToken.ImageFile = null;
            this.mImageBrowserToken.Label = "Token";
            this.mImageBrowserToken.Location = new System.Drawing.Point(18, 94);
            this.mImageBrowserToken.Name = "mImageBrowserToken";
            this.mImageBrowserToken.Size = new System.Drawing.Size(128, 141);
            this.mImageBrowserToken.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AcceptButton = this.mButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mButtonCancel;
            this.ClientSize = new System.Drawing.Size(478, 399);
            this.Controls.Add(this.mPanelMain);
            this.Controls.Add(this.mMenu);
            this.Controls.Add(this.mStatusStrip);
            this.MainMenuStrip = this.mMenu;
            this.Name = "MainForm";
            this.Text = "TokenAssist";
            this.mMenu.ResumeLayout(false);
            this.mMenu.PerformLayout();
            this.mPanelMain.ResumeLayout(false);
            this.mPanelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mStatusStrip;
        private System.Windows.Forms.Button mButtonOK;
        private System.Windows.Forms.Button mBrowseDestination;
        private System.Windows.Forms.Label mLabelDestination;
        private System.Windows.Forms.Button mButtonBrowseSource;
        private System.Windows.Forms.Label mLabelSource;
        private System.Windows.Forms.Button mButtonBrowseDropbox;
        private System.Windows.Forms.Button mButtonCancel;
        private System.Windows.Forms.ComboBox mComboBoxSource;
        private System.Windows.Forms.ComboBox mComboBoxDestination;
        private ImageBrowser mImageBrowserToken;
        private ImageBrowser mImageBrowserPortrait;
        private System.Windows.Forms.MenuStrip mMenu;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemOpen;
        private System.Windows.Forms.ToolStripSeparator mSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemRecentFiles;
        private System.Windows.Forms.ToolStripSeparator mSeparator3;
        private System.Windows.Forms.ToolStripSeparator mSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemAbout;
        private System.Windows.Forms.Panel mPanelMain;
    }
}

