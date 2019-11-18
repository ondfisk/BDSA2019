using BDSA2019.Lecture10.Web.Controllers;
using BDSA2019.Lecture10.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BDSA2019.Lecture10.Web.Models.ImageType;

namespace BDSA2019.Lecture10.Web.Tests.Controllers
{
    public class UploadControllerTests
    {
        [Theory]
        [InlineData("Batman", Portrait, "batman-portrait.jpg")]
        [InlineData("Wonder Woman", Background, "wonder-woman-background.jpg")]
        public async Task Post_given_input_generates_blobName(string alterEgo, ImageType type, string expectedBlobName)
        {
            var file = new Mock<IFormFile>();
            file.SetupGet(m => m.ContentType).Returns("image/jpeg");

            var blobManager = new Mock<IBlobManager>();
            blobManager.Setup(s => s.UploadAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>())).ReturnsAsync(new Uri("https://blobs.com/image.jpg"));

            var controller = new UploadController(blobManager.Object);

            await controller.Post(alterEgo, type, file.Object);

            blobManager.Verify(m => m.UploadAsync(expectedBlobName, It.IsAny<string>(), It.IsAny<Stream>()));
        }

        [Fact]
        public async Task Post_given_input_returns_result_from_blobManager()
        {
            var stream = new MemoryStream();
            var file = new Mock<IFormFile>();
            file.Setup(s => s.OpenReadStream()).Returns(stream);
            file.SetupGet(m => m.ContentType).Returns("image/jpeg");

            var blobManager = new Mock<IBlobManager>();
            blobManager.Setup(s => s.UploadAsync("batman-portrait.jpg", "image/jpeg", stream)).ReturnsAsync(new Uri("https://blobs.com/image.jpg"));

            var controller = new UploadController(blobManager.Object);

            var result = await controller.Post("batman", Portrait, file.Object) as CreatedResult;

            Assert.Equal("https://blobs.com/image.jpg", result.Location);
        }

        [Fact]
        public async Task Post_given_non_jpg_contentType_returns_BadRequest()
        {
            var blobManager = new Mock<IBlobManager>();
            var file = new Mock<IFormFile>();
            file.SetupGet(m => m.ContentType).Returns("image/png");

            var controller = new UploadController(blobManager.Object);

            var result = await controller.Post("batman", Portrait, file.Object);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
