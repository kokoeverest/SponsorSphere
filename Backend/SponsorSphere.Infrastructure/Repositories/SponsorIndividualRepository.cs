﻿using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class SponsorIndividualRepository : ISponsorIndividualRepository
    {
        private readonly SponsorSphereDbContext _context;

        public SponsorIndividualRepository(SponsorSphereDbContext context)
        {
            _context = context;
        }
        public async Task<SponsorIndividual> CreateAsync(SponsorIndividual sponsorIndividual)
        {
            try
            {
                await _context.SponsorIndividuals.AddAsync(sponsorIndividual);
                await _context.SaveChangesAsync();
                return sponsorIndividual;
            }
            catch (DbUpdateException)
            {
                throw new InvalidDataException("User with that email already exists");
            }
        }

        public async Task<int> DeleteAsync(int userId)
        {
            await _context.Users
                .Where(si => si.Id.Equals(userId))
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(si => si.IsDeleted, true)
                .SetProperty(si => si.DeletedOn, DateTime.UtcNow));

            return 1;
        }

        public async Task<SponsorIndividual> GetByIdAsync(int userId)
        {
            var sponsorIndividual = await _context.SponsorIndividuals
                .Include(si => si.BlogPosts)
                    .ThenInclude(bp => bp.Pictures)
                .Include(si => si.Sponsorships)
                .FirstOrDefaultAsync(si => si.Id == userId)
                    ?? throw new NotFoundException($"Sponsor with id {userId} not found");

            return sponsorIndividual;
        }

        public async Task<List<SponsorIndividual>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.SponsorIndividuals
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }

        public async Task<List<SponsorIndividual>> GetByAgeAsync(int age, int pageNumber, int pageSize)
        {
            var birthYearLimit = DateTime.UtcNow.Year - age;

            return await _context.SponsorIndividuals
                .Where(sponsorIndividual => birthYearLimit <= sponsorIndividual.BirthDate.Year)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }

        public async Task<List<SponsorIndividual>> GetByCountryAsync(CountryEnum country, int pageNumber, int pageSize)
        {
            return await _context.SponsorIndividuals
                .Where(sponsorIndividual => sponsorIndividual.Country == country)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(sponsor => sponsor.Name)
                .ToListAsync();
        }

        public async Task<SponsorIndividualDto> UpdateAsync(SponsorIndividualDto updatedSponsorIndividual)
        {
            await _context.Users.Where(u => u.Id == updatedSponsorIndividual.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(si => si.Name, updatedSponsorIndividual.Name)
                .SetProperty(si => si.UserName, updatedSponsorIndividual.Email)
                .SetProperty(si => si.Email, updatedSponsorIndividual.Email)
                .SetProperty(si => si.Country, updatedSponsorIndividual.Country)
                .SetProperty(si => si.PhoneNumber, updatedSponsorIndividual.PhoneNumber)
                .SetProperty(si => si.PictureId, updatedSponsorIndividual.PictureId)
                .SetProperty(si => si.Website, updatedSponsorIndividual.Website)
                .SetProperty(si => si.FaceBookLink, updatedSponsorIndividual.FaceBookLink)
                .SetProperty(si => si.InstagramLink, updatedSponsorIndividual.InstagramLink)
                .SetProperty(si => si.TwitterLink, updatedSponsorIndividual.TwitterLink)
                .SetProperty(si => si.StravaLink, updatedSponsorIndividual.StravaLink)
                );

            await _context.SponsorIndividuals.Where(si => si.Id == updatedSponsorIndividual.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(si => si.LastName, updatedSponsorIndividual.LastName)
                .SetProperty(si => si.BirthDate, updatedSponsorIndividual.BirthDate)
            );
            return updatedSponsorIndividual;
        }

        public async Task<int> GetSponsorIndividualsCount()
        {
            var result = await _context.SponsorIndividuals.CountAsync();
            return result;
        }
    }
}
