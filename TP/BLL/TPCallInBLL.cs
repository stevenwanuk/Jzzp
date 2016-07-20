using EntitiesDABL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP.BLL
{
    public class TPCallInBLL
    {

        public void UpdateCellNumber(string telno, long tpCallInId, long tpBillRefId)
        {

            using (var entities = new JZZPEntities())
            {

                var tpCallIn = entities.TPCallIns.Where(i => i.CallInId == tpCallInId).FirstOrDefault();
                if (tpCallIn != null)
                {
                    tpCallIn.CellNumber = telno;

                    //looking for useraddress
                    var billRef = entities.TPBillRefs.Where(i => i.BillRefId == tpBillRefId).FirstOrDefault();
                    if (billRef != null)
                    {

                        var query = entities.TPBillRefs.Where(i => i.TPCallIn != null
                            && i.TPCallIn.CellNumber == telno && i.BillRefId != tpBillRefId)
                            .OrderByDescending(i => i.BillRefId).FirstOrDefault();
                                    
                        if (query != null)
                        {
                            billRef.UserId_FK = query.UserId_FK;
                            billRef.AddressId_FK = query.AddressId_FK;
                            billRef.DeliverFeeOrigin = query.DeliverFeeOrigin;
                            billRef.DeliverFee = query.DeliverFeeOrigin;
                            billRef.DeliverMiles = query.DeliverMiles;
                        }
                        
                    }
                    entities.SaveChanges();
                }
            }

        }
    }
}
