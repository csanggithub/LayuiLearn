using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

namespace LayuiLearn.Entity.Models
{
    [TableName("districtinfo")]
    public class DistrictInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 地市名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public int Pid { get; set; }
    }
}
