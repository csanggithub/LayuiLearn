using Entitys.Models;
using IRepository;
using IServices;
using Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial class DistrictInfoServices : BaseServices<DistrictInfo>, IDistrictInfoServices
    {
        IDistrictInfoRepository dal;
        public DistrictInfoServices(IDistrictInfoRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
    }
}
