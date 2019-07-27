using LibCore;

namespace DbCore.Models
{
    public partial class DeviceInfo : IPrimaryKeyContainer
    {
        public long Id { get; set; }
        public long DeviceId { get; set; }
        public int Type { get; set; }
        public string Svalue { get; set; }
        public decimal Nvalue { get; set; }

        public virtual Devices Device { get; set; }
    }
}
