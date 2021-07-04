using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using RecentlyUsed;

namespace Test
{
	public partial class Form1 : Form
	{
		private object syncGate = new object();
		private Process cmdProc;
		private StringBuilder outLog = new StringBuilder();
		private bool outputChanged;
		private MRU mru = new MRU(Properties.Settings.Default);

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs ea)
		{
			Annotate_New();
		}


		private void Annotate_New()
		{

			//! SEE THESE ARTICLES:
			// https://stackoverflow.com/questions/186822/capturing-console-output-from-a-net-application-c
			// https://stackoverflow.com/questions/3633653/how-to-capture-the-standard-output-error-of-a-process
			// https://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why?lq=1

			string pathWork = "C:\\Users\\Wizard\\AppData\\Local\\Temp\\UtilORama\\VampORama\\";
			string fileConfig = "vamp_qm-vamp-plugins_qm-barbeattracker_beats.n3";
			//string fileOutput = "song_vamp_qm-vamp-plugins_qm-barbeattracker_beats.csv";
			string fileSong = "song.mp3";
			string WRITEformat = " -f -w csv --csv-force ";
			string annotatorProgram = "C:\\PortableApps\\SonicAnnotator\\sonic-annotator.exe";


			string resultsFile = "";
			string ex = Path.GetExtension(fileSong);
			//! string annotatorArguments = "-t " + vampParams;
			string annotatorArguments = "-t " + fileConfig;
			annotatorArguments += " " + fileSong; // + ex;
			annotatorArguments += WRITEformat;
			//annotatorArguments += " 2>output.txt";
			//string outputFile = tempPath + "output.log";
			//string fileOutput = vampParams.Replace(':', '_') + ".n3";

			try
			{
				string emsg = annotatorProgram + " " + annotatorArguments;
				Console.WriteLine(emsg);
				//DialogResult dr = MessageBox.Show(this, emsg, "About to launch", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
				DialogResult dr = DialogResult.Yes;

				if (dr == DialogResult.Yes)
				{
					Clipboard.SetText(emsg);
				}
				if (dr != DialogResult.Cancel)
				{
					resultsFile = pathWork;
					resultsFile += "song_";
					resultsFile += Path.GetFileNameWithoutExtension(fileConfig);
					resultsFile += ".csv";

					//! FOR TESTING DEBUGGING, if set to re-use old files, OR if no file from a previous run exists
					//if (!System.IO.File.Exists(resultsFile))
					if (true)
					{
						lock (syncGate)
						{
							if (cmdProc != null) return;
						}
						
						string runthis = annotatorProgram + " " + annotatorArguments;
						runthis = "/c " + runthis; // + " 2>output.txt";

						string vampCommandLast = runthis;
						Clipboard.SetText(runthis);

						cmdProc = new Process();
						ProcessStartInfo procInfo = new ProcessStartInfo();
						//x procInfo.FileName = annotatorProgram;
						procInfo.FileName = "cmd.exe";
						procInfo.RedirectStandardOutput = true;
						procInfo.RedirectStandardError = true;
						procInfo.CreateNoWindow = true;

						//annotatorArguments += " > " + outputFile;
						//procInfo.WindowStyle = ProcessWindowStyle.Hidden;
						procInfo.RedirectStandardInput = true;
						procInfo.UseShellExecute = false;
						//x procInfo.Arguments = annotatorArguments;
						procInfo.Arguments = runthis;
						procInfo.WorkingDirectory = pathWork;

						cmdProc.StartInfo = procInfo;
						cmdProc.EnableRaisingEvents = true;
						cmdProc.ErrorDataReceived += ProcessVampError;
						cmdProc.OutputDataReceived += ProcessVampError;
						cmdProc.Exited += VampProcessEnded;
						cmdProc.Start();
						cmdProc.BeginErrorReadLine();
						cmdProc.BeginOutputReadLine();




						//cmdProc.WaitForExit();
						while (cmdProc != null)
						{
							//while (!cmdProc.HasExited)
							//{
								Application.DoEvents(); // This keeps your form responsive by processing events
							//}
						}
					}
				}

				if (System.IO.File.Exists(resultsFile))
				{
					// return resultsFile;
					// errCount = 99999;
				}
				else
				{
					// NO RESULTS FILE!	
					System.Diagnostics.Debugger.Break();
				}
			}
			catch (Exception ex2)
			{
				string msg = ex2.Message;
				System.Diagnostics.Debugger.Break();
				resultsFile = "";
			}



			string msg2 = "Annotation Failed!";
			if (resultsFile.Length > 3)
			{
				msg2 = "Results in " + resultsFile;
			}

			DialogResult dr2 = MessageBox.Show(this, msg2, "Annotation Results");




		}

