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
        public IList<string> Tags { get; private set; }
        public IList<Comment> Chat { get; private set; }

        public void CreateProject(
            Guid id, 
            string title, 
            string description)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("A title and a description needs to be provided.");
            }

            var payload = new { title, description };
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
            var payload = new { addedTags, removedTags };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var tagsUpdatedEvent = new DomainEvent(
                "ProjectTagsUpdated",
                Id,
                Version + 1,
                jsonPayload);

            Apply(tagsUpdatedEvent);
        }

        public void PostComment(Comment comment)
        {
            var payload = new { comment };
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

                    Tags = new List<string>();
                    Chat = new List<Comment>();

                    break;

                case "ProjectTitleUpdated":
                    Title = data.title;
                    break;

                case "ProjectDescriptionUpdated":
                    Description = data.description;
                    break;

                case "ProjectTagsUpdated":
                    foreach (var removedTag in data.removedTags)
                    {
                        Tags.Remove(removedTag);
                    }
                    foreach (var addedTag in data.addedTags)
                    {
                        Tags.Add(addedTag);
                    }
                    break;

                case "ProjectCommentPosted":
                    Chat.Add(data.comment);
                    break;

                default:
                    throw new Exception("Unknown event");
            }
        }
    }
}
