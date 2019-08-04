using System.Collections.Generic;

namespace Zcore.NetModels
{
    public class BatchModel<T> where T: class, new()
    {
        public T[] Collection { get; set; }
    }
}
