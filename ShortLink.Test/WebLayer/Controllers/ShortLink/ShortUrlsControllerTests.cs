using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShortLink.BL.DoubleUrl.CreateShortUrl.CreateDoubleUrl;
using ShortLink.Web.Controllers;

namespace ShortLink.Test.WebLayer.Controllers.ShortLink
{
    [TestFixture]
    public class ShortUrlsControllerTests
    {
        private Mock<IMediator> _mockMediator;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Structure", "NUnit1032:An IDisposable field/property should be Disposed in a TearDown method", Justification = "<Pending>")]
        private ShortUrlsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new ShortUrlsController(_mockMediator.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (_controller != null)
            {
                (_controller as IDisposable)?.Dispose();
            }
        }

        [Test]
        public async Task CreateShortUrl_ReturnsOkResult_WithShortUrl()
        {
            // Arrange
            var command = new CreateDoubleUrlCommand { OriginalUrl = "https://validurl.com" };
            var shortUrl = "short123";

            _mockMediator.Setup(m => m.Send(It.IsAny<CreateDoubleUrlCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(shortUrl);

            // Act
            var result = await _controller.CreateShortUrl(command);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.AreEqual(shortUrl, okResult.Value.GetType().GetProperty("shortUrl")?.GetValue(okResult.Value, null));
        }

        [Test]
        public async Task CreateShortUrl_ReturnsBadRequest_OnException()
        {
            // Arrange
            var command = new CreateDoubleUrlCommand { OriginalUrl = "https://validurl.com" };
            var exceptionMessage = "Url is not valid.";

            _mockMediator.Setup(m => m.Send(It.IsAny<CreateDoubleUrlCommand>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.CreateShortUrl(command);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(exceptionMessage, badRequestResult.Value);
        }
    }
}

