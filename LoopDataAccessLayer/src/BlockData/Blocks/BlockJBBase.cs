using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Features;
using DocumentFormat.OpenXml.Math;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public abstract class BlockJBBase: BlockDataExcel
    {
        public BlockJBBase(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
        }

        protected virtual void PopulateAttributesForSingleJB(bool isAnalog)
        {
            var jbsData = dataLoader.GetJBData(Tag);
            if (jbsData is null || jbsData.Count == 0)
            {
                return;
            }

            PopulateJBAttributes(jbsData[0], isAnalog);
        }

        protected void PopulateAttributesForDualJB(bool isAnalog)
        {
            var jbsData = dataLoader.GetJBData(Tag);
            if (jbsData is null || jbsData.Count != 2)
            {
                return;
            }

            var ioData = dataLoader.GetIOData(Tag);
            if (ioData is not null)
            {
                var jb1 = jbsData.First(jb => jb.TerminalData[0].JBTag == ioData.JB1);
                PopulateJBAttributes(jb1, isAnalog, "1");

                var jb2 = jbsData.First(jb => jb.TerminalData[0].JBTag == ioData.JB2);
                PopulateJBAttributes(jb2, isAnalog, "2");

                var cableTag = jb1.TerminalData[0].RightSide.Cable;
                var cableData = dataLoader.GetCableData(cableTag);
                Attributes["CABLE_TAG_FIELD"] = cableTag;
                Attributes["CABLE_SIZE"] = cableData?.CableSizeType ?? string.Empty;
                Attributes["PAIR_NO"] = jb1.TerminalData[0].RightSide?.Core ?? string.Empty;
            }
        }

        private void PopulateJBAttributes(ExcelJBData jbData, bool isAnalogJB, string jbNum= "1")
        {
            Attributes[$"JB_TAG-{jbNum}"] = jbData.TerminalData[0].JBTag;
            Attributes[$"JB_TS-{jbNum}"] = jbData.TerminalData[0].TerminalStrip;
            for (int i = 0; i < jbData.TerminalData.Count; i++)
            {
                var terminal = jbData.TerminalData[i];
                if (terminal is null)
                {
                    continue;
                }
                string terminalNum = (i + 1).ToString();
                Attributes[$"TB{terminalNum}-{jbNum}"] = terminal.Terminal;
                Attributes[$"CLR_COND{terminalNum}-{jbNum}L"] = isAnalogJB ? terminal.LeftSide?.Color : terminal.LeftSide?.Core;
                Attributes[$"CLR_COND{terminalNum}-{jbNum}R"] = isAnalogJB ? terminal.RightSide?.Color : terminal.RightSide?.Core;
            }
        }
    }
}
