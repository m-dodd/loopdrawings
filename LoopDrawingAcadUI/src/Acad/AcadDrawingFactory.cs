using Autodesk.AutoCAD.DatabaseServices;
using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoopDrawingAcadUI
{
    public class AcadDrawingFactory : IAcadDrawingFactory
    {
        public AcadDrawing CreateDrawing(AcadDrawingData drawingData)
        {
            // need to handle file not found exceptions here
            // also this exception
            // Autodesk.AutoCAD.Runtime.Exception: 'eRepeatedDwgRead'
            Database database = new Database(false, true);
            database.ReadDwgFile(drawingData.TemplateDrawingFileName, System.IO.FileShare.Read, false, "");

            return new AcadDrawing(database, drawingData);
        }
    }
}
