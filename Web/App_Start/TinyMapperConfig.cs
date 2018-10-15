using Entitys.Models;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App_Start
{
    public class TinyMapperConfig
    {
        public static void Bind()
        {
            TinyMapper.Bind<UserRole, UserRoleExt>();
        }
    }
}