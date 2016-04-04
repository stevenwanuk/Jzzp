using EntitiesDABL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL;

namespace TP.BLL
{
    public class TPUserBLL
    {
        public TPUser GeTPUserById(Guid tPUserId)
        {
            TPUser result = null;
            using (var entities = new JZZPEntities())
            {

                result = new TPUserDAL(entities).GeTPUserById(tPUserId);
            }

            return result;
        }
    }
}
