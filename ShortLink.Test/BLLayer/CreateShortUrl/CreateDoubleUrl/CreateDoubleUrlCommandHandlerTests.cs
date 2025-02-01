using Microsoft.EntityFrameworkCore;
using Moq;
using ShortLink.BL.DoubleUrl.CreateShortUrl.CreateDoubleUrl;
using ShortLink.BL.Interfaces;
using ShortLink.DAL.Data;

namespace ShortLink.Test.BLLayer.CreateShortUrl.CreateDoubleUrl
{
    [TestFixture]
    public class CreateDoubleUrlCommandHandlerTests
    {
        private ApplicationDbContext _context;
        private Mock<IUrlService> _mockUrlService;
        private CreateDoubleUrlCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _mockUrlService = new Mock<IUrlService>();
            _handler = new CreateDoubleUrlCommandHandler(_context, _mockUrlService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Handle_ValidUrl_ReturnsShortUrl()
        {
            // Arrange
            var request = new CreateDoubleUrlCommand { OriginalUrl = "https://validurl.com" };
            var shortUrl = "short123";

            _mockUrlService.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(true);
            _mockUrlService.Setup(x => x.GenerateShortUrl()).Returns(shortUrl);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.AreEqual(shortUrl, result);
            Assert.AreEqual(1, _context.DoubleUrls.Count());
        }

        [Test]
        public void Handle_InvalidUrl_ThrowsException()
        {
            // Arrange
            var request = new CreateDoubleUrlCommand { OriginalUrl = "invalidurl" };

            _mockUrlService.Setup(x => x.IsValidUrl(It.IsAny<string>())).Returns(false);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.AreEqual("Url is not valid.", ex.Message);
        }
    }
}
