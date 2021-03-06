using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RawRabbit;
using Venture.Common.Extensions;

namespace Venture.Common.Tests.Extensions
{
    [TestClass]
    public class ServiceCollectionExtensionsTests
    {
        private IServiceProvider _sut;

        [TestInitialize]
        public void SetUp()
        {
            _sut = new ServiceCollection()
                .AddVentureCommon("test")
                .BuildServiceProvider();
        }

        [TestCleanup]
        public void TearDown()
        {
            _sut = null;
        }

        [TestMethod]
        public void When_AddVentureCommonIsCalled_BusClientShouldBeRegistered()
        {
            // Arrange

            // Act
            var busClient = _sut.GetService<IBusClient>();

            // Assert
            busClient.Should().NotBeNull();
        }
    }
}
