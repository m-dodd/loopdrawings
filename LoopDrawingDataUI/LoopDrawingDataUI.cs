using System.Drawing.Text;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Threading.Tasks;
using System.Threading;

using LoopDataAccessLayer;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System.Diagnostics;

namespace LoopDrawingDataUI
{
    public partial class frmLoopUI : Form
    {
        private string configDirectoryName = string.Empty;
        private string excelFileName = string.Empty;
        private string templatePath = string.Empty;
        private string outputResultPath = string.Empty;
        private string outputDrawingPath = string.Empty;
        private string logFilePath = string.Empty;

        public frmLoopUI()
        {
            InitializeComponent();
        }

        private async void btnBuildObjects_Click(object sender, EventArgs e)
        {
            UpdateStatusLabel("Validating data sources..."); 

            (string timestamp, string resultFile) = InitializeAndGetTimestampAndResultFile();
            
            if (!AreFilesAndFoldersValid()) return;
            if (IsOutputFileLocked(resultFile)) return;

            InitializeLogger(timestamp);

            UpdateStatusLabel("Excuting...");
            await RunApplication(resultFile, timestamp);
        }

        private void UpdateStatusLabel(string message)
        {
            if (lblStatusInfo.InvokeRequired)
            {
                // If we're not on the UI thread, use Invoke to execute the update on the UI thread.
                Invoke((Action)(() => { lblStatusInfo.Text = message; }));
            }
            else
            {
                // We're already on the UI thread, so we can directly update the label.
                lblStatusInfo.Text = message;
            }
        }

        private (string Timestamp, string ResultFile) InitializeAndGetTimestampAndResultFile()
        {
            progressDrawingsComplete.Value = 0;
            lblLastDrawingComplete.Text = string.Empty;

            string timestamp = DateTime.Now.ToString("yyyy.MM.dd-HHmmss");
            string resultFile = CreateOutputFileName(outputResultPath, timestamp);

            return (Timestamp: timestamp, ResultFile: resultFile);
        }

        private bool AreFilesAndFoldersValid()
        {
            if (!FilesAndFoldersValid())
            {
                ShowErrorMessage("Invalid Input", "Please check configuration. One or more paths / filenames are invalid.");
                return false;
            }
            return true;
        }

        private bool IsOutputFileLocked(string resultFile)
        {
            if (IsFileLocked(resultFile))
            {
                ShowErrorMessage("Output File Open", "Output file appears to be open. Close the file and try again.");
                return true;
            }
            return false;
        }

        private static void ShowErrorMessage(string caption, string message)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void InitializeLogger(string timestamp)
        {
            
            string logFileName = $"log_{timestamp}.log";
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                // is it important though? I don't think so...
                //.WriteTo.File(new JsonFormatter(),
                //    Path.Combine(logFilePath, "important.json"), restrictedToMinimumLevel: LogEventLevel.Warning)
                .WriteTo.File(Path.Combine(logFilePath, logFileName))
                .CreateLogger();
        }

        private async Task RunApplication(string resultFile, string timestamp)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;
            progressDrawingsComplete.Visible = true;

