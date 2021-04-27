﻿using System;
using Funzone.Domain.PostDrafts.Rules;
using Funzone.Domain.Posts;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Domain.PostDrafts
{
    public class PostDraft : Entity, IAggregateRoot
    {
        public PostDraftId Id { get; private set; }
        public ZoneId ZoneId { get; private set; }
        public UserId AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public bool IsPosted { get; private set; }

        public PostDraft(ZoneUser zoneUser, string title, string content)
        {
            ZoneId = zoneUser.ZoneId;
            AuthorId = zoneUser.UserId;
            Title = title;
            Content = content;

            Id = new PostDraftId(Guid.NewGuid());
            CreatedTime = Clock.Now;
        }

        public Post Post(UserId postingUserId)
        {
            CheckRule(new PostDraftCanBePostedOnlyByAuthorRule(AuthorId, postingUserId));
            IsPosted = true;
            return new Post(ZoneId, AuthorId, Title, Content);
        }

        public void Edit(UserId editorId, string title, string content)
        {
            CheckRule(new PostDraftCanBeEditedOnlyByAuthorRule(AuthorId, editorId));
            Title = title;
            Content = content;
        }
    }
}