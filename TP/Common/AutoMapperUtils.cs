using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EntitiesDABL;
using EntitiesDABL.DTO;
using TP.ModelView;

namespace TP.Common
{
    public class AutoMapperUtils
    {
        protected static MapperConfiguration config = new MapperConfiguration(i =>
        {

            //TPCallIn
            i.CreateMap<TPCallInMV, TPCallIn>()
                .ForMember(s => s.TPBillRef, m => m.Ignore());
            i.CreateMap<TPCallIn, TPCallInMV>()
                .ForMember(s => s.TPBillRef, m => m.Ignore());


            //TPBillRef
            i.CreateMap<TPBillRefMV, TPBillRef>()
                .ForMember(s => s.TPCallIn, m => m.Ignore())
                .ForMember(s => s.TPUser, m => m.Ignore())
                .ForMember(s => s.TPUserAddress, m => m.Ignore())
                .ForMember(s => s.TPDeliver, m => m.Ignore());
            i.CreateMap<TPBillRef, TPBillRefMV>()
                .ForMember(s => s.TPCallIn, m => m.Ignore())
                .ForMember(s => s.TPUser, m => m.Ignore())
                .ForMember(s => s.TPUserAddress, m => m.Ignore())
                .ForMember(s => s.TPDeliver, m => m.Ignore());


            //TPDeliver
            i.CreateMap<TPDeliverMV, TPDeliver>()
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPDriver, m => m.Ignore());
            i.CreateMap<TPDeliver, TPDeliverMV>()
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPDriver, m => m.Ignore());

            //TPDriver
            i.CreateMap<TPDriverMV, TPDriver>()
                .ForMember(s => s.TPDeliver, m => m.Ignore());
            i.CreateMap<TPDriver, TPDriverMV>()
                .ForMember(s => s.TPDeliver, m => m.Ignore());

            //TPUserAddress
            i.CreateMap<TPUserAddress, TPUserAddressMV>()
                .ForMember(s => s.TPUser, m => m.Ignore())
                .ForMember(s => s.TPBillRef, m => m.Ignore());
            i.CreateMap<TPUserAddressMV, TPUserAddress>()
                .ForMember(s => s.TPUser, m => m.Ignore())
                .ForMember(s => s.TPBillRef, m => m.Ignore());

            //TPUserCell
            i.CreateMap<TPUserCell, TPUserCellMV>()
                .ForMember(s => s.TPUser, m => m.Ignore());
            i.CreateMap<TPUserCellMV, TPUserCell>()
                .ForMember(s => s.TPUser, m => m.Ignore());

            //TPUser
            i.CreateMap<TPUser, TPUserMV>()
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPUserAddress, m => m.Ignore())
                .ForMember(s => s.TPUserCell, m => m.Ignore());
            i.CreateMap<TPUserMV, TPUser>()
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPUserAddress, m => m.Ignore())
                .ForMember(s => s.TPUserCell, m => m.Ignore());

            //TempBill
            i.CreateMap<TempBill, TempBillMV > ();

            //TempBillItem
            i.CreateMap<TempBillItem, TempBillItemMV>();

            //Bill
            i.CreateMap<Bill, BillMV>();

            //TempBillItem
            i.CreateMap<BillItem, BillItemMV>();

            //OrderHistory
            i.CreateMap<OrderHistoryDTO, OrderHistoryMV>();


        });

        public static IMapper GetMapper()
        {
            return config.CreateMapper();
        }
    }
}
