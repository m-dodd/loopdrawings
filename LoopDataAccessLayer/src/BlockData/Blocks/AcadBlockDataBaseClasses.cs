using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public abstract class BlockDataMappable : AcadBlockData, IMappableBlock
    {
        protected readonly IDataLoader dataLoader;
        public BlockDataMappable(IDataLoader dataLoader)
        {
            this.dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
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

        protected string[] GetTag1Tag2(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentException("Tag cannot be null or empty", nameof(tag));
            }

             return tag.Split('-');
        }

        protected void PopulateTag1Tag2(string tag, string attribute1, string attribute2)
        {
            string[] tagComponents = GetTag1Tag2(tag);
            if (tagComponents.Length == 2)
            {
                PopulateTag1Tag2(tagComponents[0], tagComponents[1], attribute1, attribute2);
            }
        }

    }

    public abstract class BlockDataExcel : BlockDataMappable
    {

        public BlockDataExcel(IDataLoader dataLoader) : base(dataLoader) { }

        public override void MapData()
        {
            FetchExcelData();
        }

        protected abstract void FetchExcelData();
    }


    public abstract class BlockDataDB : BlockDataMappable
    {
        public BlockDataDB(IDataLoader dataLoader) : base(dataLoader) { }

        public override void MapData()
        {
            FetchDBData();
        }

        protected abstract void FetchDBData();
    }


    public abstract class BlockDataExcelDB : BlockDataMappable
    {
        public BlockDataExcelDB(IDataLoader dataLoader) : base(dataLoader) { }

        public override void MapData()
        {
            FetchExcelData(); 
            FetchDBData();
        }

        protected abstract void FetchExcelData();
        protected abstract void FetchDBData();
    }
}
