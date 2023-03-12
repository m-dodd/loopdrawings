using Autodesk.AutoCAD.DatabaseServices;
using LoopDataAdapterLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace LoopDrawingAcadUI
{
    public class AcadBlockProcessor : IAcadBlockProcessor
    {
        private readonly Database db;
        private readonly Transaction tr;
        Dictionary<string, BlockReference> uidBlockMap;

        public AcadBlockProcessor(Database db, Transaction tr)
        {
            this.db = db;
            this.tr = tr;
            uidBlockMap = new Dictionary<string, BlockReference>();
        }

        public void ProcessBlocks(IEnumerable<AcadBlockData> blocks)
        {
            BuildUIDBlockRefMap(blocks);
            foreach (AcadBlockData block in blocks)
            {
                ProcessBlock(block);
            }
        }

        //public void ProcessBlock(AcadBlockData block)
        //{
        //    BlockReference br = GetAcadBlockReference(block.Name);
        //    if (br != null)
        //    {
        //        if (block.Name == "VALVE_BODY")
        //        {
        //            SetDynamicPropertyValue(br, "Visibility1", block.Attributes["VALVE_TYPE"]);
        //        }
        //        ProcessBlockRefAttributes(br, block.Attributes);
        //    }
        //}

        public void ProcessBlock(AcadBlockData block)
        {
            string[] valveBlocks = new string[]
            {
                "VALVE_BODY", "VALVE_2-SOL"
            };
            if (uidBlockMap.TryGetValue(block.UID.ToUpper(), out BlockReference br))
            {
                if (br != null)
                {
                    if (valveBlocks.Contains(block.Name))
                    {
                        SetDynamicPropertyValue(br, "Visibility1", block.Attributes["VALVE_TYPE"]);
                    }
                    ProcessBlockRefAttributes(br, block.Attributes);
                }
            }
        }

        private void BuildUIDBlockRefMap(IEnumerable<AcadBlockData> blocks)
        {
            // build a new map each time this is run
            uidBlockMap = new Dictionary<string, BlockReference>();
            foreach(string blockName in GetUniqueBlockNames(blocks))
            {
                foreach(ObjectId id in GetAllBlockReferenceIDs(blockName))
                {
                    BlockReference br = GetBlockReferenceFromID(id);
                    string uid = GetUID(br);
                    if (!string.IsNullOrEmpty(uid))
                    {
                        uidBlockMap[uid.ToUpper()] = br;
                    }
                }
            }
        }

        private HashSet<string> GetUniqueBlockNames(IEnumerable<AcadBlockData> blocks)
        {
            HashSet<string> blockNames = new HashSet<string>();
            foreach (AcadBlockData block in blocks)
            {
                blockNames.Add(block.Name);
            }
            return blockNames;
        }
        
        private string GetUID(BlockReference br)
        {
            foreach (ObjectId attributeId in br.AttributeCollection)
            {
                AttributeReference ar = tr.GetObject(attributeId, OpenMode.ForWrite) as AttributeReference;
                if (ar.Tag == "UID")
                {
                    return ar.TextString;
                }
            }
            return string.Empty;
        }
        
        private BlockTable GetBlocktable() => tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

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
            ObjectIdCollection ids = GetAllBlockReferenceIDs(blockName);
            return ids.Count > 0 ? GetBlockReferenceFromID(ids[0]) : null;
        }

        private BlockReference GetBlockReferenceFromID(ObjectId id)
        {
            return (BlockReference)tr.GetObject(id, OpenMode.ForWrite);
        }

        private ObjectIdCollection GetAllBlockReferenceIDs(string blockName)
        {
            var blockReferenceIds = new ObjectIdCollection();
            var bt = GetBlocktable();
            if (bt.Has(blockName))
            {
                var btr = (BlockTableRecord)tr.GetObject(bt[blockName], OpenMode.ForRead);
                foreach (ObjectId id in btr.GetBlockReferenceIds(true, false))
                {
                    blockReferenceIds.Add(id);
                }
                foreach (ObjectId anonymousBlockId in btr.GetAnonymousBlockIds())
                {
                    var anonymousBtr = (BlockTableRecord)tr.GetObject(anonymousBlockId, OpenMode.ForRead);
                    foreach (ObjectId id in anonymousBtr.GetBlockReferenceIds(true, false))
                    {
                        blockReferenceIds.Add(id);
                    }
                }
            }
            return blockReferenceIds;
        }

        private void SetDynamicPropertyValue(BlockReference br, string propertyName, object value)
        {
            foreach (DynamicBlockReferenceProperty property in br.DynamicBlockReferencePropertyCollection)
            {
                if (property.PropertyName == propertyName)
                {
                    property.Value = value;
                    break;
                }
            }
        }
    }
}
