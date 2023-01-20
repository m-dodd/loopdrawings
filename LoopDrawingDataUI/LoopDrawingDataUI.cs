using System.Text;
using LoopDataAccessLayer;
using LoopDataAdapterLayer;

namespace LoopDrawingDataUI
{
    public partial class frmLoopUI : Form
    {
        public frmLoopUI()
        {
            InitializeComponent();
        }


        private static string DictToString(Dictionary<string, string> dict)
        {
            return string.Join(System.Environment.NewLine, dict.Select(x => x.Key + ": " + x.Value?.ToString()));
        }

        private void GetData()
        {

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

            if (ExcelDataLoader.IsExcelFile(fileName))
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

        private void btnReadConfig_Click(object sender, EventArgs e)
        {
            LoopDataConfig config = new LoopDataConfig();
            config.LoadConfig(lblConfigFile.Text);
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
                    if (ExcelDataLoader.IsExcelFile(fileName))
                    {
                        return new ExcelDataLoader(fileName);
                    }
                }
              
                return null;
            }
        }
    }
}