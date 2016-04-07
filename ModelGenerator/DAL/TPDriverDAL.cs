using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesDABL.DAL
{
    public class TPDriverDAL : DALBase
    {
        public TPDriverDAL(JZZPEntities entities)
        {
            this.Entities = entities;
        }

        public IQueryable<TPDriver> GetDriversIfAvailable(bool? isAvailable = null)
        {

            var query = Entities.TPDrivers;
            if (isAvailable != null)
            {
                //TODO
                //query.Where(i => i)
            }
            return query;
        }
    }
}
