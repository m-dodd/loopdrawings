//using Autodesk.AutoCAD.DatabaseServices;
//using LoopDrawingAcadUI;
//using System.Collections.Generic;

//class DrawingProcessorFactory
//{
//    public static DrawingProcessorxxx Create(DrawingData data)
//    {
//        var db = new Database(false, true);
//        db.ReadDwgFile(data.DrawingFileName, FileOpenMode.OpenForReadAndAllShare, false, null);
//        return new DrawingProcessorxxx(db, data);
//    }
//}

//class DrawingProcessorxxx
//{
//    private readonly Database _db;
//    private readonly DrawingData _data;

//    private DrawingProcessorxxx(Database db, DrawingData data)
//    {
//        _db = db;
//        _data = data;
//    }

//    public void ReplaceAttributeValues()
//    {
//        using (Transaction tr = _db.TransactionManager.StartTransaction())
//        {
//            BlockTable bt = tr.GetObject(_db.BlockTableId, OpenMode.ForRead) as BlockTable;
//            foreach (AcadBlockData block in _data.Blocks)
//            {
//                var blockId = GetBlockId(bt, block.Name);
//                if (blockId != ObjectId.Null)
//                {
//                    ReplaceAttributeValues(tr, blockId, block.Attributes);
//                }
//            }
//            tr.Commit();
//        }
//    }

//    private ObjectId GetBlockId(BlockTable bt, string blockName)
//    {
//        return bt.Cast<ObjectId>().FirstOrDefault(id =>
//        {
//            BlockTableRecord btr = id.GetObject(OpenMode.ForRead) as BlockTableRecord;
//            return btr.Name.Equals(blockName, StringComparison.InvariantCultureIgnoreCase);
//        });
//    }

//    private void ReplaceAttributeValues(Transaction tr, ObjectId blockId, Dictionary<string, string> attributeValues)
//    {
//        BlockTableRecord btr = tr.GetObject(blockId, OpenMode.ForWrite) as BlockTableRecord;
//        if (btr.HasAttributeDefinitions)
//        {
//            btr.ForEach(id =>
//            {
//                AttributeDefinition attdef = tr.GetObject(id, OpenMode.ForWrite) as AttributeDefinition;
//                if (attdef != null && attdef.Constant && attributeValues.TryGetValue(attdef.Tag, out var value))
//                {
//                    attdef.TextString = value;
//                }
//            });
//        }
//    }
//}


//var drawingData = new DrawingData();
////populate the drawingData object
//var drawingProcessor = DrawingProcessorFactory.Create(drawingData);
//drawingProcessor.ReplaceAttributeValues();



/////////////////////////////////////////////////////////////////
//// another idea
//////////////////////////////////////////////////////////////////

//class DrawingProcessorxxx
//{
//    private readonly Database _db;
//    private readonly DrawingData _data;

//    public DrawingProcessorxxx(DrawingData data)
//    {
//        _db = new Database(false, true);
//        _db.ReadDwgFile(data.DrawingFileName, FileOpenMode.OpenForReadAndAllShare, false, null);
//        _data = data;
//    }

//    public void ReplaceAttributeValues()
//    {
//        using (Transaction tr = _db.TransactionManager.StartTransaction())
//        {
//            BlockTable bt = GetBlockTable(tr);
//            foreach (AcadBlockData block in _data.Blocks)
//            {
//                var blockId = GetBlockId(bt, block.Name);
//                if (blockId != ObjectId.Null)
//                {
//                    ReplaceAttributeValuesInBlock(tr, blockId, block.Attributes);
//                }
//            }
//            tr.Commit();
//        }
//    }

//    private BlockTable GetBlockTable(Transaction tr)
//    {
//        return tr.GetObject(_db.BlockTableId, OpenMode.ForRead) as BlockTable;
//    }

//    private ObjectId GetBlockId(BlockTable bt, string blockName)
//    {
//        return bt.Cast<ObjectId>().FirstOrDefault(id =>
//        {
//            BlockTableRecord btr = id.GetObject(OpenMode.ForRead) as BlockTableRecord;
//            return btr.Name.Equals(blockName, StringComparison.InvariantCultureIgnoreCase);
//        });
//    }

