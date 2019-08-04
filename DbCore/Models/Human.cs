using System;
using System.Collections.Generic;
using LibCore;

namespace DbCore.Models
{
    public partial class Human : IPrimaryKeyContainer, IAuthAffectedModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Sex { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public bool? RightHand { get; set; }

        public virtual Users User { get; set; }
    }
}
