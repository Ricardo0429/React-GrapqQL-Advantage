using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using ReactAdvantage.Api.GraphQLSchema.Types;

namespace ReactAdvantage.Api.GraphQLSchema
{
    public static class GraphQLSetup
    {
        public static IServiceCollection AddGraphqlServices(this IServiceCollection services)
        {
            services.AddTransient<IDocumentExecuter, DocumentExecuter>();
            services.AddTransient<ReactAdvantageQuery>();
            services.AddTransient<ReactAdvantageMutation>();
            services.AddTransient<TaskType>();
            services.AddTransient<TaskInputType>();
            services.AddTransient<TenantType>();
            services.AddTransient<TenantInputType>();
            services.AddTransient<UserType>();
            services.AddTransient<UserInputType>();
            services.AddTransient<ProjectType>();
            services.AddTransient<ProjectInputType>();
            services.AddTransient<RoleType>();
            services.AddTransient<RoleInputType>();
            services.AddTransient<ISchema, ReactAdvantageSchema>();

            return services;
        }
    }
}
