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
            this.mLabelSource = new System.Windows.Forms.Label();
            this.mButtonBrowseSource = new System.Windows.Forms.Button();
            this.mBrowseDestination = new System.Windows.Forms.Button();
            this.mLabelDestination = new System.Windows.Forms.Label();
            this.mButtonOK = new System.Windows.Forms.Button();
            this.mPanelMain = new System.Windows.Forms.Panel();
            this.mComboBoxSource = new System.Windows.Forms.ComboBox();
            this.mButtonCancel = new System.Windows.Forms.Button();
            this.mButtonBrowseDropbox = new System.Windows.Forms.Button();
            this.mMenu = new System.Windows.Forms.MenuStrip();
            this.mMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mProgressBar = new System.Windows.Forms.ProgressBar();
            this.mComboBoxDestination = new System.Windows.Forms.ComboBox();
            this.mPanelMain.SuspendLayout();
            this.mMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mLabelSource
            // 
            this.mLabelSource.AutoSize = true;
            this.mLabelSource.Location = new System.Drawing.Point(12, 40);
            this.mLabelSource.Name = "mLabelSource";
            this.mLabelSource.Size = new System.Drawing.Size(380, 13);
            this.mLabelSource.TabIndex = 0;
            this.mLabelSource.Text = "Please specify the Character Builder file (.dnd4e) that you would like to convert" +
                ".";
            // 
            // mButtonBrowseSource
            // 
            this.mButtonBrowseSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mButtonBrowseSource.Location = new System.Drawing.Point(436, 47);
            this.mButtonBrowseSource.Name = "mButtonBrowseSource";
            this.mButtonBrowseSource.Size = new System.Drawing.Size(30, 20);
            this.mButtonBrowseSource.TabIndex = 2;
            this.mButtonBrowseSource.Text = "...";
            this.mButtonBrowseSource.UseVisualStyleBackColor = true;
            this.mButtonBrowseSource.Click += new System.EventHandler(this.mButtonBrowseSource_Click);
            // 
            // mBrowseDestination
            // 
            this.mBrowseDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mBrowseDestination.Location = new System.Drawing.Point(436, 131);
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
            this.mLabelDestination.Location = new System.Drawing.Point(12, 115);
            this.mLabelDestination.Name = "mLabelDestination";
            this.mLabelDestination.Size = new System.Drawing.Size(273, 13);
            this.mLabelDestination.TabIndex = 3;
            this.mLabelDestination.Text = "Please specify a folder for your resulting TokenAssist file.";
            // 
            // mButtonOK
            // 
            this.mButtonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mButtonOK.Location = new System.Drawing.Point(262, 166);
            this.mButtonOK.Name = "mButtonOK";
            this.mButtonOK.Size = new System.Drawing.Size(75, 23);
            this.mButtonOK.TabIndex = 6;
            this.mButtonOK.Text = "OK";
            this.mButtonOK.UseVisualStyleBackColor = true;
            this.mButtonOK.Click += new System.EventHandler(this.mButtonOK_Click);
            // 
            // mPanelMain
            // 
            this.mPanelMain.Controls.Add(this.mComboBoxDestination);
            this.mPanelMain.Controls.Add(this.mComboBoxSource);
            this.mPanelMain.Controls.Add(this.mButtonCancel);
            this.mPanelMain.Controls.Add(this.mButtonBrowseDropbox);
            this.mPanelMain.Controls.Add(this.mLabelSource);
            this.mPanelMain.Controls.Add(this.mButtonBrowseSource);
            this.mPanelMain.Controls.Add(this.mLabelDestination);
            this.mPanelMain.Controls.Add(this.mBrowseDestination);
            this.mPanelMain.Controls.Add(this.mButtonOK);
            this.mPanelMain.Controls.Add(this.mMenu);
            this.mPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPanelMain.Location = new System.Drawing.Point(0, 0);
            this.mPanelMain.Name = "mPanelMain";
            this.mPanelMain.Size = new System.Drawing.Size(478, 204);
            this.mPanelMain.TabIndex = 7;
            // 
            // mComboBoxSource
            // 
            this.mComboBoxSource.FormattingEnabled = true;
            this.mComboBoxSource.Location = new System.Drawing.Point(15, 56);
            this.mComboBoxSource.Name = "mComboBoxSource";
            this.mComboBoxSource.Size = new System.Drawing.Size(415, 21);
            this.mComboBoxSource.TabIndex = 10;
            // 
            // mButtonCancel
            // 
            this.mButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mButtonCancel.Location = new System.Drawing.Point(142, 166);
            this.mButtonCancel.Name = "mButtonCancel";
            this.mButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.mButtonCancel.TabIndex = 8;
            this.mButtonCancel.Text = "Cancel";
            this.mButtonCancel.UseVisualStyleBackColor = true;
            this.mButtonCancel.Click += new System.EventHandler(this.mButtonCancel_Click);
            // 
            // mButtonBrowseDropbox
            // 
            this.mButtonBrowseDropbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mButtonBrowseDropbox.BackgroundImage = global::TokenAssist.Properties.Resources.dropbox;
            this.mButtonBrowseDropbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mButtonBrowseDropbox.Location = new System.Drawing.Point(436, 67);
            this.mButtonBrowseDropbox.Name = "mButtonBrowseDropbox";
            this.mButtonBrowseDropbox.Size = new System.Drawing.Size(30, 30);
            this.mButtonBrowseDropbox.TabIndex = 7;
            this.mButtonBrowseDropbox.UseVisualStyleBackColor = true;
            this.mButtonBrowseDropbox.Click += new System.EventHandler(this.mButtonBrowseDropbox_Click);
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
            this.mMenuItemExit});
            this.mMenuItemFile.Name = "mMenuItemFile";
            this.mMenuItemFile.Size = new System.Drawing.Size(35, 20);
            this.mMenuItemFile.Text = "&File";
            // 
            // mMenuItemOpen
            // 
            this.mMenuItemOpen.Name = "mMenuItemOpen";
            this.mMenuItemOpen.Size = new System.Drawing.Size(111, 22);
            this.mMenuItemOpen.Text = "&Open";
            this.mMenuItemOpen.Click += new System.EventHandler(this.mMenuItemOpen_Click);
            // 
            // mSeparator1
            // 
            this.mSeparator1.Name = "mSeparator1";
            this.mSeparator1.Size = new System.Drawing.Size(108, 6);
            // 
            // mMenuItemExit
            // 
            this.mMenuItemExit.Name = "mMenuItemExit";
            this.mMenuItemExit.Size = new System.Drawing.Size(111, 22);
            this.mMenuItemExit.Text = "E&xit";
            this.mMenuItemExit.Click += new System.EventHandler(this.mMenuItemExit_Click);
            // 
            // mMenuItemHelp
            // 
            this.mMenuItemHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mMenuItemAbout});
            this.mMenuItemHelp.Name = "mMenuItemHelp";
            this.mMenuItemHelp.Size = new System.Drawing.Size(40, 20);
            this.mMenuItemHelp.Text = "&Help";
            // 
            // mMenuItemAbout
            // 
            this.mMenuItemAbout.Name = "mMenuItemAbout";
            this.mMenuItemAbout.Size = new System.Drawing.Size(114, 22);
            this.mMenuItemAbout.Text = "&About";
            this.mMenuItemAbout.Click += new System.EventHandler(this.mMenuItemAbout_Click);
            // 
            // mStatusStrip
            // 
            this.mStatusStrip.Location = new System.Drawing.Point(0, 204);
            this.mStatusStrip.Name = "mStatusStrip";
            this.mStatusStrip.Size = new System.Drawing.Size(478, 22);
            this.mStatusStrip.TabIndex = 0;
            this.mStatusStrip.Text = "statusStrip1";
            // 
            // mProgressBar
            // 
            this.mProgressBar.Location = new System.Drawing.Point(43, 72);
            this.mProgressBar.MarqueeAnimationSpeed = 50;
            this.mProgressBar.Name = "mProgressBar";
            this.mProgressBar.Size = new System.Drawing.Size(392, 23);
            this.mProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.mProgressBar.TabIndex = 7;
            // 
            // mComboBoxDestination
            // 
            this.mComboBoxDestination.FormattingEnabled = true;
            this.mComboBoxDestination.Location = new System.Drawing.Point(15, 131);
            this.mComboBoxDestination.Name = "mComboBoxDestination";
            this.mComboBoxDestination.Size = new System.Drawing.Size(415, 21);
            this.mComboBoxDestination.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AcceptButton = this.mButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mButtonCancel;
            this.ClientSize = new System.Drawing.Size(478, 226);
            this.Controls.Add(this.mPanelMain);
            this.Controls.Add(this.mProgressBar);
            this.Controls.Add(this.mStatusStrip);
            this.MainMenuStrip = this.mMenu;
            this.Name = "MainForm";
            this.Text = "TokenAssist";
            this.mPanelMain.ResumeLayout(false);
            this.mPanelMain.PerformLayout();
            this.mMenu.ResumeLayout(false);
            this.mMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mLabelSource;
        private System.Windows.Forms.Button mButtonBrowseSource;
        private System.Windows.Forms.Button mBrowseDestination;
        private System.Windows.Forms.Label mLabelDestination;
        private System.Windows.Forms.Button mButtonOK;
        private System.Windows.Forms.Panel mPanelMain;
        private System.Windows.Forms.StatusStrip mStatusStrip;
        private System.Windows.Forms.ProgressBar mProgressBar;
        private System.Windows.Forms.Button mButtonBrowseDropbox;
        private System.Windows.Forms.Button mButtonCancel;
        private System.Windows.Forms.MenuStrip mMenu;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemOpen;
        private System.Windows.Forms.ToolStripSeparator mSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem mMenuItemAbout;
        private System.Windows.Forms.ComboBox mComboBoxSource;
        private System.Windows.Forms.ComboBox mComboBoxDestination;
    }
}

