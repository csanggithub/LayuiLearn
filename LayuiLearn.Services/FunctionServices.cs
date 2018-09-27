﻿using LayuiLearn.Entity.Models;
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
    public partial class FunctionServices : BaseServices<Function>, IFunctionServices
    {
        IFunctionRepository dal;
        public FunctionServices(IFunctionRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
    }
}
