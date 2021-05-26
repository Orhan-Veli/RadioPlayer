using Radio_Player.Dal.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Dal.Repositories.Abstract
{
    public interface IRedisRepository : IEntityRepository<Radio>
    {
        public Task BulkCreate(List<Radio> model);

        public Task<List<Radio>> BulkGet(string id);
    }
}
