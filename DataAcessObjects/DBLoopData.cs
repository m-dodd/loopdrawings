using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObjects
{

    public class DBLoopData
    {
        public string Tag { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string JB1Tag { get; set; }
        public string JB2Tag { get; set; }
        public int Rack { get; set; }
        public int Slot { get; set; }
        public int Channel { get; set; }
        public string DrawingNumber { get; set; }
        public decimal MinCalRange { get; set; }
        public decimal MaxCalRange { get; set; }
        public string LoLoAlarm { get; set; }
        public string LoAlarm { get; set; }
        public string HiAlarm { get; set; }
        public string HiHiAlarm { get; set; }
        public string LoControl { get; set; }
        public string HiControl { get; set; }

        public Dictionary<string, string> ToDict()
        {
            return new Dictionary<string, string>
            {
                { "Tag", Tag },
                { "Description", Description },
                { "Manufacturer", Manufacturer },
                { "Model", Model },
                { "JB1Tag", JB1Tag },
                { "JB2Tag", JB2Tag },
                { "Rack", Rack.ToString() },
                { "Slot", Slot.ToString() },
                { "Channel", Channel.ToString() },
                { "DrawingNumber", DrawingNumber },
                { "MinCalRange", MinCalRange.ToString() },
                { "MaxCalRange", MaxCalRange.ToString() },
                { "LoLoAlarm", LoLoAlarm },
                { "LoAlarm", LoAlarm },
                { "HiAlarm", HiAlarm },
                { "HiHiAlarm", HiHiAlarm },
                { "LoControl", LoControl },
                { "HiControl", HiControl },
            };
        }

        public override string ToString()
        {
            return string.Join(System.Environment.NewLine, ToDict().Select(x => x.Key + ": " + x.Value?.ToString()));
        }
    }
}
