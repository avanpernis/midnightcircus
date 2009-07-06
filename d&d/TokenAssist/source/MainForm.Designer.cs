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
            this.mTextBoxSource = new System.Windows.Forms.TextBox();
            this.mButtonBrowseSource = new System.Windows.Forms.Button();
            this.mBrowseDestination = new System.Windows.Forms.Button();
            this.mTextBoxDestination = new System.Windows.Forms.TextBox();
            this.mLabelDestination = new System.Windows.Forms.Label();
            this.mButtonOK = new System.Windows.Forms.Button();
            this.mPanelMain = new System.Windows.Forms.Panel();
            this.mStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mProgressBar = new System.Windows.Forms.ProgressBar();
            this.mButtonBrowseDropbox = new System.Windows.Forms.Button();
            this.mPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mLabelSource
            // 
            this.mLabelSource.AutoSize = true;
            this.mLabelSource.Location = new System.Drawing.Point(12, 14);
            this.mLabelSource.Name = "mLabelSource";
            this.mLabelSource.Size = new System.Drawing.Size(380, 13);
            this.mLabelSource.TabIndex = 0;
            this.mLabelSource.Text = "Please specify the Character Builder file (.dnd4e) that you would like to convert" +
                ".";
            // 
            // mTextBoxSource
            // 
            this.mTextBoxSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mTextBoxSource.Location = new System.Drawing.Point(15, 30);
            this.mTextBoxSource.Name = "mTextBoxSource";
            this.mTextBoxSource.Size = new System.Drawing.Size(415, 20);
            this.mTextBoxSource.TabIndex = 1;
            // 
            // mButtonBrowseSource
            // 
            this.mButtonBrowseSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mButtonBrowseSource.Location = new System.Drawing.Point(436, 21);
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
            this.mBrowseDestination.Location = new System.Drawing.Point(436, 105);
            this.mBrowseDestination.Name = "mBrowseDestination";
            this.mBrowseDestination.Size = new System.Drawing.Size(30, 20);
            this.mBrowseDestination.TabIndex = 5;
            this.mBrowseDestination.Text = "...";
            this.mBrowseDestination.UseVisualStyleBackColor = true;
            this.mBrowseDestination.Click += new System.EventHandler(this.mBrowseDestination_Click);
            // 
            // mTextBoxDestination
            // 
            this.mTextBoxDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mTextBoxDestination.Location = new System.Drawing.Point(15, 105);
            this.mTextBoxDestination.Name = "mTextBoxDestination";
            this.mTextBoxDestination.Size = new System.Drawing.Size(415, 20);
            this.mTextBoxDestination.TabIndex = 4;
            // 
            // mLabelDestination
            // 
            this.mLabelDestination.AutoSize = true;
            this.mLabelDestination.Location = new System.Drawing.Point(12, 89);
            this.mLabelDestination.Name = "mLabelDestination";
            this.mLabelDestination.Size = new System.Drawing.Size(273, 13);
            this.mLabelDestination.TabIndex = 3;
            this.mLabelDestination.Text = "Please specify a folder for your resulting TokenAssist file.";
            // 
            // mButtonOK
            // 
            this.mButtonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mButtonOK.Location = new System.Drawing.Point(202, 141);
            this.mButtonOK.Name = "mButtonOK";
            this.mButtonOK.Size = new System.Drawing.Size(75, 23);
            this.mButtonOK.TabIndex = 6;
            this.mButtonOK.Text = "OK";
            this.mButtonOK.UseVisualStyleBackColor = true;
            this.mButtonOK.Click += new System.EventHandler(this.mButtonOK_Click);
            // 
            // mPanelMain
            // 
            this.mPanelMain.Controls.Add(this.mButtonBrowseDropbox);
            this.mPanelMain.Controls.Add(this.mLabelSource);
            this.mPanelMain.Controls.Add(this.mTextBoxSource);
            this.mPanelMain.Controls.Add(this.mButtonBrowseSource);
            this.mPanelMain.Controls.Add(this.mLabelDestination);
            this.mPanelMain.Controls.Add(this.mTextBoxDestination);
            this.mPanelMain.Controls.Add(this.mBrowseDestination);
            this.mPanelMain.Controls.Add(this.mButtonOK);
            this.mPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPanelMain.Location = new System.Drawing.Point(0, 0);
            this.mPanelMain.Name = "mPanelMain";
            this.mPanelMain.Size = new System.Drawing.Size(478, 179);
            this.mPanelMain.TabIndex = 7;
            // 
            // mStatusStrip
            // 
            this.mStatusStrip.Location = new System.Drawing.Point(0, 179);
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
            // mButtonBrowseDropbox
            // 
            this.mButtonBrowseDropbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mButtonBrowseDropbox.BackgroundImage = global::TokenAssist.Properties.Resources.dropbox;
            this.mButtonBrowseDropbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mButtonBrowseDropbox.Location = new System.Drawing.Point(436, 41);
            this.mButtonBrowseDropbox.Name = "mButtonBrowseDropbox";
            this.mButtonBrowseDropbox.Size = new System.Drawing.Size(30, 30);
            this.mButtonBrowseDropbox.TabIndex = 7;
            this.mButtonBrowseDropbox.UseVisualStyleBackColor = true;
            this.mButtonBrowseDropbox.Click += new System.EventHandler(this.mButtonBrowseDropbox_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 201);
            this.Controls.Add(this.mPanelMain);
            this.Controls.Add(this.mProgressBar);
            this.Controls.Add(this.mStatusStrip);
            this.Name = "MainForm";
            this.Text = "TokenAssist";
            this.mPanelMain.ResumeLayout(false);
            this.mPanelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mLabelSource;
        private System.Windows.Forms.TextBox mTextBoxSource;
        private System.Windows.Forms.Button mButtonBrowseSource;
        private System.Windows.Forms.Button mBrowseDestination;
        private System.Windows.Forms.TextBox mTextBoxDestination;
        private System.Windows.Forms.Label mLabelDestination;
        private System.Windows.Forms.Button mButtonOK;
        private System.Windows.Forms.Panel mPanelMain;
        private System.Windows.Forms.StatusStrip mStatusStrip;
        private System.Windows.Forms.ProgressBar mProgressBar;
        private System.Windows.Forms.Button mButtonBrowseDropbox;
    }
}

