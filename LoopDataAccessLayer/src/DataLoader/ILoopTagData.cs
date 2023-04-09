namespace LoopDataAccessLayer
{
    public interface ILoopTagData
    {
        string InstrumentType { get; set; }
        string IOType { get; set; }
        string Tag { get; set; }
        string System { get; set; }
        string SystemType { get; set; }
    }
}