//    private void ReplaceAttributeValuesInBlock(Transaction tr, ObjectId blockId, Dictionary<string, string> attributeValues)
//    {
//        BlockTableRecord btr = tr.GetObject(blockId, OpenMode.ForWrite) as BlockTableRecord;
//        if (btr.HasAttributeDefinitions)
//        {
//            ReplaceAttributeValuesInAttributes(tr, btr.GetObjectIds(), attributeValues);
//        }
//    }

//    private void ReplaceAttributeValuesInAttributes(Transaction tr, IEnumerable<ObjectId> attDefIds, Dictionary<string, string> attributeValues)
//    {
//        attDefIds.ForEach(id =>
//        {
//            AttributeDefinition attdef = tr.GetObject(id, OpenMode.ForWrite) as AttributeDefinition;
//            if (attdef != null && attdef.Constant && attributeValues.TryGetValue(attdef.Tag, out var value))
//            {
//                attdef.TextString = value;
//            }
//        });
//    }

//    /////////////////////////////////////////////////////////////////////
//    // Another idea
//    /////////////////////////////////////////////////////////////////////
//    class DrawingProcessorFactory
//    {
//        public IDrawingProcessor Create(DrawingData data)
//        {
//            return new DrawingProcessor(new AutoCADDrawing(data));
//        }
//    }

//    //interface IDrawingProcessor
//    //{
//    //    void ReplaceAttributeValues();
//    //    void Close();
//    //}

//    class DrawingProcessor : IDrawingProcessor
//    {
//        private readonly IAutoCADDrawing _drawing;
//        private readonly IBlockProcessor _blockProcessor;
//        private readonly IAttributeProcessor _attributeProcessor;

//        public DrawingProcessor(IAutoCADDrawing drawing, IBlockProcessor blockProcessor, IAttributeProcessor attributeProcessor)
//        {
//            _drawing = drawing;
//            _blockProcessor = blockProcessor;
//            _attributeProcessor = attributeProcessor;
//        }

//        public void ReplaceattributeValues()
//        {
//            using (Transaction tr = _drawing.StartTransaction())
//            {
//                BlockTable bt = _drawing.GetBlockTable(tr);
//                foreach (AcadBlockData block in _data.Blocks)
//                {
//                    var blockId = _blockProcessor.GetBlockId(bt, block.Name);
//                    if (blockId != ObjectId.Null)
//                    {
//                        _blockProcessor.ReplaceattributeValuesInBlock(tr, blockId, block.Attributes);
//                    }
//                }
//                tr.Commit();
//            }
//        }

//        public void ProcessDrawing(DrawingData drawingData)
//        {
//            using (Database db = new Database(false, true))
//            {
//                db.ReadDwgFile(drawingData.DrawingFileName, FileOpenMode.OpenForReadAndAllShare, false, null);
//                using (Transaction tr = db.TransactionManager.StartTransaction())
//                {
//                    BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
//                    foreach (AcadBlockData block in drawingData.Blocks)
//                    {
//                        _blockProcessor.ProcessBlock(tr, bt, block);
//                    }
//                    tr.Commit();
//                }
//            }
//        }
//    }

//    public void Close()
//    {
//        _drawing.Close();
//    }


//    interface IAutoCADDrawing
//    {
//        BlockTable GetBlockTable(Transaction tr);
//        Transaction StartTransaction();
//        void Close();
//    }

//    class AutoCADDrawing : IAutoCADDrawing
//    {
//        private readonly Database _db;
//        private readonly DrawingData _data;

//        public AutoCADDrawing(DrawingData data)
//        {
//            _db = new Database(false, true);
//            _db.ReadDwgFile(data.DrawingFileName, FileOpenMode.OpenForReadAndAllShare, false, null);
//            _data = data;
//        }

//        public BlockTable GetBlockTable(Transaction tr)
//        {
//            return tr.GetObject(_db.BlockTableId, OpenMode.ForRead) as BlockTable;
//        }

//        public Transaction StartTransaction()
//        {
//            return _db.TransactionManager.StartTransaction();
//        }

//        public void Close()
//        {
//            _db.Dispose();
//        }
//    }

