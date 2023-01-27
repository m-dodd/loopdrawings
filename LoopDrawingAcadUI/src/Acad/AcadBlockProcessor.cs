using Autodesk.AutoCAD.DatabaseServices;
using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;

namespace LoopDrawingAcadUI
{
    public class AcadBlockProcessor : IAcadBlockProcessor
    {
        private readonly Database db;
        private readonly Transaction tr;

        public AcadBlockProcessor(Database db, Transaction tr)
        {
            this.db = db;
            this.tr = tr;
        }

        public void ProcessBlocks(IEnumerable<AcadBlockData> blocks)
        {
            foreach (AcadBlockData block in blocks)
            {
                ProcessBlock(block);
            }
        }
        public void ProcessBlock(AcadBlockData block)
        {
            BlockReference br = GetAcadBlockReference(block.Name);
            ProcessBlockRefAttributes(br, block.Attributes);
        }

        private void ProcessBlockRefAttributes(BlockReference br, Dictionary<string, string> attributeData)
        {
            foreach (ObjectId attributeId in br.AttributeCollection)
            {
                AttributeReference ar = tr.GetObject(attributeId, OpenMode.ForWrite) as AttributeReference; // get first attribute reference
                if (attributeData.ContainsKey(ar.Tag))
                {
                    ar.TextString = attributeData[ar.Tag];
                }
            }
        }
        private BlockReference GetAcadBlockReference(string blockName)
        {
            BlockTable bt = GetBlocktable();
            foreach (ObjectId id in bt)
            {
                BlockTableRecord btr = tr.GetObject(id, OpenMode.ForRead) as BlockTableRecord;
                if (btr.Name == blockName)
                {
                    var ids = btr.GetBlockReferenceIds(true, true) as ObjectIdCollection;
                    // if ids.Count == 1 then there is only one instance of that block in the drawing
                    // but with our new idea of seperate blocks it is easily possible to have multiple instances 
                    // of each block - not sure how I will deal with each of these references
                    // obviously I can return a collection (list), but then I need to know what they are
                    // for now just assume one instance per drawing
                    return tr.GetObject(ids[0], OpenMode.ForRead) as BlockReference;
                }
            }

            return null;
        }

        private BlockTable GetBlocktable()
        {
            return tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
        }
    }
}
