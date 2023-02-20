using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class INST_AI_2W : BlockFieldDevice
    {
        public INST_AI_2W(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);

            Attributes["MANUFACTURER"] = data.Manufacturer;
            Attributes["MODEL"] = data.Model;
            Attributes["RANGE"] = data.Range;

            IEnumerable<string> descriptions = GetFourLineDescription(data.Description, 14);
            for (int i = 0; i < 4; i++)
            {
                Attributes["DESCRIPTION_LINE" + (i + 1).ToString()] = descriptions.ElementAt(i);
            }

            string[] tagComponents = Tag.Split('-');
            if (tagComponents.Length == 2)
            {
                Attributes["TAG1"] = tagComponents[0];
                Attributes["TAG2"] = tagComponents[1];
            }
        }

        protected override void FetchExcelData()
        {
            var IOData = dataLoader.GetIOData(Tag)?.Device;
            if (IOData is not null)
            {
                Attributes["TERM1"] = IOData.TerminalPlus;
                Attributes["TERM2"] = IOData.TerminalNeg;
                Attributes["CLR1"] = IOData.WireColorPlus;
                Attributes["CLR2"] = IOData.WireColorNeg;
                Attributes["PAIR_NO"] = IOData.CorePairPlus + "PR";
                Attributes["WIRE_TAG_FIELD"] = IOData.WireTagPlus;
                Attributes["CABLE_TAG_FIELD"] = IOData.CableTag;
            }
        }
    }
}
