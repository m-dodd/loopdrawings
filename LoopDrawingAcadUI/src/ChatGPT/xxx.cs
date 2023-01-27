//using Autodesk.AutoCAD.DatabaseServices;
//using LoopDrawingAcadUI;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LoopDrawingAcadUI
//{
//    public interface IDrawingProcessorZZZ
//    {
//        void ProcessDrawing();
//    }
//    public class DrawingProcessorZZZ : IDrawingProcessorZZZ
//    {
//        private readonly Database db;
//        private readonly DrawingData drawingData;
//        public DrawingProcessorZZZ(Database db, DrawingData drawingData) 
//        {
//            this.db = db;
//            this.drawingData = drawingData;
//        }

//        private void OpenDrawing() 
//        {
//            db.ReadDwgFile(drawingData.DrawingFileName, System.IO.FileShare.Read, false, "");
//        }
//        private void SaveDrawing()
//        {
//            // figure out how to save the drawing
//        }
//        public void ProcessDrawing()
//        {
//            OpenDrawing();
//            ProcessBlocks();
//            SaveDrawing();
//        }

//        private void ProcessBlocks() { }
//    }

//    interface IBlockProcessorZZZ
//    {
//        void ProcessBlock(AcadBlockData block);
//    }

//    public class BlockProcessorZZZ : IBlockProcessorZZZ
//    {
//        private readonly Database db;
//        private readonly Transaction tr;
//        public BlockProcessorZZZ(Database db, Transaction tr)
//        {
//            this.db = db;
//            this.tr = tr;
//        }

//        public void ProcessBlock(AcadBlockData block)
//        {
//            BlockReference br = GetBlockReference(block.Name);

//        }

//        private BlockReference GetBlock(string name)
//        {
//            throw new NotImplementedException();
//        }
//    }





//}
