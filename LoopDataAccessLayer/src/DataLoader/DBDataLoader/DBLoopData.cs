namespace LoopDataAccessLayer
{
    public class DBLoopData
    {
        public const int CALERROR = -9999;
        public const int RACKERROR = -99;
        
        private string failPosition;
        private string lolo, lo, hi, hihi, hiControl, loControl;

        public DBLoopData()
        {
            failPosition = string.Empty;
            lolo = lo = hi = hihi = hiControl = loControl = string.Empty;
        }

        public string Tag { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
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

        public string FailPosition
        {
            get { return failPosition; } 
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
        public string LoLoAlarm
        {
            get => lolo;
            set { lolo = BuildAlarmString("LL", value); }
        }

        public string LoAlarm
        {
            get => lo;
            set { lo = BuildAlarmString("L", value); }
        }

        public string HiAlarm
        {
            get => hi;
            set { hi = BuildAlarmString("H", value); }
        }

        public string HiHiAlarm
        {
            get => hihi;
            set { hihi = BuildAlarmString("HH", value); }
        }

        public string LoControl
        {
            get => loControl;
            set { loControl = BuildAlarmString("LC", value); }
        }

        public string HiControl
        {
            get => hiControl;
            set { hiControl = BuildAlarmString("HC", value); }
        }
        
        public string IoPanel { get; set; } = string.Empty;

        private string BuildAlarmString(string prefix, string value)
        {
            return (string.IsNullOrEmpty(value) || value == "---")
                ? string.Empty
                : prefix + "=" + value;
        }

        // additional fields that may be useful
        public string LoopNo { get; set; } = string.Empty;
    }
}
