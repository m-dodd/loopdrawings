﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class PNL_3_or_4_TERM_24VDC : BlockDataExcel
    {
        public PNL_3_or_4_TERM_24VDC(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = GetTag(blockMap, tagMap, 0);
        }

        protected override void FetchExcelData()
        {
            IExcelIOData<string>? IOData = dataLoader.GetIOData(Tag);
            if (IOData is not null)
            {
                var cableData = dataLoader.GetCableData(IOData.IO.CableTag);

                Attributes["PNL_TAG"] = IOData.PanelTag;
                Attributes["PNL_TS"] = IOData.PanelTerminalStrip;
                Attributes["TB1"] = IOData.IO.Terminal1;
                Attributes["TB2"] = IOData.IO.Terminal2;
                Attributes["TB3"] = IOData.IO.Terminal3;
                Attributes["TB4"] = IOData.IO.Terminal4;
                Attributes["CLR1"] = IOData.IO.WireColor1;
                Attributes["CLR2"] = IOData.IO.WireColor2;
                Attributes["CLR3"] = IOData.IO.WireColor3;
                Attributes["PAIR_NO"] = IOData.IO.CorePair1;
                Attributes["TRI_NO"] = IOData.IO.CorePair1;
                Attributes["WIRE_TAG_PANEL"] = IOData.IO.WireTag1;
                Attributes["CABLE_TAG_PANEL"] = IOData.IO.CableTag;
                Attributes["CABLE_SIZE"] = cableData?.CableSizeType ?? string.Empty;
                Attributes["BREAKER_NO"] = IOData.BreakerNumber;

                Attributes["WIRE_TAG_PANEL1"] = IOData.IO.WireTag1;
                Attributes["WIRE_TAG_PANEL2"] = IOData.IO.WireTag2;
                Attributes["COND_NO1"] = IOData.IO.CorePair1;
                Attributes["COND_NO2"] = IOData.IO.CorePair2;
            }
        }
    }
}
