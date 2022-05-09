using System;
using System.IO;
using System.Windows.Forms;

namespace pluginShortcutPro
{
    public partial class frmAdd : DevExpress.XtraEditors.XtraForm
    {
        public frmAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            shortCutHelpers cutHelpers = new shortCutHelpers();
            cutHelpers.AddShortCut(txtName.Text, txtPath.Text, txtNote.Text);
            Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            xtraOpenFileDialog1.FileName = "";
            if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = xtraOpenFileDialog1.FileName.Replace("\\", "/");
                txtName.Text = Path.GetFileNameWithoutExtension(txtPath.Text);
            }
        }
    }
}