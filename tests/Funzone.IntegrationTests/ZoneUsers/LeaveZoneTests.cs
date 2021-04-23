﻿using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.Commands.ZoneUsers;
using Funzone.Application.Queries.ZoneUsers;
using Funzone.IntegrationTests.Zones;
using MediatR;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.ZoneUsers
{
    using static TestFixture;

    public class LeaveZoneTests : TestBase
    {
        [Test]
        public async Task LeaveZone_WhenJoinedZone_Successful()
        {
            var zoneId = await ZoneTestHelper.CreateZone();

            await Run<IMediator>(async mediator =>
            {
                await mediator.Send(new JoinZoneCommand
                {
                    ZoneId = zoneId
                });

                await mediator.Send(new LeaveZoneCommand
                {
                    ZoneId = zoneId
                });

                var zones = await mediator.Send(new GetUserJoinZonesQuery());
                zones.Count().ShouldBe(0);
            });
        }
    }
}