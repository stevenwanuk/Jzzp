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
            if (user.UserId != Guid.Empty)
            {
                var count = Entities.TPUsers.Where(i => i.UserId == user.UserId).Count();
                if (count <= 0)
                {
                    Entities.TPUsers.Add(user);
                }
                else
                {

                    //msserver 2000 doesn't work with top (2).....
                    Entities.TPUsers.Attach(user);
                    Entities.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    
                }
                Entities.SaveChanges();
            }
        }
    }
}
