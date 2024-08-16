namespace SponsorSphere.Application.Common.Constants
{
    public class FileConstants
    {
        // the maximum size of images is 2 MB
        public const int MaxFileSize = 2097152;

        // the minimum size of images is 1 KB
        public const long MinFileSize = 1024;

        internal static List<string> validPictureFormats = ["image/png", "image/jpeg", "image/jpg"];
        internal static List<string> validExtensions = [".png", ".jpeg", ".jpg"];
    }
}
