using LayuiLearn.Entity.Models;
using LayuiLearn.IRepository;
using LayuiLearn.IServices;
using LayuiLearn.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiLearn.Services
{

    public partial class UserServices : BaseServices<User>, IUserServices
    {
        IUserRepository dal;

        public UserServices(IUserRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }

        public UserServices()
        {
        }

    }
}
