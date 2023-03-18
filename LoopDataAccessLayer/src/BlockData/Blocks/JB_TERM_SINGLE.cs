using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class JB_TERM_SINGLE : BlockDataExcel
    {
        public JB_TERM_SINGLE(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
        }

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
                Attributes["COND_NO" + terminalString + "_L"] = t.LeftSide?.Core ?? string.Empty;
                Attributes["COND_NO" + terminalString + "_R"] = t.RightSide?.Core ?? string.Empty;

                if (i == 0)
                {
                    Attributes["JB_TAG-1"] = t.JBTag;
                    Attributes["JB_TS-1"] = t.TerminalStrip;
                }
            }
        }
    }
}
