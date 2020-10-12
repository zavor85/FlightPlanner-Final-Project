﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using StructureMap;

namespace FlightPlanner_SOLID.DependencyResolution
{
    public class StructureMapApiScope : IDependencyScope
    {
        private readonly IContainer _container;

        public StructureMapApiScope(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return _container.TryGetInstance(serviceType);
            }

            return _container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        { 
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}