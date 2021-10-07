using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace IaeBoraLibrary.Utils
{
    public static class DynamicTools
    {
        public static dynamic ToDynamic(object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            var props = TypeDescriptor.GetProperties(value.GetType());
            foreach (PropertyDescriptor property in props)
                expando.Add((char.ToLower(property.Name[0]) + property.Name.Substring(1)), property.GetValue(value));

            return expando as ExpandoObject;
        }
    }
}
