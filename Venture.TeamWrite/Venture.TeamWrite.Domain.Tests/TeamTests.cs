using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Venture.TeamWrite.Domain.Tests
{
    [TestClass]
    public class TeamTests
    {
        public Team Sut { get; set; }

        private readonly Guid _id = Guid.NewGuid();
        private readonly Guid _ownerId = Guid.NewGuid();
        private readonly Guid _memberId = Guid.NewGuid();

        [TestInitialize]
        public void SetUp()
        {
            Sut = new Team();
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
            CreateTeam();
            JoinTeam();
            ApproveUser();
            Delete();

            // Assert
            Sut.UncommitedChanges.Should().HaveCount(4);
        }

        [TestMethod]
        public void When_Created_Then_ShouldHaveCorrectData()
        {
            // Arrange

            // Act
            CreateTeam();

            // Assert
            Sut.Id.Should().Be(_id);
            Sut.ProjectOwner.Id.Should().Be(_ownerId);

            Sut.Chat.Should().BeEmpty();
        }

        [TestMethod]
        public void When_UserJoined_Then_ShouldNotBeApproved()
        {
            // Arrange
            CreateTeam();

            // Act
            JoinTeam();

            // Assert
            Sut.Users.First().Approved.Should().BeFalse();
        }

        [TestMethod]
        public void When_UserApproved_Then_ShouldBeApproved()
        {
            // Arrange
            CreateTeam();
            JoinTeam();

            // Act
            ApproveUser();

            // Assert
            Sut.Users.First().Approved.Should().BeTrue();
        }

        [TestMethod]
        public void When_CommentPosted_Then_ShouldHaveComment()
        {
            // Arrange
            CreateTeam();
            JoinTeam();
            ApproveUser();

            // Act
            PostComment();

            // Assert
            Sut.Chat.Count.Should().Be(1);
        }

        private void CreateTeam()
        {
            Sut.Create(_id, new User(_ownerId));
        }

        private void JoinTeam()
        {
            Sut.Join(new User(_memberId));
        }

        private void ApproveUser()
        {
            Sut.Approve(new User(_memberId));
        }

        private void PostComment()
        {
            var user = new User(_memberId);
            user.Approve();
            Sut.PostComment(new Comment(Guid.NewGuid(), user, "a comment", DateTime.Now));
        }

        private void Delete()
        {
            Sut.Delete();
        }
    }
}
