using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

using LoopDataAdapterLayer;

/*namespace LoopDrawingAcadUI
{
    public interface IDrawing : IDisposable
    {
        Block FindBlock(string blockName);
        void SaveAs(string filePath);
    }


    public class Drawing : IDrawing
    {
        private readonly string _filePath;
        private readonly Document _document;

        public Drawing(string filePath)
        {
            _filePath = filePath;
            _document = Application.DocumentManager.Open(_filePath);
        }

        public Block FindBlock(string blockName)
        {
            return _document.Blocks.Item(blockName);
        }

        public void SaveAs(string filePath)
        {
            _document.SaveAs(filePath, DwgVersion.Current);
        }

        public void Dispose()
        {
            _document.Close(true);
        }
    }

    public class Drawing2 : IDrawing
    {
        private readonly Database _database;
        private readonly string _filePath;
        private readonly Transaction _transaction;
        private readonly Document _document;
        private readonly ObjectIdCollection _objectIds;

        public Drawing2(Database database, string filePath)
        {
            _database = database;
            _filePath = filePath;
            _transaction = _database.TransactionManager.StartTransaction();
            _document = (Document)_transaction.GetObject(_database.ReadDwgFile(_filePath, FileShare.ReadWrite, false, ""), OpenMode.ForRead);
            _objectIds = new ObjectIdCollection();
        }

        public Block FindBlock(string blockName)
        {
            var blockId = _database.BlockTableId.GetObject(_transaction)
                .Cast<BlockTableRecord>()
                .FirstOrDefault(x => x.Name == blockName)
                ?.ObjectId;
            if (blockId == ObjectId.Null)
            {
                return null;
            }

            return new Block(_transaction, _database, blockId);
        }

        public void SaveAs(string filePath)
        {
            _database.SaveAs(filePath, DwgVersion.Current);
        }

        public void Dispose()
        {
            _transaction.Commit();
            _transaction.Dispose();
        }
    }



    /////////////////////////////////////////////////////////////////

    public interface IBlock
    {
        void SetValue(string attributeName, string attributeValue);
    }

    public class Block : IBlock
    {
        private readonly Autodesk.AutoCAD.DatabaseServices.BlockReference _blockReference;

        public Block(Autodesk.AutoCAD.DatabaseServices.BlockReference blockReference)
        {
            _blockReference = blockReference;
        }

        public void SetValue(string attributeName, string attributeValue)
        {
            foreach (ObjectId id in _blockReference.AttributeCollection)
            {
                using (var attributeReference = id.Open(OpenMode.ForWrite, false) as AttributeReference)
                {
                    if (attributeReference.Tag == attributeName)
                    {
                        attributeReference.TextString = attributeValue;
                    }
                }
            }
        }
    }

    //////////////////////////////////////////////////////////////////
    public interface IDrawingFactory
    {
        IDrawing Open();
    }


    public class DrawingFactory : IDrawingFactory
    {
        private readonly string _filePath;

        public DrawingFactory(string filePath)
        {
            _filePath = filePath;
        }

        public IDrawing Open()
        {
            var database = new Database(false, true);
            database.ReadDwgFile(_filePath, System.IO.FileShare.Read, false, "");

            return new Drawing(database);
        }
    }

    ///////////////////////////////////////////////////////////////////////

    public class AttributePopulator
    {
        private readonly IDrawingFactory _drawingFactory;

        public AttributePopulator(IDrawingFactory drawingFactory)
        {
            _drawingFactory = drawingFactory;
        }

        public void PopulateAttributes(AutoCadData data)
        {
            using (var drawing = _drawingFactory.Open())
            {
                var block = drawing.FindBlock(data.BlockName);
                block.SetValues(data.AttributeValues);
                drawing.SaveAs(data.OutputPath);
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////
    class Program
    {
        static void Main(string[] args)
        {
            var drawingFactory = new DrawingFactory("template.dwg");
            var attributePopulator = new AttributePopulator(drawingFactory);

            var data = AutoCadData.ReadJson("data.json");
            foreach (var item in data)
            {
                attributePopulator.PopulateAttributes(item);
            }
        }
    }
}
*/