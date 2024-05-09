using AutoMapper;

namespace SponsorSphere.Application.Common.Converters
{
    public class StringToDateConverter : IValueConverter<string, DateTime>
    {
        public DateTime Convert(string value, ResolutionContext context) => DateTime.Parse(value).ToUniversalTime();
    }
}
