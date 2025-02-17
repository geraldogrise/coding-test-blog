using MediatR;
using Blog.Data.Context;
using Blog.Data.Repository;
using Blog.Application.App;
using Blog.CrossCutting.Log;
using Blog.Domain.Notifications;
using Blog.Application.Interfaces;
using Blog.Domain.Aggreagates.Posts.Services;
using Blog.Domain.Aggreagates.Users.Services;
using Blog.Domain.Aggreagates.Users.Repository;
using Blog.Domain.Aggreagates.Posts.Repository;
using Microsoft.Extensions.DependencyInjection;
using Blog.Domain.Aggreagates.Auth;



namespace Blog.CrossCuttingIoc
{
    public static class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IPostAppService, PostAppService>();

            // Domain
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Service
            services.AddSingleton<ILogger, Logger>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();

            // Data
            services.AddScoped<DatabaseContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
        }
    }
}
