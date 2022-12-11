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

        private void btnPopulateAttributes_Click(object sender, EventArgs e)
        {
            AcadLoopDrawingTest acadTest = new AcadLoopDrawingTest();
            acadTest.OpenTemplatePopulateBlock();
        }

        private void btnReadBlocks_Click(object sender, EventArgs e)
        {
            AcadLoopDrawingTest acadTest = new AcadLoopDrawingTest();
            txtBlocks.Text = "";
            acadTest.OpenDrawingReadBlocks(txtBlocks, txtAttributes);
        }
    }
}
