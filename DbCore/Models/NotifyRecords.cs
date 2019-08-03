using System;
using System.Collections.Generic;
using LibCore;

namespace DbCore.Models
{
    public partial class NotifyRecords : IPrimaryKeyContainer, IAuthAffected
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Notification { get; set; }
        public int State { get; set; }

        public virtual Users User { get; set; }
    }
}
