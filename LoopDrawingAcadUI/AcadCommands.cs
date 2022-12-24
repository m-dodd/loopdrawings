using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDrawingAcadUI
{
    public class AcadCommands
    {
        [CommandMethod("DucoLoop")]
        public void ShowAcadForm()
        {
            LoopDrawingUI startupForm = new LoopDrawingUI();
            startupForm.Show();
        }
    }
}
