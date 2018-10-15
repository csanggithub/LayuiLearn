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
    public partial class DepartmentServices : BaseServices<Department>, IDepartmentServices
    {
        IDepartmentRepository dal;
        public DepartmentServices(IDepartmentRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
    }
}