//    interface IBlockProcessor
//    {
//        void ProcessBlock(Transaction tr, BlockTable bt, DrawingData drawingData);
//    }


//    class BlockProcessor : IBlockProcessor
//    {
//    {
//        private IattributeProcessor _attributeProcessor;

//        public BlockProcessor(IattributeProcessor attributeProcessor)
//        {
//            _attributeProcessor = attributeProcessor;
//        }

//        public void ProcessBlock(Transaction tr, BlockTable bt, DrawingData drawingData)
//        {
//            ObjectId blockId = GetBlockId(bt, drawingData.Name);
//            if (blockId != ObjectId.Null)
//            {
//                IEnumerable<ObjectId> attDefIds = GetattributeDefIds(tr, blockId);
//                _attributeProcessor.ReplaceattributeValuesInAttributes(tr, attDefIds, drawingData.Attributes);
//            }
//        }
//    }

//    public void ProcessBlock(Transaction tr, BlockTable bt, DrawingData drawingData)
//    {
//        var blockId = GetBlockId(bt, drawingData.TemplateName);
//        if (blockId == ObjectId.Null)
//        {
//            return;
//        }

//        var attributeDefIds = GetattributeDefIds(tr, blockId);
//        var attributeProcessor = new AttributeProcessor();
//        attributeProcessor.ReplaceattributeValuesInAttributes(tr, attributeDefIds, drawingData.Blocks.First().Attributes);
//    }

//    private ObjectId GetBlockId(BlockTable bt, string blockName)
//    {
//        return bt.Cast<ObjectId>().FirstOrDefault(id =>
//        {
//            BlockTableRecord btr = id.GetObject(OpenMode.ForRead) as BlockTableRecord;
//            return btr.Name.Equals(blockName, StringComparison.InvariantCultureIgnoreCase);
//        });
//    }

//    // this replaces the above - another idea it gave
//    private ObjectId GetBlockId(BlockTable bt, string blockName)
//    {
//        return bt.OfType<BlockTableRecord>()
//                 .Where(btr => btr.Name.Equals(blockName, StringComparison.InvariantCultureIgnoreCase))
//                 .Select(btr => btr.ObjectId)
//                 .FirstOrDefault();
//    }


//    private IEnumerable<ObjectId> GetattributeDefIds(Transaction tr, ObjectId blockId)
//    {
//        BlockTableRecord btr = tr.GetObject(blockId, OpenMode.ForRead) as BlockTableRecord;
//        return btr.Cast<ObjectId>().OfType<AttributeDefinition>();
//    }
//}






//public static class EnumerableExtensions
//{
//    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
//    {
//        foreach (var item in enumerable)
//        {
//            action(item);
//        }
//    }
//}

////class AttributeProcessor : IAttributeProcessor
////{
////    public void ProcessAttributes(Transaction tr, IEnumerable<ObjectId> attDefIds, Dictionary<string, string> attributeValues)
////    {
////        attDefIds.ForEach(id =>
////        {
////            AttributeDefinition attdef = tr.GetObject(id, OpenMode.ForWrite) as AttributeDefinition;
////            if (attdef != null && attdef.Constant && attributeValues.TryGetValue(attdef.Tag, out var value))
////            {
////                attdef.TextString = value;
////            }
////        });
////    }
////}




///// final version
////public void ProcessBlock(Transaction tr, BlockTable bt, DrawingData drawingData)
////{
////    ObjectId blockId = GetBlockId(bt, drawingData.Name);
////    if (blockId != ObjectId.Null)
////    {
////        IEnumerable<ObjectId> attDefIds = GetattributeDefIds(tr, blockId);
////        _attributeProcessor.ReplaceattributeValuesInAttributes(tr, attDefIds, drawingData.Attributes);
////    }
////}

////private ObjectId GetBlockId(BlockTable bt, string blockName)
////{
////    return bt.Cast<ObjectId>().FirstOrDefault(id =>
////    {
////        BlockTableRecord btr = id.GetObject(OpenMode.ForRead) as BlockTableRecord;
////        return btr.Name.Equals(blockName, StringComparison.InvariantCultureIgnoreCase);
////    });
////}

//// this replaces the above - another idea it gave