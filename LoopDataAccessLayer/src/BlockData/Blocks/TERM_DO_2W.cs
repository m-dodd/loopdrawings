using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class TERM_DO_2W : INST_DO_2W
    {

        public TERM_DO_2W(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader, blockMap, tagMap)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
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
