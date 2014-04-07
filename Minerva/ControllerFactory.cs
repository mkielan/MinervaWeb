using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel;
using System.Web.Mvc;

namespace Minevra
{
    class ControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public ControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            try
            {
                _kernel.ReleaseComponent(controller);
            }
            catch (Exception)
            {
                base.ReleaseController(controller);
            }
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            try
            {
                return _kernel.Resolve(controllerType) as IController;
            }
            catch (Exception)
            {
                try
                {
                    return base.GetControllerInstance(requestContext, controllerType);
                }
                catch (Exception) { }
            }

            throw new InvalidOperationException();
        }
    }
}