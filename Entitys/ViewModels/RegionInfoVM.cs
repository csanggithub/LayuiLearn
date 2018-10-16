using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.ViewModels
{
    /// <summary>
    /// 地区信息
    /// </summary>
    public class RegionInfoVM
    {
        /// <summary>
        /// 地区名
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 地区编号
        /// </summary>
        public int RegionCode { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        public int Pid { get; set; }
    }
}
