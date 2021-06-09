using System.Collections.Generic;
using Funzone.Application.Configuration.Queries;
using Funzone.Application.Configuration.Responses;

namespace Funzone.Application.Users.Queries
{
    public class GetUsersQuery : IQuery<Response<IEnumerable<UserDto>>>
    {
    }
}