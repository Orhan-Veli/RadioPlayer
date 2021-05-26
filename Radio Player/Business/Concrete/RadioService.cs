using Radio_Player.Business.Abstract;
using Radio_Player.Business.Constants;
using Radio_Player.Business.Utilities.Abstract;
using Radio_Player.Business.Utilities.Concrete;
using Radio_Player.Dal.Data.Concrete;
using Radio_Player.Dal.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Business.Concrete
{
    public class RadioService : IRadioService
    {
        private readonly IEntityRepository<Radio> _radioRepository;
        public RadioService(IEntityRepository<Radio> radioRepository)
        {
            _radioRepository = radioRepository;
        }
        public async Task<IResult<Radio>> Create(Radio model)
        {
            if (model == null)
            {
                return new Result<Radio>(false, Messages.ModelNotValid);
            }
            await _radioRepository.Create(model);
            return new Result<Radio>(true);
        }

        public async Task<IResult<object>> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new Result<object>(false, Messages.IdNotFound);
            }
            await _radioRepository.Delete(id);
            return new Result<object>(true);
        }

        public async Task<IResult<Radio>> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new Result<Radio>(false, Messages.IdNotFound);
            }
            var result = await _radioRepository.Get(id);
            return new Result<Radio>(true, result);
        }

        public async Task<IResult<List<Radio>>> GetAll()
        {
            var results = await _radioRepository.GetAll();
            return new Result<List<Radio>>(true,results);
        }

        public async Task<IResult<Radio>> Update(Radio model)
        {
            if (model == null)
            {
                return new Result<Radio>(false,Messages.ModelNotValid);
            }
           var result =  await _radioRepository.Update(model);
            return new Result<Radio>(true,result);
        }
    }
}
