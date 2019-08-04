using System.Collections.Generic;

namespace LibCore
{
    public interface IBatchModel <T> where T: class, new()
    {
        ICollection<T> Collection { get; }
    }
}
