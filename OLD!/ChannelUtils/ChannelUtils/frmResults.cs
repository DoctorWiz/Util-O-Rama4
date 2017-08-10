using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChannelUtils
{
    public partial class frmResults : Form
    {
        private string filename;
        
        public frmResults()
        {
            InitializeComponent();
        }

        public void SetInfo(string richTextInfo)
        {
            rtfInfo.Text = richTextInfo;
        }

        public void SetFilename(string fileName)
        {
            this.Text = "Results for " + fileName;
            filename = fileName;
        }
        
        public void AddListItem(string itemInfo)
        {

            lstErrors.Items.Add(itemInfo);
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmResults_Load(object sender, EventArgs e)
        {

        }
    }
}
