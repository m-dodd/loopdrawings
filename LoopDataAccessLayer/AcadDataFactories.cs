using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    // With this factory the drawing can just look at the block names defined in the template json config file
    // and instantiate the correct block
    // the block will have the correct map defined in it's version of the MapData() function
    // and finally the list of blocks can just look and call the MapData funcion of each block as built above
    //
    // I think this is beginning to come together
    public class BlockFactory
    {
        private readonly DBDataLoader dbLoader;
        private readonly ExcelDataLoader excelLoader;

        public BlockFactory(DBDataLoader dbLoader, ExcelDataLoader excelLoader)
        {
            this.dbLoader = dbLoader;
            this.excelLoader = excelLoader;
        }

        public BlockDataMappable GetBlock(string blockName, string Tag)
        {
            switch (blockName)
            {
                case "JB_3-TERM_SINGLE":
                    return new JB_3_TERM_SINGLE(this.excelLoader) { Name=blockName, Tag=Tag };
                case "PNL_3-TERM_24VDC":
                    return new PNL_3_TERM_24VDC(this.excelLoader) { Name=blockName, Tag=Tag };
                case "MOD_1-TERM":
                    return new MOD_1_TERM(this.dbLoader) { Name=blockName, Tag=Tag };
                case "INST_AI_2W":
                    return new INST_AI_2W(this.excelLoader, this.dbLoader) { Name=blockName, Tag=Tag };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
