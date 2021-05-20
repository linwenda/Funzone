using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.Zones.Commands;
using Funzone.Application.Zones.Queries;
using Funzone.Domain.SharedKernel;
using MediatR;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.Zones
{
    using static TestFixture;

    public class ZoneTests : TestBase
    {
        [Test]
        public async Task ShouldCreatedZone()
        {
            await Run<IMediator>(async mediator =>
            {
                var command = new CreateZoneCommand
                {
                    Title = "dotnet",
                    Color = "#FFFFFF",
                    Icon = "emoji",
                    Visibility =  Visibility.Private
                };

                var zoneId = await mediator.Send(command);
                var zones = await mediator.Send(new GetZonesQuery());
                zones.Count().ShouldBe(1);
                zones.First().Id.ShouldBe(zoneId);
                zones.First().Title.ShouldBe(command.Title);
                zones.First().Color.ShouldBe(command.Color);
                zones.First().Icon.ShouldBe(command.Icon);
                zones.First().Visibility.ShouldBe((int)command.Visibility);
            });
        }
    }
}