namespace SponsorSphere.Application.Common.Helpers
{
    /// <summary>
    /// Helper class for working with enums.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the display name of the specified enum value.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The display name of the enum value.</returns>
        public static string GetDisplayName(this Enum value)
        {
            var attribute = value
                            .GetType()
                            .GetMember(value.ToString())
                            .First();

            return attribute?.Name ?? value.ToString();
        }

        /// <summary>
        /// Gets all the display names of the specified enum type.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>An enumerable of all the display names of the enum type.</returns>
        public static IEnumerable<string> GetAllDisplayNames<T>() where T : Enum
        {
            var enumType = typeof(T);
            var enumValues = Enum.GetValues(enumType).Cast<Enum>();

            return enumValues.Select(ev => ev.GetDisplayName()).OrderBy(name => name);
        }
    }
}
