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
    public partial class ResourceDataInfoServices : BaseServices<ResourceDataInfo>, IResourceDataInfoServices
    {
        IResourceDataInfoRepository dal;
        public ResourceDataInfoServices(IResourceDataInfoRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
    }
}
