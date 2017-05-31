using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Extensions;

namespace Venture.Common.Tests.Cqrs
{
    [TestClass]
    public class CommandDispatcherTests
    {
        [TestMethod]
        public void When_CommandHandlerRegistered_Should_FindAndExecuteIt()
        {
            // Arrange
            var command = new Command();

            var commandHandler = new CommandHandler();

            var provider = new Mock<IServiceProvider>();

            provider.Setup(p => p.GetService(typeof(ICommandHandler<Command>))).Returns(commandHandler);

            var dispatcher = new CommandDispatcher(provider.Object);
            
            // Act
            dispatcher.Handle(command);

            // Assert
            command.Executed.Should().BeTrue();
        }

        [TestMethod]
        public void When_CommandHandlerNotRegistered_Should_ThrowException()
        {
            // Arrange
            var command = new Command();

            var provider = new Mock<IServiceProvider>();

            provider.Setup(p => p.GetService(typeof(ICommandHandler<Command>))).Returns(null);

            var dispatcher = new CommandDispatcher(provider.Object);

            Action handle = () => dispatcher.Handle(command);

            // Act

            // Assert
            handle.ShouldThrow<Exception>();
        }
    }

    internal class Command : ICommand
    {
        public bool Executed = false;
    }

    internal class CommandHandler : ICommandHandler<Command>
    {
        public void Execute(Command command)
        {
            command.Executed = true;
        }
    }
}
