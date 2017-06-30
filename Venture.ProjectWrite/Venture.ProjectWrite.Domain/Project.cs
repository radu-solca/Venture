using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.ProjectWrite.Domain.DomainEvents;

namespace Venture.ProjectWrite.Domain
{
    public sealed class Project : AggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public User Owner { get; private set; }
        public ICollection<Comment> Chat { get; private set; }

        public void Create(
            Guid id, 
            string title, 
            string description,
            User owner)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || owner == null)
            {
                throw new ArgumentException("A title, description and owner needs to be provided.");
            }

            var payload = new { Title = title, Description = description, OwnerId = owner.Id };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var projectCreatedEvent = new ProjectCreatedEvent(
                id,
                1,
                jsonPayload);

            Apply(projectCreatedEvent);
        }

        public void UpdateTitle(string newTitle)
        {
            CheckIfCreated();

            var payload = new { NewTitle = newTitle };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var titleUpdatedEvent = new ProjectTitleUpdatedEvent(
                Id,
                Version + 1,
                jsonPayload);

            Apply(titleUpdatedEvent);
        }

        public void UpdateDescription(string newDescription)
        {
            CheckIfCreated();

            var payload = new { NewDescription = newDescription };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var descriptionUpdatedEvent = new ProjectDescriptionUpdatedEvent(
                Id,
                Version + 1,
                jsonPayload);

            Apply(descriptionUpdatedEvent);
        }

        public void PostComment(Comment comment)
        {
            CheckIfCreated();

            var payload = new { Id = comment.Id, AuthorId = comment.Author.Id, Content = comment.Content, PostedOn = comment.PostedOn };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var commentPostedEvent = new ProjectCommentPostedEvent(
                Id,
                Version + 1,
                jsonPayload);

            Apply(commentPostedEvent);
        }

        public override void Delete()
        {
            CheckIfCreated();

            var projectDeletedEvent = new ProjectDeletedEvent(
                Id,
                Version + 1,
                "");

            Apply(projectDeletedEvent);
        }

        protected override void ChangeState(DomainEvent domainEvent)
        {
            dynamic data = JsonConvert.DeserializeObject(domainEvent.JsonPayload);
            switch (domainEvent.Type)
            {
                case "ProjectCreatedEvent":
                    Id = domainEvent.AggregateId;
                    Title = (string)data.Title;
                    Description = (string)data.Description;
                    Owner = new User((Guid)data.OwnerId);

                    Chat = new List<Comment>();

                    break;

                case "ProjectTitleUpdatedEvent":
                    Title = (string)data.NewTitle;
                    break;

                case "ProjectDescriptionUpdatedEvent":
                    Description = (string)data.NewDescription;
                    break;

                case "ProjectCommentPostedEvent":
                    var comment = new Comment((Guid)data.Id, new User((Guid)data.AuthorId), (string)data.Content, (DateTime)data.PostedOn);
                    Chat.Add(comment);
                    break;

                case "ProjectDeletedEvent":
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
                throw new Exception("Project not created");
            }
        }
    }
}
