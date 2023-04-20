using System.Drawing.Text;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using LoopDataAccessLayer;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace LoopDrawingDataUI
{
    public partial class frmLoopUI : Form
    {
        private string configFileName = string.Empty;
        private string excelFileName = string.Empty;
        private string templatePath = string.Empty;
        private string outputResultPath = string.Empty;
        private string outputDrawingPath = string.Empty;
        private string logFilePath = string.Empty;

        public frmLoopUI()
        {
            InitializeComponent();
        }

        private void btnBuildObjects_Click(object sender, EventArgs e)
        {
            if (!FilesAndFoldersValid())
            {
                string invalidFilesMessage = "Please check configuration. One or more paths / filenames are invalid.";
                string invalidCaption = "Invalid Input";
                MessageBox.Show(invalidFilesMessage, invalidCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(IsFileLocked(outputResultPath))
            {
                string invalidFilesMessage = "Output file appears to be open. Not going to run, as it would be pointless as we can't save if it is open. Close the file and try again.";
                string invalidCaption = "Output File Open";
                MessageBox.Show(invalidFilesMessage, invalidCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }

            try
            {
                // build the logger
                string timestamp = DateTime.Now.ToString("yyyy.MM.dd-HHmmss");
                string logFileName = $"log_{timestamp}.log";
                Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.File(new JsonFormatter(),
                                          Path.Combine(logFilePath, "important.json"), restrictedToMinimumLevel: LogEventLevel.Warning)
                            //.WriteTo.File(Path.Combine(logFilePath, "log.log"), rollingInterval: RollingInterval.Day)
                            .WriteTo.File(Path.Combine(logFilePath, logFileName))
                            .CreateLogger();
                
                // run the application
                Log.Debug($"Creating drawings - run started at {timestamp}...");
                AcadDrawingController controller = new(excelFileName, configFileName, templatePath, outputDrawingPath, Log.Logger);
                controller.BuildDrawings();
                controller.SaveDrawingsToFile(TestOutputFileName(outputResultPath));
                string resultMessage;
                if (controller.ErrorsDetected)
                {
                    resultMessage = "Drawings completed - WITH errors! Please check log file";
                }
                else
                {
                    resultMessage = "Drawings completed - without errors!";
                }
                Log.Information(resultMessage);
                MessageBox.Show(resultMessage, "Duco Loop Drawing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException ex)
            {
                Log.Error(ex.Message);
                MessageBox.Show(ex.Message, "Loop configuration file missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (IOException ex)
            {
                Log.Error(ex.Message);
                MessageBox.Show(ex.Message, "Close Excel File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private string TestOutputFileName(string outputResultPath)
        {
            return AppendDateToFileName(outputResultPath);
        }

        public string AppendDateToFileName(string filePath)
        {
            // Get the directory path, file name, and file extension
            string directory = Path.GetDirectoryName(filePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            // Append the current date to the file name
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string newFileName = $"{fileNameWithoutExtension}_{date}{extension}";

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
            lblLogFilePath.Text = string.Empty;
        }

        private void btnConfigFile_Click(object sender, EventArgs e)
        {
            configFileName = GetFileName();
            lblConfigFile.Text = GetShortPath(configFileName);
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
            //GetFolderSetLabel(lblResultOutputPath);
            outputResultPath = SetFileName();
            logFilePath = Path.Combine(Path.GetDirectoryName(outputResultPath), "logs");

            lblResultOutputPath.Text = GetShortPath(outputResultPath);
        }

        private void btnExcelFile_Click(object sender, EventArgs e)
        {
            excelFileName = GetFileName();
            lblExcelFile.Text = GetShortPath(excelFileName);
        }

        private void btnLoadTestConfig_Click(object sender, EventArgs e)
        {
            configFileName = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\code\DucoLoopDrawings\ConfigFiles\loop_drawing_config.json";
            excelFileName = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\config\2023.03.28 - Automation Wiring Data.xlsm";
            templatePath = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\templates";
            outputResultPath = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\output\output_test_data.json";
            outputDrawingPath = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\output";
            logFilePath = Path.Combine(Path.GetDirectoryName(outputResultPath), "logs");

            lblConfigFile.Text = GetShortPath(configFileName);
            lblExcelFile.Text = GetShortPath(excelFileName);
            lblTemplatePath.Text = GetShortPath(templatePath);
            lblDrawingOutputPath.Text = GetShortPath(outputDrawingPath);
            lblResultOutputPath.Text = GetShortPath(outputResultPath);
            lblLogFilePath.Text = GetShortPath(logFilePath);
        }

        private void btnLogPath_Click(object sender, EventArgs e)
        {
            logFilePath = GetFolderName();
            lblLogFilePath.Text = GetShortPath(logFilePath);
        }
    }
}