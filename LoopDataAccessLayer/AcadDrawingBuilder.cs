using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class LoopNoTemplatePair
    {
        public string LoopNo { get; set; } = string.Empty;
        public string Template { get; set; } = string.Empty;
    }

    public class AcadDrawingBuilder
    {
        public List<LoopDrawingData> Drawings { get; set; }
        //private AcadDrawingDataFactory _drawingFactory;
        private LoopDataConfig config;
        private DataLoader dataLoader;

        public AcadDrawingBuilder(DataLoader dataLoader, string configFileName)
        {
            Drawings = new();

            // do I need to check that the dataloader was correctly instantiated?
            //  Yes, it can fail to load the excel or the db and if it does then I can't
            //  do any work. figure this out
            this.dataLoader = dataLoader;

            // probably need a graceful excel if no config file found or not the right format
            // handle these events
            config = new(configFileName);
            config.LoadConfig();
        }

        public void GetLoops()
        {
            // read from database and get the loop / loop template pair objects
            throw new NotImplementedException();
        }

        public void GetLoopTags(LoopNoTemplatePair loop)
        {
            // what is the return type of this?
            //      a dictionary maybe?
            //      a loop tag getter object
            throw new NotImplementedException();
        }

        public List<BlockDataMappable> BuildBlocks(LoopNoTemplatePair loop)
        {
            List<BlockDataMappable> blocks = new();
            AcadBlockFactory blockFactory = new(dataLoader);
            foreach (BlockMapData blockMap in config.TemplateDefs[loop.Template].BlockMap)
            {
                blocks.Add(blockFactory.GetBlock(blockMap));
            }
        }

        public LoopDrawingData BuildDrawing(LoopNoTemplatePair loop)
        {
            // for a given LoopNoTemplatePair
            //      get all tags for the loop
            GetLoopTags(loop);
            //      map all tags to the type required by the template
            //      build all blocks
            //      create drawing data object
            //      add it to the list of drawings
            throw new NotImplementedException();
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
