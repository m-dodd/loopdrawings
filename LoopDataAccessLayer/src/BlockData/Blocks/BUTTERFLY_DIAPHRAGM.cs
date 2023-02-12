using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class BUTTERFLY_DIAPHRAGM : BlockDataDB
    {
        public BUTTERFLY_DIAPHRAGM(IDataLoader dataLoader) : base(dataLoader) { }

        protected override void FetchDBData()
        {
            DBLoopData data = dataLoader.GetLoopData(Tag);
            string[] tagComponents = Tag.Split('-');
            if (tagComponents.Length == 2)
            {
                Attributes["TAG1"] = tagComponents[0];
                Attributes["TAG2"] = tagComponents[1];
            }
            // NOTE - Can't find SIZE right now so this is just going to be Fail position
            Attributes["SIZE/FAIL_POSITION"] = data.FailPosition;
        }
    }
}
