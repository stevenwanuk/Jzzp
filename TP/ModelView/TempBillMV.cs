using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesDABL;
using TP.Common;

namespace TP.ModelView
{
    public class TempBillMV : BindableBase
    {

        public static TempBillMV Mapper(TempBill entity)
        {
            return AutoMapper.Mapper.Map<TempBill, TempBillMV>(entity);
        }

        private string _billId;
        public string BillID
        {
            get { return _billId; }
            set { SetProperty(ref _billId, value, "BillID"); }
        }
        public string TableID { get; set; }
        public Nullable<int> SubTableID { get; set; }

        private string _tableName;
        public string TableName
        {
            get { return _tableName; }
            set { SetProperty(ref _tableName, value, "TableName"); }
        }
        public string TableFullName { get; set; }
        public string TableAreaID { get; set; }
        public string TableAreaName { get; set; }

        private string _tableTypeId;

        public string TableTypeID
        {
            get { return _tableTypeId; }
            set { SetProperty(ref _tableTypeId, value, "TableTypeID"); }
        }

        public string TableTypeName { get; set; }
        public string MemberID { get; set; }
        public string MemberCardID { get; set; }
        public string MemberName { get; set; }
        public Nullable<int> PeopleCount { get; set; }
        public string ChargePersonID { get; set; }
        public string ChargePerson { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreatePersonID { get; set; }
        public string CreatePerson { get; set; }
        public Nullable<bool> IsCheckOuting { get; set; }
        public Nullable<System.DateTime> CheckOutTime { get; set; }
        public string CheckOutPersonID { get; set; }
        public string CheckOutPerson { get; set; }
        public Nullable<bool> CheckOutNull { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
        public string DeletePersonID { get; set; }
        public string DeletePerson { get; set; }
        public string DiscountID { get; set; }
        public string DiscountName { get; set; }
        public Nullable<decimal> DiscountRate { get; set; }
        public string DiscountPersonID { get; set; }
        public string DiscountPerson { get; set; }


        private decimal? _sumOfConsume;

        public Nullable<decimal> SumOfConsume
        {
            get { return _sumOfConsume; }
            set { SetProperty(ref _sumOfConsume, value, "SumOfConsume"); }
        }

        private decimal? _serviceRate;
        public Nullable<decimal> ServiceRate
        {
            get { return _serviceRate; }
            set { SetProperty(ref _serviceRate, value, "ServiceRate"); }
        }

        private decimal? _sumOfService;
        public Nullable<decimal> SumOfService
        {
            get { return _sumOfService; }
            set { SetProperty(ref _sumOfService, value, "SumOfService"); }
        }

        private decimal? _sumForDiscount;
        public Nullable<decimal> SumForDiscount
        {
            get { return _sumForDiscount; }
            set { SetProperty(ref _sumForDiscount, value, "SumForDiscount"); }
        }

        private decimal? _sumOfCarry;

        public Nullable<decimal> SumOfCarry
        {
            get { return _sumOfCarry; }
            set { SetProperty(ref _sumOfCarry, value, "SumOfCarry"); }
        }

        private decimal? _sumToPay;

        public Nullable<decimal> SumToPay
        {
            get { return _sumToPay; }
            set { SetProperty(ref _sumToPay, value, "SumToPay"); }
        }
        public Nullable<decimal> SumPaid { get; set; }
        public Nullable<decimal> SumInCash { get; set; }
        public Nullable<decimal> SumOfInvoice { get; set; }
        public Nullable<decimal> SumOfCashPaid { get; set; }
        public Nullable<decimal> SumOfCashBack { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public string BillPeriod { get; set; }
        public Nullable<int> BillYear { get; set; }
        public Nullable<int> BillMonth { get; set; }
        public Nullable<int> BillDay { get; set; }
        public Nullable<bool> IsArchived { get; set; }
        public Nullable<bool> IsUploaded { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> PrintCount { get; set; }
        public Nullable<decimal> ReduceMantissa { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string WorkStationID { get; set; }
        public string WorkStationName { get; set; }
        public string Remark { get; set; }
        public string OriginalID { get; set; }
        public string ShiftID { get; set; }
    }
}
