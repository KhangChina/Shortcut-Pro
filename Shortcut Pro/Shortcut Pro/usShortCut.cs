using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Shortcut_Pro
{
    public partial class usShortCut : UserControl
    {

        public usShortCut(string name, string note, string path, Image image)
        {
            InitializeComponent();
            groupUsShortCut.Text = name;
            this.note = note;
            this.path = path;
            if (File.Exists(path))
            {
               // pic.Image = image;
                groupUsShortCut.CaptionImageOptions.Image= image;
            }
            else
            {
                btnRun.Enabled = false;
            }
        }
        string path, note;
        shortCutHelpers sc = new shortCutHelpers();
        private void btnLocation_Click(object sender, EventArgs e)
        {
            var dir = Path.GetDirectoryName(path);
            System.Diagnostics.Process.Start(dir);
        }
        private void btnRun_Click(object sender, EventArgs e)
        {       
            sc.RunShortcuts(path);
        }
        shortCutHelpers shortCut = new shortCutHelpers();

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler deleteShortCut;
        public void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("You are you sure delete "+ groupUsShortCut.Text, "Title", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                shortCut.deleteShortCut(groupUsShortCut.Text);
                if (this.deleteShortCut != null)
                    this.deleteShortCut(this, e);
            }
        }
        private void btnDetail_Click(object sender, EventArgs e)
        {
            frmDetail frm = new frmDetail(groupUsShortCut.Text, note, path);
            frm.ShowDialog();
            if (this.deleteShortCut != null)
                this.deleteShortCut(this, e);
        }
    }
}
