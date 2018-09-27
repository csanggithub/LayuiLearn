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
    public partial class UserFunctionServices : BaseServices<UserFunction>, IUserFunctionServices
    {
        IUserFunctionRepository dal;
        public UserFunctionServices(IUserFunctionRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
    }
}
