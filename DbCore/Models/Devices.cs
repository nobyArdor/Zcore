using System.Collections.Generic;
using LibCore;

namespace DbCore.Models
{
    public partial class Devices : IPrimaryKeyContainer
    {
        public Devices()
        {
            DeviceInfo = new HashSet<DeviceInfo>();
            UserDevices = new HashSet<UserDevices>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string PhotoLink { get; set; }

        public virtual ICollection<DeviceInfo> DeviceInfo { get; set; }
        public virtual ICollection<UserDevices> UserDevices { get; set; }
    }
}
