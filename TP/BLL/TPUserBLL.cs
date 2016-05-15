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

        public List<TPUser> GetUsersWithParameters(TPUser qUser, TPUserAddress qAddress, TPUserCell qUserCell)
        {
            var result = new List<TPUser>();
            using (var entities = new JZZPEntities())
            {
                var query = entities.TPUsers.AsQueryable();
                if (qUser != null)
                {
                    if (qUser.UserId != Guid.Empty)
                    {
                        query = query.Where(i => i.UserId == qUser.UserId);
                    }
                    if (!string.IsNullOrEmpty(qUser.FirstName))
                    {
                        query = query.Where(i => qUser.FirstName.Equals(i.FirstName, StringComparison.CurrentCultureIgnoreCase));
                    }
                    if (!string.IsNullOrEmpty(qUser.LastName))
                    {
                        query = query.Where(i => qUser.LastName.Equals(i.LastName, StringComparison.CurrentCultureIgnoreCase));
                    }
                }
                if (qAddress != null)
                {
                    if (!string.IsNullOrEmpty(qAddress.Postcode))
                    {
                        query =
                            query.Where(
                                i =>
                                    i.TPUserAddress.Count(
                                        j =>
                                            qAddress.Postcode.Equals(j.Postcode,
                                                StringComparison.CurrentCultureIgnoreCase)) > 0);
                    }
                }

                if (qUserCell != null)
                {
                    if (!string.IsNullOrEmpty(qUserCell.CellNumber))
                    {
                        query =
                            query.Where(
                                i =>
                                    i.TPUserCell.Count(
                                        j =>
                                            qUserCell.CellNumber.Equals(j.CellNumber,
                                                StringComparison.CurrentCultureIgnoreCase)) > 0);
                    }
                }

                result = query.ToList();
            }
            return result;
        }
    }
}
