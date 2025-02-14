using Npgsql.Replication;
using Api;
using System.Reflection;

// This class is used to map all endpoints in the application, in a way that is more maintainable and scalable.

public static class EndpointExtensions
{
    public static WebApplication MapAllEndpoints(this WebApplication app)
    {
        var endPointType = typeof(IEndpoint);

        var assembly = Assembly.GetExecutingAssembly();

        var endpointTypes = assembly.GetExportedTypes()
            .Where(t => t.IsAbstract == false &&
                        t.GetInterfaces().Contains(endPointType));

        foreach (var type in endpointTypes)
        {
            if (Activator.CreateInstance(type) is IEndpoint instance)
            {
                instance.Map(app);
            }
        }

        return app;
    }
}