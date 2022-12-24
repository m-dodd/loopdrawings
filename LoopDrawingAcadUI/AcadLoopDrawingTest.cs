using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoopDataAdapterLayer;

namespace LoopDrawingAcadUI
{
    internal class AcadLoopDrawingTest
    {
    
        private const string DefaultPathName = @"Z:\Matalino\Projects\Duco Development\LoopDrawings\acadtesting\";
        private const string TITPathName = DefaultPathName + @"\Loop Drawing Typical - TIT.dwg";

        public void OpenDrawingReadBlocks(TextBox txtBlockList, TextBox txtAttributeList)
        {
            string dwgFlpath = TITPathName;

            using (Database db = new Database(false, true))
            {
                db.ReadDwgFile(dwgFlpath, FileOpenMode.OpenForReadAndAllShare, false, null);
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    int index = 0;
                    foreach (ObjectId id in bt)
                    {
                        BlockTableRecord btRecord = (BlockTableRecord)tr.GetObject(id, OpenMode.ForRead);
                        // BlockTableRecord model = tr.GetObject(SymbolUtilityServices.GetBlockModelSpaceId(db), OpenMode.ForRead) as BlockTableRecord;

                        if (!btRecord.IsLayout)
                        {
                            txtBlockList.AppendText(index.ToString() + " - " + btRecord.Name + System.Environment.NewLine);
                        }
                        index++;
                        if (btRecord.Name == "AI_01_1JB_DUCO")
                        {
                            ObjectIdCollection blockReferenceIDs = btRecord.GetBlockReferenceIds(true, true) as ObjectIdCollection; // get all references to loop block
                            BlockReference refLoop = tr.GetObject(blockReferenceIDs[0], OpenMode.ForRead) as BlockReference; // get first specific block
                            AttributeCollection ac = refLoop.AttributeCollection; // get list of attributes in the block reference
                            foreach (ObjectId attributeId in ac)
                            {
                                AttributeReference ar = tr.GetObject(attributeId, OpenMode.ForRead) as AttributeReference; // get first attribute reference
                                string attributeString = ar.Tag + " - " + ar.TextString;
                                if (txtAttributeList.Text == null)
                                {
                                    txtAttributeList.Text = attributeString + System.Environment.NewLine;
                                }
                                else
                                {
                                    txtAttributeList.AppendText(attributeString + System.Environment.NewLine);
                                }

                            }
                        }
                    }

                }
                // db.SaveAs(dwgFlpath, DwgVersion.Current);
            }

        }

        public Database GetDrawingDB(string drawingName)
        {
            Database db = new Database(false, true);
            db.ReadDwgFile(drawingName, FileOpenMode.OpenForReadAndAllShare, false, null);
            return db;
        }

        public void GetModelSpaceText(TextBox modelTextBox)
        {
            string dwgpath = TITPathName;
            string textString = "";
            using (Database db = GetDrawingDB(dwgpath))
            {
                using (Transaction tr = GetDrawingDB(dwgpath).TransactionManager.StartTransaction())
                {
                    BlockTableRecord model = tr.GetObject(SymbolUtilityServices.GetBlockModelSpaceId(db), OpenMode.ForRead) as BlockTableRecord;
                    foreach (ObjectId id in model)
                    {
                        switch (id.ObjectClass.DxfName)
                        {
                            case "TEXT":
                                var text = (DBText)tr.GetObject(id, OpenMode.ForRead);
                                textString += text.TextString + System.Environment.NewLine;
                                break;
                            default:
                                break;
                        }
                    }
                    modelTextBox.Text = textString;
                }
            }
        }

        public void OpenTemplatePopulateBlock(string jsonfile)
        {
            string dwgFlpath = TITPathName;
            string savePath = DefaultPathName + @"testing_loop_attribute_updates.dwg";
            LoopDataCollection loopdata = new LoopDataCollection();
            loopdata.Load(jsonfile);
            //IDictionary<string, string> tagdata = TestData.TagDict;
            IDictionary<string, string> tagdata = loopdata.Data[0].Attributes;

            using (Database db = new Database(false, true))
            {
                db.ReadDwgFile(dwgFlpath, FileOpenMode.OpenForReadAndAllShare, false, null);
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    foreach (ObjectId id in bt)
                    {
                        BlockTableRecord btRecord = (BlockTableRecord)tr.GetObject(id, OpenMode.ForRead);
                        if (btRecord.Name == "AI_01_1JB_DUCO")
                        {
                            ObjectIdCollection blockReferenceIDs = btRecord.GetBlockReferenceIds(true, true) as ObjectIdCollection; // get all references to loop block
                            BlockReference refLoop = tr.GetObject(blockReferenceIDs[0], OpenMode.ForRead) as BlockReference; // get first specific block
                            AttributeCollection ac = refLoop.AttributeCollection; // get list of attributes in the block reference
                            foreach (ObjectId attributeId in ac)
                            {
                                AttributeReference ar = tr.GetObject(attributeId, OpenMode.ForWrite) as AttributeReference; // get first attribute reference
                                if (tagdata.ContainsKey(ar.Tag))
                                {
                                    ar.TextString = tagdata[ar.Tag];
                                }
                            }
                        }
                    }
                    tr.Commit();
                }
                db.SaveAs(savePath, true, DwgVersion.Current, db.SecurityParameters);
            }
        }

        
    }
}
