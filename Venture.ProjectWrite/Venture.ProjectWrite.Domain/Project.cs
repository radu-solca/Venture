using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Venture.Common.Data;
using Venture.Common.Events;

namespace Venture.ProjectWrite.Domain
{
    public sealed class Project : AggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid OwnerId { get; private set; }
        public ICollection<string> Tags { get; private set; }
        public ICollection<Comment> Chat { get; private set; }

        public void CreateProject(
            Guid id, 
            string title, 
            string description,
            Guid ownerId)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || ownerId == Guid.Empty)
            {
                throw new ArgumentException("A title, description and ownerId needs to be provided.");
            }

            var payload = new { title, description, ownerId };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var projectCreatedEvent = new DomainEvent(
                "ProjectCreated",
                id,
                1,
                jsonPayload);

            Apply(projectCreatedEvent);
        }

        public void UpdateTitle(string newTitle)
        {
            CheckIfCreated();

            var payload = new {newTitle};
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var titleUpdatedEvent = new DomainEvent(
                "ProjectTitleUpdated",
                Id,
                Version + 1,
                jsonPayload);

            Apply(titleUpdatedEvent);
        }

        public void UpdateDescription(string newDescription)
        {
            CheckIfCreated();

            var payload = new { newDescription };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var descriptionUpdatedEvent = new DomainEvent(
                "ProjectDescriptionUpdated",
                Id,
                Version + 1,
                jsonPayload);

            Apply(descriptionUpdatedEvent);
        }

        public void UpdateTags(IList<string> addedTags, IList<string> removedTags)
        {
            CheckIfCreated();

            var payload = new { addedTags, removedTags };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var tagsUpdatedEvent = new DomainEvent(
                "ProjectTagsUpdated",
                Id,
                Version + 1,
                jsonPayload);

            Apply(tagsUpdatedEvent);
        }

        public void PostComment(Guid authorId, string content, DateTime postedOn)
        {
            CheckIfCreated();

            var payload = new { authorId, content, postedOn };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var commentPostedEvent = new DomainEvent(
                "ProjectCommentPosted",
                Id,
                Version + 1,
                jsonPayload);

            Apply(commentPostedEvent);
        }

        protected override void ChangeState(DomainEvent domainEvent)
        {
            dynamic data = JsonConvert.DeserializeObject(domainEvent.JsonPayload);
            switch (domainEvent.Type)
            {
                case "ProjectCreated":
                    Id = domainEvent.AggregateId;
                    Title = data.title;
                    Description = data.description;
                    OwnerId = data.ownerId;

                    Tags = new List<string>();
                    Chat = new List<Comment>();

                    break;

                case "ProjectTitleUpdated":
                    CheckIfCreated();
                    Title = data.newTitle;
                    break;

                case "ProjectDescriptionUpdated":
                    CheckIfCreated();
                    Description = data.newDescription;
                    break;

                case "ProjectTagsUpdated":
                    CheckIfCreated();
                    foreach (var removedTag in data.removedTags)
                    {
                        Tags.Remove((string)removedTag);
                    }
                    foreach (var addedTag in data.addedTags)
                    {
                        Tags.Add((string)addedTag);
                    }
                    break;

                case "ProjectCommentPosted":
                    CheckIfCreated();
                    var comment = new Comment(Guid.NewGuid(), (Guid)data.authorId, (string)data.content, (DateTime)data.postedOn);
                    Chat.Add(comment);
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
