using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class SponsorCompanyRepository : ISponsorCompanyRepository
    {
        private readonly SponsorSphereDbContext _context;

        public SponsorCompanyRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }

        public async Task<SponsorCompany> CreateAsync(SponsorCompany sponsorCompany)
        {
            try
            {
                await _context.SponsorCompanies.AddAsync(sponsorCompany);
                await _context.SaveChangesAsync();
                return sponsorCompany;
            }
            catch (DbUpdateException)
            {
                throw new InvalidDataException("User with that email already exists");
            }
        }

        public async Task<int> DeleteAsync(int userId)
        {
            await _context.Users
                .Where(sc => sc.Id.Equals(userId))
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(sc => sc.IsDeleted, true)
                .SetProperty(sc => sc.DeletedOn, DateTime.UtcNow));

            return 1;
        }

        public async Task<SponsorCompany> GetByIdAsync(int userId)
        {
            var sponsorCompany = await _context.SponsorCompanies.FirstOrDefaultAsync(sc => sc.Id == userId);

            if (sponsorCompany is null)
            {
                throw new NotFoundException($"Sponsor with id {userId} not found");
            }

            sponsorCompany.BlogPosts = await _context.BlogPosts
                .Include(bp => bp.Pictures)
                .Where(bp => bp.AuthorId == sponsorCompany.Id)
                .ToListAsync();

            sponsorCompany.Sponsorships = await _context.Sponsorships
                .Where(sh => sh.SponsorId == sponsorCompany.Id)
                .ToListAsync();

            return sponsorCompany;
        }

        public async Task<List<SponsorCompany>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.SponsorCompanies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }

        public async Task<List<SponsorCompany>> GetByCountryAsync(CountryEnum country)
        {
            return await _context.SponsorCompanies
                .Where(sponsorCompany => sponsorCompany.Country == country)
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }
        public async Task<SponsorCompanyDto> UpdateAsync(SponsorCompanyDto updatedSponsorCompany)
        {
            await _context.Users.Where(u => u.Id == updatedSponsorCompany.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(sc => sc.Name, updatedSponsorCompany.Name)
                .SetProperty(sc => sc.Email, updatedSponsorCompany.Email)
                .SetProperty(sc => sc.Country, updatedSponsorCompany.Country)
                .SetProperty(sc => sc.PhoneNumber, updatedSponsorCompany.PhoneNumber)
                .SetProperty(sc => sc.PictureId, updatedSponsorCompany.PictureId)
                .SetProperty(sc => sc.Website, updatedSponsorCompany.Website)
                .SetProperty(sc => sc.FaceBookLink, updatedSponsorCompany.FaceBookLink)
                .SetProperty(sc => sc.InstagramLink, updatedSponsorCompany.InstagramLink)
                .SetProperty(sc => sc.TwitterLink, updatedSponsorCompany.TwitterLink)
                .SetProperty(sc => sc.StravaLink, updatedSponsorCompany.StravaLink)
            );

            await _context.SponsorCompanies.Where(sc => sc.Id == updatedSponsorCompany.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(sc => sc.IBAN, updatedSponsorCompany.IBAN)
            );

            return updatedSponsorCompany;
        }
    }
}
