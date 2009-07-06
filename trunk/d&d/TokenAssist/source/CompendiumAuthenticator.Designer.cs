namespace TokenAssist
{
    partial class CompendiumAuthenticator
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
            this.mWebBrowser = new System.Windows.Forms.WebBrowser();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mButtonCancel = new System.Windows.Forms.Button();
            this.mProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // mWebBrowser
            // 
            this.mWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.mWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.mWebBrowser.Name = "mWebBrowser";
            this.mWebBrowser.Size = new System.Drawing.Size(1016, 344);
            this.mWebBrowser.TabIndex = 0;
            this.mWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.mWebBrowser_Navigating);
            this.mWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.mWebBrowser_DocumentCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 344);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1016, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mButtonCancel
            // 
            this.mButtonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mButtonCancel.Location = new System.Drawing.Point(471, 299);
            this.mButtonCancel.Name = "mButtonCancel";
            this.mButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.mButtonCancel.TabIndex = 3;
            this.mButtonCancel.Text = "Cancel";
            this.mButtonCancel.UseVisualStyleBackColor = true;
            // 
            // mProgressBar
            // 
            this.mProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.mProgressBar.Location = new System.Drawing.Point(302, 36);
            this.mProgressBar.MarqueeAnimationSpeed = 10;
            this.mProgressBar.Name = "mProgressBar";
            this.mProgressBar.Size = new System.Drawing.Size(413, 27);
            this.mProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.mProgressBar.TabIndex = 4;
            // 
            // CompendiumAuthenticator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mButtonCancel;
            this.ClientSize = new System.Drawing.Size(1016, 366);
            this.Controls.Add(this.mProgressBar);
            this.Controls.Add(this.mButtonCancel);
            this.Controls.Add(this.mWebBrowser);
            this.Controls.Add(this.statusStrip1);
            this.Name = "CompendiumAuthenticator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser mWebBrowser;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button mButtonCancel;
        private System.Windows.Forms.ProgressBar mProgressBar;
    }
}