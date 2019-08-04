using System;
using System.Collections.Generic;
using LibCore;

namespace DbCore.Models
{
    public partial class UserRelations : IPrimaryKeyContainer, IAuthAffectedModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long UserDestId { get; set; }
        public string Name { get; set; }

        public virtual Users UserDest { get; set; }
        public virtual Users UserSource { get; set; }
    }
}
