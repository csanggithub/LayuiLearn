using Entitys.Models;
using IServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public partial interface IUserServices : IBaseServices<User> 
    {
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="provinceCode"></param>
        /// <param name="cityCode"></param>
        /// <param name="areaCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="userName"></param>
        /// <param name="accountName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<User> GetUsersByWheres(int page, int limit, string provinceCode, string cityCode, string areaCode, string deptCode, string userName, string accountName, string startTime, string endTime, out int total);
    }
}
