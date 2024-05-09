using CloudinaryDotNet.Actions;


namespace NomsNoms.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
