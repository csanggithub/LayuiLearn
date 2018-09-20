using LayuiLearn.Entity.Models;
using LayuiLearn.IRepository;
using LayuiLearn.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiLearn.Repository
{
    public partial class UserRepository : BaseRepository<User>, IUserRepository
    {
    }
}
