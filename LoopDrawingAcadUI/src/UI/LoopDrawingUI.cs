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
            lblSourceFile.Text = string.Empty;
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
