using System;
using System.Threading.Tasks;
using CrossExchange.Controller;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CrossExchange.Tests
{
    public class PortfolioControllerTests
    {
        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();
        private readonly Mock<ITradeRepository> _tradeRepositoryMock = new Mock<ITradeRepository>();
        private readonly Mock<IPortfolioRepository> _portfolioRepositoryMock = new Mock<IPortfolioRepository>();

        private readonly PortfolioController _portfolioController;

        public PortfolioControllerTests()
        {
            _portfolioController = new PortfolioController(_shareRepositoryMock.Object, _tradeRepositoryMock.Object, _portfolioRepositoryMock.Object);
        }

        [Test]
        public async Task GetPortfolioInfo_ShouldRetrunBadRequest()
        {
            int portFolioid = 1;

            // Arrange

            // Act
            var result = await _portfolioController.GetPortfolioInfo(portFolioid);

            // Assert
            var returnResult = result as NotFoundResult;
            Assert.IsInstanceOf(typeof(NotFoundResult), returnResult);
        }

        [Test]
        public async Task Post_ShouldRetrunBadRequest()
        {
            PortfolioModel model = null;

            // Arrange

            // Act
            var result = await _portfolioController.Post(model);

            // Assert
            var returnResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), returnResult);
        }

    }
}
