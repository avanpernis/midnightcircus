using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TokenAssist
{
    public partial class ImageBrowser : UserControl
    {
        private const string ImageFileFilter = @"Image files (*.png, *.jpg)|*.png;*.jpg|All files (*.*)|*.*";

        public ImageBrowser()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Category("Tweaks")]
        public string Label
        {
            get
            {
                return mLabel.Text;
            }
            set
            {
                mLabel.Text = value;
            }
        }

        public string ImageFile
        {
            get
            {
                return mImageFile;
            }
            set
            {
                mImageFile = value;

                if (mImageFile != null)
                {
                    mPictureBox.Image = Image.FromFile(value);
                }
            }
        }

        private void mButtonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = ImageFileFilter;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImageFile = dialog.FileName;
            }
        }

        private void mButtonBrowseDropbox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = ImageFileFilter;
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
                ImageFile = dialog.FileName;
            }
        }

        private string mImageFile;
    }
}
