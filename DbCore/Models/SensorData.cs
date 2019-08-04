using System;
using LibCore;

namespace DbCore.Models
{
    public partial class SensorData : IPrimaryKeyContainer, IAuthAffectedModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Type { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }

        public virtual Users User { get; set; }
    }
}