            try
            {
                Log.Debug($"Creating drawings - run started at {timestamp}...");
                lblStatusInfo.Text = "Opening Excel and connecting to the database...";
                AcadDrawingController controller = new(excelFileName, configDirectoryName, templatePath, outputDrawingPath, Log.Logger);

                // Use Task.Run to run the operation on a background thread
                await Task.Run( () => controller.BuildDrawings(progress) );
                
                controller.SaveDrawingsToFile(resultFile);

                stopwatch.Stop();
                float elapsedSeconds = stopwatch.ElapsedMilliseconds / 1000f;
                string resultMessage = controller.ErrorsDetected
                    ? $"Drawings completed ({elapsedSeconds:F1}s) - WITH errors! Please check the log file."
                    : $"Drawings completed ({elapsedSeconds:F1}s)- without errors!";
                Log.Information(resultMessage);

                // Marshal UI update back to the main thread
                Invoke((Action)(() =>
                {
                    MessageBox.Show(resultMessage, "Duco Loop Drawing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
                //MessageBox.Show(resultMessage, "Duco Loop Drawing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (LoopDataException ex)
            {
                HandleException(ex, "Error reading loop configuration files.");
            }
            catch (IOException ex)
            {
                HandleException(ex, "Close Excel File");
            }
            catch (ExcelColumnNotFoundException ex)
            {
                HandleException(ex, "Excel file missing column(s).");
            }
        }

        private void HandleException(Exception ex, string errorMessage)
        {
            InitStatusDisplays();
            Log.Error(ex.Message + errorMessage);
            MessageBox.Show(ex.Message, errorMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ReportProgress(object? sender, ProgressReportModel e)
        {
            // Marshal UI update back to the main thread
            Invoke((Action)(() =>
            {
                progressDrawingsComplete.Value = e.PercentageComplete;
                lblLastDrawingComplete.Text = e.LoopsComplete.Last();
                string errorMsg = e.ErrorsFound ? "Errors detected - please check log file." : "No errors detected.";
                string status = $"Processing {e.NumberOfCurrentLoop} of {e.NumberOfLoops} total. {errorMsg}";
                lblStatusInfo.Text = status;
            }));
        }

        private void InitStatusDisplays()
        {
            progressDrawingsComplete.Value = 0;
            progressDrawingsComplete.Visible = false;
            lblLastDrawingComplete.Text = string.Empty;
            lblStatusInfo.Text = string.Empty;
        }

        private string CreateOutputFileName(string outputResultPath, string timestamp)
        {
            return AppendDateToFileName(outputResultPath, timestamp);
        }

        public string AppendDateToFileName(string filePath, string timestamp)
        {
            // Get the directory path, file name, and file extension
            string directory = Path.GetDirectoryName(filePath) ?? string.Empty;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            // Append the current date to the file name
            //string date = DateTime.Now.ToString("yyyy-MM-dd");
            string newFileName = $"{fileNameWithoutExtension}_{timestamp}{extension}";

            // Combine the directory path and the new file name
            string newFilePath = Path.Combine(directory, newFileName);
            return newFilePath;
        }

        private void frmLoopUI_Load(object sender, EventArgs e)
        {
            lblConfigFile.Text = string.Empty;
            lblDrawingOutputPath.Text = string.Empty;
            lblTemplatePath.Text = string.Empty;
            lblResultOutputPath.Text = string.Empty;
            lblExcelFile.Text = string.Empty;
            InitStatusDisplays();
        }

        private void btnConfigFile_Click(object sender, EventArgs e)
        {
            configDirectoryName = GetFolderName();
            lblConfigFile.Text = GetShortPath(configDirectoryName);
        }

        private void btnTemplatePath_Click(object sender, EventArgs e)
        {
            templatePath = GetFolderName();
            lblTemplatePath.Text = GetShortPath(templatePath);
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            outputDrawingPath = GetFolderName();
            lblDrawingOutputPath.Text = GetShortPath(outputDrawingPath);
        }

        private void btnResultOutputPath_Click(object sender, EventArgs e)
        {
            outputResultPath = SetFileName();
            if (outputResultPath != null)
            {
                logFilePath = Path.Combine(Path.GetDirectoryName(outputResultPath) ?? "", "logs");
                lblResultOutputPath.Text = GetShortPath(outputResultPath);
            }
        }


        private void btnExcelFile_Click(object sender, EventArgs e)
        {
            excelFileName = GetFileName();
            lblExcelFile.Text = GetShortPath(excelFileName);
        }

        private void btnLoadTestConfig_Click(object sender, EventArgs e)
        {
            configDirectoryName = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\code\DucoLoopDrawings\ConfigFiles\";
            //configFileName = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\code\DucoLoopDrawings\ConfigFiles\loop_drawing_config.json";
            excelFileName = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\config\2023.06.14 - Automation Wiring Data.xlsm";
            templatePath = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\templates";
            outputResultPath = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\output\output_test_data.json";
            outputDrawingPath = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\output\drawings";
            logFilePath = Path.Combine(Path.GetDirectoryName(outputResultPath) ?? "", "logs");

            lblConfigFile.Text = GetShortPath(configDirectoryName);
            lblExcelFile.Text = GetShortPath(excelFileName);
            lblTemplatePath.Text = GetShortPath(templatePath);
            lblDrawingOutputPath.Text = GetShortPath(outputDrawingPath);
            lblResultOutputPath.Text = GetShortPath(outputResultPath);
            //lblLogFilePath.Text = GetShortPath(logFilePath);
        }

    }
}