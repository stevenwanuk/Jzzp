using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Jzzp.DAL
{
    public class TestDAL
    {
        public static void Test()
        {
            try
            {
                var i = new TPCallInDAL().GetCallInsByBillStatus();
                Console.WriteLine(i.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }
    }
}
