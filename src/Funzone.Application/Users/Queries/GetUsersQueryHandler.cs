using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Funzone.Application.Configuration.Data;
using Funzone.Application.Configuration.Queries;
using Funzone.Application.Configuration.Responses;

namespace Funzone.Application.Users.Queries
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, Response<IEnumerable<UserDto>>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Response<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[User].[Id], " +
                               "[User].[UserName], " +
                               "[User].[EmailAddress], " +
                               "[User].[PasswordSalt], " +
                               "[User].[PasswordHash] " +
                               "FROM [Users] AS [User] ";

            var users = await connection.QueryAsync<UserDto>(sql);

            return Response<IEnumerable<UserDto>>.Success(users);
        }
    }
}