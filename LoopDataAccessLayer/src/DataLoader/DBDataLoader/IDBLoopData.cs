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
        string Slot { get; set; }
        string System { get; set; }
        string Tag { get; set; }
    }
}