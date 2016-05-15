using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL;
using EntitiesDABL.DAL;
using Jzzp.Enum;
using TP.ModelView;

namespace TP.BLL
{
    public class TPDriverBLL
    {

        public List<TPDriver> GetDriversIfAvailable(bool? isAvailable = null)
        {
            List<TPDriver> result = new List<TPDriver>();
            using (var entities = new JZZPEntities())
            {

                result = new TPDriverDAL(entities).GetDriversIfAvailable(isAvailable).ToList();
            }

            return result;
        }

        public long GetDeliverCount(long driverId)
        {
            var result = 0;
            using (var entities = new JZZPEntities())
            {

                result = entities.TPDelivers.Where(i => i.DriverId_FK == driverId).Count();
            }
            return result;
        }

        public void SaveOrUpdate(TPDriver driver)
        {
            using (var entities = new JZZPEntities())
            {
                /**
                var count = entities.TPDrivers.Where(i => i.DriverId == driver.DriverId).Count();
                if (count <= 0)
                {
                    entities.TPDrivers.Add(driver);
                }
                else
                {

                    
                    entities.TPDrivers.Attach(driver);
                    entities.Entry(driver).State = System.Data.Entity.EntityState.Modified;

                }
                **/
                entities.TPDrivers.AddOrUpdate(driver);
                entities.SaveChanges();


            }
        }

        public void Remove(long driverId)
        {
            using (var entities = new JZZPEntities())
            {
                var driver = entities.TPDrivers.Where(i => i.DriverId == driverId).ToList().FirstOrDefault();
                if (driver != null)
                {
                    entities.TPDrivers.Remove(driver);
                    entities.SaveChanges();
                }
            }
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

                    billRef.TPDeliver = deliver;
                    TPBillRefBLL.BillRefStatusProcess(billRef, (int)BillRefStatus.Distributed);
                    entities.SaveChanges();
                }
            }
        }
    }
}
