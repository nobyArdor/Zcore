using System.Collections.Generic;

namespace LibCore
{
    public interface IPostBatchModel
    {
        IPostResponseModel[] Ids { get; }
    }
}
