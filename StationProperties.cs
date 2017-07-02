using System;

namespace bikeshare
{
    public class StationProperties
    {
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressStreet { get; set; }
        public string AddressZipCode { get; set; }
        public int BikesAvailable { get; set; }
        public TimeSpan CloseTime { get; set; }
        public int DocksAvailable { get; set; }
        public DateTimeOffset? EventEnd { get; set; }
        public DateTimeOffset? EventStart { get; set; }
        public bool IsEventBased { get; set; }
        public bool IsVirtual { get; set; }
        public string KioskConnectionStatus { get; set; }
        public int KioskId { get; set; }
        public string KioskPublicStatus { get; set; }
        public string KioskStatus { get; set; }
        public string Name { get; set; }
        public TimeSpan OpenTime { get; set; }
        public string PublicText { get; set; }
        public string TimeZone { get; set; }
        public int TotalDocks { get; set; }
        public int TrikesAvailable { get; set; }
    }
}
