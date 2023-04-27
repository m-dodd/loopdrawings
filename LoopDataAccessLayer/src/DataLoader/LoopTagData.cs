using System.Text.RegularExpressions;

namespace LoopDataAccessLayer
{
    public class LoopTagData : ILoopTagData
    {
        private string tag = string.Empty;
        private string ioType = string.Empty;
        private string instrumentType = string.Empty;
        private string system = string.Empty;
        private string systemType = string.Empty;

        public string Tag
        {
            get => tag;
            set => tag = value ?? string.Empty;
        }

        public string IOType
        {
            get => ioType;
            set => ioType = value ?? string.Empty;
        }

        public string InstrumentType
        {
            get => instrumentType;
            set => instrumentType = value ?? string.Empty;
        }

        public string System
        {
            get => system;
            set => system = value ?? string.Empty;
        }

        public string SystemType
        {
            get => systemType;
            set => systemType = value ?? string.Empty;
        }

        public bool IsAI => IsIOType("AI");
        public bool IsAO => IsIOType("AO");
        public bool IsDI => IsIOType("DI");
        public bool IsDO => IsIOType("DO");
        public bool IsSoft => IsIOType("SOFT");

        public bool IsEmptyIOType => string.IsNullOrEmpty(IOType) || IOType == "---";

        public bool IsBPCS => IsSystemType("BPCS");
        public bool IsSIS => IsSystemType("SIS");

        public bool IsValve => IsEmptyIOType && Regex.IsMatch(InstrumentType, @"ball|gate|globe|butterfly", RegexOptions.IgnoreCase);
        public bool IsSolenoid => IsDO && IsInstrumentType("SOLENOID");
        public bool IsMotor => IsDO && IsInstrumentType("MOTOR-SD");


        public bool IsIOType(string ioType)
        {
            return IOType.Equals(ioType, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsSystemType(string systemType)
        {
            return SystemType.Equals(systemType, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsInstrumentType(string instrumentType)
        {
            string pattern = @"^\w+[-_]?.*$";

            return Regex.IsMatch(InstrumentType, pattern, RegexOptions.IgnoreCase);
        }

        public bool EndsWith(string suffix)
        {
            return Tag.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
        }

        public bool TagContains(string value)
        {
            return Tag.Contains(value, StringComparison.OrdinalIgnoreCase);
        }
    }

}
