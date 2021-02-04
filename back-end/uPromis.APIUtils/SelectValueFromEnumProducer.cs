using uPromis.APIUtils.APIMessaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace uPromis.APIUtils
{
    public class SelectValueFromEnumProducer
    {
        public List<ListValue> ProduceValues(List<ListValue> list, Type t)
        {
            if (t != null)
            {
                foreach (var item in Enum.GetValues(t))
                {
                    list.Add(new ListValue() { Label = Enum.GetName(t, item), Value = Enum.GetName(t, item) });
                }
            }
            return list;
        }
    }
}
