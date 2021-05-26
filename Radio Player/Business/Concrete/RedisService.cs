using Radio_Player.Business.Abstract;
using Radio_Player.Business.Constants;
using Radio_Player.Business.Utilities.Abstract;
using Radio_Player.Business.Utilities.Concrete;
using Radio_Player.Dal.Data.Concrete;
using Radio_Player.Dal.Repositories.Abstract;
using Radio_Player.Dal.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Business.Concrete
{
    public class RedisService : IRedisService
    {
        private readonly IRedisRepository _entityRepository;
        public RedisService(IRedisRepository entityRepository)
        {
            _entityRepository = entityRepository;    
        }       
        public async Task<IResult<List<Radio>>> Create(List<Radio> model)
        {
            if (model == null || model.Count==0)            
                return new Result<List<Radio>>(false,Messages.ModelNotValid);            
           await _entityRepository.BulkCreate(model);
            return new Result<List<Radio>>(true);
        }

        public async Task<IResult<object>> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new Result<object>(false);
           await _entityRepository.Delete(id);
            return new Result<object>(true);
        }

        public async Task<IResult<List<Radio>>> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new Result<List<Radio>>(false,Messages.IdNotFound);
            var result = await _entityRepository.BulkGet(id);
            if (result == null)
                return new Result<List<Radio>>(false);
            return new Result<List<Radio>> (true,result);
        }

        public async Task<IResult<List<Radio>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<Radio>> Update(Radio model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.RadioConnection) || string.IsNullOrEmpty(model.Category))            
                return new Result<Radio>(false, Messages.ModelNotValid);
            var result = await _entityRepository.Update(model);
            if(result == null)
                return new Result<Radio>(false);
            return new Result<Radio>(true);
        }
    }
}
