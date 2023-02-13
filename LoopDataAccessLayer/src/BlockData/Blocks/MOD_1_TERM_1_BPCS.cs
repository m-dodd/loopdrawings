using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class MOD_1_TERM_1_BPCS : BlockDataExcelDB
    {
        public string ControllerTag { get; set; } = string.Empty;

        public MOD_1_TERM_1_BPCS(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);

            Attributes["RACK"] = data.Rack;
            Attributes["SLOT"] = data.Slot;
            Attributes["CHANNEL"] = data.Channel;


            string[] tagComponents = ControllerTag.Split('-');
            if (tagComponents.Length == 2)
            {
                Attributes["FUNCTIONAL_ID"] = tagComponents[0];
                Attributes["LOOP_NO"] = tagComponents[1];
            }

            Attributes["ALARM1"] = data.HiHiAlarm;
            Attributes["ALARM2"] = data.HiAlarm;
            Attributes["ALARM3"] = data.LoAlarm;
            Attributes["ALARM4"] = data.LoLoAlarm;
            Attributes["ALARM5"] = data.HiControl;
            Attributes["ALARM6"] = data.LoControl;

            Attributes["DRAWING_NO"] = data.PidDrawingNumber;
        }

        protected override void FetchExcelData()
        {
            var IOData = dataLoader.GetIOData(Tag);
            if (IOData is not null)
            {
                Attributes["MOD_TERM"] = IOData.ModuleTerm01;
                Attributes["WIRE_TAG_IO"] = IOData.ModuleWireTag01;
            }
        }
    }
}
