using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesDABL.DTO
{
    public class OrderHistoryDTO
    {

        public string MenuID { get; set; }
        public string MenuName { get; set; }

        public decimal? TotalAmount { get; set; }
        public int TotalCount { get; set; }
    }
}
