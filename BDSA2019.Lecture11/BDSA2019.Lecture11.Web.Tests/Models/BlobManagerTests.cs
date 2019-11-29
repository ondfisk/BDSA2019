using System;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BDSA2019.Lecture11.Web.Models;
using Moq;
using Xunit;

namespace BDSA2019.Lecture11.Web.Tests.Models
{
    public class BlobManagerTests
    {
        [Fact]
        public async Task UploadAsync_uploads_to_storage_with_overwrite_and_returns_Uri()
        {
            var stream = new MemoryStream();
            var uri = new Uri("https://blob.uri/container/blob.name");
            var info = new Mock<Response<BlobContentInfo>>();

            var blob = new Mock<BlobClient>(uri, default(BlobClientOptions));
            blob.Setup(s => s.UploadAsync(stream, true, default)).ReturnsAsync(info.Object);
            blob.SetupGet(s => s.Uri).Returns(uri);

            var client = new Mock<BlobContainerClient>("UseDevelopmentStorage=true", "container");
            client.Setup(s => s.GetBlobClient("blob.name")).Returns(blob.Object);

            var manager = new BlobManager(client.Object);

            var result = await manager.UploadAsync("blob.name", "image/jpeg", stream);

            Assert.Equal(uri, result);
        }

        [Fact]
        public async Task UploadAsync_sets_contentType_of_blob()
        {
            var stream = new MemoryStream();
            var uri = new Uri("https://blob.uri/container/blob.name");
            var info = new Mock<Response<BlobContentInfo>>();

            var blob = new Mock<BlobClient>(uri, default(BlobClientOptions));
            blob.Setup(s => s.UploadAsync(stream, true, default)).ReturnsAsync(info.Object);
            blob.SetupGet(s => s.Uri).Returns(uri);

            var client = new Mock<BlobContainerClient>("UseDevelopmentStorage=true", "container");
            client.Setup(s => s.GetBlobClient("blob.name")).Returns(blob.Object);

            var manager = new BlobManager(client.Object);

            await manager.UploadAsync("blob.name", "image/jpeg", stream);

            blob.Verify(b => b.SetHttpHeadersAsync(It.Is<BlobHttpHeaders>(h => h.ContentType == "image/jpeg"), default, default));
        }
    }
}
