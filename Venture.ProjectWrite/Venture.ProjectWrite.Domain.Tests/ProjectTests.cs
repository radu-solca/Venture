using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Venture.ProjectWrite.Domain.Tests
{
    [TestClass]
    public class ProjectTests
    {
        public Project Sut { get; set; }

        private readonly Guid _id = new Guid("84231485-0eb6-4b76-b4fe-fc639083eb0b");
        private readonly string _title = "some title";
        private readonly string _description = "some desc";
        private readonly Guid _ownerId = new Guid("95d7418e-1dc5-4fcd-9cc3-51dd2edf50bd");

        [TestInitialize]
        public void SetUp()
        {
            Sut = new Project();
        }

        [TestCleanup]
        public void TearDown()
        {
            Sut = null;
        }

        [TestMethod]
        public void When_Instanciated__Then_ShouldNotBeCreated()
        {
            // Arrange

            // Act
            var isCreated = Sut.IsCreated();

            // Assert
            isCreated.Should().BeFalse();
        }

        [TestMethod]
        public void When_ActedUpon_Then_ShouldHaveUncommitedChanges()
        {
            // Arrange

            // Act
            CreateProject();
            Sut.UpdateTitle("new title");
            Sut.PostComment(_ownerId, "a comment", DateTime.Now);

            // Assert
            Sut.UncommitedChanges.Should().HaveCount(3);
        }

        [TestMethod]
        public void When_ChangesMarkedAsCommited_Then_ShouldHaveNoUncommitedChanges()
        {
            // Arrange
            CreateProject();
            Sut.UpdateTitle("new title");
            Sut.PostComment(_ownerId, "a comment", DateTime.Now);

            // Act
            Sut.MarkChangesAsCommited();

            // Assert
            Sut.UncommitedChanges.Should().BeEmpty();
        }

        [TestMethod]
        public void When_Created_Then_ShouldHaveCorrectData()
        {
            // Arrange

            // Act
            CreateProject();

            // Assert
            Sut.Id.Should().Be(_id);
            Sut.Title.Should().Be(_title);
            Sut.Description.Should().Be(_description);
            Sut.OwnerId.Should().Be(_ownerId);

            Sut.Chat.Should().BeEmpty();
        }

        [TestMethod]
        public void When_CommentPosted_Then_ShouldHaveComment()
        {
            // Arrange
            CreateProject();

            // Act
            Sut.PostComment(_ownerId, "a comment", DateTime.Now);

            // Assert
            Sut.Chat.Should().HaveCount(1);
        }

        private void CreateProject()
        {
            Sut.CreateProject(_id, _title, _description, _ownerId);
        }
    }
}
