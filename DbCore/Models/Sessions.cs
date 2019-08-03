using System;
using System.Collections.Generic;
using LibCore;

namespace DbCore.Models
{
    public partial class Sessions : IPrimaryKeyContainer, IAuthAffected
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expire { get; set; }

        public virtual Users User { get; set; }
    }
}
