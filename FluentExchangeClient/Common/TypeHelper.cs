using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentExchangeClient.Common;

public static class TypeHelper
{
    public static bool IsNumericType(object o)
    {
        return Type.GetTypeCode(o.GetType()) switch
        {
            TypeCode.Byte or 
            TypeCode.SByte or 
            TypeCode.UInt16 or 
            TypeCode.UInt32 or 
            TypeCode.UInt64 or 
            TypeCode.Int16 or 
            TypeCode.Int32 or 
            TypeCode.Int64 or 
            TypeCode.Decimal or 
            TypeCode.Double or 
            TypeCode.Single => true,
            _ => false,
        };
    }

    public static bool IsNotDefault(object? value)
    {
        if (value != null)
        {
            Type? type = value.GetType();
            if (type == typeof(string))
            {
                return !String.IsNullOrEmpty(value?.ToString());
            }
            if (type != null && type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                return !value.Equals(defaultValue);
            }
        }
        return false;
    }
}
