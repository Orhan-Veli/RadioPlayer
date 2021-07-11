using Radio_Player.Dal.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Dal.Repositories.Abstract
{
    public interface IEntityRepository<T> where T: class
    {
        Task Create(T model);
        Task Delete(string id);
        Task<T> Update(T model);
        Task<List<T>> GetAll(string id);
        Task<T> Get(string id);
    }
}
