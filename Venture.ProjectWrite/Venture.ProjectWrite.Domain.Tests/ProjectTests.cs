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
        private const string Title = "some title";
        private const string Description = "some desc";
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
        public void When_ActedUpon_Then_ShouldHaveUncommitedChanges()
        {
            // Arrange

            // Act
            CreateProject();
            UpdateTitle();
            PostComment();

            // Assert
            Sut.UncommitedChanges.Should().HaveCount(3);
        }

        [TestMethod]
        public void When_Created_Then_ShouldHaveCorrectData()
        {
            // Arrange

            // Act
            CreateProject();

            // Assert
            Sut.Id.Should().Be(_id);
            Sut.Title.Should().Be(Title);
            Sut.Description.Should().Be(Description);
            Sut.Owner.Id.Should().Be(_ownerId);

            Sut.Chat.Should().BeEmpty();
        }

        [TestMethod]
        public void When_CommentPosted_Then_ShouldHaveComment()
        {
            // Arrange
            CreateProject();

            // Act
            PostComment();

            // Assert
            Sut.Chat.Should().HaveCount(1);
        }

        private void CreateProject()
        {
            Sut.Create(_id, Title, Description, new User(_ownerId));
        }

        private void UpdateTitle()
        {
            Sut.UpdateTitle("new title");
        }

        private void PostComment()
        {
            Sut.PostComment(new Comment(Guid.NewGuid(), new User(_ownerId), "a comment", DateTime.Now));
        }
    }
}
