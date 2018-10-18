using Entitys.Models;
using IRepository;
using IServices;
using LinqKit;
using Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{

    public partial class UserServices : BaseServices<User>, IUserServices
    {
        IUserRepository dal;

        public UserServices(IUserRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }

        public UserServices()
        {
        }

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
        public List<User> GetUsersByWheres(int page,int limit,string provinceCode,string cityCode,string areaCode,string deptCode,string userName,string accountName,string startTime,string endTime,out int total)
        {
            total = 0;
            var _where = PredicateBuilder.New<User>();
            _where = _where.And(m => 1 == 1);
            if (!string.IsNullOrEmpty(provinceCode))
                _where = _where.And(m => m.ProvinceCode == provinceCode);
            if (!string.IsNullOrEmpty(cityCode))
                _where = _where.And(m => m.CityCode == cityCode);
            if (!string.IsNullOrEmpty(areaCode))
                _where = _where.And(m => m.RegionCode == areaCode);
            if (!string.IsNullOrEmpty(deptCode))
                _where = _where.And(m => m.DeptCode == deptCode);
            if (!string.IsNullOrEmpty(userName))
                _where = _where.And(m => m.RealName == userName);
            if (!string.IsNullOrEmpty(accountName))
                _where = _where.And(m => m.UserName == accountName);
            if (!string.IsNullOrEmpty(startTime))
            {
                var start = Convert.ToDateTime(startTime);
                _where = _where.And(m => m.Crdt >= start);

            }
            if (!string.IsNullOrEmpty(endTime))
            {
                var end = Convert.ToDateTime(endTime);
                _where = _where.And(m => m.Crdt <= end);
            }
            var list = dal.QueryByPage(page, limit, out total, _where, p => p.Crdt, false);
            return list;
        }
    }
}
