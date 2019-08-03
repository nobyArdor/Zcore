using System;
using System.Collections.Generic;
using LibCore;

namespace DbCore.Models
{
    public partial class PersonalInfo : IPrimaryKeyContainer, IAuthAffected
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public byte[] Value { get; set; }

        public virtual Users User { get; set; }
    }
}
