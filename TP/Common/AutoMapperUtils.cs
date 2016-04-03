using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EntitiesDABL;
using TP.ModelView;

namespace TP.Common
{
    public class AutoMapperUtils
    {
        protected static MapperConfiguration config = new MapperConfiguration(i =>
        {
            i.CreateMap<TPCallIn, TPCallInMV>()
                .ForMember(s => s.TPBillRef, m => m.Ignore());
            i.CreateMap<TPBillRef, TPBillRefMV>()
                .ForMember(s => s.TPCallIn, m => m.Ignore())
                .ForMember(s => s.TPUser, m => m.Ignore())
                .ForMember(s => s.TPUserAddress, m => m.Ignore())
                .ForMember(s => s.TPDeliver, m => m.Ignore());

            i.CreateMap<TPDeliver, TPDeliverMV>()
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPDriver, m => m.Ignore());
            i.CreateMap<TPDriver, TPDriverMV>()
                .ForMember(s => s.TPDeliver, m => m.Ignore());
            i.CreateMap<TPUserAddress, TPUserAddressMV>()
                .ForMember(s => s.TPUser, m => m.Ignore())
                .ForMember(s => s.TPBillRef, m => m.Ignore());
            i.CreateMap<TPUserCell, TPUserCellMV>()
                .ForMember(s => s.TPUser, m => m.Ignore());
            i.CreateMap<TPUser, TPUserMV>()
                .ForMember(s => s.TPBillRef, m => m.Ignore())
                .ForMember(s => s.TPUserAddress, m => m.Ignore())
                .ForMember(s => s.TPUserCell, m => m.Ignore());

        });

        public static IMapper GetMapper()
        {
            return config.CreateMapper();
        }
    }
}
