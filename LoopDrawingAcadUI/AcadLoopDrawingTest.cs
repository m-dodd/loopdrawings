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

namespace LoopDrawingAcadUI
{
    internal class AcadLoopDrawingTest
    {
    
        private const string DefaultPathName = @"c:\mark\acadtesting\some_drawing.dwg";
        private const string TITPathName = @"c:\mark\acadtesting\Loop Drawing Typical - TIT.dwg";

        private Dictionary<string, string> TagDict;

        public AcadLoopDrawingTest()
        {
            TagDict = new Dictionary<string, string>()
                {
                    { "DESCRIPTION_01", "DESCRIPTION_01"},
                    {"DESCRIPTION_02", "DESCRIPTION_02"},
                    {"DESCRIPTION_03", "DESCRIPTION_03"},
                    {"MANUFACTURER_01", "MANUFACTURER_01"},
                    {"MODEL_01", "MODEL_01"},
                    {"RANGE_01", "RANGE_01"},
                    {"TAG_01", "TAG_01"},
                    {"TAG_02", "TAG_02"},
                    {"CON_01", "CON_01"},
                    {"CON_02", "CON_02"},
                    {"JB_TAG_01", "JB_TAG_01"},
                    {"JB_TS_01", "JB_TS_01"},
                    {"JB_TB_01", "JB_TB_01"},
                    {"JB_TB_02", "JB_TB_02"},
                    {"JB_TB_03", "JB_TB_03"},
                    {"PNL_TS_01", "PNL_TS_01"},
                    {"PNL_TB_01", "PNL_TB_01"},
                    {"PNL_TB_02", "PNL_TB_02"},
                    {"PNL_TB_03", "PNL_TB_03"},
                    {"WIRE_CLR_01", "WIRE_CLR_01"},
                    {"WIRE_CLR_02", "WIRE_CLR_02"},
                    {"WIRE_CLR_03", "WIRE_CLR_03"},
                    {"WIRE_CLR_04", "WIRE_CLR_04"},
                    {"WIRE_CLR_05", "WIRE_CLR_05"},
                    {"WIRE_CLR_06", "WIRE_CLR_06"},
                    {"WIRE_CLR_07", "WIRE_CLR_07"},
                    {"WIRE_CLR_08", "WIRE_CLR_08"},
                    {"PAIR_01", "PAIR_01"},
                    {"PAIR_02", "PAIR_02"},
                    {"WIRE_TAG_01", "WIRE_TAG_01"},
                    {"WIRE_TAG_02", "WIRE_TAG_02"},
                    {"WIRE_TAG_03", "WIRE_TAG_03"},
                    {"CABLE_TAG_01", "CABLE_TAG_01"},
                    {"CABLE_TAG_02", "CABLE_TAG_02"},
                    {"BREAKER_01", "BREAKER_01"},
                    {"MOD_TERM_01", "MOD_TERM_01"},
                    {"FUNCTIONAL_ID_01", "FUNCTIONAL_ID_01"},
                    {"LOOP_NO_01", "LOOP_NO_01"},
                    {"HIGH_ALARM_01", "HIGH_ALARM_01"},
                    {"LOW_ALARM_01", "LOW_ALARM_01"},
                    {"RACK_01", "RACK_01"},
                    {"SLOT_01", "SLOT_01"},
                    {"CHANNEL_01", "CHANNEL_01"},
                    {"DRAWING_NO_01", "DRAWING_NO_01"},
                };
        }

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

        public void OpenTemplatePopulateBlock()
        {
            string dwgFlpath = TITPathName;
            string savePath = DefaultPathName;

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
                                ar.TextString = TagDict[ar.Tag];
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
