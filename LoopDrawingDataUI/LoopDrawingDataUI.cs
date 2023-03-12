using System.Drawing.Text;
using System.Text;
using DocumentFormat.OpenXml;
using LoopDataAccessLayer;
using Microsoft.VisualBasic;

namespace LoopDrawingDataUI
{
    public partial class frmLoopUI : Form
    {
        private string configFileName = string.Empty;
        private string excelFileName = string.Empty;
        private string templatePath = string.Empty;
        private string outputResultPath = string.Empty;
        private string outputDrawingPath = string.Empty;
        private string siteId;

        public frmLoopUI()
        {
            InitializeComponent();
            siteId = lblSiteID.Text;
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
            // figure out the rigth exception when the excel is open
            ExcelDataLoader excelLoader;
            try
            {
                excelLoader = new(excelFileName);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Excel must be open - please close it.", "Close Excel File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DBDataLoader dbLoader = new();
            DataLoader dataLoader = new(excelLoader, dbLoader);

            LoopDataConfig loopConfig = new(configFileName);
            loopConfig.LoadConfig();
            loopConfig.TemplateDrawingPath = templatePath;
            loopConfig.OutputDrawingPath = outputDrawingPath;
            loopConfig.SiteID = siteId;

            AcadDrawingController controller = new(dataLoader, loopConfig);
            controller.BuildDrawings();
            controller.SaveDrawingsToFile(TestOutputFileName(outputResultPath));
            MessageBox.Show("Drawings completed!", "Duco Loop Drawing", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string TestOutputFileName(string outputResultPath)
        {
            // wanted to create a better file name, but complexity not worht it right now
            // want to basically add a date to the end, and then look at directory to see if it exists
            // and if it does then add a number behind it - but hte number would have to increment
            // so what's the real value?
            //string date = DateTime.Now.ToString("YYYY.MM.DD");
            //return Path.Combine(outputResultPath, "output_test_data.json");
            return outputResultPath;
        }

        private void frmLoopUI_Load(object sender, EventArgs e)
        {
            lblConfigFile.Text = string.Empty;
            lblDrawingOutputPath.Text = string.Empty;
            lblTemplatePath.Text = string.Empty;
            lblResultOutputPath.Text = string.Empty;
            lblExcelFile.Text = string.Empty;
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
            lblResultOutputPath.Text = GetShortPath(outputResultPath);
        }

        private void btnExcelFile_Click(object sender, EventArgs e)
        {
            excelFileName = GetFileName();
            lblExcelFile.Text = GetShortPath(excelFileName);
        }

        private void btnLoadTestConfig_Click(object sender, EventArgs e)
        {
            configFileName = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\config\loop_drawing_config_v4.json";
            excelFileName = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\config\2023.02.02 - Automation Wiring Data (5).xlsm";
            templatePath = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\templates";
            outputResultPath = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\output\output_test_data.json";
            outputDrawingPath = @"\\vmware-host\Shared Folders\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\Working\output";

            lblConfigFile.Text = GetShortPath(configFileName);
            lblExcelFile.Text = GetShortPath(excelFileName);
            lblTemplatePath.Text = GetShortPath(templatePath);
            lblDrawingOutputPath.Text = GetShortPath(outputDrawingPath);
            lblResultOutputPath.Text = GetShortPath(outputResultPath);
        }
    }
}