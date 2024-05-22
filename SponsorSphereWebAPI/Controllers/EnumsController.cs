using Microsoft.AspNetCore.Mvc;
using SponsorSphere.Application.Common.Helpers;
using SponsorSphere.Domain.Enums;

namespace SponsorSphereWebAPI.Controllers
{
    [ApiController]
    [Route("enums/")]
    public class EnumsController : ControllerBase
    {

        [HttpGet]
        [Route("countries")]
        public IActionResult GetCountries()
        {
            var countries = EnumHelper.GetAllDisplayNames<CountryEnum>();

            return Ok(countries);
        }

        [HttpGet]
        [Route("events")]
        public IActionResult GetEventTypes()
        {
            var events = EnumHelper.GetAllDisplayNames<EventsEnum>();

            return Ok(events);
        }

        [HttpGet]
        [Route("sports")]
        public IActionResult GetSports()
        {
            var sports = EnumHelper.GetAllDisplayNames<SportsEnum>();

            return Ok(sports);
        }

        [HttpGet]
        [Route("levels")]
        public IActionResult GetSponsorshipLevels()
        {
            var levels = EnumHelper.GetAllDisplayNames<SponsorshipLevel>();

            return Ok(levels);
        }
    }
}
