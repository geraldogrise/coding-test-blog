using MediatR;
using Blog.CrossCuttingIoc;
using Blog.Endpoints.Middleware;


namespace Blog.Endpoints.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            BootStrapper.RegisterServices(services);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MediatorMiddleware<,>));
        }
    }
}
