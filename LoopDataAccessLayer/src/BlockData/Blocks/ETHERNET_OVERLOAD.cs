using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class ETHERNET_OVERLOAD : BlockDataExcel
    {
        public ETHERNET_OVERLOAD(
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
            var OverloadData = dataLoader.GetIOData(Tag)?.Overload;

            if (OverloadData is not null)
            {
                Attributes["DESCRIPTION_LINE1"] = OverloadData.Tag1; 
                Attributes["DESCRIPTION_LINE2"] = OverloadData.Tag2;
                Attributes["DEVICE_LINE1"] = OverloadData.Description1;
                Attributes["DEVICE_LINE2"] = OverloadData.Description2;
                Attributes["TERM1"] = OverloadData.PortNum;
            }
        }
    }
}
