

namespace GGuerra.Cardamatic.CardReader.Common.Desfire
{
    public class DesfireCardVersion
    {
        private const int YearReference = 2000;
        public string Uid { get; set; }
        public string Batch { get; set; }
        public string HardwareVersion { get; set; } = string.Empty;

        public string SoftwareVersion { get; set; } = string.Empty;

        public string ProductionDate => $"Week {ProductionWeek}, Year {YearReference + ProductionYear}";

        public int ProductionWeek { get; set; }

        public int ProductionYear { get; set; }
    }
}
