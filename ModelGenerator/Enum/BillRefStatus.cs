using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jzzp.Enum
{
    public enum BillRefStatus
    {
        Initial = 0,
        Started = 2,
        Distributed = 4,
        OnDelivery = 6,
        Done = 10,
        Cancelled = 11
    }
}
