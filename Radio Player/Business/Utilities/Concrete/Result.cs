using Radio_Player.Business.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Business.Utilities.Concrete
{
    public class Result<T> : IResult<T>
    {
        public Result(string message,bool success, T data) : this(success)
        {
            Message = message;
            Data = data;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, T data)
        {
            Success = success;
            Data = data;
        }

        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        public bool Success { get; }

        public string Message { get; }

        public T Data { get; }
    }
}
