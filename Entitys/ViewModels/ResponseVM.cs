using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entitys.ViewModels
{
    public class ResponseVM
    {
        public string ResultCode { get; set; }

        public string ResultMessage { get; set; }

        public bool IsSuccess { get; set; }
    }

    public class ResponseVM<T>
    {
        public string ResultCode { get; set; }

        public string ResultMessage { get; set; }

        public bool IsSuccess { get; set; }

        public int Count { get; set; }

        public T Data { get; set; }
    }
}