using Microsoft.AspNetCore.Http;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.Common.Helpers
{
    public static class PictureHelper
    {
        public static async Task<Picture> TransformFileToPicture(IFormFile picture, CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();

            await picture.CopyToAsync(memoryStream, cancellationToken);

            if (memoryStream.Length < FileConstants.FileMaxSize)
            {
                return new Picture
                {
                    Content = memoryStream.ToArray(),
                    Modified = DateTime.UtcNow,
                };
            }
            else
            {
                throw new BadRequestException("The file is too large.");
            }
        }
    }
}
