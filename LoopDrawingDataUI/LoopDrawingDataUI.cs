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

        private void btnGetData_Click(object sender, EventArgs e)
        {
            DBDataLoader dataLoader = new();

            // ok, so we are only interested in two tags at the moment
            // LIT-7100
            // LIT-1910
            StringBuilder sb = new();
            string[] tags = { "LIT-7100", "LIT-1910" };
            foreach (string tag in tags)
            {
                sb.Append(dataLoader?.GetLoopData(tag)?.ToString() + System.Environment.NewLine + System.Environment.NewLine);
            }
            txtDisplayConnection.Text = sb.ToString();
        }


        private void btnReadExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (openFileDialog1.ShowDialog() == DialogResult.OK) // Test result.
            {
                string fileName = openFileDialog1.FileName;
                if (IsExcelFile(fileName))
                {
                    ExcelDataLoader excelLoader = new(fileName);
                    var data = excelLoader.GetLoopData("LIT-7100");
                    txtDisplayConnection.Text = DictToString(data);
                }
            }
        }

        private static bool IsExcelFile(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string[] validExtensions = { ".xlsx", ".xlsm" };
            foreach (string ext in validExtensions)
            {
                if (extension.ToLower() == ext) return true;
            }
            return false;
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

            if (IsExcelFile(fileName))
            {
                excelLoader = new(fileName);
            }
            else
            {
                return;
            }

            dbLoader = new();
            

            DataLoader loader = new(dbLoader, excelLoader);

            // ok, so we are only interested in two tags at the moment
            // LIT-7100
            // LIT-1910
            string[] tags = { "LIT-7100", "LIT-1910" };
            loader.FetchLoopsData(tags);
            txtDisplayConnection.Text = loader.ToString();
            string DefaultPathName = @"Z:\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\";
            loader.Data.Save(DefaultPathName + @"testjson.json");

            // just to test that the load function is workign as well.
            // we won't need that until we do the autocad ui
            DataLoader loaderTestLoad = new(dbLoader, excelLoader);
            loaderTestLoad.Data.Load(DefaultPathName + @"testjson.json");
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
                    lblConfigFile.Text = String.Empty;
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
            BlockFactory blockFactory = new BlockFactory(dbLoader, excelLoader);
            BlockDataMappable someBlock = blockFactory.GetBlock("JB_3_TERM_SINGLE", "LIT-7100");
            someBlock.MapData();
        }

        private ExcelDataLoader? GetExcelLoader()
        {
            using (OpenFileDialog openFileDialog1 = new())
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog1.FileName;
                    if (IsExcelFile(fileName)) { return new ExcelDataLoader(fileName); }
                }
              
                return null;
            }
        }
    }
}