namespace LoopDataAccessLayer
{
    public interface ILoopTagData
    {
        string InstrumentType { get; set; }
        string IOType { get; set; }
        string Tag { get; set; }
        public string System { get; set; }
    }
}