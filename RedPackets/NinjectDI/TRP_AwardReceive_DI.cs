﻿using BLL;
using DAL;
using Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjectDI
{
    public class TRP_AwardReceive_DI
    {
        public TRP_AwardReceive_BLL getBll()
        {
            #region Ninject DI依赖注入

            //创建Ninject内核实例  前者为Ikernel接口 ，再用StandardKernel类作为接口的实例化
            IKernel ninjectKernel = new StandardKernel();

            //接口绑定实现接口的实例
            ninjectKernel.Bind<I_TRP_AwardReceive_DAL>().To<TRP_AwardReceive_DAL>();

            //获取接口实现
            I_TRP_AwardReceive_DAL idal = ninjectKernel.Get<I_TRP_AwardReceive_DAL>();

            //依赖注入-实现接口的实例传给构造函数
            TRP_AwardReceive_BLL bll = new TRP_AwardReceive_BLL(idal);

            return bll;
            #endregion
        }
    }
}
