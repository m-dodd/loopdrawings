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

        private void btnReadData_Click(object sender, EventArgs e)
        {

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
                    var data = excelLoader.GetTagData("LIT-7100");
                    txtDisplayConnection.Text = DictToString(data);
                    bool nothing = false;
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
    }
}