using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using EntitiesDABL;
using TP.Common;

namespace TP.ModelView
{
    public class BillItemMV : BindableBase
    {
        public static BillItemMV Mapper(BillItem entity)
        {
            return AutoMapperUtils.GetMapper().Map<BillItem, BillItemMV>(entity);
        }

        private string _billItemId;
        public string BillItemID
        {
            get { return _billItemId; }
            set { SetProperty(ref _billItemId, value, "BillItemID"); }
        }
        public string BillID { get; set; }
        public string MenuID { get; set; }

        private string _menuName;
        public string MenuName {
            get { return _menuName; }
            set { SetProperty(ref _menuName, value, "MenuName"); }
        }
        public string MenuUnitID { get; set; }
        public string MenuUnitName { get; set; }
        public string MenuTypeID { get; set; }

        private string _menuTypeName;
        public string MenuTypeName {
            get { return _menuTypeName; }
            set { SetProperty(ref _menuTypeName, value, "MenuTypeName"); }
        }
        public string DepartID { get; set; }
        public string DepartName { get; set; }
        public string DepartTypeID { get; set; }
        public string DepartTypeName { get; set; }

        private decimal? _amountOrder;


        public Nullable<decimal> AmountOrder
        {
            get { return _amountOrder; }
            set { SetProperty(ref _amountOrder, value, "AmountOrder"); }
        }

        private decimal? _amountOnTable;

        public Nullable<decimal> AmountOnTable
        {
            get { return _amountOnTable; }
            set { SetProperty(ref _amountOnTable, value, "AmountOnTable"); }
        }

        private decimal? _amountCancel;
        public Nullable<decimal> AmountCancel {
            get { return _amountCancel; }
            set { SetProperty(ref _amountCancel, value, "AmountCancel"); }
        }

        public decimal? Amount => (_amountOrder ?? 0) - (_amountCancel ?? 0);

        private decimal? _menuPrice;

        public Nullable<decimal> MenuPrice
        {
            get { return _menuPrice; }
            set { SetProperty(ref _menuPrice, value, "MenuPrice"); }
        }

        private decimal? _menuPrice2;
        public Nullable<decimal> MenuPrice2
        {
            get { return _menuPrice2; }
            set { SetProperty(ref _menuPrice2, value, "MenuPrice2"); }
        }

        private decimal? _sumOfConsume;

        public Nullable<decimal> SumOfConsume
        {
            get { return _sumOfConsume; }
            set { SetProperty(ref _sumOfConsume, value, "SumOfConsume"); }
        }


        private decimal? _sumForDiscount;

        public Nullable<decimal> SumForDiscount
        {
            get { return _sumForDiscount; }
            set { SetProperty(ref _sumForDiscount, value, "SumForDiscount"); }
        }

        public decimal? SUMNet => (_sumOfConsume ?? 0) - (_sumForDiscount ?? 0);

        private decimal? _sumOfService;
        public Nullable<decimal> SumOfService {
            get { return _sumOfService; }
            set { SetProperty(ref _sumOfService, value, "SumOfService"); }
        }
        public Nullable<decimal> SumOfCookWay { get; set; }
        public string CookWayID { get; set; }
        public string CookWay { get; set; }
        public Nullable<decimal> CookWayPrice { get; set; }
        public string MenuPart { get; set; }
        public string Request { get; set; }
        public string TasteType { get; set; }
        public string CreatePersonID { get; set; }
        public string CreatePerson { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> ServingState { get; set; }
        public Nullable<bool> IsSentMenu { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsSpecialPrice { get; set; }
        public Nullable<bool> IsDiscount { get; set; }
        public Nullable<decimal> DiscountRate { get; set; }
        public Nullable<bool> IsPrinted { get; set; }
        public Nullable<System.DateTime> PrintTime { get; set; }
        public Nullable<bool> IsOnTable { get; set; }
        public Nullable<System.DateTime> OnTableTime { get; set; }
        public string MenuSetID { get; set; }
        public string MenuSetName { get; set; }
        public string MenuSetPrefix { get; set; }
        public string MenuSetItemID { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> TaxRate { get; set; }
        public Nullable<decimal> SumOfTax { get; set; }
    }
}
