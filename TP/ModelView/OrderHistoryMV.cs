using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL;
using EntitiesDABL.DTO;
using TP.Common;

namespace TP.ModelView
{
    public class OrderHistoryMV : BindableBase
    {
        public static OrderHistoryMV Mapper(OrderHistoryDTO entity)
        {
            return AutoMapperUtils.GetMapper().Map<OrderHistoryDTO, OrderHistoryMV>(entity);
        }

        private string _menuId;
        public string MenuID
        {
            get { return _menuId; }
            set { SetProperty(ref _menuId, value); }
        }

        private string _menuName;

        public string MenuName
        {
            get { return _menuName; }
            set { SetProperty(ref _menuName, value); }
        }

        private decimal? _totalAmount;
        public decimal? TotalAmount {
            get { return _totalAmount; }
            set { SetProperty(ref _totalAmount, value); }
        }

        private int _totalCount;
        public int TotalCount {
            get { return _totalCount; }
            set { SetProperty(ref _totalCount, value); }
        }

    }
}
