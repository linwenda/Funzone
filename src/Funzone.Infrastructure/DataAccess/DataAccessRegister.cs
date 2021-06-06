using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Funzone.Application.Configuration.Data;
using Funzone.Domain.SeedWork;
using Funzone.Infrastructure.DataAccess.EventSourcing;
using Funzone.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Funzone.Infrastructure.DataAccess
{
    public static class DataAccessRegister
    {
        public static ContainerBuilder RegisterDatabase(this ContainerBuilder builder, string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<FunzoneDbContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString);
            dbContextOptionsBuilder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

            builder.RegisterType<FunzoneDbContext>()
                .AsSelf()
                .As<DbContext>()
                .WithParameters(new List<Parameter>
                {
                    new NamedParameter("options", dbContextOptionsBuilder.Options),
                })
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());

            builder.RegisterType<MsSqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", connectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<AggregateStoreDomainEventsAccessor>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<SqlStreamAggregateStore>()
                .As<IAggregateStore>()
                .InstancePerLifetimeScope();

            return builder;
        }
    }
}