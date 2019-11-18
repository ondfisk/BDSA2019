using System;
using System.IO;
using System.Threading.Tasks;

namespace BDSA2019.Lecture10.Web.Models
{
    public interface IBlobManager
    {
        Task<Uri> UploadAsync(string blobName, string contentType, Stream stream);
    }
}
