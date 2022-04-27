using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shortcut_Pro
{
    public partial class frmAdd : Form
    {
        public frmAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            shortCutHelpers cutHelpers = new shortCutHelpers();
            cutHelpers.AddShortCut(txtName.Text,txtPath.Text,txtNote.Text);
            Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName.Replace("\\","/");
                txtName.Text = Path.GetFileNameWithoutExtension(txtPath.Text);
            }
        }
    }
}
