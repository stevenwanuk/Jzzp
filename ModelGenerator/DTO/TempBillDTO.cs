using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntitiesDABL.DTO
{
    public class TempBillDTO
    {
        public TempBillDTO()
        {
            TempBillItems = new List<TempBillItem>();
        }

        public TempBill TempBill { get; set; }
        public ICollection<TempBillItem> TempBillItems { get; set; }
    }
}
