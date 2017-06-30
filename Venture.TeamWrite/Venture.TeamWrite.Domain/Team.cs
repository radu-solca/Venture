using System;
using System.Collections.Generic;
using System.Linq;
using LiteGuard;
using Newtonsoft.Json;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.TeamWrite.Domain.DomainEvents;

namespace Venture.TeamWrite.Domain
{
    public class Team : AggregateRoot
    {
        public User ProjectOwner { get; private set; }
        public ICollection<User> Users { get; private set; }
        public ICollection<Comment> Chat { get; private set; }

        public void Create(Guid id, User projectOwner)
        {
            var payload = new {ProjectOwnerId = projectOwner.Id};
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var teamCreatedEvent = new TeamCreatedEvent(
                id,
                1,
                jsonPayload);

            Apply(teamCreatedEvent);
        }

        public void Join(User user)
        {
            CheckIfCreated();

            var payload = new { UserId = user.Id};
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var teamJoinedEvent = new TeamJoinedEvent(
                Id,
                Version +1 ,
                jsonPayload);

            Apply(teamJoinedEvent);
        }

        public void Approve(User user)
        {
            CheckIfCreated();

            if (! Users.Any(u => u.Id == user.Id))
            {
                throw new ArgumentException("Cannot approve user that hasn't joined");
            }

            var payload = new { UserId = user.Id };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var teamUserApprovedEvent = new TeamUserApprovedEvent(
                Id,
                Version + 1,
                jsonPayload);

            Apply(teamUserApprovedEvent);
        }

        public void PostComment(Comment comment)
        {
            CheckIfCreated();

            if (!comment.Author.Approved && !(comment.Author.Id == ProjectOwner.Id))
            {
                throw new ArgumentException("Comment author not approved to post in team chat.");
            }

            var payload = new { Id = comment.Id, AuthorId = comment.Author.Id, Content = comment.Content, PostedOn = comment.PostedOn };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var teamCommentPostedEvent = new TeamCommentPostedEvent(
                Id,
                Version + 1,
                jsonPayload);

            Apply(teamCommentPostedEvent);
        }

        public override void Delete()
        {
            CheckIfCreated();

            var teamDeletedEvent = new TeamDeletedEvent(
                Id,
                Version + 1,
                "");

            Apply(teamDeletedEvent);
        }

        protected override void ChangeState(DomainEvent domainEvent)
        {
            dynamic data = JsonConvert.DeserializeObject(domainEvent.JsonPayload);
            switch (domainEvent.Type)
            {
                case "TeamCreatedEvent":
                    Id = domainEvent.AggregateId;
                    ProjectOwner = new User((Guid)data.ProjectOwnerId);

                    Chat = new List<Comment>();
                    Users = new List<User>();

                    break;

                case "TeamJoinedEvent":
                    Users.Add(new User((Guid)data.UserId));
                    break;

                case "TeamUserApprovedEvent":
                    var user = Users.FirstOrDefault(u => u.Id == (Guid)data.UserId);

                    user.Approve();

                    break;

                case "TeamCommentPostedEvent":
                    var comment = new Comment((Guid)data.Id, new User((Guid)data.AuthorId), (string)data.Content, (DateTime)data.PostedOn);
                    Chat.Add(comment);
                    break;

                case "TeamDeletedEvent":
                    Deleted = true;
                    break;

                default:
                    throw new Exception("Unknown event");
            }
        }

        private void CheckIfCreated()
        {
            if (!IsCreated())
            {
                throw new Exception("Team not created");
            }
        }
    }
}
