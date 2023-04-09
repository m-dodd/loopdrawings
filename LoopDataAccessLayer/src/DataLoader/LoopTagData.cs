namespace LoopDataAccessLayer
{
    public class LoopTagData : ILoopTagData
    {
        public string Tag { get; set; } = string.Empty;
        public string IOType { get; set; } = string.Empty;
        public string InstrumentType { get; set; } = string.Empty;
        public string System { get; set; } = string.Empty;
        public string SystemType { get; set; } = string.Empty;
    }
}
