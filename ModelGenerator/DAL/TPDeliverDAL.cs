using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL;
using EntitiesDABL.DAL;

namespace Jzzp.DAL
{
    public class TPDeliverDAL : DALBase
    {

        public TPDeliverDAL(JZZPEntities entities)
        {
            this.Entities = entities;
        }

    }
}
