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
            this.dataLoader = dataLoader;
        }

        public abstract void MapData(); 
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

    public abstract class BlockFieldDevice : BlockDataExcelDB
    {
        public BlockFieldDevice(IDataLoader dataLoader) : base(dataLoader) { }

        protected static List<string> SplitDeviceDescriptionToFourLines(string stringToSplit, int maximumLineLength)
        {
            // This is a bit tricky, but we use a fancy regex expression to look for any characters (except terminators)
            // between 1-maximumLineLength in length, but less than the white space
            // not entirely sure I understand it, but it is essentially is two regex groups, one captures, and one non-capturing
            // (.{1,10})(?:\s|$)
            // the parenthesis are the groups... (.{1,10}) and (?:\s|$)
            // (.{1,10}) == match any set of characters between 1-10 characters in length
            // (?:\s|$) == do not capture any white space or terminating charactrs
            // ?: makes it non-capturing
            // https://stackoverflow.com/questions/22368434/best-way-to-split-string-into-lines-with-maximum-length-without-breaking-words
            // https://stackoverflow.com/questions/11416191/converting-a-matchcollection-to-string-array
            return Regex.Matches(stringToSplit, @"(.{1," + maximumLineLength +@"})(?:\s|$)")
                .Cast<Match>()
                .Select(m => m.Value.Trim()) // if not then the regex gives whitespace at the end
                .Take(4)
                .ToList();
        }

        protected static IEnumerable<string> GetFourLineDescription(string deviceDescription, int maximumLineLength)
        {
            List<string> descriptions = SplitDeviceDescriptionToFourLines(deviceDescription, maximumLineLength);
            while (descriptions.Count < 4)
            {
                descriptions.Add(string.Empty);
            }
            return descriptions;
        } 
        
    }

    public class EMPTY_BLOCK : BlockDataMappable
    {
        public EMPTY_BLOCK(IDataLoader dataLoader) : base(dataLoader) { }
        public override void MapData() { }
    }
}
