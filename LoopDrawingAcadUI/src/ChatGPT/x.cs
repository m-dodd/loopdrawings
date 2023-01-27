//using Autodesk.AutoCAD.DatabaseServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LoopDrawingAcadUI
//{
//    public interface IDrawingProcessor
//    {
//        public void ProcessDrawing();
//    }
//    public class DrawingProcessor : IDrawingProcessor
//    {
//        private readonly AutoCADDrawing _autoCADDrawing;
//        private readonly BlockProcessor _blockProcessor;

//        public DrawingProcessor(DrawingData drawingData)
//        {
//            _autoCADDrawing = new AutoCADDrawing(drawingData.DrawingFileName);
//            _blockProcessor = new BlockProcessor(new AttributeProcessor());
//        }

//        public void ProcessDrawing()
//        {
//            _autoCADDrawing.Open();
//            using (Transaction tr = _autoCADDrawing.Transaction)
//            {
//                BlockTable bt = tr.GetObject(_autoCADDrawing.BlockTableId, OpenMode.ForRead) as BlockTable;
//                foreach (AcadBlockData block in drawingData.Blocks)
//                {
//                    _blockProcessor.ProcessBlock(tr, bt, block);
//                }
//                tr.Commit();
//            }
//            _autoCADDrawing.Close();
//        }
//    }

//    //////////////////////////////////////////////////////////
//    interface IAttributeProcessor
//    {
//        void ProcessAttributes(Transaction tr, IEnumerable<ObjectId> attDefIds, Dictionary<string, string> attributeValues);
//    }

    

//    class AttributeProcessor : IAttributeProcessor
//    {
//        public void ProcessAttributes(Transaction tr, IEnumerable<ObjectId> attDefIds, Dictionary<string, string> attributeValues)
//        {
//            foreach (var id in attDefIds)
//            {
//                var attdef = tr.GetObject(id, OpenMode.ForWrite) as AttributeDefinition;
//                if (IsAttributeDefinitionValid(attdef) && HasAttributeValue(attdef, attributeValues))
//                {
//                    ReplaceAttributeValue(attdef, attributeValues[attdef.Tag]);
//                }
//            }
//        }

//        private bool IsAttributeDefinitionValid(AttributeDefinition attDef)
//        {
//            return attDef != null && attDef.Constant;
//        }

//        private bool HasAttributeValue(AttributeDefinition attDef, Dictionary<string, string> attributeValues)
//        {
//            return attributeValues.TryGetValue(attDef.Tag, out _);
//        }

//        private void ReplaceAttributeValue(AttributeDefinition attribute, string value)
//        {
//            attribute.TextString = value;
//        }
//    }

    
    
//    //////////////////////////////////////////////////////////////////////////////////////////////////////
//    interface IBlockProcessor
//    {
//        void ProcessBlock(Transaction tr, BlockTable bt, DrawingData drawingData);
//    }


//    class BlockProcessor : IBlockProcessor
//    {
//        //private IAttributeProcessor attributeProcessor;
//        private Transaction transaction;

//        public BlockProcessor(Transaction tr)
//        {
//            //this.attributeProcessor = attributeProcessor;
//            this.transaction = tr;
//        }

        
        

//        public void ProcessBlock(Transaction tr, BlockTable bt, DrawingData drawingData)
//        {
//            var blockId = GetBlockId(bt, drawingData.TemplateName);
//            if (blockId == ObjectId.Null)
//            {
//                return;
//            }

//            var attributeDefIds = GetAttributeDefIds(tr, blockId);
//            var attributeProcessor = new AttributeProcessor();
//            attributeProcessor.ProcessAttributes(tr, attributeDefIds, drawingData.Blocks.First().Attributes);
//        }

        
//        private ObjectId GetBlockId(BlockTable bt, string blockName)
//        {
//            BlockTable bt = transaction.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
//            return bt.OfType<BlockTableRecord>()
//                     .Where(btr => btr.Name.Equals(blockName, StringComparison.InvariantCultureIgnoreCase))
//                     .Select(btr => btr.ObjectId)
//                     .FirstOrDefault();
//        }


//        private IEnumerable<ObjectId> GetAttributeDefIds(Transaction tr, ObjectId blockId)
//        {
//            BlockTableRecord btr = tr.GetObject(blockId, OpenMode.ForRead) as BlockTableRecord;
//            return (IEnumerable<ObjectId>)btr.Cast<ObjectId>().OfType<AttributeDefinition>();
//        }

//        private IEnumerable<ObjectId> GetAttributeDefIds(BlockTable bt, string blockName)
//        {

//        }
//    }
//}
