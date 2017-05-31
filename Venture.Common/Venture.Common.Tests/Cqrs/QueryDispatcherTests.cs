using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Cqrs.Queries;

namespace Venture.Common.Tests.Cqrs
{
    [TestClass]
    public class QueryDispatcherTests
    {
        [TestMethod]
        public void When_QueryHandlerRegistered_Should_FindAndExecuteIt()
        {
            // Arrange
            var query = new Query();

            var queryHandler = new QueryHandler();

            var provider = new Mock<IServiceProvider>();

            provider.Setup(p => p.GetService(typeof(IQueryHandler<Query, string>))).Returns(queryHandler);

            var dispatcher = new QueryDispatcher(provider.Object);

            // Act
            var result = dispatcher.Handle(query);

            // Assert
            result.Should().Be("result");
        }

        [TestMethod]
        public void When_QueryHandlerNotRegistered_Should_ThrowException()
        {
            // Arrange
            var query = new Query();

            var provider = new Mock<IServiceProvider>();

            provider.Setup(p => p.GetService(typeof(IQueryHandler<Query, string>))).Returns(null);

            var dispatcher = new QueryDispatcher(provider.Object);

            Action handle = () => dispatcher.Handle(query);

            // Act

            // Assert
            handle.ShouldThrow<Exception>();
        }
    }

    internal class Query : IQuery<string>
    {
    }

    internal class QueryHandler : IQueryHandler<Query, string>
    {
        public string Retrieve(Query query)
        {
            return "result";
        }
    }
}
