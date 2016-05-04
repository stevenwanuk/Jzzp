using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDABL;

namespace TP.BLL
{
    public class TPAddressBLL
    {
        public TPAddress GetAddressByPostCode(string postCode)
        {

            TPAddress result = null;
            if (!string.IsNullOrEmpty(postCode))
            {
                postCode = postCode.Replace(" ", "");
                using (var entities = new JZZPEntities())
                {

                    //look history first
                    var userAddress =
                        entities.TPUserAddresses.FirstOrDefault(
                            i =>
                                i.Postcode != null &&
                                i.Postcode.Equals(postCode, StringComparison.CurrentCultureIgnoreCase));

                    if (userAddress != null)
                    {
                        result = AutoMapper.Mapper.Map<TPUserAddress, TPAddress>(userAddress);
                    }
                    else
                    {
                        result =
                            entities.TPAddresses.FirstOrDefault(i => i.Postcode != null &&
                                    i.Postcode.Equals(postCode, StringComparison.CurrentCultureIgnoreCase));
                    }
                }
                
            }
            return result;
        }
    }
}
