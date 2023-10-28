using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace LoopDataAccessLayer
{
    public class BlockDataMappableKeyNotFoundException : Exception
    {
        private const string defaultMessage = "Error in block: {0}. Cannot access tagtype {1}. Possibly an error in template configuration or the wrong template was selected.";
        public BlockDataMappableKeyNotFoundException(string blockName, string key) : base(string.Format(defaultMessage, blockName, key))
        {
            BlockName = blockName;
            Key = key;
        }

        public BlockDataMappableKeyNotFoundException(string blockName, string key, Exception innerException) : base(string.Format(defaultMessage, blockName, key))
        {
            BlockName = blockName;
            Key = key;
        }

        public BlockDataMappableKeyNotFoundException(string msg, string blockName, string key) : base(msg)
        {
            BlockName = blockName; 
            Key = key;
        }

        public BlockDataMappableKeyNotFoundException(string msg, string blockName, string key, Exception innerException) : base(msg, innerException)
        {
            BlockName = blockName; 
            Key = key;
        }

        public string Key { get; }
        public string BlockName { get; }
    }



    public abstract class BlockDataMappable : AcadBlockData, IMappableBlock
    {
        protected readonly IDataLoader dataLoader;
        protected readonly ILogger logger;
        public BlockDataMappable(ILogger logger, IDataLoader dataLoader)
        {
            this.dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            this.logger = logger;
        }

        public abstract void MapData(); 

        protected void PopulateTag1Tag2()
        {
            PopulateTag1Tag2(Tag, "TAG1", "TAG2");
        }

        protected void PopulateTag1Tag2(string tag)
        {
            PopulateTag1Tag2(tag, "TAG1", "TAG2");
        }

        protected void PopulateTag1Tag2(string tag1, string tag2, string attribute1, string attribute2)
        {
            Attributes[attribute1] = tag1;
            Attributes[attribute2] = tag2;
        }

        protected void PopulateTag1Tag2(string tag1, string tag2)
        {
            PopulateTag1Tag2(tag1, tag2, "TAG1", "TAG2");
        }

        protected static string[] ExtractInstrumentIdentifierAndLoopNumber(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentException("Tag cannot be null or empty", nameof(tag));
            }

            // Split the tag based on the dash character
            string[] tagParts = tag.Split('-');

            // Check if there are at least two parts
            if (tagParts.Length >= 2)
            {
                // Extract the instrument identifier and instrument loop number
                string instrumentIdentifier = tagParts[0];
                string instrumentLoopNumber = string.Join("-", tagParts, 1, tagParts.Length - 1);

                return new string[] { instrumentIdentifier, instrumentLoopNumber };
            }
            else
            {
                // Return an empty array if there are not enough parts
                return Array.Empty<string>();
            }
        }

        protected void PopulateTag1Tag2(string tag, string attribute1, string attribute2)
        {
            string[] tagComponents = ExtractInstrumentIdentifierAndLoopNumber(tag);
            if (tagComponents.Length == 2)
            {
                PopulateTag1Tag2(tagComponents[0], tagComponents[1], attribute1, attribute2);
            }
        }

        protected string GetTag(BlockMapData blockMap, Dictionary<string, string> tagMap, int index)
        {
            try
            {
                return tagMap[blockMap.Tags[index]];
            }
            catch (KeyNotFoundException ex)
            {
                var e = new BlockDataMappableKeyNotFoundException(Name, blockMap.Tags[index], ex);
                throw e;
            }
        }

    }

    public abstract class BlockDataExcel : BlockDataMappable
    {

        public BlockDataExcel(ILogger logger, IDataLoader dataLoader) : base(logger, dataLoader) { }

        public override void MapData()
        {
            FetchExcelData();
        }

        protected abstract void FetchExcelData();
    }


    public abstract class BlockDataDB : BlockDataMappable
    {
        public BlockDataDB(ILogger logger, IDataLoader dataLoader) : base(logger, dataLoader) { }

        public override void MapData()
        {
            FetchDBData();
        }

        protected abstract void FetchDBData();
    }


    public abstract class BlockDataExcelDB : BlockDataMappable
    {
        public BlockDataExcelDB(ILogger logger, IDataLoader dataLoader) : base(logger, dataLoader) { }

        public override void MapData()
        {
            FetchExcelData(); 
            FetchDBData();
        }

        protected abstract void FetchExcelData();
        protected abstract void FetchDBData();
    }
}
