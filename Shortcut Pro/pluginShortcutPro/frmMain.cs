using DevExpress.XtraEditors;
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

namespace pluginShortcutPro
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
            notifyIcon1.Visible = false;
        }
        shortCutHelpers shortCut = new shortCutHelpers();
        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReloadData();
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAdd frm = new frmAdd();
            frm.ShowDialog();
            ReloadData();
        }
        void ReloadData()
        {
            flowMain.Controls.Clear();
            contextMenuStrip1.Items.Clear();
            var data = shortCut.getData();
            foreach (var item in data)
            {
                Icon theIcon = shortCut.ExtractIconFromFilePath(item["path"].ToString());
                Image image = Bitmap.FromHicon(new Icon(theIcon, new Size(40, 40)).Handle);

                usShortCut us = new usShortCut(item["name"].ToString(), item["note"].ToString(), item["path"].ToString(), image);
                flowMain.Controls.Add(us);
                ToolStripMenuItem newMenuItem = new ToolStripMenuItem();
                newMenuItem.Text = item["name"].ToString();
                newMenuItem.ToolTipText = item["path"].ToString();
                newMenuItem.Image = image;
                //contextMenuStrip1.Items.Add(item["name"].ToString());contextMenuStrip1.Items.Add(item["name"].ToString());
                contextMenuStrip1.Items.Add(newMenuItem);
                us.deleteShortCut += new EventHandler(btnRemove_Click);

            }
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
        private void btnHide_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            notifyIcon1.Visible = true;
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            shortCut.RunShortcuts(item.ToolTipText);
        }

        private void flowMain_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }
        private void flowMain_DragDrop(object sender, DragEventArgs e)
        {
            string path = "";
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  
            if (files != null && files.Any())
            {
                path = files.First(); //select the first one  
                string txtPath = path.Replace("\\", "/");
                string txtName = Path.GetFileNameWithoutExtension(txtPath);
                shortCut.AddShortCut(txtName, txtPath, "");
                ReloadData();
            }
        }
    }
}