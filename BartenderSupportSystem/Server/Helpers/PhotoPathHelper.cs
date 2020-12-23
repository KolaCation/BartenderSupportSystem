namespace BartenderSupportSystem.Server.Helpers
{
    public static class PhotoPathHelper
    {
        public static string GetBase64String(string photoPath)
        {
            if (!string.IsNullOrEmpty(photoPath))
            {
                return photoPath.Split(",")[1];
            }
            else
            {
                return null;
            }
        }
    }
}
