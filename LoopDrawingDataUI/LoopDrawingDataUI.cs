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

        private TitleBlockData BuildTitleBlock()
        {
            //throw new NotImplementedException();
            return new TitleBlockData();
        }

        private void btnBuildObjects_Click(object sender, EventArgs e)
        {
            if(!FilesAndFoldersValid())
            {
                string invalidFilesMessage = "Please check configuration. One or more paths / filenames are invalid.";
                string invalidCaption = "Invalid Input";
                MessageBox.Show(invalidFilesMessage, invalidCaption, MessageBoxButtons.OK , MessageBoxIcon.Error);
                return;
            }

            ExcelDataLoader excelLoader = new(excelFileName);
            DBDataLoader dbLoader = new();
            DataLoader dataLoader = new(excelLoader, dbLoader, BuildTitleBlock());

            LoopDataConfig loopConfig = new(configFileName);
            loopConfig.LoadConfig();
            loopConfig.TemplateDrawingPath = templatePath;
            loopConfig.OutputDrawingPath = outputDrawingPath;
            loopConfig.SiteID = siteId;

            string jsonOutputFilename = Path.Combine(outputResultPath, "output_test_data.json");
            AcadDrawingController controller = new(dataLoader, loopConfig);
            controller.BuildDrawings();
            controller.SaveDrawingsToFile(jsonOutputFilename);
            MessageBox.Show("Drawings completed!");
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
            outputResultPath = GetFolderName();
            lblResultOutputPath.Text = GetShortPath(outputResultPath);
        }

        private void btnExcelFile_Click(object sender, EventArgs e)
        {
            excelFileName = GetFileName();
            lblExcelFile.Text = GetShortPath(excelFileName);
        }
    }
}