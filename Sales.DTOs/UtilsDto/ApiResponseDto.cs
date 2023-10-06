using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs
{
    public enum CodeResponseEnum
    {
        OK = 200, ERROR = 400
    }
    public class ApiResponseDto
    {
        private int _code = (int)CodeResponseEnum.OK;
        public int Code { get { return _code; } set { _code = value; } }
        private string _message = "OK";
        public string Message { get { return _message; } set { _message = value; } }
        private object _data = new { };
        public object Data { get { return _data; } set { _data = value; } }
        private int? _total = 0;
        public int? Total { get { return _total; } set { _total = value; } }

        public static string GetErrMessage(Exception exc)
        {
            return exc.Message + " - " + (exc.InnerException == null ? "" : "InnerException: " + exc.InnerException.Message);
        }

        public static ApiResponseDto ErrorHandler(Exception ex)
        {
            return new ApiResponseDto
            {
                Code = (int)CodeResponseEnum.ERROR,
                Message = ApiResponseDto.GetErrMessage(ex)
            };
        }
    }
}
