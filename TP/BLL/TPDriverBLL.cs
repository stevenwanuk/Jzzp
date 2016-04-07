using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL;
using EntitiesDABL.DAL;
using TP.ModelView;

namespace TP.BLL
{
    public class TPDriverBLL
    {
        public ICollection<TPDriver> GetDriversIfAvailable(bool? isAvailable = null)
        {
            ICollection<TPDriver> result = new List<TPDriver>();
            using (var entities = new JZZPEntities())
            {

                result = new TPDriverDAL(entities).GetDriversIfAvailable(isAvailable).ToList();
            }

            return result;
        }

        public void SaveOrUpdate(long driverId, long billRefId)
        {
            using (var entities = new JZZPEntities())
            {

                var billRef = new TPBillRefDAL(entities).GetTPBillRefById(billRefId).Include(i => i.TPDeliver).ToList().FirstOrDefault();
                if (billRef != null)
                {
                    if (billRef.TPDeliver != null )
                    {
                        if (billRef.TPDeliver.DriverId_FK == driverId)
                        {
                            return;
                        }
                        else
                        {
                            entities.TPDelivers.Remove(billRef.TPDeliver);
                        }
                    }
                    var deliver = new TPDeliver()
                    {
                        DriverId_FK = driverId,
                        StartDate = DateTime.Now,
                        Status = 1
                    };
                    entities.TPDelivers.Add(deliver);


                    billRef.DeliverId_FK = deliver.DeliverId;

                    entities.SaveChanges();
                }
            }
        }
    }
}
