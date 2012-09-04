using System;
using System.Web.Mvc;
using System.Web.Routing;
using CustomMvcSolution.WebUI.Infrastructure.Abstract;
using CustomMvcSolution.WebUI.Infrastructure.Concrete;
using Ninject;

namespace CustomMvcSolution.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController) _kernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // Add here your binding...
            _kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}