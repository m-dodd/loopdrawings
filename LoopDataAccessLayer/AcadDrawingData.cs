using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoopDataAccessLayer
{
    public class AcadDrawingData : IMappable
    {
        //private readonly TemplateConfig templateConfig;
        //private readonly DBDataLoader dbLoader;
        //private Dictionary<string, string> tagMap;

        public string LoopID { get; set; } = string.Empty;
        //public string TemplateID { get; set; } = string.Empty;
        public string TemplateName { get; set; } = string.Empty; 
        public string DrawingFileName { get; set; } = string.Empty;
        public List<BlockDataMappable> Blocks { get; set; } = new List<BlockDataMappable>();

        public AcadDrawingData() { }

        //public AcadDrawingData(DBDataLoader dbLoader, string LoopID, TemplateConfig templateConfig)
        //public AcadDrawingData(DBDataLoader dbLoader, string LoopID, TemplateConfig templateConfig)
        //{
            //this.dbLoader = dbLoader;
            //this.templateConfig = templateConfig;
            //this.LoopID = LoopID;
            //this.TemplateID = templateConfig.TemplateName;
            //this.TemplateName = templateConfig.DrawingFilename;
        //}

        //private void BuildTagMap()
        //{
        //    LoopTagMapper tagMapper = new(dbLoader, LoopID, templateConfig);
        //    tagMapper.MapData();
        //    tagMap = tagMapper.TagMap;
        //}

        //private void BuildBlockList()
        //{
        //    throw new NotImplementedException();
        //}

        private void MapBlocks()
        {
            foreach (BlockDataMappable block in Blocks)
            {
                block.MapData();
            }
        }
        
        public void MapData()
        {
            //BuildTagMap();
            //BuildBlockList();
            MapBlocks();
        }
    }
}
