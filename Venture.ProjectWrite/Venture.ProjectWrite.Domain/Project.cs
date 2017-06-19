using System.Collections.Generic;
using Newtonsoft.Json;
using Venture.Common.Data;
using Venture.Common.Events;

namespace Venture.ProjectWrite.Domain
{
    public sealed class Project : AggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<string> Tags { get; }
        public IList<Comment> Chat { get; }

        public Project()
        {
            Tags = new List<string>();
            Chat = new List<Comment>();
        }

        public void ChangeTitle(string newTitle)
        {
            var payload = new {newTitle};
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var titleChangedEvent = new DomainEvent(
                "ProjectTitleChanged",
                Id,
                1,
                jsonPayload);

            Apply(titleChangedEvent);
        }

        protected override void ChangeState(DomainEvent domainEvent)
        {
            dynamic data = JsonConvert.DeserializeObject(domainEvent.JsonPayload);
            switch (domainEvent.Type)
            {
                //case "ProjectCreated":
                //    Id = domainEvent.AggregateId;
                //    Title = data.Title;
                //    Description = data.Description;
                //    break;
                //case "ProjectTitleChanged":
                //    Title = data.Title;
                //    break;
                //case "ProjectDescriptionChanged":
                //    Description = data.Description;
                //    break;
                //case "ProjectTagsUpdated":
                //    Tags.Add(data.AddedTags);
                //    Tags = 
                default:
                    break;
            }
        }
    }
}
