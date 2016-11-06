using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using efsemop.Controllers;

namespace efsemop.Framework.Pepemosca.Data
{
    public class BasicDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(SubAlcaldiaController))
                return new SubAlcaldiaController(new RepoSubAlcaldia());
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }
    }
}