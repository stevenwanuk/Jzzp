using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDABL;

namespace TP.BLL
{
    public class DBBLL
    {

        public bool DBTestBillRef()
        {

            bool result = true;
            using (var entities = new JZZPEntities())
            {
                var i = entities.TPBillRefs.Count();
                if (i > 0)
                    result = entities.TPBillRefs.FirstOrDefault() == null;
            }
            return result;
        }
    }
}
