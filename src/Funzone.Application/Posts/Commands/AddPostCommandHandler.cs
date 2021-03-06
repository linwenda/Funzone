﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Application.Posts.Commands
{
    public class AddPostCommandHandler : ICommandHandler<AddPostCommand, Guid>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserContext _userContext;

        public AddPostCommandHandler(
            IZoneRepository zoneRepository,
            IPostRepository postRepository,
            IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _postRepository = postRepository;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));

            var post = zone.AddPost(_userContext.UserId, request.Title, request.Content, PostType.Of(request.Type));

            await _postRepository.AddAsync(post);
            await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return post.Id.Value;
        }
    }
}