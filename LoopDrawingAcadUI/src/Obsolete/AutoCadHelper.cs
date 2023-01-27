//using Autodesk.AutoCAD.DatabaseServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices.ComTypes;
//using System.Text;
//using System.Threading.Tasks;

//namespace LoopDrawingAcadUI
//{

//    // It's Jan 22, 2023 - I have no idea waht I was doign in this file.
//    // Seems important
//    // Does it integrate with the other code I have with respect to the JSON files?
//    public interface IDrawingFactory : IDisposable
//    {
//        IDrawing Open(string filePath);
//    }


//    public class DrawingFactory : IDrawingFactory
//    {
//        private Database _database;

//        public DrawingFactory()
//        {
            
//            _database = new Database(false, true);
//        }

//        public IDrawing Open(string filePath)
//        {
            
//            _database.ReadDwgFile(filePath, System.IO.FileShare.Read, false, "");

//            return new Drawing(_database);
//        }


//        public void Dispose()
//        {
//            _database.Dispose();
//        }
//    }

//    public interface IDrawing : IDisposable
//    {
//        Block GetBlock(string blockName);
//        void SaveAs(string filePath);
//    }

//    public class Drawing : IDrawing
//    {
//        private Database _db;
//        private Transaction _tr;
//        public Drawing(Database db)
//        {
//            this._db = db;
//            this._tr = _db.TransactionManager.StartTransaction() as Transaction;
//        }

//        public Block? GetBlock(string blockName)
//        {
//            BlockTable bt = _tr.GetObject(_db.BlockTableId, OpenMode.ForRead) as BlockTable;
//            foreach (ObjectId id in bt)
//            {
//                BlockTableRecord btr = _tr.GetObject(id, OpenMode.ForRead) as BlockTableRecord;
//                if (btr.Name == blockName)
//                {
//                    var ids = btr.GetBlockReferenceIds(true, true) as ObjectIdCollection;
//                    // if ids.Count == 1 then there is only one instance of that block in the drawing
//                    // but with our new idea of seperate blocks it is easily possible to have multiple instances 
//                    // of each block - not sure how I will deal with each of these references
//                    // obviously I can return a collection (list), but then I need to know what they are
//                    // for now just assume one instance per drawing
//                    BlockReference br = _tr.GetObject(ids[0], OpenMode.ForRead) as BlockReference;
//                    return new Block(br, _tr);
//                }
//            }
//            return null;
//        }

//        public void SaveAs(string filePath)
//        {
//            _db.SaveAs(filePath, true, DwgVersion.Current, _db.SecurityParameters);
//        }
    
//        public void Dispose()
//        {
//            _tr.Commit();
//            _tr.Dispose();
//        }
//    }

//    public interface IBlock : IDisposable
//    {
//        void SetValue(string attributeName, string attributeValue);
//        void SetValues(Dictionary<string, string> attributes);
//    }

//    public class Block : IBlock
//    {
//        private readonly BlockReference _br;
//        private readonly Transaction _tr;


//        public Block(BlockReference br, Transaction tr)
//        {
//            this._br = br;
//            this._tr = tr;
//        }
//        public void SetValue(string attributeName, string attributeValue)
//        {
//            foreach (ObjectId id in _br.AttributeCollection)
//            {
//                using (var attributeReference = _tr.GetObject(id,OpenMode.ForWrite, false) as AttributeReference)
//                {
//                    if (attributeReference.Tag == attributeName)
//                    {
//                        attributeReference.TextString = attributeValue;
//                        break;
//                    }
//                }
//            }

//        }

//        public void SetValues(Dictionary<string, string> tagAttributes)
//        {
//            foreach (ObjectId attributeId in _br.AttributeCollection)
//            {
//                AttributeReference ar = _tr.GetObject(attributeId, OpenMode.ForWrite) as AttributeReference; // get first attribute reference
//                if (tagAttributes.ContainsKey(ar.Tag))
//                {
//                    ar.TextString = tagAttributes[ar.Tag];
//                }
//            }
//        }

//        public void Dispose()
//        {
//            this._br.Dispose();
//        }

//    }
//}
