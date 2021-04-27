﻿using System;
using Funzone.Domain.Posts;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;

namespace Funzone.Domain.PostVotes
{
    public class PostVote
    {
        public PostId PostId { get; private set; }
        public DateTime VotedTime { get; private set; }
        public UserId VoterId { get; private set; }
        public VoteType VoteType { get; private set; }
    }
}