using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.SportEvents.Dtos
{
    public class SportEventDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CountryEnum Country { get; set; }
        public bool Finished { get; set; }
        public DateTime EventDate { get; set; }
        public EventsEnum EventType { get; set; }
        public SportsEnum Sport { get; set; }
        public SportEventStatus Status { get; set; }
    }
}
