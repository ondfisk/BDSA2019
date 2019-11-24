using System.Threading.Tasks;
using BDSA2019.Lecture11.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BDSA2019.Lecture11.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IBlobManager _blobManager;

        public UploadController(IBlobManager blobManager)
        {
            _blobManager = blobManager;
        }

        [HttpPost("{alterEgo}/{type}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(string alterEgo, ImageType type, [FromForm]IFormFile image)
        {
            if (image.ContentType != "image/jpeg")
            {
                return BadRequest("Only JPGs are supported");
            }

            var blobName = $"{alterEgo.Replace(" ", "-")}-{type.ToString()}.jpg".ToLower();

            var uri = await _blobManager.UploadAsync(blobName, image.ContentType, image.OpenReadStream());

            return Created(uri, null);
        }
    }
}
