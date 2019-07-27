using System;
using System.Collections.Generic;
using LibCore;

namespace DbCore.Models
{
    public partial class Users : IPrimaryKeyContainer
    {
        public Users()
        {
            Human = new HashSet<Human>();
            NotifyRecords = new HashSet<NotifyRecords>();
            PersonalInfo = new HashSet<PersonalInfo>();
            SensorData = new HashSet<SensorData>();
            Sessions = new HashSet<Sessions>();
            UserDevices = new HashSet<UserDevices>();
            UserRelationsUserDest = new HashSet<UserRelations>();
            UserRelationsUserSource = new HashSet<UserRelations>();
        }

        public long Id { get; set; }
        public byte[] RegSecret { get; set; }
        public DateTime LastLogin { get; set; }

        public virtual ICollection<Human> Human { get; set; }
        public virtual ICollection<NotifyRecords> NotifyRecords { get; set; }
        public virtual ICollection<PersonalInfo> PersonalInfo { get; set; }
        public virtual ICollection<SensorData> SensorData { get; set; }
        public virtual ICollection<Sessions> Sessions { get; set; }
        public virtual ICollection<UserDevices> UserDevices { get; set; }
        public virtual ICollection<UserRelations> UserRelationsUserDest { get; set; }
        public virtual ICollection<UserRelations> UserRelationsUserSource { get; set; }
    }
}
