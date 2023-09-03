namespace LoopDataAccessLayer
{
    public interface ILoopTagData
    {
        string InstrumentType { get; set; }
        string IOType { get; set; }
        bool IsAI { get; }
        bool IsAO { get; }
        bool IsBPCS { get; }
        bool IsDI { get; }
        bool IsDO { get; }
        bool IsEmptyIOType { get; }
        bool IsMotor { get; }
        bool IsSIS { get; }
        bool IsSoft { get; }
        bool IsSolenoid { get; }
        bool IsValve { get; }
        string System { get; set; }
        string SystemType { get; set; }
        string Tag { get; set; }

        bool EndsWith(string suffix);
        bool IsInstrumentTypeMatch(string instrumentType);
        bool IsIOType(string ioType);
        bool IsSystemType(string systemType);
    }
}