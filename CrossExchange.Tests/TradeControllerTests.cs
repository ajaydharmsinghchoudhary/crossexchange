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
    public class TradeControllerTests
    {
        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();
        private readonly Mock<ITradeRepository> _tradeRepositoryMock = new Mock<ITradeRepository>();
        private readonly Mock<IPortfolioRepository> _portfolioRepositoryMock = new Mock<IPortfolioRepository>();

        private readonly TradeController _tradeController;

        public TradeControllerTests()
        {
            _tradeController = new TradeController(_shareRepositoryMock.Object, _tradeRepositoryMock.Object, _portfolioRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllTradings_ShouldRetrunBadRequest()
        {
            int portFolioid = 1;

            // Arrange

            // Act
            var result = await _tradeController.GetAllTradings(portFolioid);

            // Assert
            var returnResult = result as BadRequestResult;
            Assert.IsInstanceOf(typeof(BadRequestResult), returnResult);
        }

        [Test]
        public async Task Post_ShouldRetrunBadRequest()
        {
            TradeModel model = null;

            // Arrange

            // Act
            var result = await _tradeController.Post(model);

            // Assert
            var returnResult = result as BadRequestObjectResult;
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), returnResult);
        }

    }
}
