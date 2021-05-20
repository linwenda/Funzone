﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Application.Zones.Commands
{
    public class CreateZoneCommandHandler : ICommandHandler<CreateZoneCommand,Guid>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IUserContext _userContext;

        public CreateZoneCommandHandler(
            IZoneRepository zoneRepository,
            IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _userContext = userContext;
        }
        
        public async Task<Guid> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
        {
            var zone = new Zone(
                _userContext.UserId, 
                request.Title,
                request.Visibility,
                request.Color,
                request.Icon);

            await _zoneRepository.AddAsync(zone);   
            await _zoneRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return zone.Id.Value;
        }
    }
}