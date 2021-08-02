using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace ChristmassTimingTrax
{
    public partial class AudacityForm : Form
    {
        #region Fields
        AlertForm analyzer;
        AlertForm generator;
        private bool FormLoaded = false;
        private Queue<string> myQueue;
        public static StringCollection LastSelectedFrequencies;
        //public static IList<int> LastSelectedFrequencies = new List<int>();
        public static DataTable LastInputFileData;
        #endregion

        #region Methods
        public AudacityForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        //Form
        private void AudacityForm_Load(object sender, EventArgs e)
        {
            
            cbOutputFormat.DataSource = Globals.AvailableOutputs;
            cbOutputFormat.DisplayMember = "Name";
            cbOutputFormat.ValueMember = "Value";
            cbOutputFormat.SelectedValue = Globals.LastOutputFormat;
            cbOutputFormat.Refresh();

            tbOutputFolder.Text = Globals.LastOutputFolder;
            tbOutputFileName.Text = Globals.LastOutputFileName;
            tbOutputFileName.Enabled = true;
            chkUseFrequency.Checked = Globals.LastOutputFileNameUseFrequencyName;
            
            btnGenerateTimingFiles.Enabled = false;
            btnAnalyzeInputFile.Enabled = false;

            LastSelectedFrequencies = new StringCollection();

            FormLoaded = true;
        }

        private void AudacityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.LastInputFile_Audacity = tbInputFile.Text;
            Globals.LastInputFileIsPoly_Audacity = chkPolyTranscription.Checked;
            Globals.LastOutputFileName = (chkUseFrequency.Checked) ? "" : tbOutputFileName.Text;
            Globals.LastOutputFolder = tbOutputFolder.Text;
            Globals.LastOutputFormat = cbOutputFormat.SelectedValue.ToString();
        }

        //Dialog Boxes
        private void dlgInputFile(object sender, EventArgs e)
        {
            //string PreviousInputFile = (tbInputFile.Text.Length == 0) ? Globals.Audacity_PreviousInputFile : tbInputFile.Text;
            string PreviousInputFile = (tbInputFile.Text.Length == 0) ? "" : tbInputFile.Text;
            DialogInputFile.FileName = PreviousInputFile;
            DialogResult result = DialogInputFile.ShowDialog();
            tbInputFile.Text = ((result == DialogResult.OK) && (DialogInputFile.FileName.Length != 0)) ? DialogInputFile.FileName.ToString() : PreviousInputFile;
            Globals.LastInputFile_Audacity = tbInputFile.Text;
        }

        private void dlgOutputFolder(object sender, EventArgs e)
        {
            string PreviousOutputFolder = (tbOutputFolder.Text.Length == 0) ? "" : tbOutputFolder.Text;
            DialogOutputFolder.SelectedPath = PreviousOutputFolder;
            DialogResult result = DialogOutputFolder.ShowDialog();
            tbOutputFolder.Text = ((result == DialogResult.OK) && (DialogOutputFolder.SelectedPath.Length != 0)) ? DialogOutputFolder.SelectedPath.ToString() : PreviousOutputFolder;
            Globals.LastOutputFolder = tbOutputFolder.Text;
        }
        
        //Check boxes
        private void chkPolyTranscription_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            Globals.LastInputFileIsPoly_Audacity = chk.Checked;
            chkUseFrequency.Enabled = chk.Checked;
        }

        private void chkUseFrequency_CheckedChanged(object sender, EventArgs e)
        {
            tbOutputFileName.Enabled = !chkUseFrequency.Checked;
            tbOutputFileName.Text = (chkUseFrequency.Checked) ? "" : tbOutputFileName.Text;
            Globals.LastOutputFileNameUseFrequencyName = chkUseFrequency.Checked;
        }

        //Buttons
        private void btnAnalyzeInputFile_Click(object sender, EventArgs e)
        {
            myQueue = new Queue<string>();
            myQueue.Enqueue("Analyzer");
            if (Globals.LastInputFileIsPoly_Audacity)
                myQueue.Enqueue("Frequency");
            ProcessQueue();
        }

        private void btnGenerateTimingFiles_Click(object sender, EventArgs e)
        {
            myQueue = new Queue<string>();
            if ((LastInputFileData is DataTable) && (LastInputFileData.Rows.Count > 0))
            {
                StringCollection sc = Globals.LastSelectedFrequencies;
                if ((Globals.LastInputFileIsPoly_Audacity == true) && (sc.Count == 0))
                {
                    myQueue.Enqueue("Frequency");
                }
                myQueue.Enqueue("Generator");
            }
            else
            {
                myQueue.Enqueue("Analyzer");
                if (Globals.LastInputFileIsPoly_Audacity)
                    myQueue.Enqueue("Frequency");
                myQueue.Enqueue("Generator");
            }
            ProcessQueue();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (bwAnalyzer.IsBusy == true)
                bwAnalyzer.CancelAsync();

            if (bwGenerator.IsBusy == true)
                bwGenerator.CancelAsync();

            Close();
        }

        //Text boxes
        private void tbInputFile_TextChanged(object sender, EventArgs e)
        {
            if(File.Exists(tbInputFile.Text))
            {
                btnAnalyzeInputFile.Enabled = true;
                btnGenerateTimingFiles.Enabled = true;
                Globals.LastInputFile_Audacity = tbInputFile.Text;
                Globals.LastSelectedFrequencies = new StringCollection();
            }
            else
            {
                MessageBox.Show(string.Format("Input file missing\nFile:\n{0}",tbInputFile.Text), "Missing Input File", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void tbOutputFolder_TextChanged(object sender, EventArgs e)
        {
            if(!Directory.Exists(tbOutputFolder.Text))
            {
                DialogResult dr = MessageBox.Show(string.Format("The selected output folder does not exist.\n\nWould you like to create this directory?"), "Output Folder Invalid", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(tbOutputFolder.Text);
                        Globals.LastOutputFolder = tbOutputFolder.Text;
                        checkOutputSettings();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Unable to create output folder.\nError Message:\n{0}", ex.ToString()),"", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void tbOutputFileName_Leave(object sender, EventArgs e)
        {
            if(tbOutputFileName.Text.Length > 0)
            {
                try
                {
                    Globals.LastOutputFileName = tbOutputFileName.Text;
                    string path = Globals.LastOutputFolder;
                    if(path.EndsWith("\\") == false)
                    {
                        path += "\\";
                    }
                    path += Globals.LastOutputFileName;
                    path += ".test";
                    using (File.Create(path))
                    { }
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("File name entered is invalid.", ex.ToString()), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            checkOutputSettings();
        }
        
        //Combo Boxes
        private void cbOutputFormat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!FormLoaded) return;
            ComboBox cmb = (ComboBox)sender;
            int selectedIndex = cmb.SelectedIndex;
            string selectedValue = cmb.SelectedValue.ToString();
            Globals.LastOutputFormat = selectedValue;
            checkOutputSettings();
        }
        
        //Group Boxes
        private void checkOutputSettings()
        {
            bool blnFolder = (tbOutputFolder.Text.Length > 0) ? true : false;
            bool blnFormat = (cbOutputFormat.SelectedValue.ToString().Length > 0) ? true : false;
            bool blnFileName = ((chkUseFrequency.Checked) || (tbOutputFileName.Text.Length > 0)) ? true : false;
            if (blnFileName || blnFolder || blnFormat)
            { btnGenerateTimingFiles.Enabled = true; }
            else
            { btnGenerateTimingFiles.Enabled = false; }
        }
        #endregion

        #region Analyzer Events

        private void buttonCancelAnalyzer_Click(object sender, EventArgs e)
        {
            if (bwAnalyzer.WorkerSupportsCancellation == true)
            {
                bwAnalyzer.CancelAsync();
                analyzer.Close();
            }
        }

        private void bwAnalyzer_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
            string Message = "Analyzing file, please wait...";
            int iPercent = 0;
            worker.ReportProgress(iPercent, Message);
            if (File.Exists(Globals.LastInputFile_Audacity))
            {
                bwAnalyzer_AnalyzeDataFile(sender, e);
            }
            else
            {
                throw new FileNotFoundException(string.Format("Unable to locate file: {0}", Globals.LastInputFile_Audacity));
            }
        }

        private void bwAnalyzer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblAnalyzerResult.Text = string.Format("{0}% completed", e.ProgressPercentage.ToString());
            analyzer.DisplayMessage = e.UserState.ToString();
            analyzer.DisplayPercentage = e.ProgressPercentage;
        }

        private void bwAnalyzer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Analyzer has been canceled.", "Analyzer Canceled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblAnalyzerResult.Text = "Canceled";
            }
            else if (e.Error != null)
            {
                MessageBox.Show(string.Format("An error was encountered while analyzing file.\n\nError Message:\n{0}", e.Error.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblAnalyzerResult.Text = "Error";
            }
            else
            {
                myQueue.Dequeue();
                lblAnalyzerResult.Text = "Completed!";
            }
            // Close the AlertForm
            analyzer.Close();
            ProcessQueue();
        }

        private void bwAnalyzer_AnalyzeDataFile(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            DataTable dtInputData = (Globals.LastInputFileIsPoly_Audacity) ? Utils.CreateDataTable(typeof(PolyphonicFileLayout)) : Utils.CreateDataTable(typeof(AudacityFileLayout));
            LastInputFileData = dtInputData;
            int iTotalLines = File.ReadAllLines(Globals.LastInputFile_Audacity).Count();
            int iRead = 0;
            int iPercent = 0;
            string Message;
            string lineData;
            using (StreamReader sr = File.OpenText(Globals.LastInputFile_Audacity))
            {
                while ((lineData = sr.ReadLine()) != null)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        iRead++;
                        string[] splitData = Regex.Split(lineData, "\t\\s*");
                        if (splitData.Length != 3)
                        {
                            throw new InvalidFileFormatException(string.Format("Input file format is invalid.\n\nFile: {0}", Globals.LastInputFile_Audacity));
                        }
                        else
                        {
                            decimal outNumber;
                            decimal Start = (decimal.TryParse(splitData[0], out outNumber)) ? Convert.ToDecimal(splitData[0]) : -1;
                            decimal Stop = (decimal.TryParse(splitData[1], out outNumber)) ? Convert.ToDecimal(splitData[1]) : -1;
                            var Other = (decimal.TryParse(splitData[2], out outNumber)) ? Convert.ToDecimal(splitData[2]) : -1;
                            if (Globals.LastInputFileIsPoly_Audacity) Other = Convert.ToInt32(Other);
                            if (Start == -1 || Stop == -1 || Other == -1)
                            {
                                throw new BadDataRowException(string.Format("Bad Data Row\n\nFile:{0}\nLine [{1}] : {2}", Globals.LastInputFile_Audacity, iRead.ToString(), lineData));
                            }
                            else
                            {
                                dtInputData.Rows.Add(Start, Stop, Other);
                                decimal dPercent = Convert.ToDecimal((Convert.ToDouble(iRead) / Convert.ToDouble(iTotalLines)) * 100);
                                iPercent = Convert.ToInt32(dPercent);
                                Message = string.Format("Analyzing line {0} of {1}.", iRead.ToString(), iTotalLines.ToString());
                                worker.ReportProgress(iPercent, Message);
                                Thread.Sleep(10);
                            }
                        }
                    }
                }
            }
            if(dtInputData.Rows.Count > 0)
            {
                LastInputFileData = dtInputData;
            }
                
        }
        
        #endregion

        #region Generator Events
        private void buttonCancelGenerator_Click(object sender, EventArgs e)
        {
            if(bwGenerator.WorkerSupportsCancellation == true)
            {
                bwGenerator.CancelAsync();
                generator.Close();
            }
        }

        private void bwGenerator_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string Message = "Generating file(s), please wait...";
            int iPercent = 0;
            worker.ReportProgress(iPercent, Message);
            bwGenerator_CreateTimingFiles(sender, e);
        }

        private void bwGenerator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            generator.DisplayMessage = e.UserState.ToString();
            generator.DisplayPercentage = e.ProgressPercentage;
        }

        private void bwGenerator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Generator has been canceled.", "Generator Canceled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Error != null)
            {
                MessageBox.Show(string.Format("An error was encountered while generating file(s).\n\nError Message:\n{0}", e.Error.Message), "Generator Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // What to do when completed!
                myQueue.Dequeue();
            }
            // Close the AlertForm
            generator.Close();
            ProcessQueue();
        }

        private void bwGenerator_CreateTimingFiles(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string outputFolder = Globals.LastOutputFolder + "\\";
            string outputFileName = Globals.LastOutputFileName;
            string outputFormat = Globals.LastOutputFormat;
            string outpath = "";
            string frequencyName = "";
            string message = "";
            StringCollection selectedFreq = (Globals.LastInputFileIsPoly_Audacity == true) ? LastSelectedFrequencies : new StringCollection();
            
            bool isPoly = Globals.LastInputFileIsPoly_Audacity;
            bool useFreqName = Globals.LastOutputFileNameUseFrequencyName;
            
            DataTable TimingFileData = (isPoly) ? Utils.CreateDataTable(typeof(PolyphonicTimingData)) : Utils.CreateDataTable(typeof(TimingData));
            DataTable inputData = LastInputFileData;
            if (isPoly)
            {
                DataView inputDataView = inputData.DefaultView;
                inputDataView.Sort = "Frequency ASC";
                inputData = inputDataView.ToTable();
            }

            int iFileCount = 0;
            int iPercent = 0;
            int iCurrentFreq = -1;
            int iTotalFileCount = selectedFreq.Count;
            if(isPoly)
            {
                foreach (string freq in selectedFreq)
                {
                    iCurrentFreq = Convert.ToInt32(freq);
                    frequencyName = MIDI.GetFrequencyName(iCurrentFreq);
                    outpath = (useFreqName) ? string.Format("{0}{1}", outputFolder, frequencyName) : string.Format("{0}{1}_{2}", outputFolder, outputFileName, iFileCount.ToString());
                    outpath += Globals.GetTimingFileExtention(outputFormat);
                    string FileName = Path.GetFileName(outpath);
                    iFileCount++;
                    message = string.Format("Generating timing file {0} of {1} : File Name - {2}", iFileCount.ToString(), iTotalFileCount.ToString(), FileName);
                    decimal dFilePercent = Convert.ToDecimal((Convert.ToDouble(iFileCount) / Convert.ToDouble(iTotalFileCount)) * 100);
                    iPercent = Convert.ToInt32(dFilePercent);
                    worker.ReportProgress(iPercent, message);
                    TimingFileData.Clear();
                    var dataOutput = (isPoly) ? inputData.AsEnumerable().Where(dr => dr.Field<int>("Frequency") == iCurrentFreq) : inputData.AsEnumerable().Where(dr => dr.Field<double>("Other") == Convert.ToDouble(iCurrentFreq));
                    message = string.Format("Extracting frequency [{2}] {3} lines from input data", iFileCount.ToString(), iTotalFileCount.ToString(), iCurrentFreq.ToString(), frequencyName);
                    worker.ReportProgress(iPercent, message);
                    foreach (DataRow dr in dataOutput)
                    {
                        double StartTime = Utils.ConvertSecondsToMilliseconds(dr.Field<double>("Start"));
                        double StopTime = Utils.ConvertSecondsToMilliseconds(dr.Field<double>("Stop"));
                        TimingFileData.Rows.Add(StartTime, StopTime, iCurrentFreq, frequencyName);
                    }
                    message = string.Format("Generating timing file {0}", FileName);
                    worker.ReportProgress(iPercent, message);
                    switch (outputFormat)
                    {
                        case "LOR":
                            
                            break;
                        case "xLights":
                            int iWriteLine = 0;
                            int iTotalWrites = inputData.Rows.Count;
                            int iWritePercent = 0;
                            message = string.Format("Writing timing file : {0}", FileName);
                            worker.ReportProgress(iWritePercent, message);
                            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                            xmlWriterSettings.Indent = true;
                            xmlWriterSettings.IndentChars = ("\t");
                            xmlWriterSettings.NewLineChars = ("\r\n");
                            using (XmlWriter writer = XmlWriter.Create(outpath, xmlWriterSettings))
                            {
                                writer.WriteStartDocument();
                                writer.WriteStartElement("timing");
                                    writer.WriteStartAttribute("name");
                                        writer.WriteString(Path.GetFileNameWithoutExtension(outpath));
                                    writer.WriteEndAttribute();
                                    writer.WriteStartAttribute("SourceVersion");
                                        writer.WriteString("");
                                    writer.WriteEndAttribute();
                                    writer.WriteStartElement("EffectLayer");
                                    foreach (DataRow dr in TimingFileData.Rows)
                                    {
                                        iWriteLine++;
                                        double StartTime = dr.Field<double>("Start");
                                        double StopTime = dr.Field<double>("Stop");
                                        writer.WriteStartElement("Effect");
                                            writer.WriteStartAttribute("label");
                                                writer.WriteString(MIDI.GetFrequencyName(iCurrentFreq));
                                            writer.WriteEndAttribute();
                                            writer.WriteStartAttribute("starttime");
                                                writer.WriteString(StartTime.ToString());
                                            writer.WriteEndAttribute();
                                            writer.WriteStartAttribute("endtime");
                                                writer.WriteString(StopTime.ToString());
                                            writer.WriteEndAttribute();
                                        writer.WriteEndElement();
                                        decimal dWritePercent = Convert.ToDecimal((Convert.ToDouble(iWriteLine) / Convert.ToDouble(iTotalWrites)) * 100);
                                        iWritePercent = Convert.ToInt32(dWritePercent);
                                        worker.ReportProgress(iWritePercent, message);
                                        Thread.Sleep(10);
                                    }
                                    writer.WriteEndElement();
                                writer.WriteEndElement();
                                writer.WriteEndDocument();
                            }
                            message = string.Format("File created successfully. {0}", outpath);
                            worker.ReportProgress(100, message);
                            Thread.Sleep(50);
                            break;
                        case "Vixen":
                            
                            break;
                        case "JSON":
                            
                            break;
                    }
                }
            }
            else
            {
                outpath = string.Format("{0}{1}", outputFolder, outputFileName);
                outpath += Globals.GetTimingFileExtention(outputFormat);
                string FileName = Path.GetFileName(outpath);
                switch (outputFormat)
                {
                    case "LOR":
                        //retValue = ".lor";
                        break;
                    case "xLights":
                        int iWriteLine = 0;
                        int iTotalWrites = inputData.Rows.Count;
                        int iWritePercent = 0;
                        message = string.Format("Writing timing file : {0}", FileName);
                        worker.ReportProgress(iWritePercent, message);
                        using (XmlWriter writer = XmlWriter.Create(outpath))
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartElement("timing");
                                writer.WriteStartAttribute("name");
                                    writer.WriteString(FileName);
                                writer.WriteEndAttribute();
                                writer.WriteStartAttribute("SourceVersion");
                                    writer.WriteString("");
                                writer.WriteEndAttribute();
                                writer.WriteStartElement("EffectLayer");
                                foreach (DataRow dr in inputData.Rows)
                                {
                                    iWriteLine++;
                                    double StartTime = Utils.ConvertSecondsToMilliseconds(dr.Field<double>("Start"));
                                    double StopTime = Utils.ConvertSecondsToMilliseconds(dr.Field<double>("Stop"));
                                    writer.WriteStartElement("Effect");
                                        writer.WriteStartAttribute("label");
                                            writer.WriteString("");
                                        writer.WriteEndAttribute();
                                        writer.WriteStartAttribute("starttime");
                                            writer.WriteString(StartTime.ToString());
                                        writer.WriteEndAttribute();
                                        writer.WriteStartAttribute("endtime");
                                            writer.WriteString(StopTime.ToString());
                                        writer.WriteEndAttribute();
                                    writer.WriteEndElement();
                                    decimal dWritePercent = Convert.ToDecimal((Convert.ToDouble(iWriteLine) / Convert.ToDouble(iTotalWrites)) * 100);
                                    iWritePercent = Convert.ToInt32(dWritePercent);
                                    worker.ReportProgress(iWritePercent, message);
                                    Thread.Sleep(10);
                                }
                                writer.WriteEndElement();
                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                        }
                        message = string.Format("File created successfully. {0}", outpath);
                        worker.ReportProgress(100, message);
                        Thread.Sleep(50);
                        break;
                    case "Vixen":
                        //retValue = ".vixenTiming";
                        break;
                    case "JSON":
                        //retValue = ".json";
                        break;
                }

            }
            
        }


        #endregion

        #region Queue Work
        private void ProcessQueue()
        {
            if(myQueue.Any())
            {
                switch (myQueue.First())
                {
                    case "Analyzer":
                        if (bwAnalyzer.IsBusy != true)
                        {
                            analyzer = new AlertForm();
                            analyzer.Canceled += new EventHandler<EventArgs>(buttonCancelAnalyzer_Click);
                            analyzer.DisplayTitle = "Analyzing File";
                            analyzer.Show();
                            bwAnalyzer.RunWorkerAsync();
                        }
                        break;

                    case "Frequency":
                        // Show Dialog window so user can select frequencies to be outputted to timing files.
                        FrequencySelection FreqSelection = FrequencySelection.SetupFrequencyForm();

                        if (FreqSelection.ShowDialog(this) == DialogResult.Cancel)
                        {
                            string message = "Selection of frequencies has been canceled.\nIn order to create timing files based off a polyphonic file you must select at least one frequency.\n\nWould you like to go back and select the frequencies?";
                            DialogResult dr = MessageBox.Show(message, "No Selected Frequencies", MessageBoxButtons.YesNo, MessageBoxIcon.Hand );
                            if(dr == DialogResult.No)
                                myQueue.Clear();
                            ProcessQueue();
                        }
                        else
                        {
                            myQueue.Dequeue();
                            ProcessQueue();
                        }
                        break;

                    case "Generator":
                        if (bwGenerator.IsBusy != true)
                        {
                            generator = new AlertForm();
                            generator.Canceled += new EventHandler<EventArgs>(buttonCancelGenerator_Click);
                            generator.DisplayTitle = "Generating Files";
                            generator.Show();
                            bwGenerator.RunWorkerAsync();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
