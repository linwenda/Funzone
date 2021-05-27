using Autofac;
using Funzone.Application.Users;
using Funzone.Domain.Users;

namespace Funzone.Infrastructure.Domain
{
    public static class DomainRegister
    {
        public static ContainerBuilder RegisterDomainService(this ContainerBuilder builder)
        {
            builder.RegisterType<UserChecker>()
                .As<IUserChecker>();


            return builder;
        }
    }
}