using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* NOTES:
 *  - so let's start from how this will work
 *  - I need to instantiate some object, LoopBuilder
 *      - read teh config file
 *      - start, or get the db and excel
 *      - connect to DB and get list of loop / templates
 *          - for each loop / template
     *          - create new drawing
     *              - build tag map from db
     *              - create all blocks
     *              - map data into blocks
     *          - add drawing to collection
 * 
 */

namespace LoopDataAccessLayer
{
    public class LoopDrawingData : IMappable
    {
        private readonly TemplateConfig templateConfig;
        private readonly DBDataLoader dbLoader;
        private Dictionary<string, string> tagMap;

        public string LoopID { get; set; } = string.Empty;
        public string TemplateID { get; set; } = string.Empty;
        public string TemplateName { get; set; } = string.Empty; 
        public string DrawingName { get; set; } = string.Empty;
        public List<BlockDataMappable> Blocks { get; set; } = new List<BlockDataMappable>();

        public LoopDrawingData(DBDataLoader dbLoader, string LoopID, TemplateConfig templateConfig)
        {
            this.dbLoader = dbLoader;
            this.templateConfig = templateConfig;
            this.LoopID = LoopID;
            this.TemplateID = templateConfig.TemplateName;
            this.TemplateName = templateConfig.DrawingFilename;
        }

        private void BuildTagMap()
        {
            LoopTagMapper tagMapper = new(dbLoader, LoopID, templateConfig);
            tagMapper.MapData();
            tagMap = tagMapper.TagMap;
        }

        private void BuildBlockList()
        {
            throw new NotImplementedException();
        }

        private void MapBlocks()
        {
            foreach (BlockDataMappable block in Blocks)
            {
                block.MapData();
            }
        }
        
        public void MapData()
        {
            BuildTagMap();
            BuildBlockList();
            MapBlocks();
        }
    }


    public class LoopTagMapper : IMappable
    {
        private readonly DBDataLoader dbLoader;
        public Dictionary<string, string> TagMap { get; } = new Dictionary<string, string>();

        public LoopTagMapper(DBDataLoader dbLoader, string LoopID, TemplateConfig templateConfig)
        {
            this.dbLoader = dbLoader;
        }

        public void MapData()
        {
            throw new NotImplementedException();
        }
    }
}
