using Autodesk.AutoCAD.DatabaseServices;
using LoopDataAdapterLayer;
using System;


namespace LoopDrawingAcadUI
{
    public interface IAcadDrawing : IDisposable
    {
        Database Database { get; set; }
        AcadDrawingData AcadDrawingData { get; set; }
        Transaction Transaction { get; set; }
        void Save();
    }
}
