using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.Pictures.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly SponsorSphereDbContext _context;

        public PictureRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }

        public async Task<Picture> CreateAsync(Picture picture)
        {
            await _context.Pictures.AddAsync(picture);
            await _context.SaveChangesAsync();
            return picture;
        }

        public async Task<int> DeleteAsync(PictureDto picture)
        {
            return await _context.Pictures
                .Where(p => p.Id == picture.Id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<PictureDto>> GetByBlogPostIdAsync(int blogPostId)
        {
            return await _context.Pictures
                .Where(p => _context.BlogPostPictures.Any(bp => bp.BlogPostId == blogPostId && bp.PictureId == p.Id))
                .Select(p => new PictureDto
                {
                    Id = p.Id,
                    Url = p.Url,
                    Content = p.Content,
                    Modified = p.Modified,
                })
                .ToListAsync();
        }

        public async Task<PictureDto> UpdateAsync(PictureDto updatedPicture)
        {
            await _context.Pictures
                .Where(p => p.Id == updatedPicture.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.Content, updatedPicture.Content)
                .SetProperty(p => p.Url, updatedPicture.Url)
                .SetProperty(p => p.Modified, DateTime.UtcNow));

            return updatedPicture;
        }
    }
}
