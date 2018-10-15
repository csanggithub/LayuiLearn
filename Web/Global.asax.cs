using Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LayuiLearn.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //string filePath = Server.MapPath("~/log4net.config");
            //FileInfo fil = new FileInfo(filePath);
            //log4net.Config.XmlConfigurator.Configure(fil); //将创建的log4net.config文件加载我到我们的项目中来  
            //以上三句代码可以用下面这一句替代  
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/log4net.config")));
            //利用autofac实现MVC项目的IOC和DI
            AutofacConfig.Register();
        }
    }
}
