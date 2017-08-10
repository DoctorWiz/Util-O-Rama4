using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChristmassTimingTrax
{
    public partial class AlertForm : Form
    {
        #region Properties
        public string DisplayMessage { set { this.labelMessage.Text = value; } }
        public string DisplayTitle { set { this.Text = value; } }
        public int DisplayPercentage { set { this.progressBar.Value = value; } }

        //public string DisplayPercentageComplete { set { this.lblPercentComplete.Text = value; } }
        #endregion

        #region Methods
        public AlertForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        public event EventHandler<EventArgs> Canceled;

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            EventHandler<EventArgs> ea = Canceled;
            if (ea != null)
                ea(this, e);
        }

        #endregion
    }
}
