using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LoopDataAccessLayer.src.DataLoader
{
    public class TagComponents
    {
        public string OriginalTag { get; private set; } = string.Empty;
        public string MeasureVariable { get; set; } = string.Empty;
        public string MeasureModifier { get; set; } = string.Empty;
        public string ReadoutFunction { get; set; } = string.Empty;
        public string OutputFunction { get; set; } = string.Empty;
        public string OutputModifier { get; set; } = string.Empty;
        public string LoopNo { get; set; } = string.Empty;

        public static TagComponents ParseTagIdentifer(string tagIdentifier)
        {
            //const string identifierPattern = @"([A-Z][DFJKMQSXYZ]?)([ABEGILNOPRUWX]?)([BCKNSTUVXYZ]?)([BHLMNUX]?)";
            string outputFunctionPattern = "BCKNSTUVXYZ";
            string readoutFunctionPattern = "ABEGHILNOPRUWX";
            string identifierPattern = $@"([A-Z])(ZH|[DFHJKMQXYZ]?)([{readoutFunctionPattern}]?)([{outputFunctionPattern}]?)([BCHLMNOUX]{{1,2}})?";

            Match match = Regex.Match(tagIdentifier, identifierPattern);
            if (match.Success)
            {
                string measureVariable = match.Groups[1].Value;
                string measureModifier = match.Groups[2].Value;
                string readoutFunction = match.Groups[3].Value;
                string outputFunction = match.Groups[4].Value;
                string outputModifier = match.Groups[5].Value;

                bool isOutputEmpty = string.IsNullOrEmpty(outputFunction);
                bool hasReadoutFunction = !string.IsNullOrEmpty(readoutFunction);
                bool isOutputModifierEmpty = string.IsNullOrEmpty(outputModifier);
                bool isMeasureModifierValid = readoutFunctionPattern.Contains(measureModifier);
                bool isOutputValid = outputFunctionPattern.Contains(measureModifier);

                if (isOutputEmpty)
                {
                    if (hasReadoutFunction && isOutputModifierEmpty && isOutputValid)
                    {
                        outputFunction = measureModifier;
                        measureModifier = "";
                    }
                    else if (!hasReadoutFunction && isMeasureModifierValid)
                    {
                        readoutFunction = measureModifier;
                        measureModifier = "";
                    }
                }

                return new TagComponents
                {
                    OriginalTag = tagIdentifier,
                    MeasureVariable = measureVariable,
                    MeasureModifier = measureModifier,
                    ReadoutFunction = readoutFunction,
                    OutputFunction = outputFunction,
                    OutputModifier = outputModifier
                };
            }
            else
            {
                return new TagComponents
                {
                    OriginalTag = tagIdentifier
                };
            }
        }

        public static TagComponents ParseTag(string tag)
        {
            string[] tagParts = tag.Split('-');
            if (tagParts.Length >= 2)
            {
                // Extract the instrument identifier and instrument loop number
                string instrumentIdentifier = tagParts[0];
                string instrumentLoopNumber = string.Join("-", tagParts, 1, tagParts.Length - 1);

                TagComponents tagComponents = TagComponents.ParseTagIdentifer(instrumentIdentifier);
                tagComponents.LoopNo = instrumentLoopNumber;
                return tagComponents;
            }
            else
            {
                return new TagComponents
                {
                    OriginalTag = tag
                };
            }
        }
    }

    public class InstrumentTag
    {
        public InstrumentTag(string tag)
        {
            this.Tag = tag;
            this.Components = TagComponents.ParseTag(tag);
        }

        public string Tag { get; set; }
        
        public TagComponents Components { get; set; }
        
    }
}
