using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.Application.Configuration.Data;
using Funzone.Application.Configuration.Queries;
using Funzone.Domain.Users;

namespace Funzone.Application.Zones.Queries
{
    public class GetZonesQueryHandler : IQueryHandler<GetZonesQuery, IEnumerable<ZoneDto>>
    {
        private readonly IUserContext _userContext;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetZonesQueryHandler(IUserContext userContext,ISqlConnectionFactory sqlConnectionFactory)
        {
            _userContext = userContext;
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public async Task<IEnumerable<ZoneDto>> Handle(GetZonesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"SELECT TOP 1  
                                   [Zone].[Id],
                                   [Zone].[CreatedTime] ,
                                   [Zone].[CreatorId] ,
                                   [Zone].[Title] ,
                                   [Zone].[Color] , 
                                   [Zone].[Icon] ,
                                   [Zone].[Visibility]
                                   FROM [Zones] AS [Zone] 
                                   WHERE [Zone].[CreatorId] = @UserId";

            return await connection.QueryAsync<ZoneDto>(sql, 
                new
                {
                    UserId = _userContext.UserId.Value
                });
        }
    }
}