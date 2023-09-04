using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class TERM_DO_2W : INST_DO_2W
    {

        public TERM_DO_2W(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader, blockMap, tagMap)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = GetTag(blockMap, tagMap, 0);
        }

        protected override void FetchDBData()
        {
        }

        protected override void FetchExcelData()
        {
            base.FetchExcelData();

            var IOData = dataLoader.GetIOData(Tag);
            if (IOData is not null)
            {
                Attributes["DESCRIPTION_LINE1"] = IOData.Tag;
                Attributes["PNL_TAG"] = IOData.Device.PanelTag;
                Attributes["PNL_TS"] = IOData.Device.PanelTerminalStrip;
            }
        }
    }
}
