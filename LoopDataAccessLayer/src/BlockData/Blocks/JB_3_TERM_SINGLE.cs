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

        //protected override void FetchExcelData()
        //{
        //    var jbsData = dataLoader.GetJBData(Tag);
        //    if (jbsData is null || jbsData.Count == 0)
        //    {
        //        return;
        //    }

        //    ExcelJBData jbData = jbsData[0];
        //    if (jbData is null)
        //    {
        //        return;
        //    }

        //    IExcelJBRowData<string>? terminal1 = jbData.TerminalData.ElementAtOrDefault(0);
        //    IExcelJBRowData<string>? terminal2 = jbData.TerminalData.ElementAtOrDefault(1);
        //    IExcelJBRowData<string>? terminal3 = jbData.TerminalData.ElementAtOrDefault(2);

        //    if (terminal1 is null)
        //    {
        //        return;
        //    }

        //    Attributes["JB_TAG-1"] = terminal1.JBTag;
        //    Attributes["JB_TS-1"] = terminal1.TerminalStrip;
        //    Attributes["TB1-1"] = terminal1.Terminal;
        //    IExcelJBRowSide<string> left = terminal1.LeftSide;
        //    IExcelJBRowSide<string> right = terminal1.RightSide;
            
        //    if (left is not null)
        //    {
        //        Attributes["CLR1-L1"] = left.Color;
        //    }

        //    if (right is not null)
        //    {
        //        Attributes["CLR1-R1"] = right.Color;
        //    }

        //    if (terminal2 is not null)
        //    {
        //        Attributes["TB2-1"] = terminal2.Terminal;
        //        left = terminal2.LeftSide;
        //        right = terminal2.RightSide;

        //        if (left is not null)
        //        {
        //            Attributes["CLR2-L2"] = left.Color;
        //        }

        //        if (right is not null)
        //        {
        //            Attributes["CLR2-R2"] = right.Color;
        //        }
        //    }

        //    if (terminal3 is not null)
        //    {
        //        Attributes["TB3-1"] = terminal3.Terminal;
        //    }
        //}

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
