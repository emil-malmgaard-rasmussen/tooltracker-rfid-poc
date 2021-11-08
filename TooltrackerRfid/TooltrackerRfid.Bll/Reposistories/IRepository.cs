using System.Collections.Generic;
using System.Threading.Tasks;
using Smoerfugl.Database.Postgres;
using Smoerfugl.Database.Postgres.BaseEntities;

namespace TooltrackerRfid.Bll.Reposistories
{
    public interface IRepository<T, TY> where T : IBaseEntity<TY>
    {
        Task<PaginatedList<T>> GetAsPaginatedList(int index, int pageSize);
        Task<IList<T>> GetList();
        Task<T> GetById(TY id);
        Task<T> Add(T t);
        void Update(T t);
        Task Save();
        void Delete(T t);
    }
}