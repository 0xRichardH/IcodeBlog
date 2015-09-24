﻿using System;
using System.Web.Mvc;
using Abp.Dependency;
using Abp.Web;
using Castle.Facilities.Logging;

namespace Icode.Blog.Web
{
    public class MvcApplication : AbpWebApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            //移除X-AspNetMvc-Version httpt响应头
            MvcHandler.DisableMvcResponseHeader = true;
            IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
            base.Application_Start(sender, e);
        }
    }
}
