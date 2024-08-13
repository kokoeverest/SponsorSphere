using Microsoft.AspNetCore.Http;
using NSubstitute;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Common.Helpers;
using SponsorSphere.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SponsorSphere.UnitTests.HelperTests
{
    public class PictureHelperTests
    {
        private static byte[] _validFileSize = new byte[FileConstants.FileMaxSize - 1];
        private static byte[] _invalidFileSize = new byte[FileConstants.FileMaxSize];
        private static MemoryStream _validStream = new(_validFileSize);
        private static MemoryStream _invalidStream = new(_invalidFileSize);
        private static string _fileName = "filename.jpg";
        private static string _name = "name";

        [Fact]
        public async Task PictureHelper_ShouldTransform_IFormFile_ToPicture()
        {
            // Arrange
            var fakeIFormFile = new FormFile(_validStream, 0, _validStream.Length, _name, _fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            // Act
            var result = await PictureHelper.TransformFileToPicture(fakeIFormFile, default);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Picture>(result);
        }

        [Fact]
        public async Task PictureHelper_ThrowsBadRequestException_IfFileTooLarge()
        {
            // Arrange
            var fakeIFormFile = new FormFile(_invalidStream, 0, _invalidStream.Length, _name, _fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => PictureHelper.TransformFileToPicture(fakeIFormFile, default));
        }
    }
}
