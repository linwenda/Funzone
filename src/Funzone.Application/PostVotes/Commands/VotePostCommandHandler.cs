﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Posts;
using Funzone.Domain.PostVotes;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;

namespace Funzone.Application.PostVotes.Commands
{
    public class VotePostCommandHandler : ICommandHandler<VotePostCommand, Guid>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostVoteRepository _postVoteRepository;
        private readonly IUserContext _userContext;

        public VotePostCommandHandler(
            IPostRepository postRepository,
            IPostVoteRepository postVoteRepository,
            IUserContext userContext)
        {
            _postRepository = postRepository;
            _postVoteRepository = postVoteRepository;
            _userContext = userContext;
        }

        public async Task<Guid> Handle(VotePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(new PostId(request.PostId));

            var postVote = post.Vote(_userContext.UserId, VoteType.Of(request.VoteType));

            await _postVoteRepository.AddAsync(postVote);
            await _postVoteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return postVote.Id.Value;
        }
    }
}