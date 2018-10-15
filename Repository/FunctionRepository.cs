using Entitys.Models;
using IRepository;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public partial class FunctionRepository : BaseRepository<Function>, IFunctionRepository
    {
    }
    
}
