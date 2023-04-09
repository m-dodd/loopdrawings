using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class RELAY : BlockDataExcel
    {
        public RELAY(
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
            var data = dataLoader.GetIOData(Tag);

            if (data is not null)
            {
                Attributes["PNL_TAG"] = data.PanelTag;
                Attributes["BREAKER_NO"] = data.BreakerNumber;

                Attributes["PNL_TS-1"] = string.Empty;
                Attributes["CONT_TAG"] = data.Relay.ContactTag;
                Attributes["CTERM1"] = data.Relay.ContactTerm1;
                Attributes["CTERM2"] = data.Relay.ContactTerm2;

                Attributes["PNL_TS-2"] = data.Relay.PanelTerminalStrip;
                Attributes["RELAY_TAG"] = data.Relay.Tag;
                Attributes["RTERM1"] = data.Relay.Term1;
                Attributes["RTERM2"] = data.Relay.Term2;
            }
        }
    }
}
