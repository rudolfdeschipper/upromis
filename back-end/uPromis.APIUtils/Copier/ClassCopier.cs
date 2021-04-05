using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uPromis.APIUtils.Copier
{
    public class ClassCopier<TParent, TChild> where TParent : class
                                              where TChild : class
    {
        /// <summary>
        /// Copies all properties that are decorated with the ClassCopierAttribute attribute that specified from which parent class/property
        /// the copy shall be performed. Note that NO deep copy is done (yet)
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static void CopyClassProperties(TParent parent, TChild child)
        {
            var childProperties = child.GetType().GetProperties();
            foreach (var childProperty in childProperties)
            {
                var attributesForProperty = childProperty.GetCustomAttributes(typeof(ClassCopierAttribute), true);

                var isOfTypeMatchParentAttribute = false;

                ClassCopierAttribute currentAttribute = null;

                foreach (var attribute in attributesForProperty)
                {
                    if (attribute.GetType() == typeof(ClassCopierAttribute) && (attribute as ClassCopierAttribute).ParentClass.FullName == parent.GetType().FullName)
                    {
                        isOfTypeMatchParentAttribute = true;
                        currentAttribute = (ClassCopierAttribute)attribute;
                        break;
                    }
                }

                if (isOfTypeMatchParentAttribute)
                {
                    var parentProperties = parent.GetType().GetProperties();
                    object parentPropertyValue = null;
                    foreach (var parentProperty in parentProperties)
                    {
                        if (parentProperty.Name == currentAttribute.ParentPropertyName)
                        {
                            if (parentProperty.PropertyType == childProperty.PropertyType)
                            {
                                parentPropertyValue = parentProperty.GetValue(parent);
                                break;
                            }
                        }
                    }
                    if (parentPropertyValue != null)
                    {
                        childProperty.SetValue(child, parentPropertyValue);
                    }
                }
            }
        }
    }
}
