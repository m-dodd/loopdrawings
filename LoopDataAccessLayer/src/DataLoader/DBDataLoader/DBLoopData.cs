using System.Text.RegularExpressions;

namespace LoopDataAccessLayer
{
    public interface IDBLoopData
    {
        string Channel { get; set; }
        string Description { get; set; }
        string FailPosition { get; set; }
        IEnumerable<string> FourLineDescription { get; }
        string HiAlarm { get; set; }
        string HiControl { get; set; }
        string HiHiAlarm { get; set; }
        string InstrumentType { get; set; }
        string IoType { get; set; }
        string IoPanel { get; set; }
        string JB1Tag { get; set; }
        string JB2Tag { get; set; }
        string LoAlarm { get; set; }
        string LoControl { get; set; }
        string LoLoAlarm { get; set; }
        string LoopNo { get; set; }
        string Manufacturer { get; set; }
        string MaxCalRange { get; set; }
        string MinCalRange { get; set; }
        string Model { get; set; }
        string ModTerm1 { get; set; }
        string ModTerm2 { get; set; }
        string PidDrawingNumber { get; set; }
        string Rack { get; set; }
        string RangeUnits { get; set; }
        string SignalLevel { get; set; }
        string Slot { get; set; }
        string System { get; set; }
        string SystemType { get; set; }
        string Tag { get; set; }
        bool IsMotorSD { get; }
        bool IsMotorStart { get; }
        bool IsMotorStop { get; }
        bool IsMotorStartStop { get; }

        static bool IsValidDatabaseString(string value)
        {
            return !(string.IsNullOrEmpty(value) || value == "---");
        }
    }
    public class DBLoopData : IDBLoopData
    {
        public const int CALERROR = -9999;
        public const int RACKERROR = -99;

        private string failPosition;
        private string manufacturer;
        private string signalLevel;


        public DBLoopData()
        {
            failPosition = string.Empty;
            manufacturer = string.Empty;
            signalLevel = string.Empty;
        }

        public string Tag { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manufacturer
        {
            get => manufacturer;
            set
            {
                manufacturer = IDBLoopData.IsValidDatabaseString(value) ? value : string.Empty;
            }
        }
        public string Model { get; set; } = string.Empty;
        public string JB1Tag { get; set; } = string.Empty;
        public string JB2Tag { get; set; } = string.Empty;
        public string Rack { get; set; } = RACKERROR.ToString();
        public string Slot { get; set; } = RACKERROR.ToString();
        public string Channel { get; set; } = RACKERROR.ToString();
        public string ModTerm1 { get; set; } = string.Empty;
        public string ModTerm2 { get; set; } = string.Empty;
        public string PidDrawingNumber { get; set; } = string.Empty;
        public string MinCalRange { get; set; } = CALERROR.ToString();
        public string MaxCalRange { get; set; } = CALERROR.ToString();
        public string RangeUnits { get; set; } = string.Empty;
        public string InstrumentType { get; set; } = string.Empty;
        public string IoType { get; set; } = string.Empty;

        public string FailPosition
        {
            get => failPosition;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] failPositionsOK = new string[] { "FC", "FO", "FL" };
                    string cleanValue = value.ToUpper().Trim();
                    if (cleanValue == "CLOSED")
                    {
                        failPosition = "FC";
                    }
                    else if (cleanValue == "OPEN")
                    {
                        failPosition = "FO";
                    }
                    else if (failPositionsOK.Contains(cleanValue))
                    {
                        failPosition = cleanValue;
                    }
                    else failPosition = string.Empty;
                }
                else
                {
                    failPosition = string.Empty;
                }
            }
        }
        public string LoLoAlarm { get; set; } = string.Empty;
        public string LoAlarm { get; set; } = string.Empty;
        public string HiAlarm { get; set; } = string.Empty;
        public string HiHiAlarm { get; set; } = string.Empty;
        public string LoControl { get; set; } = string.Empty;
        public string HiControl { get; set; } = string.Empty;
        public string IoPanel { get; set; } = string.Empty;
        public string LoopNo { get; set; } = string.Empty;

        public string Range
        {
            get
            {
                if (IsCalRangeOK())
                {
                    return MinCalRange + " TO " + MaxCalRange + RangeUnits;
                }
                else
                {
                    return string.Empty;
                }
            }
        
        }
        public string SignalLevel {
            get { return signalLevel; }
            set
            {
                string cleanedValue = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : Regex.Replace(value, @"[\s\-_]", "", RegexOptions.IgnoreCase).ToLower();

                var validValues = new HashSet<string> { "120vac", "24vdc" };

                if (!string.IsNullOrEmpty(cleanedValue) && validValues.Contains(cleanedValue))
                {
                    signalLevel = cleanedValue;
                }
                else
                {
                    signalLevel = string.Empty;
                }
            }

        }

        public string System { get; set; } = string.Empty;
        public string SystemType { get; set; } = string.Empty;

        public IEnumerable<string> FourLineDescription
        {
            get
            {
                List<string> descriptions = SplitDescriptionToFourLines();
                while (descriptions.Count < 4)
                {
                    descriptions.Add(string.Empty);
                }
                return descriptions;
            }
        }

        public bool IsMotorSD
        {
            get
            {
                return Regex.IsMatch(InstrumentType, @"motor[-_]sd", RegexOptions.IgnoreCase);
            }
        }

        public bool IsMotorStart => EndsWith("ST");
        public bool IsMotorStop => EndsWith("SP");
        public bool IsMotorStartStop => IsMotorStart || IsMotorStop;

        private bool EndsWith(string suffix)
        {
            return Tag.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
        }

        private bool EndsWith(params string[] suffixes)
        {
            bool endsWith = suffixes.Any(suffix => Tag.EndsWith(suffix, StringComparison.OrdinalIgnoreCase));
            return endsWith;
        }


        private bool IsCalRangeOK()
        {
            return MinCalRange != DBLoopData.CALERROR.ToString() && MaxCalRange != DBLoopData.CALERROR.ToString();
        }


        private List<string> SplitDescriptionToFourLines()
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
            
            int maximumLineLength = 20; 
            return Regex.Matches(Description, @"(.{1," + maximumLineLength +@"})(?:\s|$)")
                .Cast<Match>()
                // regex gives whitespace at the end, it is not supposed to but I'm not going to troubleshoot it
                // Trim is a simple solution
                .Select(m => m.Value.Trim())
                .Take(4)
                .ToList();
        }

    }
}
