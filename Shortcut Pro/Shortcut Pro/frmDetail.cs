﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shortcut_Pro
{
    public partial class frmDetail : Form
    {
        public frmDetail(string name,string note,string path)
        {
            InitializeComponent();
            txtName.Text = name;
            txtNote.Text = note;
            txtPath.Text = path;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            shortCutHelpers shortCut = new shortCutHelpers();
            shortCut.EditShortCut(txtNote.Text,txtPath.Text);
            this.Close();
        }

    }
}
