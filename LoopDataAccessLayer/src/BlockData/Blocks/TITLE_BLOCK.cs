using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class TITLE_BLOCK : BlockDataMappable
    {
        public TITLE_BLOCK(IDataLoader dataLoader) : base(dataLoader)
        {
        }

        public override void MapData()
        {
            MapTitleBlockData();
        }

        private void MapTitleBlockData()
        {
            if (dataLoader.TitleBlock is not null)
            {
                // populate attributes from titleblockdata
                //Attributes[""] = TitleBlockData.DrawingName;
            }

            throw new NotImplementedException();
        }
    }
}
