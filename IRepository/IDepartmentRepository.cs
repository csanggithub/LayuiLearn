using Entitys.Models;
using IRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public partial interface IDepartmentRepository : IBaseRepository<Department>
    {
    }
}
