using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbCore.Models;

namespace Zcore.NetModels
{
    public class SensorMarkedData : SensorData
    {
        public bool IsNotify { get; set; }
    }
}
