using AutoMapper;
using Entitys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserRole, UserRoleExt>();
            });
        }
    }
}