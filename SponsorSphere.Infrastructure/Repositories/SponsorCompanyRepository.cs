using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.SponsorCompanies.Responses;
using SponsorSphere.Application.Interfaces;
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
            return await _context.Users.Where(user => user.Id.Equals(userId)).ExecuteDeleteAsync();
            //var sponsorCompanyToDelete = await GetByIdAsync(userId);

            //if (sponsorCompanyToDelete is not null)
            //{
            //    _context.Users.Remove(sponsorCompanyToDelete);
            //}
            //return sponsorCompanyToDelete is not null ? await _context.SaveChangesAsync() : 0;
        }
        public async Task<SponsorCompany> GetByIdAsync(int userId)
        {
            var sponsorCompany = await _context.SponsorCompanies.FirstOrDefaultAsync(sc => sc.Id == userId);

            if (sponsorCompany is not null)
            {
                return sponsorCompany;
            }
            throw new ApplicationException($"Sponsor with id {userId} not found");
        }

        public async Task<List<SponsorCompany>> GetAllAsync()
        {
            return await _context.SponsorCompanies
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }

        public async Task<List<SponsorCompany>> GetByCountryAsync(string country)
        {
            return await _context.SponsorCompanies
                .Where(sponsorCompany => sponsorCompany.Country == country)
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }
        public async Task<SponsorCompany> UpdateAsync(SponsorCompanyDto updatedSponsorCompany)
        {
            var sponsorCompanyToUpdate = await GetByIdAsync(updatedSponsorCompany.Id);
            // Add more of the properties which can be changed
            sponsorCompanyToUpdate.Website = updatedSponsorCompany.Website ?? sponsorCompanyToUpdate.Website;
            sponsorCompanyToUpdate.FaceBookLink = updatedSponsorCompany.FaceBookLink ?? sponsorCompanyToUpdate.FaceBookLink;
            sponsorCompanyToUpdate.StravaLink = updatedSponsorCompany.StravaLink ?? sponsorCompanyToUpdate.StravaLink;
            sponsorCompanyToUpdate.InstagramLink = updatedSponsorCompany.InstagramLink ?? sponsorCompanyToUpdate.InstagramLink;
            sponsorCompanyToUpdate.TwitterLink = updatedSponsorCompany.TwitterLink ?? sponsorCompanyToUpdate.TwitterLink;

            _context.SponsorCompanies.Update(sponsorCompanyToUpdate);
            await _context.SaveChangesAsync();
            return sponsorCompanyToUpdate;
        }
    }
}
