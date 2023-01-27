using System.Text;
using LoopDataAccessLayer;


namespace LoopDrawingDataUI
{
    public partial class frmLoopUI : Form
    {
        public frmLoopUI()
        {
            InitializeComponent();
        }


        
        private void btnReadTagData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new();
            ExcelDataLoader excelLoader;
            DBDataLoader dbLoader;
            string fileName;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
            }
            else
            {
                return;
            }

            if (ExcelHelper.IsExcelFile(fileName))
            {
                excelLoader = new(fileName);
            }
            else
            {
                return;
            }

            dbLoader = new();
            

            // DataLoader loader = new(dbLoader, excelLoader);

            // ok, so we are only interested in two tags at the moment
            // LIT-7100
            // LIT-1910
            string[] tags = { "LIT-7100", "LIT-1910" };
            // loader.FetchLoopsData(tags);
            // txtDisplayConnection.Text = loader.ToString();
            string DefaultPathName = @"Z:\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\";
            // loader.Data.Save(DefaultPathName + @"testjson.json");

            // just to test that the load function is workign as well.
            // we won't need that until we do the autocad ui
            // DataLoader loaderTestLoad = new(dbLoader, excelLoader);
            // loaderTestLoad.Data.Load(DefaultPathName + @"testjson.json");
        }

        private void btnConfigFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oFile = new OpenFileDialog())
            {
                if (oFile.ShowDialog() == DialogResult.OK)
                {
                    lblConfigFile.Text = oFile.FileName;
                }
                else
                {
                    lblConfigFile.Text = string.Empty;
                }
            }
        }

        private void btnReadDataClasses_Click(object sender, EventArgs e)
        {
            
            ExcelDataLoader? excelLoader = GetExcelLoader();
            if (excelLoader == null)
            {
                return;
            }
            DBDataLoader dbLoader = new();

            // BlockFactory blockFactory = new BlockFactory(dbLoader, excelLoader);
            // BlockDataMappable someBlock = blockFactory.GetBlock("JB_3-TERM_SINGLE", "LIT-7100");
            // someBlock.MapData();

        }

        private ExcelDataLoader? GetExcelLoader()
        {
            using (OpenFileDialog openFileDialog1 = new())
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog1.FileName;
                    if (ExcelHelper.IsExcelFile(fileName))
                    {
                        return new ExcelDataLoader(fileName);
                    }
                }
              
                return null;
            }
        }

        private TitleBlockData BuildTitleBlock()
        {
            //throw new NotImplementedException();
            return new TitleBlockData();
        }
        private void btnBuildObjects_Click(object sender, EventArgs e)
        {
            ExcelDataLoader? excelLoader = GetExcelLoader();
            if (excelLoader == null)
            {
                return;
            }
            DBDataLoader dbLoader = new();
            DataLoader dataLoader = new(excelLoader, dbLoader);
            string configFileName = lblConfigFile.Text;
            string outputPath = lblDrawingOutputPath.Text;
            string templatePath = lblTemplatePath.Text;
            string siteId = lblSiteID.Text;

            if (!string.IsNullOrEmpty(configFileName) 
                && !string.IsNullOrEmpty(outputPath) 
                && !string.IsNullOrEmpty(templatePath))
            {
                LoopDataConfig loopConfig = new(configFileName);
                loopConfig.LoadConfig();
                loopConfig.TemplateDrawingPath = templatePath;
                loopConfig.OutputDrawingPath = outputPath;
                loopConfig.SiteID = siteId;

                string jsonOutputFilename = Path.Combine(outputPath, "output_test_data.json");
                AcadDrawingController controller = new(dataLoader, loopConfig, BuildTitleBlock());
                controller.BuildDrawings();
                controller.SaveDrawingsToFile(jsonOutputFilename);
            }
        }

        private void frmLoopUI_Load(object sender, EventArgs e)
        {
            lblConfigFile.Text = string.Empty;
            lblDrawingOutputPath.Text = string.Empty;
            lblTemplatePath.Text = string.Empty;
        }


        private void GetFolderSetLabel(Label label)
        {
            using (FolderBrowserDialog fld = new())
            {
                if (fld.ShowDialog() == DialogResult.OK)
                {
                    label.Text = fld.SelectedPath;
                }
                else
                {
                    label.Text = string.Empty;
                }
            }
        }

        private void btnTemplatePath_Click(object sender, EventArgs e)
        {
            GetFolderSetLabel(lblTemplatePath);
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            GetFolderSetLabel(lblDrawingOutputPath);
        }
    }
}