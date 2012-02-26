using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using JokeStore.Core.Repository;
using JokeStore.Core.Repository.EntityFramework;
using JokeStore.Web.Core.Auth;

namespace JokeStore.Web.Core
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            ninjectKernel.Rebind<IDomainResolver>().To<RequestDomainResolver>().WithConstructorArgument("domainName", requestContext.HttpContext.Request.Url.Host);

            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // Repositories
            ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            ninjectKernel.Bind<IRepository>().To<Repository>();
            ninjectKernel.Bind<IDomainRepository>().To<Repository>();
            ninjectKernel.Bind<IEntryRepository>().To<Repository>();
        }
    }
}