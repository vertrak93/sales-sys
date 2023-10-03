using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.DTOs
{
    public enum CodeResponseEnum
    {
        OK = 1, ERROR = 0
    }
    public class ApiResponseDto
    {
        private int code = (int)CodeResponseEnum.OK;
        public int CODE { get { return code; } set { code = value; } }
        private string message = "OK";
        public string MESSAGE { get { return message; } set { message = value; } }
        private object data = new { };
        public object DATA { get { return data; } set { data = value; } }
        private int? total = 0;
        public int? TOTAL { get { return total; } set { total = value; } }

        public static string GetErrMessage(Exception exc)
        {
            return exc.Message + " - " + (exc.InnerException == null ? "" : "InnerException: " + exc.InnerException.Message);
        }

        public static ApiResponseDto HandlerError(Exception ex)
        {
            return new ApiResponseDto
            {
                CODE = (int)CodeResponseEnum.ERROR,
                MESSAGE = ApiResponseDto.GetErrMessage(ex)
            };
        }
    }
}
