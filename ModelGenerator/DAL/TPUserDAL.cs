using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EntitiesDABL.DAL
{
    public class TPUserDAL : DALBase
    {

        public TPUserDAL(JZZPEntities entities)
        {
            this.Entities = entities;
        }

        public TPUser GeTPUserById(Guid tPUserId)
        {

            var query = Entities.TPUsers.Where(i => i.UserId == tPUserId).ToList().FirstOrDefault();
            return query;
        }

        public void Save(TPUser user)
        {
            Entities.TPUsers.AddOrUpdate(user);
        }
    }
}
