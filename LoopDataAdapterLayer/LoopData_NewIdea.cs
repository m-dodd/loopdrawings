using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Just some notes
 * 
 * First I go to the database (or excel) and I can get a list of every LOOP in the system
 *      - for each loop I will have a template associated with it
 *      - I then have a JSON config file that will tell me what blocks are in each loop template
 *      - the blocks will have a map defined in code
 *      
 * Json example
 * {
 *      TemplateID1: [
 *          BlockA,
 *          BlockB,
 *          BlockC,
 *          ],
 *      TemplateID2 [
 *          BlockA,
 *          BlockB,
 *          BlockC,
 *          ],
 * }
 * 
 * at the end of this I will end up with a LoopDrawingMappable object with a list of BlockDataMappable objects, each populated with an attribute map
 * 
 * well, now that I think of it I will need a List of LoopDrawingMappable Objects
 *      - this will get written to JSON
 *      - and then read back from JSON in the acad application to populate the blocks for each template
 *
 */

namespace LoopDataAdapterLayer
{
    public interface IMappable
    {
        void MapData();
    }

    public abstract class BlockDataMappable : IMappable
    {
        /* Maybe this won't be abstract - maybe this is a concrete class as I think all blocks will be hte same */
        /* NO - the whole point is that MapData is different for each and every block */
        public string Name { get; set; } = String.Empty;
        public string Tag { get; set; } = String.Empty;
        public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();
        
        public abstract void MapData(); // maps the block data
    }

    public class SomeBlock : BlockDataMappable
    {
        public override void MapData()
        {
            throw new NotImplementedException();
        }
    }

    public class SomeOtherBlock : BlockDataMappable
    {
        public override void MapData()
        {
            throw new NotImplementedException();
        }
    }

    public class LoopDrawingData : IMappable
    {
        public string LoopID { get; set; } = String.Empty;
        public string TemplateID { get; set; } = String.Empty;
        public string DrawingName { get; set; } = String.Empty;
        public List<BlockDataMappable> Blocks { get; set; } = new List<BlockDataMappable>();

        public void MapData()
        {
            foreach(BlockDataMappable block in Blocks)
            {
                block.MapData();
            }
        }
    }

    public static class LoopDrawingDataFactory
    {
        public static LoopDrawingData GetLoop(string TemplateID)
        {
            throw new NotImplementedException();
        }
    }

    // With this factory the drawing can just look at the block names defined in the template json config file
    // and instantiate the correct block
    // the block will have the correct map defined in it's version of the MapData() function
    // and finally the list of blocks can just look and call the MapData funcion of each block as built above
    //
    // I think this is beginning to come together
    public static class BlockFactoryXXX
    {
        public static BlockDataMappable GetBlock(string blockName)
        {
            switch (blockName)
            {
                case "A":
                    return new SomeBlock() { Name=blockName };
                case "B":
                    return new SomeOtherBlock() { Name=blockName };
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public class AllLoopDrawings
    {
        public List<LoopDrawingData> Drawings { get; set; } = new List<LoopDrawingData>();
        
        public string ToJson()
        {
            throw new NotImplementedException();
        }

        public List<LoopDrawingData> FromJson()
        {
            throw new NotImplementedException();
        }
    }
}
