using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uPromis.APIUtils.Copier
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ClassCopierAttribute : Attribute
    {
        public readonly string ParentPropertyName;
        public readonly Type ParentClass;
        public ClassCopierAttribute(Type parenClass,  string parentPropertyName)
        {
            ParentPropertyName = parentPropertyName;
            ParentClass = parenClass;
        }
    }
}
