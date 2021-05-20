using System;
using System.Threading.Tasks;
using Autofac;
using Funzone.Application.Zones.Commands;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using MediatR;
using NSubstitute;

namespace Funzone.IntegrationTests.Zones
{
    using static TestFixture;

    public class ZoneTestHelper
    {
        public static async Task<Guid> CreateZoneAsync()
        {
            return await Run<IMediator, Guid>(
                async mediator =>
                {
                    var command = new CreateZoneCommand
                    {
                        Title = "LOL",
                    };

                    return await mediator.Send(command);
                });
        }

        public static async Task<Guid> CreateZoneWithExtraUserAsync()
        {
            return await RunAsRegisterExtra<IMediator, Guid>(
                async mediator =>
                {
                    var command = new CreateZoneCommand
                    {
                        Title = "LOL",
                    };

                    return await mediator.Send(command);
                },
                ReRegisterUserContext);
        }

        private static void ReRegisterUserContext(ContainerBuilder builder)
        {
            var userContext = Substitute.For<IUserContext>();
            userContext.UserId.Returns(new UserId(Guid.NewGuid()));
            builder.RegisterInstance(userContext).AsImplementedInterfaces();
        }
    }
}