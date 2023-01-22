using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopDataAdapterLayer;

namespace LoopDataAccessLayer
{
    // With this factory the drawing can just look at the block names defined in the template json config file
    // and instantiate the correct block
    // the block will have the correct map defined in it's version of the MapData() function
    // and finally the list of blocks can just look and call the MapData funcion of each block as built above
    //
    // I think this is beginning to come together
    public class AcadBlockFactory
    {
        //private readonly DBDataLoader dbLoader;
        //private readonly ExcelDataLoader excelLoader;
        private readonly DataLoader dataLoader;

        public AcadBlockFactory(DataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public AcadBlockFactory(DBDataLoader dbLoader, ExcelDataLoader excelLoader)
        {
            this.dataLoader = new(excelLoader, dbLoader);
        }

        public BlockDataMappable GetBlock(BlockMapData blockMap, Dictionary<string, string> tagMap )
        {
            switch (blockMap.Name)
            {
                case "JB_3-TERM_SINGLE":
                    return new JB_3_TERM_SINGLE(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "PNL_3-TERM_24VDC":
                    return new PNL_3_TERM_24VDC(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "PNL_3-TERM":
                    return new PNL_3_TERM(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "MOD_1-TERM":
                    return new MOD_1_TERM(this.dataLoader)
                    {
                        Name = blockMap.Name,
                        Tag = tagMap[blockMap.Tags[0]],
                        ControllerTag = tagMap[blockMap.Tags[1]],
                    };
                
                case "MOD_2-TERM":
                    return new MOD_2_TERM(this.dataLoader)
                    {
                        Name = blockMap.Name,
                        Tag = tagMap[blockMap.Tags[0]],
                        ControllerTag = tagMap[blockMap.Tags[1]],
                    };
                
                case "INST_AI_2W":
                    return new INST_AI_2W(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "INST_AO_2W":
                    return new INST_AO_2W(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };

                case "BUTTERFLY_DIAPHRAGM":
                    return new EMPTY_BLOCK(this.dataLoader);

                case "STD B SIZE SHEET":
                    return new EMPTY_BLOCK(this.dataLoader);

                default:
                    return new EMPTY_BLOCK(this.dataLoader);
            }
        }
    }
}
