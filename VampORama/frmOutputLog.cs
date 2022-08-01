using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FileHelper;
using FormHelper;

namespace UtilORama4
{
  public partial class frmOutputLog : Form
  {
    private bool firstPaint = false;
    private bool firstResize = false;
    private bool firstShow = false;
    private Settings userSettings = Settings.Default;


    Form ownerForm = null;
    public frmOutputLog()
    {
      InitializeComponent();
    }

    public frmOutputLog(Form parentForm)
    {
      InitializeComponent();
      ownerForm = parentForm;
      this.RestoreView();
      this.CenterOverParent();
    }
    public string LogText
    {
      get
      {
        return txtOutput.Text;
      }
      set
      {
        txtOutput.Text = value;
        txtOutput.SelectionStart = txtOutput.TextLength;
        txtOutput.ScrollToCaret();
      }
    }


    public bool Done
    {
      get
      {
        return btnClose.Enabled;
      }
      set
      {
        btnClose.Enabled = value;
        btnSaveLog.Enabled = value;
        if (value)
        {
          this.Cursor = Cursors.Default;
        }
        else
        {
          this.Cursor = Cursors.WaitCursor;
        }
      }
    }
    private void frmOutputLog_ResizeEnd(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Normal)
      {
        txtOutput.Height = ClientSize.Height - 40;
        btnClose.Top = ClientSize.Height - 30;
        btnClose.Left = ClientSize.Width / 2 - btnClose.Width - 16;
        btnSaveLog.Top = ClientSize.Height - 30;
        btnSaveLog.Left = ClientSize.Width / 2 + 16;
        string t = btnClose.Left.ToString() + ", " + btnSaveLog.Left.ToString();
        lblDebug.Text = t;
        lblDebug.Top = btnClose.Top;
      }
      if (this.WindowState == FormWindowState.Minimized)
      {
        // Handled in [regular] Resize event
        //ownerForm.WindowState = FormWindowState.Minimized;
      }
    }

    private void frmOutputLog_Load(object sender, EventArgs e)
    {

    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      //this.Hide();
      //SaveFormPosition();
      this.Close(); //?
    }

    private void frmOutputLog_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!btnClose.Enabled)
      {
        e.Cancel = true;
      }
      else
      {
        this.SaveView();
      }
      // Do Nothing?
    }

    private void btnSaveLog_Click(object sender, EventArgs e)
    {
      dlgFileSave.Title = "Save Output Log File As...";
      dlgFileSave.InitialDirectory = "shell:::{679F85CB-0220-4080-B29B-5540CC05AAB6}";
      dlgFileSave.Filter = "Log Files (*.log)|*.log|Text Files (*.txt)|*.txt";
      dlgFileSave.FilterIndex = 1;
      dlgFileSave.DefaultExt = ".log";
      dlgFileSave.FileName = "Sonic Annotator Output.log";
      dlgFileSave.CheckPathExists = true;
      dlgFileSave.OverwritePrompt = false;
      dlgFileSave.SupportMultiDottedExtensions = true;
      dlgFileSave.ValidateNames = true;
      //newFileIn = Path.GetFileNameWithoutExtension(fileCurrent) + " Part " + member.ToString(); // + ext;
      //newFileIn = "Part " + member.ToString() + " of " + Path.GetFileNameWithoutExtension(fileCurrent);
      //newFileIn = "Part Mother Fucker!!";
      //dlgFileSave.FileName = initFile;
      DialogResult result = dlgFileSave.ShowDialog(this);
      if (result == DialogResult.OK)
      {
        DialogResult ow = Fyle.SafeOverwriteFile(dlgFileSave.FileName);
        if (ow == DialogResult.Yes)
        {
          StreamWriter writer = new StreamWriter(dlgFileSave.FileName);
          writer.Write(txtOutput.Text);
          writer.Close();
        }
      }
    }

    private void frmOutputLog_Resize(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Normal)
      {
        // Ignore for now, handle at ResizeEnd
      }
      if (this.WindowState == FormWindowState.Minimized)
      {
        ownerForm.WindowState = FormWindowState.Minimized;
      }

    }

    private void frmOutputLog_Move(object sender, EventArgs e)
    {
      //Fyle.BUG("Output form has been moved.  Check call stack and find out why.");
    }

    private void frmOutputLog_Shown(object sender, EventArgs e)
    {
      if (!firstShow)
      {
        firstShow = true;
        frmOutputLog_Resize(this, null);
      }
    }

    private void frmOutputLog_Paint(object sender, PaintEventArgs e)
    {
      if (!firstPaint)
      {
        //this.CenterOverParent();
        firstPaint = true;
      }
    }

    /*
    private void CenterThisOverOwner()
    {
      System.Drawing.Size savedSize = userSettings.OutputSize;
      this.Width = savedSize.Width;
      this.Height = savedSize.Height; ;
      int X = (ownerForm.Width - this.Width) / 2 + ownerForm.Left;
      int Y = (ownerForm.Height - this.Height) / 2 + ownerForm.Top;
      this.Location = new Point(X, Y);
    }
    */


  }
}
