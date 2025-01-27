using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Common.Helpers;
using SponsorSphere.Domain.Models;
using SponsorSphere.UnitTests.Helpers;

namespace SponsorSphere.UnitTests.HelperTests
{
    public class PictureHelperTests
    {
        [Fact]
        public async Task PictureHelper_ShouldTransform_IFormFile_ToPicture()
        {
            // Arrange & Act
            var result = await PictureHelper.TransformFileToPicture(TestData.fakeValidIFormFile, default);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Picture>(result);
        }

        [Fact]
        public async Task PictureHelper_ThrowsBadRequestException_IfFileTooLarge()
        {
            // Arrange, Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => PictureHelper.TransformFileToPicture(TestData.fakeTooLargeIFormFile, default));
        }

        [Fact]
        public async Task PictureHelper_ThrowsBadRequestException_IfFileTooSmall()
        {
            // Arrange, Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => PictureHelper.TransformFileToPicture(TestData.fakeTooSmallIFormFile, default));
        }

        [Fact]
        public async Task PictureHelper_ThrowsBadRequestException_IfInvalidContentType()
        {
            // Arrange, Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => PictureHelper.TransformFileToPicture(TestData.fakeTooLargeIFormFile, default));
        }

        [Fact]
        public async Task PictureHelper_ThrowsBadRequestException_IfInvalidFileExtension()
        {
            // Arrange, Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => PictureHelper.TransformFileToPicture(TestData.fakeTooLargeIFormFile, default));
        }
    }
}
