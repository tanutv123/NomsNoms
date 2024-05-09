using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.Entities;
using NomsNoms.Interfaces;
using System.Net;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var result = await _imageService.UploadAsync(file);
            if (result == null)
            {
                ModelState.AddModelError("Upload image", " Something went wrong");
                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }
            return Ok(new { link = result.SecureUri.AbsoluteUri, publicId = result.PublicId });

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] RecipeImage image)
        {
            var result = await _imageService.DeletePhotoAsync(image.PublicId);
            if (result == null)
            {
                ModelState.AddModelError("Delete image", "Something went wrong during deleting the image");
                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }
            return Ok(new { message = "Image successfully deleted.", deletedPublicId = image.PublicId });
        }
        [HttpDelete("user-image")]
        public async Task<IActionResult> DeleteAsync([FromBody] UserPhoto image)
        {
            var result = await _imageService.DeletePhotoAsync(image.PublicId);
            if (result == null)
            {
                ModelState.AddModelError("Delete image", "Something went wrong during deleting the image");
                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }
            return Ok(new { message = "Image successfully deleted.", deletedPublicId = image.PublicId });
        }
    }
}
