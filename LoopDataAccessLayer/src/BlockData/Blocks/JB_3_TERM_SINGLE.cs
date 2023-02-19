using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class JB_3_TERM_SINGLE : BlockDataExcel
    {
        public JB_3_TERM_SINGLE(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchExcelData()
        {
            var jbsData = dataLoader.GetJBData(Tag);
            if (jbsData is null || jbsData.Count == 0)
            {
                return;
            }

            ExcelJBData jbData = jbsData[0];
            if (jbData is null)
            {
                return;
            }

            for (int i = 0; i < jbData.TerminalData.Count; i++)
            {
                var t = jbData.TerminalData[i];
                if (t is null)
                {
                    continue;
                }
                string terminalString = (i + 1).ToString();
                Attributes["TB" + terminalString + "-1"] = t.Terminal;
                Attributes["CLR" + terminalString + "-L" + terminalString] = t.LeftSide?.Color ?? string.Empty;
                Attributes["CLR" + terminalString + "-R" + terminalString] = t.RightSide?.Color ?? string.Empty;

                if (i == 0)
                {
                    Attributes["JB_TAG-1"] = t.JBTag;
                    Attributes["JB_TS-1"] = t.TerminalStrip;
                }
            }
        }
    }
}
