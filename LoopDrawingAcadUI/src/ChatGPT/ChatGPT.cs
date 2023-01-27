/////////////////////////////////////////////////////
// EXAMPLE 
//////////////////////////////////////////////////////
/*
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace chatgpt
{
    public interface IDrawingFactory
    {
        Drawing Open(string filePath);
        void SaveAndClose(Drawing drawing);
    }

    public class Drawing : IDisposable
    {
        private readonly Database _database;
        private readonly Document _document;
        private readonly Transaction _transaction;

        public Drawing(Database database, Transaction transaction, Document document)
        {
            _database = database;
            this.document = document;
            _transaction = transaction;
        }

        public Block FindBlock(string blockName)
        {
            BlockTable blockTable = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
            if (blockTable.Has(name))
            {
                return transaction.GetObject(blockTable[name], OpenMode.ForRead) as Block;
            }
            return null;
        }

        public void SaveAs(string filePath)
        {
            database.SaveAs(path, DwgVersion.Current);
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _document.CloseAndDiscard();
            _database.Dispose();
        }
    }

    public class DrawingFactory : IDrawingFactory
    {
        public Drawing Open(string filePath)
        {
            // Open the drawing from the specified file path
            return null;
        }

        public void SaveAndClose(Drawing drawing)
        {
            // Save and close the drawing
        }
    }

    public class AttributePopulator
    {
        private IDrawingFactory _drawingFactory;

        public AttributePopulator(IDrawingFactory drawingFactory)
        {
            _drawingFactory = drawingFactory;
        }

        public void Populate(AutoCadData data)
        {
            using (Drawing drawing = _drawingFactory.Open(data.TemplatePath))
            {
                Block block = drawing.FindBlock(data.BlockName);
                foreach (KeyValuePair<string, string> kvp in data.Values)
                {
                    Attribute attribute = block.GetAttribute(kvp.Key);
                    attribute.SetValue(kvp.Value);
                }
                drawing.SaveAs(data.OutputPath);
            }
        }
    }

    public static class Program
    {
        public static void Main()
        {
            IDrawingFactory drawingFactory = new DrawingFactory();
            AttributePopulator attributePopulator = new AttributePopulator(drawingFactory);

            string filePath = "data.json";
            List<AutoCadData> dataList;
            try
            {
                dataList = AutoCadData.ReadJson(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            foreach (AutoCadData data in dataList)
            {
                try
                {
                    attributePopulator.Populate(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: An unexpected error occurred while populating the attributes.");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
*/