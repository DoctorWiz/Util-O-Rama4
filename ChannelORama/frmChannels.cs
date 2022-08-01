using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOR4;
using FileHelper;
using FormHelper;
//using xAdmin;

namespace UtilORama4
{
  public partial class frmChannels : Form
  {
    private static Settings userSettings = Settings.Default;
    private const string helpPage = "http://wizlights.com/utilorama/blankorama";
    private List<DMXUniverse> Universes = new List<DMXUniverse>();



    public frmChannels()
    {
      InitializeComponent();
    }

    private void frmBlank_Load(object sender, EventArgs e)
    {
      this.RestoreView();
      RestoreUserSettings();
    }

    private void frmBlank_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.SaveView();
      SaveUserSettings();
    }


    private void SaveUserSettings()
    {

    }

    private void RestoreUserSettings()
    {

    }

    public void ImBusy(bool busy)
    {
      if (busy)
      {
        this.Cursor = Cursors.WaitCursor;
        this.Enabled = false;
      }
      else
      {
        this.Cursor = Cursors.Default;
        this.Enabled = true;
      }
    }

    private void pnlHelp_Click(object sender, EventArgs e)
    { System.Diagnostics.Process.Start(helpPage); }

    private void pnlAbout_Click(object sender, EventArgs e)
    {
      ImBusy(true);
      frmAbout aboutBox = new frmAbout();
      aboutBox.Icon = this.Icon;
      aboutBox.Text = "About Sort-O-Rama";
      aboutBox.AppIcon = picAboutIcon.Image;
      aboutBox.ShowDialog(this);
      ImBusy(false);
    }

    private string PathToDB(int year)
    {
      string ret = Fyle.DefaultDocumentsPath;
      ret += "Christmas\\";
      ret += year.ToString();
      ret += "\\Docs\\ChannelDB\\";
      return ret;
    }



  }
}
