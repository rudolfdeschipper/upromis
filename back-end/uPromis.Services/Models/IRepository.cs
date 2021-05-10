using uPromis.APIUtils.APIMessaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace uPromis.Services.Models
{
    public interface IRepository<T>
    {
        IQueryable<T> List { get; }

        Task<(List<T>, double)> FilteredAndSortedList(SortAndFilterInformation sortAndFilterInfo, bool paging);

        Task<T> Get(int id);

        IQueryable<T> GetAllByExternalID(Guid guid);

        Task<T> Post(T rec);
        Task<T> Put(T rec);
        Task<bool> Delete(T rec);

    }
}