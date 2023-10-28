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
        public bool IsTE => IsTagType("TE");

        public bool IsEmptyIOType => string.IsNullOrEmpty(IOType) || IOType == "---";

        public bool IsBPCS => IsSystemType("BPCS");
        public bool IsSIS => IsSystemType("SIS");
        public bool IsESDLoop => IsSystemType("ESD_RELAY");

        public bool IsESDButton => IsInstrumentTypeMatch("esd") && IsInstrumentTypeMatch("button", "btn");

        public bool IsValve => IsEmptyIOType && IsInstrumentTypeMatch("ball", "gate", "globe", "butterfly");

        public bool IsSolenoid => IsDO && IsInstrumentTypeMatch("SOLENOID");
        public bool IsMotor => IsDO && IsInstrumentTypeMatch("MOTOR-SD");
        public bool IsBeacon => IsDO && (IsInstrumentTypeMatch("beacon", "light", "indicator", "strobe") || IsTagType("XL"));
        public bool IsHorn => IsDO && (IsInstrumentTypeMatch("horn") || IsTagType("XH"));


        public bool IsIOType(string ioType)
        {
            return IOType.Equals(ioType, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsSystemType(string systemType)
        {
            return SystemType.Equals(systemType, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsInstrumentTypeMatch(string targetInstrumentType)
        {
            string escapedTarget = Regex.Escape(targetInstrumentType);
            string cleanedTargetInstrumentType = Regex.Replace(escapedTarget, @"[-_]", "[-_]");
            string pattern = $"^.*{cleanedTargetInstrumentType}.*$";

            return Regex.IsMatch(InstrumentType, pattern, RegexOptions.IgnoreCase);
        }

        public bool IsInstrumentTypeMatch(params string[] targetInstrumentTypes)
        {
            return targetInstrumentTypes.Any(targetType => IsInstrumentTypeMatch(targetType));
        }

        public bool EndsWith(string suffix)
        {
            return Tag.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
        }

        public bool EndsWith(params string[] suffixes)
        {
            bool endsWith = suffixes.Any(suffix => Tag.EndsWith(suffix, StringComparison.OrdinalIgnoreCase));
            return endsWith;
        }

        public bool TagContains(string targetTagContainsThis)
        {
            return Tag.Contains(targetTagContainsThis, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsTagType(string targetTagType)
        {
            string[] tagComponents = Tag.Split('-');
            string tagType = tagComponents[0];
            return tagType.Contains(targetTagType, StringComparison.OrdinalIgnoreCase);
        }
    }

}
