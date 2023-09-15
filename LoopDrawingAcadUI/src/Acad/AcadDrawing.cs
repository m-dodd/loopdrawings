using Autodesk.AutoCAD.DatabaseServices;
using LoopDataAdapterLayer;
using System;
using System.IO;

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
            try
            {
                Transaction.Commit();
                
                // Check if the directory exists, and if not, create it
                string directoryPath = Path.GetDirectoryName(AcadDrawingData.OutputDrawingFileName);
                Directory.CreateDirectory(directoryPath);

                Database.SaveAs(AcadDrawingData.OutputDrawingFileName, true, DwgVersion.Current, Database.SecurityParameters);
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                string fileName = Path.GetFileName(AcadDrawingData.OutputDrawingFileName);
                string msg = string.Format("Failed to save drawing '{0}' - please close it", fileName);
                throw new AcadDrawingException(msg, ex);
            }
            catch (System.Exception ex)
            {
                throw new AcadDrawingException("Failed to save drawing.", ex);
            }

        }

        public void Dispose()
        {
            Transaction.Dispose();
            Database.Dispose();
        }
    }

    public class AcadDrawingException : Exception
    {
        public AcadDrawingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
