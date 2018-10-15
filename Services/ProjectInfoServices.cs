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
    public partial class ProjectInfoServices : BaseServices<ProjectInfo>, IProjectInfoServices
    {
        IProjectInfoRepository dal;
        public ProjectInfoServices(IProjectInfoRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
    }
}
