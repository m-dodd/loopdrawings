using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class INST_AI_2W : BlockDataExcelDB
    {
        public INST_AI_2W(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);

            Attributes["MANUFACTURER"] = data.Manufacturer;
            Attributes["MODEL"] = data.Model;

            if (data.MinCalRange != DBLoopData.CALERROR.ToString() && data.MaxCalRange != DBLoopData.CALERROR.ToString())
            {
                Attributes["RANGE"] = data.MinCalRange + "-" + data.MaxCalRange;
            }
            else
            {
                // this entire else statement needs to be removed, but for testing I want to see a value in that attribute
                Attributes["RANGE"] = "ERROR";
            }

            // TODO: Break the description up into multiple lines
            Attributes["DESCRIPTION_LINE1"] = data.Description;
            Attributes["DESCRIPTION_LINE2"] = string.Empty;
            Attributes["DESCRIPTION_LINE3"] = string.Empty;
            Attributes["DESCRIPTION_LINE4"] = string.Empty;

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
