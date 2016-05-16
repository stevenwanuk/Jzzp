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

        public static void InitAutoMapper()
        {
            Mapper.CreateMap<TPCallInMV, TPCallIn>()
                .ForMember(s => s.TPBillRef, m => m.Ignore());
            Mapper.CreateMap<TPCallIn, TPCallInMV>()
                .ForMember(s => s.TPBillRef, m => m.Ignore());


            //TPBillRef
            Mapper.CreateMap<TPBillRefMV, TPBillRef>()
                .ForMember(s => s.TPCallIn, m => m.Ignore())
                .ForMember(s => s.TPUser, m => m.Ignore())
                .ForMember(s => s.TPUserAddress, m => m.Ignore())
                .ForMember(s => s.TPDeliver, m => m.Ignore());
            Mapper.CreateMap<TPBillRef, TPBillRefMV>()
                .ForMember(s => s.TPCallIn, m => m.Ignore())
                .ForMember(s => s.TPUser, m => m.Ignore())
                .ForMember(s => s.TPUserAddress, m => m.Ignore())
                .ForMember(s => s.TPDeliver, m => m.Ignore());


            //TPDeliver
            Mapper.CreateMap<TPDeliverMV, TPDeliver>()
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPDriver, m => m.Ignore());
            Mapper.CreateMap<TPDeliver, TPDeliverMV>()
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPDriver, m => m.Ignore());

            //TPDriver
            Mapper.CreateMap<TPDriverMV, TPDriver>()
                .ForMember(s => s.TPDeliver, m => m.Ignore());
            Mapper.CreateMap<TPDriver, TPDriverMV>()
                .ForMember(s => s.TPDeliver, m => m.Ignore());

            //TPUserAddress
            Mapper.CreateMap<TPUserAddress, TPUserAddressMV>()
                .ForMember(s => s.TPUser, m => m.Ignore())
                .ForMember(s => s.TPBillRef, m => m.Ignore());
            Mapper.CreateMap<TPUserAddressMV, TPUserAddress>()
                .ForMember(s => s.TPUser, m => m.Ignore())
                .ForMember(s => s.TPBillRef, m => m.Ignore());

            //TPUserCell
            Mapper.CreateMap<TPUserCell, TPUserCellMV>()
                .ForMember(s => s.TPUser, m => m.Ignore());
            Mapper.CreateMap<TPUserCellMV, TPUserCell>()
                .ForMember(s => s.TPUser, m => m.Ignore());

            //TPUser
            Mapper.CreateMap<TPUser, TPUserMV>()
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPUserAddress, m => m.Ignore())
                .ForMember(s => s.TPUserCell, m => m.Ignore());
            Mapper.CreateMap<TPUserMV, TPUser>()
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPUserAddress, m => m.Ignore())
                .ForMember(s => s.TPUserCell, m => m.Ignore());

            //TempBill
            Mapper.CreateMap<TempBill, TempBillMV>();
            Mapper.CreateMap<TempBill, BillMV>();

            //TempBillItem
            Mapper.CreateMap<TempBillItem, TempBillItemMV>();
            Mapper.CreateMap<TempBillItem, BillItemMV>();

            //Bill
            Mapper.CreateMap<Bill, BillMV>();

            //TempBillItem
            Mapper.CreateMap<BillItem, BillItemMV>();

            //OrderHistory
            Mapper.CreateMap<OrderHistoryDTO, OrderHistoryMV>();

            //TPUserAddress to TPAdress
            Mapper.CreateMap<TPUserAddress, TPAddress>()
                .ForMember(s => s.HouseNumber, m => m.Ignore());

            Mapper.CreateMap<TPAddress, TPUserAddressMV>()
                .ForMember(s => s.HouseNumber, m => m.Ignore())
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPUser, m => m.Ignore());
        }


        /** only for 4.0+
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
    **/
    }
}
