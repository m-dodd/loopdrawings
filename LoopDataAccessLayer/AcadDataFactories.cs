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

        public BlockDataMappable GetBlock(KeyValuePair<string, string> blockConfig)
        {
            string blockName = blockConfig.Key;
            // this actually doesn't work, as the blockConfig.Value is a generic mapping
            // need logic somewhere to convert this to a specifc tag
            // probably need to have this as part of the looptemplate logic
            // this is a bandaid as I move things around
            string Tag = blockConfig.Value;
            switch (blockName)
            {
                case "JB_3-TERM_SINGLE":
                    return new JB_3_TERM_SINGLE(this.excelLoader) { Name=blockName, Tag=Tag };
                case "PNL_3-TERM_24VDC":
                    return new PNL_3_TERM_24VDC(this.excelLoader) { Name=blockName, Tag=Tag };
                case "PNL_3-TERM":
                    return new PNL_3_TERM(this.excelLoader) { Name=blockName, Tag=Tag };
                case "MOD_1-TERM":
                    return new MOD_1_TERM(this.excelLoader, this.dbLoader) { Name=blockName, Tag=Tag };
                case "MOD_2-TERM":
                    return new MOD_2_TERM(this.excelLoader, this.dbLoader) { Name=blockName, Tag=Tag };
                case "INST_AI_2W":
                    return new INST_AI_2W(this.excelLoader, this.dbLoader) { Name=blockName, Tag=Tag };
                default:
                    return new EMPTY_BLOCK(this.excelLoader);
            }
        }
    }

    public class LoopDrawingsBuilder
    {
        public List<LoopDrawingData> Drawings { get; set; } = new List<LoopDrawingData>();
        private LoopDrawingDataFactory _drawingFactory;
        private LoopDataConfig _config;

        public LoopDrawingsBuilder(string configFileName)
        {
            _config = new LoopDataConfig();
            _config.LoadConfig(configFileName);
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }

        public void ToJson(string fileName)
        {
            throw new NotImplementedException();
        }

        public List<LoopDrawingData> FromJson()
        {
            throw new NotImplementedException();
        }
    }


    public class LoopDrawingDataFactory
    {
        private readonly DBDataLoader dbLoader;
        private readonly ExcelDataLoader excelLoader;
        private readonly BlockFactory _blockFactory;

        public LoopDrawingDataFactory(DBDataLoader dbLoader, ExcelDataLoader excelLoader)
        {
            this.dbLoader = dbLoader;
            this.excelLoader = excelLoader;
            _blockFactory = new BlockFactory(this.dbLoader, this.excelLoader);
        }

        public LoopDrawingData GetLoop(string TemplateID, string Tag)
        {
            throw new NotImplementedException();
        }

        private void GetAllBlocks(TemplateConfig template)
        {
            foreach(KeyValuePair<string, string> b in template.BlockMap)
            {
                _blockFactory.GetBlock(b);
            }
        }
    }
}
