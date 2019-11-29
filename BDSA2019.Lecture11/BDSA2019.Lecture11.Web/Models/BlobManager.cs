using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BDSA2019.Lecture11.Web.Models
{
    public class BlobManager : IBlobManager
    {
        private readonly BlobContainerClient _containerClient;

        public BlobManager(BlobContainerClient containerClient)
        {
            _containerClient = containerClient;
        }

        public async Task<Uri> UploadAsync(string blobName, string contentType, Stream stream)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(stream, true, default);

            await blobClient.SetHttpHeadersAsync(new BlobHttpHeaders
            {
                ContentType = contentType
            });

            return blobClient.Uri;
        }
    }
}
