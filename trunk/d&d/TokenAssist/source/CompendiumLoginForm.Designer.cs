namespace TokenAssist
{
    partial class CompendiumLoginForm
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
            this.mLoginButton = new System.Windows.Forms.Button();
            this.mCancelButton = new System.Windows.Forms.Button();
            this.mEmailText = new System.Windows.Forms.TextBox();
            this.mPasswordText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mLoginButton
            // 
            this.mLoginButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mLoginButton.Location = new System.Drawing.Point(189, 104);
            this.mLoginButton.Name = "mLoginButton";
            this.mLoginButton.Size = new System.Drawing.Size(119, 25);
            this.mLoginButton.TabIndex = 5;
            this.mLoginButton.Text = "Login";
            this.mLoginButton.UseVisualStyleBackColor = true;
            this.mLoginButton.Click += new System.EventHandler(this.mLoginButton_Click);
            // 
            // mCancelButton
            // 
            this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancelButton.Location = new System.Drawing.Point(36, 104);
            this.mCancelButton.Name = "mCancelButton";
            this.mCancelButton.Size = new System.Drawing.Size(119, 25);
            this.mCancelButton.TabIndex = 4;
            this.mCancelButton.Text = "Cancel";
            this.mCancelButton.UseVisualStyleBackColor = true;
            // 
            // mEmailText
            // 
            this.mEmailText.Location = new System.Drawing.Point(95, 20);
            this.mEmailText.Name = "mEmailText";
            this.mEmailText.Size = new System.Drawing.Size(241, 20);
            this.mEmailText.TabIndex = 1;
            // 
            // mPasswordText
            // 
            this.mPasswordText.Location = new System.Drawing.Point(95, 63);
            this.mPasswordText.Name = "mPasswordText";
            this.mPasswordText.PasswordChar = '*';
            this.mPasswordText.Size = new System.Drawing.Size(132, 20);
            this.mPasswordText.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // CompendiumLoginForm
            // 
            this.AcceptButton = this.mLoginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mCancelButton;
            this.ClientSize = new System.Drawing.Size(352, 147);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mPasswordText);
            this.Controls.Add(this.mEmailText);
            this.Controls.Add(this.mCancelButton);
            this.Controls.Add(this.mLoginButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CompendiumLoginForm";
            this.Text = "Compendium Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mLoginButton;
        private System.Windows.Forms.Button mCancelButton;
        private System.Windows.Forms.TextBox mEmailText;
        private System.Windows.Forms.TextBox mPasswordText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}