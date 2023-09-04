using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class RELAY_SERIES : BlockDataExcel
    {
        public string TagRelayA { get; }
        public string TagRelayB { get; }
        public RELAY_SERIES(
            ILogger logger,
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(logger, dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
            TagRelayA = GetTag(blockMap, tagMap, 0);
            TagRelayB = GetTag(blockMap, tagMap, 1);
        }

        protected override void FetchExcelData()
        {
            var dataRelayA = dataLoader.GetIOData(TagRelayA);
            var dataRelayB = dataLoader.GetIOData(TagRelayB);

            if (dataRelayA is not null)
            {

                Attributes["PNL_TS1-1"] = string.Empty;
                Attributes["CONT_TAG-1"] = dataRelayA.Relay.ContactTag;
                Attributes["CTERM1-1"] = dataRelayA.Relay.ContactTerm1;
                Attributes["CTERM1-2"] = dataRelayA.Relay.ContactTerm2;
                Attributes["PNL_TAG-1"] = dataRelayA.PanelTag;
                Attributes["PNL_TS1-2"] = dataRelayA.Relay.PanelTerminalStrip;
                Attributes["RELAY_TAG-1"] = dataRelayA.Relay.Tag;
                Attributes["RTERM1-1"] = dataRelayA.Relay.Term1;
                Attributes["RTERM1-2"] = dataRelayA.Relay.Term2;
                Attributes["BREAKER_NO-1"] = dataRelayA.BreakerNumber;

                Attributes["WIRE_TAG_PANEL1"] = dataRelayA.IO.WireTag1;
                Attributes["WIRE_TAG_PANEL2"] = dataRelayA.IO.WireTag2;
                Attributes["PNL_TS3"] = dataRelayA.PanelTerminalStrip;
                Attributes["TERM1"] = dataRelayA.IO.Terminal1;
            }

            if (dataRelayB is not null)
            {
                Attributes["PNL_TS2-1"] = string.Empty;
                Attributes["CONT_TAG-2"] = dataRelayB.Relay.ContactTag;
                Attributes["CTERM2-1"] = dataRelayB.Relay.ContactTerm1;
                Attributes["CTERM2-2"] = dataRelayB.Relay.ContactTerm2;
                Attributes["PNL_TAG-2"] = dataRelayB.PanelTag;
                Attributes["PNL_TS2-2"] = dataRelayB.Relay.PanelTerminalStrip;
                Attributes["RELAY_TAG-2"] = dataRelayB.Relay.Tag;
                Attributes["RTERM2-1"] = dataRelayB.Relay.Term1;
                Attributes["RTERM2-2"] = dataRelayB.Relay.Term2;
                Attributes["BREAKER_NO-2"] = dataRelayB.BreakerNumber;
            }
        }
    }
}
