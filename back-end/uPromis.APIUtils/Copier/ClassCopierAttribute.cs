using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uPromis.APIUtils.Copier
{
    /// <summary>
    /// Attribute that specifies which parent class property is to be used when copying into this propertye
    /// Multiple attributes can be used to specify different source classes
    /// To be used in conjuction with ClassCopier
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ClassCopierAttribute : Attribute
    {
        public readonly string ParentPropertyName;
        public readonly Type ParentClass;
        public ClassCopierAttribute(Type parentClass,  string parentPropertyName)
        {
            ParentPropertyName = parentPropertyName;
            ParentClass = parentClass;
        }
    }
}
