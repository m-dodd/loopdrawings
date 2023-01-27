using Autodesk.AutoCAD.DatabaseServices;
using LoopDataAdapterLayer;

namespace LoopDrawingAcadUI
{
    public class AcadDrawing : IAcadDrawing
    {
        public Database Database { get; set; }
        public AcadDrawingData AcadDrawingData { get; set; }
        public Transaction Transaction { get; set; }
        public AcadDrawing(Database db, AcadDrawingData drawingData)
        {
            this.AcadDrawingData = drawingData;
            this.Database = db;
            this.Transaction = this.Database.TransactionManager.StartTransaction() as Transaction;
        }

        public void Save()
        {
            Transaction.Commit();
            Database.SaveAs(AcadDrawingData.OutputDrawingFileName, true, DwgVersion.Current, Database.SecurityParameters);
        }

        public void Dispose()
        {
            Transaction.Commit();
            Transaction.Dispose();
            Database.Dispose();
        }
    }
}
