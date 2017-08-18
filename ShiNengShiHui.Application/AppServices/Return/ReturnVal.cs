using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices.Return
{
    public class ReturnVal
    {
        public ReturnStatu Statu { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public ReturnVal()
        {

        }

        public ReturnVal(ReturnStatu statu)
        {
            Statu = statu;
        }

        public ReturnVal(ReturnStatu statu,string message)
            : this(statu)
        {
            Message = message;
        }

        public ReturnVal(ReturnStatu statu,string message,object data)
            : this(statu, message)
        {
            Data = data;
        }
    }

    public enum ReturnStatu
    {
        Success,
        Failure,
        Err
    }
}
