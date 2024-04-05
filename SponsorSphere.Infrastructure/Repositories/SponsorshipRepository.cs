using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Interfaces;

namespace SponsorSphere.Infrastructure.Repositories
{
    public class SponsorshipRepository : ISponsorshipRepository
    {
        public Sponsorship Create(Sponsorship sponsorship)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Sponsorship sponsorship)
        {
            throw new NotImplementedException();
        }

        public List<Sponsorship> GetByAmount(decimal amount)
        {
            throw new NotImplementedException();
        }

        public List<Sponsorship> GetByAthleteId(int athleteId)
        {
            throw new NotImplementedException();
        }

        public Sponsorship GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sponsorship> GetByLevel(SponsorshipLevel level)
        {
            throw new NotImplementedException();
        }

        public List<Sponsorship> GetBySponsorId(int sponsorId)
        {
            throw new NotImplementedException();
        }

        public Sponsorship Update(Sponsorship sponsorship)
        {
            throw new NotImplementedException();
        }
    }
}
