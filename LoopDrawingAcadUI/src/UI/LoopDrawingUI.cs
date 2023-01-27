using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LoopDrawingAcadUI
{
    public partial class LoopDrawingUI : Form
    {
        public LoopDrawingUI()
        {
            InitializeComponent();
        }
        
        private void LoopDrawingUI_Load(object sender, EventArgs e)
        {
            lblDwgPath.Text = string.Empty;
            lblOutputPath.Text = string.Empty;
            lblSourceFile.Text = string.Empty;
            lblTemplatePath.Text = string.Empty;
        }
        
        private void btnReadBlocks_Click(object sender, EventArgs e)
        {
            AcadLoopDrawingTest acadTest = new AcadLoopDrawingTest(lblDwgPath.Text);
            txtBlocks.Text = string.Empty;
            txtAttributes.Text = string.Empty;
            acadTest.OpenDrawingReadBlocks(txtBlocks, txtAttributes);
        }

        private void btnSelectDrawing_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    lblDwgPath.Text = openFileDialog1.FileName;
                }
                else
                {
                    lblDwgPath.Text = string.Empty;
                }
            }


        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fld = new FolderBrowserDialog())
            {
                if (fld.ShowDialog() == DialogResult.OK)
                {
                    lblOutputPath.Text = fld.SelectedPath;
                }
                else
                {
                    lblOutputPath.Text = string.Empty;
                }
            }

        }

        private void btnTemplatePath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fld = new FolderBrowserDialog())
            {
                if (fld.ShowDialog() == DialogResult.OK)
                {
                    lblTemplatePath.Text = fld.SelectedPath;
                }
                else
                {
                    lblTemplatePath.Text = string.Empty;
                }
            }
        }

        private void btnCreateDrawings_Click(object sender, EventArgs e)
        {
            string sourceFile = lblSourceFile.Text;
            AcadController controller = new AcadController();
            controller.LoadDrawingsFromFile(sourceFile);
            controller.BuildDrawings();
        }

        private void btnLoadLoopDrawingsSourceFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oFile = new OpenFileDialog())
            {
                if (oFile.ShowDialog() == DialogResult.OK)
                {
                    lblSourceFile.Text = oFile.FileName;
                }
                else
                {
                    lblSourceFile.Text = string.Empty;
                }
            }
        }
    }
}
