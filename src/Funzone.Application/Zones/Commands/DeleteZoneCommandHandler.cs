using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Application.Zones.Commands
{
    public class DeleteZoneCommandHandler : ICommandHandler<DeleteZoneCommand, bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IUserContext _userContext;

        public DeleteZoneCommandHandler(IZoneRepository zoneRepository,IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _userContext = userContext;
        }
        
        public async Task<bool> Handle(DeleteZoneCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));
            
            zone.Delete(_userContext.UserId);

            return await _zoneRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}