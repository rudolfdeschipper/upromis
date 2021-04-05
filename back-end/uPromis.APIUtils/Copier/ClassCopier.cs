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
        public static void CopyClassProperties(TParent parent, TChild child)
        {
            var childProperties = child.GetType().GetProperties();
            foreach (var childProperty in childProperties)
            {
                var attributesForProperty = childProperty.GetCustomAttributes(typeof(ClassCopierAttribute), true)
                    .Where(ca => (ca as ClassCopierAttribute)?.ParentClass is TParent);

                var isOfTypeMatchParentAttribute = false;

                ClassCopierAttribute currentAttribute = null;

                foreach (var attribute in attributesForProperty)
                {
                    if (attribute.GetType() == typeof(ClassCopierAttribute) && (attribute as ClassCopierAttribute).ParentClass is TParent)
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
