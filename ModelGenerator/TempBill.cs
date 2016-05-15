//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesDABL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TempBill
    {
        public string BillID { get; set; }
        public string TableID { get; set; }
        public Nullable<int> SubTableID { get; set; }
        public string TableName { get; set; }
        public string TableFullName { get; set; }
        public string TableAreaID { get; set; }
        public string TableAreaName { get; set; }
        public string TableTypeID { get; set; }
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
        public Nullable<decimal> SumOfConsume { get; set; }
        public Nullable<decimal> ServiceRate { get; set; }
        public Nullable<decimal> SumOfService { get; set; }
        public Nullable<decimal> SumForDiscount { get; set; }
        public Nullable<decimal> SumOfCarry { get; set; }
        public Nullable<decimal> SumToPay { get; set; }
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
