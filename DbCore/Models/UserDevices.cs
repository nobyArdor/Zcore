using System;
using System.Collections.Generic;
using LibCore;

namespace DbCore.Models
{
    public partial class UserDevices : IPrimaryKeyContainer, IAuthAffected
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long DeviceId { get; set; }

        public virtual Devices Device { get; set; }
        public virtual Users User { get; set; }
    }
}
