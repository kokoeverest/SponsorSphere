using Microsoft.AspNetCore.Http;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Common.Helpers
{
    /// <summary>
    /// A static helper class that provides methods for working with pictures.
    /// </summary>
    public static class PictureHelper
    {
        /// <summary>
        /// Transforms the provided file into a Picture object.
        /// </summary>
        /// <param name="picture">The IFormFile object representing the picture file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Picture object representing the transformed file.</returns>
        public static async Task<Picture> TransformFileToPicture(IFormFile picture, CancellationToken cancellationToken)
        {
            if (!FileConstants.validPictureFormats.Contains(picture.ContentType))
            {
                throw new BadRequestException("The file type is incorrect! Only .png, .jpg and .jpeg files are supported");
            }

            var fileExtension = Path.GetExtension(picture.FileName).ToLowerInvariant();
            if (!FileConstants.validExtensions.Contains(fileExtension))
            {
                throw new BadRequestException("The file extension is incorrect! Only .png, .jpg and .jpeg files are supported");
            }

            if (picture.Length > FileConstants.MaxFileSize)
            {
                throw new BadRequestException("The file is too large.");
            }

            if (picture.Length < FileConstants.MinFileSize)
            {
                throw new BadRequestException("The file is too small.");
            }

            using var memoryStream = new MemoryStream();
            await picture.CopyToAsync(memoryStream, cancellationToken);

            return new Picture
            {
                Content = memoryStream.ToArray(),
                Modified = DateTime.UtcNow,
            };
        }
    }
}
