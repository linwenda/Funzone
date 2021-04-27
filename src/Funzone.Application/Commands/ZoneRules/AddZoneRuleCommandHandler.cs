﻿using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.ZoneRules;
using Funzone.Domain.Zones;

namespace Funzone.Application.Commands.ZoneRules
{
    public class AddZoneRuleCommandHandler : ICommandHandler<AddZoneRuleCommand,bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IZoneRuleRepository _zoneRuleRepository;
        private readonly IUserContext _userContext;

        public AddZoneRuleCommandHandler(
            IZoneRepository zoneRepository,
            IZoneMemberRepository zoneMemberRepository,
            IZoneRuleRepository zoneRuleRepository,
            IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _zoneMemberRepository = zoneMemberRepository;
            _zoneRuleRepository = zoneRuleRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(AddZoneRuleCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));
            
            var zoneMember = await _zoneMemberRepository.FindAsync(zone.Id, _userContext.UserId);

            var zoneRule = zone.AddRule(zoneMember, request.Title, request.Description, request.Sort);

            await _zoneRuleRepository.AddAsync(zoneRule);
            return await _zoneRuleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}