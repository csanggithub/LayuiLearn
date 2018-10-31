using Entitys.Models;
using IServices.Base;
using System.Collections.Generic;

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
        List<User> GetUsersByWheresPage(int page, int limit, string provinceCode, string cityCode, string areaCode, string deptCode, string userName, string accountName, string startTime, string endTime, out int total);
    }
}
