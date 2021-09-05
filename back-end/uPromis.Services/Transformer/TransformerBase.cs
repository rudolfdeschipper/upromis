using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPromis.Microservice.DTO.Models;

namespace uPromis.Services.Transformer
{
    public class TransformerBase
    {
        protected static void TransformList<F, T>(List<F> from, List<T> to, Func<List<T>, F, T> find, Func<F, T, T> transform) where F : DTOBase where T : class, new()
        {
            foreach (F p in from)
            {
                if (p.Modifier == "Deleted")
                {
                    if (find(to, p) is T toDelete)
                    {
                        to.Remove(toDelete);
                    }
                }
                if (p.Modifier == "Added")
                {
                    var toAdd = transform(p, new T());
                    to.Add(toAdd);
                }
                if (p.Modifier == "Modified")
                {
                    if (find(to, p) is T toUpdate)
                    {
                        _ = transform(p, toUpdate);
                    }
                }
            }
        }
    }
}
