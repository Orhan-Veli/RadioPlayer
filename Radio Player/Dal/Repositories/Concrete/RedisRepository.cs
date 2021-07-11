using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;
using Radio_Player.Dal.Data.Concrete;
using Radio_Player.Dal.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radio_Player.Dal.Repositories.Concrete
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IDistributedCache _distributedCache;
        public RedisRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task BulkCreate(List<Radio> model)
        {
            var data = JsonConvert.SerializeObject(model);
            //var dataByte = Encoding.UTF8.GetBytes(data);
            await _distributedCache.SetStringAsync("model", data);
        }

        public async Task Create(Radio model)
        {
          var data = JsonConvert.SerializeObject(model);
          var dataByte = Encoding.UTF8.GetBytes(data);
          await _distributedCache.SetAsync("model",dataByte);          
        }

        public async Task Delete(string id)
        {         
            await _distributedCache.RemoveAsync(id);
        }

        public async Task<Radio> Get(string id)
        {
            string result = await _distributedCache.GetStringAsync(id);
            return JsonConvert.DeserializeObject<Radio>(result);            
        }

        public async Task<List<Radio>> GetAll(string id)
        {
            string result = await _distributedCache.GetStringAsync(id);
            if (result == null)
            {
                return null;
            }
            var val = JsonConvert.DeserializeObject<List<Radio>>(result);
            return val;
        }

        public async Task<Radio> Update(Radio model)
        {
            string result = await _distributedCache.GetStringAsync(model.Id);
            var batiAgil = JsonConvert.DeserializeObject<Radio>(result);
            batiAgil.Name = model.Name;
            batiAgil.RadioConnection = model.Name;
            batiAgil.Category = model.Category;
            var data = JsonConvert.SerializeObject(batiAgil);
            var dataByte = Encoding.UTF8.GetBytes(data);            
            await _distributedCache.SetAsync("model",dataByte);
            return model;
        }
    }
}
