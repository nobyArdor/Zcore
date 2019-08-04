using System.Collections.Generic;
using DbCore.Models;

namespace Zcore.NetModels
{
    public class SensorDataBatchModel
    {
        public ICollection<SensorData> Collection { get; set; }
    }
}
