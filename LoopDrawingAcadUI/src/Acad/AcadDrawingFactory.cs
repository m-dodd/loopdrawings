using Autodesk.AutoCAD.DatabaseServices;
using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoopDrawingAcadUI
{
    public class AcadDrawingFactory : IAcadDrawingFactory
    {
        public AcadDrawing CreateDrawing(AcadDrawingData drawingData)
        {
            try
            {
                if (!File.Exists(drawingData.TemplateDrawingFileName))
                {
                    throw new FileNotFoundException("Template drawing file not found", drawingData.TemplateDrawingFileName);
                }

                Database database = new Database(false, true);
                database.ReadDwgFile(drawingData.TemplateDrawingFileName, System.IO.FileShare.Read, false, "");
                return new AcadDrawing(database, drawingData);
            }
            catch (FileNotFoundException ex)
            {
                string fileName = Path.GetFileName(drawingData.TemplateDrawingFileName);
                string msg = string.Format("Template drawing file '{0}' not found", fileName);
                throw new AcadDrawingFactoryException(msg, ex);
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                string msg = string.Format("An error occurred while creating the drawing for loop '{0}'", drawingData.LoopID);
                throw new AcadDrawingFactoryException(msg, ex);
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("An unexpected error occurred while creating the drawing for loop '{0}'", drawingData.LoopID);
                throw new AcadDrawingFactoryException(msg, ex);
            }
        }
    }


    public class AcadDrawingFactoryException : Exception
    {
        public AcadDrawingFactoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
