using Radio_Player.Business.Utilities.Abstract;
using Radio_Player.Dal.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Business.Abstract
{
    public interface IRedisService
    {
        Task<IResult<List<Radio>>> Create(List<Radio> model);
        Task<IResult<object>> Delete(string id);
        Task<IResult<Radio>> Update(Radio model);
        Task<IResult<Radio>> Get(string id);
        Task<IResult<List<Radio>>> GetAll(string id);

    }
}