		private void ProcessVampError(object sender, DataReceivedEventArgs drea)
		{
			lock (syncGate)
			{
				if (sender != cmdProc) return;
				outLog.AppendLine(drea.Data);
				if (outputChanged) return;
				outputChanged = true;
				BeginInvoke(new Action(OnOutputChanged));
			}
		}

		private void OnOutputChanged()
		{
			lock (syncGate)
			{
				txtOutput.Text = outLog.ToString();
				txtOutput.SelectionStart = txtOutput.TextLength;
				txtOutput.ScrollToCaret();
				outputChanged = false;
			}
		}

		private void VampProcessEnded(object sender, EventArgs e)
		{
			lock (syncGate)
			{
				if (sender != cmdProc) return;
				cmdProc.Dispose();
				cmdProc = null;
			}
		}










		private void Annotate_Original()
		{ 

			//! SEE THESE ARTICLES:
			// https://stackoverflow.com/questions/186822/capturing-console-output-from-a-net-application-c
			// https://stackoverflow.com/questions/3633653/how-to-capture-the-standard-output-error-of-a-process
			// https://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why?lq=1

			string pathWork = "C:\\Users\\Wizard\\AppData\\Local\\Temp\\UtilORama\\VampORama\\";
			string fileConfig = "vamp_qm-vamp-plugins_qm-barbeattracker_beats.n3";
			//string fileOutput = "song_vamp_qm-vamp-plugins_qm-barbeattracker_beats.csv";
			string fileSong = "song.mp3";
			string WRITEformat = " -f -w csv --csv-force ";
			string annotatorProgram = "C:\\PortableApps\\SonicAnnotator\\sonic-annotator.exe";


			string resultsFile = "";
			string ex = Path.GetExtension(fileSong);
			//! string annotatorArguments = "-t " + vampParams;
			string annotatorArguments = "-t " + fileConfig;
			annotatorArguments += " " + fileSong; // + ex;
			annotatorArguments += WRITEformat;
			//annotatorArguments += " 2>output.txt";
			//string outputFile = tempPath + "output.log";
			//string fileOutput = vampParams.Replace(':', '_') + ".n3";

			try
			{
				string emsg = annotatorProgram + " " + annotatorArguments;
				Console.WriteLine(emsg);
				//DialogResult dr = MessageBox.Show(this, emsg, "About to launch", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
				DialogResult dr = DialogResult.Yes;

				if (dr == DialogResult.Yes)
				{
					Clipboard.SetText(emsg);
				}
				if (dr != DialogResult.Cancel)
				{
					resultsFile = pathWork;
					resultsFile += "song_";
					resultsFile += Path.GetFileNameWithoutExtension(fileConfig);
					resultsFile += ".csv";

					//! FOR TESTING DEBUGGING, if set to re-use old files, OR if no file from a previous run exists
					if (!System.IO.File.Exists(resultsFile))
					{
						string runthis = annotatorProgram + " " + annotatorArguments;
						runthis = "/c " + runthis + " 2>output.txt";

						string vampCommandLast = runthis;
						Clipboard.SetText(runthis);

						Process cmdProc = new Process();
						ProcessStartInfo procInfo = new ProcessStartInfo();
						//x procInfo.FileName = annotatorProgram;
						procInfo.FileName = "cmd.exe";
						//annotatorArguments += " > " + outputFile;
						procInfo.CreateNoWindow = true;
						procInfo.WindowStyle = ProcessWindowStyle.Hidden;
						//procInfo.RedirectStandardOutput = true;
						//procInfo.RedirectStandardInput = true;
						//procInfo.RedirectStandardError = true;
						//procInfo.UseShellExecute = false;
						//x procInfo.Arguments = annotatorArguments;
						procInfo.Arguments = runthis;
						procInfo.WorkingDirectory = pathWork;
						cmdProc.StartInfo = procInfo;

						//! SEE THESE ARTICLES:
						// https://stackoverflow.com/questions/186822/capturing-console-output-from-a-net-application-c
						// https://stackoverflow.com/questions/3633653/how-to-capture-the-standard-output-error-of-a-process
						// https://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why?lq=1




						cmdProc.Start();
						cmdProc.WaitForExit(120000);  // 2 minute timeout
						int x = cmdProc.ExitCode;
					}

					if (System.IO.File.Exists(resultsFile))
					{
						// return resultsFile;
						// errCount = 99999;
					}
					else
					{
						// NO RESULTS FILE!	
						System.Diagnostics.Debugger.Break();
					}
				}
			}
			catch (Exception ex2)
			{
				string msg = ex2.Message;
				System.Diagnostics.Debugger.Break();
				resultsFile = "";
			}



			string msg2 = "Annotation Failed!";
			if (resultsFile.Length > 3)
			{
				msg2 = "Results in " + resultsFile;
			}

			DialogResult dr2 = MessageBox.Show(this, msg2, "Annotation Results");




		}
	}
}
