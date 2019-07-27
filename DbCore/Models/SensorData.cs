using System;
using System.Collections.Generic;
using LibCore;

namespace DbCore.Models
{
    public partial class SensorData : IPrimaryKeyContainer
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Type { get; set; }
        public decimal Value { get; set; }

        public virtual Users User { get; set; }
    }
}
