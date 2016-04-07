using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace TP.Common
{
    public class SumConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            decimal temp = 0;

            foreach (var i in values)
            {
                if (i is decimal)
                {
                    temp += (decimal) i;
                }
            }
            return temp.ToString("C");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
