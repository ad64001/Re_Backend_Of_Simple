using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Re_Backend.Common.Attributes;
using Re_Backend.Common.Cache;
using Re_Backend.Common.SqlConfig;
using Re_Backend.Common.Transactions;
using Re_Backend.Infrastructure;
using Re_Backend.Infrastructure.CacheConfig;
using Re_Backend.Infrastructure.SqlConfig;
using SqlSugar;
using System.Configuration;
using System.Reflection;


namespace Re_Backend.Common.AutoConfiguration
{
    public static class AutofacConfig
    {
        public static void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration, params string[] assemblyNames)
        {
            

            var validAssemblies = new List<Assembly>();
            foreach (var assemblyName in assemblyNames)
            {
                try
                {
                    var assembly = Assembly.Load(assemblyName);
                    validAssemblies.Add(assembly);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"加载程序集 {assemblyName} 时出错: {ex.Message}");
                }
            }
            var types = validAssemblies.SelectMany(a => a.GetTypes())
                                       .Where(t => t.GetCustomAttributes(typeof(InjectableAttribute), true).Length > 0);
            foreach (var type in types)
            {
                var registration = containerBuilder.RegisterType(type).AsImplementedInterfaces();
                // 假设 InjectableAttribute 有一个 IsSingleton 属性来控制是否为单例
                var attribute = type.GetCustomAttribute<InjectableAttribute>();
                if (attribute != null && attribute.IsSingleton)
                {
                    registration.SingleInstance();
                }
                else
                {
                    registration.InstancePerDependency();
                }

                // 检查类型的方法是否带有 UseTranAttribute 注解
                var methodsWithAttribute = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(UseTranAttribute), true).Length > 0);
                if (methodsWithAttribute.Any())
                {
                    registration.EnableInterfaceInterceptors();
                    registration.InterceptedBy(typeof(TransactionInterceptor));
                }
            }

            // 注册 SqlSugarClient
            try
            {
                var configs = SqlSugarSetup.GetConnectionConfigs();
                Console.WriteLine($"数据库连接字符串: {configs[0].ConnectionString}");
                Console.WriteLine($"数据库类型: {configs[0].DbType}");
                containerBuilder.Register(c => new SqlSugarClient(configs))
                                .InstancePerLifetimeScope();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"注册 SqlSugarClient 时出错: {ex.Message}");
            }

            // 注册 DbContext
            containerBuilder.RegisterType<DbContext>()
                            .AsSelf()
                            .InstancePerLifetimeScope();

            //这里是事务控制
            // 注册 TransactionHandler
            containerBuilder.RegisterType<TransactionHandler>()
                            .InstancePerLifetimeScope();

            // 注册 TransactionInterceptor
            containerBuilder.RegisterType<TransactionInterceptor>()
                            .InstancePerLifetimeScope();


            // 从容器中解析 DbContext 并进行自动建表
            containerBuilder.RegisterBuildCallback(container =>
            {
                var dbContext = container.Resolve<DbContext>();
                var tableCreator = new SqlSugarTableCreator(dbContext.Db);
                tableCreator.CreateTablesFromModels();
            });

            // 注册缓存服务
            CacheConfiguration.ConfigureCache(containerBuilder, configuration);



        }



    }
}
