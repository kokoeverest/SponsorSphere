namespace SponsorSphere.Application.Common.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayName(this Enum value)
        {
            var attribute = value
                            .GetType()
                            .GetMember(value.ToString())
                            .First();

            return attribute?.Name ?? value.ToString();
        }

        public static IEnumerable<string> GetAllDisplayNames<T>() where T : Enum
        {
            var enumType = typeof(T);
            var enumValues = Enum.GetValues(enumType).Cast<Enum>();

            return enumValues.Select(ev => ev.GetDisplayName()).OrderBy(name => name);
        }
    }
}
