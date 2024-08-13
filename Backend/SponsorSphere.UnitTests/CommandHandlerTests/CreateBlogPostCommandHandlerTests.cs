using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SponsorSphere.Application.App.BlogPosts.Commands;
using SponsorSphere.Application.App.BlogPosts.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphere.UnitTests.Helpers;

namespace SponsorSphere.UnitTests.CommandHandlerTests
{
    public class CreateBlogPostCommandHandlerTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMapper _mapperMock;
        private readonly ILogger<CreateBlogPostCommandHandler> _loggerMock;

        public CreateBlogPostCommandHandlerTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mapperMock = Substitute.For<IMapper>();
            _loggerMock = Substitute.For<ILogger<CreateBlogPostCommandHandler>>();
        }

        [Fact]
        public async Task CreateBlogPost_ValidCommand_ShouldCreateBlogPost_WithNoPictures()
        {
            // Arrange
            var fakeCreateBlogPostDto = Substitute.For<CreateBlogPostDto>();
            _mapperMock.Map<BlogPost>(Arg.Any<CreateBlogPostDto>()).Returns(TestData.FakeBlogPost);

            _unitOfWorkMock.BlogPostsRepository
                .CreateAsync(Arg.Any<BlogPost>())
                .Returns(await Task.FromResult(TestData.FakeBlogPost));

            _mapperMock.Map<BlogPostDto>(Arg.Any<BlogPost>()).Returns(TestData.FakeBlogPostDto);

            var handler = new CreateBlogPostCommandHandler(_unitOfWorkMock, _mapperMock, _loggerMock);
            var command = new CreateBlogPostCommand(fakeCreateBlogPostDto);

            // Act
            var actualResult = await handler.Handle(command, default);

            // Assert
            Assert.Equal(TestData.FakeBlogPostDto, actualResult);
            Assert.Empty(actualResult.Pictures!);

            await _unitOfWorkMock.BlogPostsRepository.Received(1)
                .CreateAsync(Arg.Is<BlogPost>(bp =>
                    bp.Content == TestData.FakeBlogPost.Content &&
                    bp.AuthorId == TestData.FakeBlogPost.AuthorId));
        }

        [Fact]
        public async Task CreateBlogPost_ValidBlogPostWithPictures_ShouldHaveNonEmptyPicturesList()
        {
            // Arrange
            var fakePicture = Substitute.For<IFormFile>();
            var fakeCreateBlogPostDto = Substitute.For<CreateBlogPostDto>();
            fakeCreateBlogPostDto.Pictures = 
                [ fakePicture
                ];
            _mapperMock.Map<BlogPost>(Arg.Any<CreateBlogPostDto>()).Returns(TestData.FakeBlogPost);

            _unitOfWorkMock.BlogPostsRepository
                .CreateAsync(Arg.Any<BlogPost>())
                .Returns(await Task.FromResult(TestData.FakeBlogPostWithPictures));

            _mapperMock.Map<BlogPostDto>(Arg.Any<BlogPost>()).Returns(TestData.FakeBlogPostDtoWithPictures);

            var handler = new CreateBlogPostCommandHandler(_unitOfWorkMock, _mapperMock, _loggerMock);
            var command = new CreateBlogPostCommand(fakeCreateBlogPostDto);

            // Act
            var actualResult = await handler.Handle(command, default);

            // Assert
            Assert.NotEmpty(actualResult.Pictures!);
        }
    }
}
