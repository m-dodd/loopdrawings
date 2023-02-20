using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class VALVE_BODY : BlockValve
    {
        public VALVE_BODY(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);
            string[] tagComponents = Tag.Split('-');
            if (tagComponents.Length == 2)
            {
                Attributes["TAG1"] = tagComponents[0];
                Attributes["TAG2"] = tagComponents[1];
            }
            
            Attributes["SIZE/FAIL_POSITION"] = data.FailPosition;
            Attributes["VALVE_TYPE"] = GetValveType(data.InstrumentType);
        }
    }
}
