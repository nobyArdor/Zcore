using System.Collections.Generic;
using LibCore;

namespace Zcore.NetModels
{
    public class PostBatchModel : IPostBatchModel
    {
        public IPostResponseModel[] Ids { get; set; }
    }
}
