using Autodesk.AutoCAD.DatabaseServices;
using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;

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

        //private void ProcessBlocksMUCH_FASTER_I_THINK(IEnumerable<AcadBlockData> blocks)
        //{
        //    // this function would replace ProcessBlocks and would allow me to delete some other functions here
        //    // I think this shoudl be much much faster... I base this on the assumption that tr.GetObject as BlockTableRecord must be somewhat slow
        //    // which means my `GetAcadBlockReference` function is super slow - this makes that function obsolte

        //    // convert list to dict to look up blockdata when a block is found
        //    Dictionary<string, AcadBlockData> blockDict = blocks.ToDictionary(b => b.Name, b => b);
            
        //    // loop through block table ONCE - this is the main difference
        //    foreach (ObjectId id in GetBlocktable())
        //    {
        //        BlockTableRecord btr = tr.GetObject(id, OpenMode.ForRead) as BlockTableRecord;
                
        //        // is this block part of our data?
        //        if (blockDict.TryGetValue(btr.Name, out var block))
        //        {
        //            ProcessBlockRefAttributes(GetBlockReference2(btr), block.Attributes);
        //        }
        //    }
        //}

        //private BlockReference GetBlockReference2(BlockTableRecord btr)
        //{
        //    var ids = btr.GetBlockReferenceIds(true, true) as ObjectIdCollection;
        //    return tr.GetObject(ids[0], OpenMode.ForRead) as BlockReference;
        //}

        public void ProcessBlock(AcadBlockData block)
        {
            BlockReference br = GetAcadBlockReference(block.Name);
            if (br != null)
            {
                ProcessBlockRefAttributes(br, block.Attributes);
            }
        }

        private void ProcessBlockRefAttributes(BlockReference br, Dictionary<string, string> attributeData)
        {
            // We had an issue where just replacing the text in the attribute was causing the alignment
            // to be wrong. It has to do with confusion between what database Acad thinks it should be working
            // with and the fact that we are "sideloading" our database.
            //
            // Solution:
            //      - save a copy of the current database
            //      - replace it with our database
            //      - AdjustAlignment
            //      - restore original database

            Database activeDB = HostApplicationServices.WorkingDatabase;
            HostApplicationServices.WorkingDatabase = db;
            foreach (ObjectId attributeId in br.AttributeCollection)
            {
                AttributeReference ar = tr.GetObject(attributeId, OpenMode.ForWrite) as AttributeReference;
                if (attributeData.ContainsKey(ar.Tag))
                {
                    ar.TextString = attributeData[ar.Tag];
                    ar.AdjustAlignment(db);
                }
            }
            HostApplicationServices.WorkingDatabase = activeDB;
        }


        private BlockReference GetAcadBlockReference(string blockName)
        {
            BlockTable bt = GetBlocktable();
            if (bt.Has(blockName))
            {
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[blockName], OpenMode.ForRead);
                var ids = btr.GetBlockReferenceIds(true, true);
                return tr.GetObject(ids[0], OpenMode.ForRead) as BlockReference;
            }
            else
            {
                return null;
            }
        }

        private BlockTable GetBlocktable()
        {
            return tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
        }
    }
}
