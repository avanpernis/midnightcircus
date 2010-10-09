namespace TokenAssist
{
    partial class ImageBrowser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mLabel = new System.Windows.Forms.Label();
            this.mPictureBox = new System.Windows.Forms.PictureBox();
            this.mButtonBrowse = new System.Windows.Forms.Button();
            this.mButtonBrowseDropbox = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mLabel
            // 
            this.mLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mLabel.Location = new System.Drawing.Point(0, 0);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(128, 13);
            this.mLabel.TabIndex = 16;
            this.mLabel.Text = "Label";
            // 
            // mPictureBox
            // 
            this.mPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.mPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPictureBox.Location = new System.Drawing.Point(0, 13);
            this.mPictureBox.Name = "mPictureBox";
            this.mPictureBox.Size = new System.Drawing.Size(128, 128);
            this.mPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mPictureBox.TabIndex = 15;
            this.mPictureBox.TabStop = false;
            // 
            // mButtonBrowse
            // 
            this.mButtonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mButtonBrowse.Location = new System.Drawing.Point(103, 101);
            this.mButtonBrowse.Name = "mButtonBrowse";
            this.mButtonBrowse.Size = new System.Drawing.Size(24, 20);
            this.mButtonBrowse.TabIndex = 17;
            this.mButtonBrowse.Text = "...";
            this.mButtonBrowse.UseVisualStyleBackColor = true;
            this.mButtonBrowse.Click += new System.EventHandler(this.mButtonBrowse_Click);
            // 
            // mButtonBrowseDropbox
            // 
            this.mButtonBrowseDropbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mButtonBrowseDropbox.BackgroundImage = global::TokenAssist.Properties.Resources.dropbox;
            this.mButtonBrowseDropbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mButtonBrowseDropbox.Location = new System.Drawing.Point(103, 120);
            this.mButtonBrowseDropbox.Name = "mButtonBrowseDropbox";
            this.mButtonBrowseDropbox.Size = new System.Drawing.Size(24, 20);
            this.mButtonBrowseDropbox.TabIndex = 18;
            this.mButtonBrowseDropbox.UseVisualStyleBackColor = true;
            this.mButtonBrowseDropbox.Click += new System.EventHandler(this.mButtonBrowseDropbox_Click);
            // 
            // ImageBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mButtonBrowseDropbox);
            this.Controls.Add(this.mButtonBrowse);
            this.Controls.Add(this.mPictureBox);
            this.Controls.Add(this.mLabel);
            this.Name = "ImageBrowser";
            this.Size = new System.Drawing.Size(128, 141);
            ((System.ComponentModel.ISupportInitialize)(this.mPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label mLabel;
        private System.Windows.Forms.PictureBox mPictureBox;
        private System.Windows.Forms.Button mButtonBrowse;
        private System.Windows.Forms.Button mButtonBrowseDropbox;
    }
}
