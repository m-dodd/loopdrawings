using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer.src.BlockData.Blocks
{
    public class ESD_STRING : BlockDataExcelDB
    {
        public ESD_STRING(ILogger logger, IDataLoader dataLoader, BlockMapData blockMap, Dictionary<string, string> tagMap) : base(logger, dataLoader)
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = GetTag(blockMap, tagMap, 0);
        }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopTagData(Tag);
            // control the visibility of the SignalLevel display
            Attributes["Visibility1"] = data.SignalLevel;
        }

        protected override void FetchExcelData()
        {
            IExcelIOData<string>? IOData = dataLoader.GetIOData(Tag);
            if (IOData is not null)
            {
                Attributes["BREAKER_NO"] = IOData.BreakerNumber;
                Attributes["DRAWING_NO"] = IOData.ESDDrawing;
            }
        }
    }
}
