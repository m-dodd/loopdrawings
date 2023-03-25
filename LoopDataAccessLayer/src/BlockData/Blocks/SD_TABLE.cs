﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class SD_TABLE : BlockDataDB
    {
        public SD_TABLE(
            IDataLoader dataLoader,
            BlockMapData blockMap,
            Dictionary<string, string> tagMap) : base(dataLoader) 
        {
            Name = blockMap.Name;
            UID = blockMap.UID;
            Tag = tagMap[blockMap.Tags[0]];
        }

        protected override void FetchDBData()
        {
            List<SDKData> sdBlockData = GetSDData();

            for (int i=0; i < sdBlockData.Count; i++)
            {
                string sdNumString = (i + 1).ToString("D2");
                Attributes["INPUT_TAG_" + sdNumString] = sdBlockData[i].InputTag;
                Attributes["OUTPUT_TAG_" + sdNumString] = sdBlockData[i].OutputTag;
                Attributes["DESCRIPTION_" + sdNumString] = sdBlockData[i].OutputDescription;
                Attributes["ACTION_" + sdNumString + "A"] = sdBlockData[i].InputTag;
                Attributes["ACTION_" + sdNumString + "B"] = sdBlockData[i].InputTag;
            }
        }

        protected virtual List<SDKData> GetSDData()
        {
            return dataLoader.GetSDs(Tag);
        }
    }
}
