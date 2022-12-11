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


        public void CreateNewDrawing()
        {
            DocumentCollection documents = AcadApp.DocumentManager;

            // Add requires a template - I don't have a template
            // if it can't find the template you pass in then it defaults to the default template
            //   so dumb - it should be overloaded and not require a dumb template
            string templatePath = "acad.dwt";
            Document drawing = documents.Add(templatePath);
            Database db = drawing.Database;


            // Start a transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                drawing.LockDocument(); // wasn't in the other example, but found reference saying this was necessary and it fixed the problem
                                        // Open the Block table for read
                BlockTable table;
                table = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                if (table != null)
                {

                    // Open the Block table record Model space for write
                    BlockTableRecord record = trans.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Create a circle that is at 2,3 with a radius of 4.25
                    using (Circle acCirc = new Circle())
                    {
                        acCirc.Center = new Point3d(2, 3, 0);
                        acCirc.Radius = 4.25;

                        // Add the new object to the block table record and the transaction
                        record.AppendEntity(acCirc);
                        trans.AddNewlyCreatedDBObject(acCirc, true);
                    }
                }
                // Save the new object to the database
                trans.Commit();
            }

            // Save the active drawing
            db.SaveAs(DefaultPathName, true, DwgVersion.Current, db.SecurityParameters);

        }

        public void DrawCircle(Transaction trans, BlockTableRecord record, double x, double y, double r)
        {
            // Create a circle that is at 2,3 with a radius of 4.25
            using (Circle acCirc = new Circle())
            {
                acCirc.Center = new Point3d(x, y, 0);
                acCirc.Radius = r;

                // Add the new object to the block table record and the transaction
                record.AppendEntity(acCirc);
                trans.AddNewlyCreatedDBObject(acCirc, true);
            }

        }

        public void CreateNewDrawing2()
        {
            // Get the current active document and lock it - something to do with modal winforms. Don't really understand and feels like a bandaid.
            Document doc = AcadApp.DocumentManager.MdiActiveDocument;
            doc.LockDocument();

            // Start a transaction
            using (Database db = new Database(true, false))
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    // drawing.LockDocument(); // wasn't in the other example, but found reference saying this was necessary and it fixed the problem
                    // Open the Block table for read
                    BlockTable table = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord record = trans.GetObject(table[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    Random rnd = new Random();
                    for (int i = 1; i <= 5; i++)
                    {

                        double x = 6 * i * rnd.NextDouble();
                        double y = 4 * i * rnd.NextDouble();
                        double r = 5.0 * rnd.NextDouble();
                        DrawCircle(trans, record, x, y, r);
                    }

                    // Save the new object to the database
                    trans.Commit();
                }

                // Save the active drawing
                db.SaveAs(DefaultPathName, true, DwgVersion.Current, db.SecurityParameters);
            }

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
