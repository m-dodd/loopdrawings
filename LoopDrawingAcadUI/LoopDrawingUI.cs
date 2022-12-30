using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LoopDataAdapterLayer;

namespace LoopDrawingAcadUI
{
    public partial class LoopDrawingUI : Form
    {
        public LoopDrawingUI()
        {
            InitializeComponent();
        }

        private void btnPopulateAttributes_Click(object sender, EventArgs e)
        {
            AcadLoopDrawingTest acadTest = new AcadLoopDrawingTest();
            string DefaultPathName = @"Z:\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\";
            //acadTest.OpenTemplatePopulateBlock(DefaultPathName + @"testjson.json");
            acadTest.OpenTemplatePopulateBlock_New(DefaultPathName + @"testjson.json");
        }

        private void btnReadBlocks_Click(object sender, EventArgs e)
        {
            AcadLoopDrawingTest acadTest = new AcadLoopDrawingTest(lblDwgPath.Text);
            txtBlocks.Text = string.Empty;
            txtAttributes.Text = string.Empty;
            acadTest.OpenDrawingReadBlocks(txtBlocks, txtAttributes);
        }

        private void btnLoadAttributes_Click(object sender, EventArgs e)
        {
            // just to test that the load function is workign as well.
            // we won't need that until we do the autocad ui
            string DefaultPathName = @"Z:\Matalino Design\Projects\Duco Development\LoopDrawings\acadtesting\";

            LoopDataCollection loopdata = new LoopDataCollection();
            loopdata.Load(DefaultPathName + @"testjson.json");

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
    }
}
