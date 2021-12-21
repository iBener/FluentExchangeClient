using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Common;

public static class Extensions
{
    public static void Add(this NameValueCollection query, string name, object value)
    {
        if (TypeHelper.IsNotDefault(value))
        {
            if (TypeHelper.IsNumericType(value))
            {
                string formattedValue = Convert.ToString(value, CultureInfo.InvariantCulture);
                query.Add(name, formattedValue);
            }
            else
            {
                query.Add(name, value.ToString());
            }
        }
    }
}
