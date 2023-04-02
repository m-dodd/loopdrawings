namespace LoopDataAccessLayer
{
    public interface ISDKData
    {
        string InputTag { get; set; }
        string OutputTag { get; set; }
        string OutputDescription { get; set; }
        string ParentTag { get; set; }
        string SdAction1 { get; set; }
        string SdAction2 { get; set; }
        string SdGroup { get; set; }
    }
}