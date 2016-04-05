using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesDABL.DTO
{
    public class BillDTO
    {

        public BillDTO()
        {
            BillItems = new List<BillItem>();
        }

        public Bill Bill { get; set; }
        public ICollection<BillItem> BillItems { get; set; }

    }
}
