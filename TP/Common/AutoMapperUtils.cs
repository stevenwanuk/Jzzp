using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ModelGenerator;
using TP.ModelView;

namespace TP.Common
{
    public class AutoMapperUtils
    {
        protected static MapperConfiguration config = new MapperConfiguration(i =>
        {
            i.CreateMap<TPCallIn, TPCallInMV>();
        });

        public static IMapper GetMapper()
        {
            return config.CreateMapper();
        }
    }
}
