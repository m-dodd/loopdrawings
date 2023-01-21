using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class AcadDrawingBuilder
    {
        
        private DataLoader dataLoader;
        private LoopDataConfig loopConfig;

        public AcadDrawingBuilder(DataLoader dataLoader, LoopDataConfig loopConfig)
        {
            // do I need to check that the dataloader was correctly instantiated?
            //  Yes, it can fail to load the excel or the db and if it does then I can't
            //  do any work. figure this out
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
        }

        public LoopDrawingData BuildDrawing(LoopNoTemplatePair loop)
        {
            // for a given LoopNoTemplatePair
            //      get all tags for the loop and map them to the respective config type
            var tags = GetLoopTagMap(loop);

            //      build all blocks
            //      create drawing data object
            //      add it to the list of drawings
            throw new NotImplementedException();
        }

        private object GetLoopTags(LoopNoTemplatePair loop)
        {
            // this function should go to the database and get all of the tags for a given loop
            // we will need a few different fields
            // not sure on the type of the return yet, as I'm not sure what happens when I query for
            // a set of tags for a given loop which will return multiple values
            throw new NotImplementedException();
        }

        private Dictionary<string, string> GetLoopTagMap(LoopNoTemplatePair loop)
        {
            var tags = GetLoopTags(loop);
            // what is the return type of this?
            //      a dictionary maybe?
            //      a loop tag getter object
            throw new NotImplementedException();
        }

        private List<BlockDataMappable> BuildBlocks(LoopNoTemplatePair loop)
        {
            List<BlockDataMappable> blocks = new();
            AcadBlockFactory blockFactory = new(dataLoader);
            foreach (BlockMapData blockMap in loopConfig.TemplateDefs[loop.Template].BlockMap)
            {
                //blocks.Add(blockFactory.GetBlock(blockMap));
            }

            return blocks;
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


    
}
