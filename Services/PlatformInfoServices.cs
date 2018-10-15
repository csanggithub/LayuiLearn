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
    public partial class PlatformInfoServices : BaseServices<PlatformInfo>, IPlatformInfoServices
    {
        IPlatformInfoRepository dal;
        public PlatformInfoServices(IPlatformInfoRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
    }
}
