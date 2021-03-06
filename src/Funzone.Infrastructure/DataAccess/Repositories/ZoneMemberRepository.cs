﻿using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;
using Microsoft.EntityFrameworkCore;

namespace Funzone.Infrastructure.DataAccess.Repositories
{
    public class ZoneMemberRepository : IZoneMemberRepository
    {
        private readonly FunzoneDbContext _context;
        private readonly IUserContext _userContext;

        public ZoneMemberRepository(
            FunzoneDbContext context,
            IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ZoneMember> GetByIdAsync(ZoneMemberId id)
        {
            var member = await _context.ZoneMembers.SingleOrDefaultAsync(z => z.Id == id);

            return member ?? throw new NotFoundException(nameof(ZoneMember), id);
        }

        public async Task AddAsync(ZoneMember zoneMember)
        {
            await _context.ZoneMembers.AddAsync(zoneMember);
        }

        public async Task<ZoneMember> GetCurrentMember(ZoneId zoneId)
        {
            return await _context.ZoneMembers
                .FirstOrDefaultAsync(z => z.ZoneId == zoneId && z.UserId == _userContext.UserId);
        }
    }
}