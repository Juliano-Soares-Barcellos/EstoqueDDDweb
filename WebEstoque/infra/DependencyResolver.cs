using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

public class MicrosoftDependencyResolver : IDependencyResolver
{
    private readonly IServiceProvider _serviceProvider;

    public MicrosoftDependencyResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public object GetService(Type serviceType)
    {
        // Retorna o serviço individual ou null se não encontrado
        return _serviceProvider.GetService(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        // Retorna uma coleção de serviços ou uma coleção vazia se não encontrados
        var service = _serviceProvider.GetService(serviceType);
        return service != null ? new[] { service } : Enumerable.Empty<object>();
    }
}
