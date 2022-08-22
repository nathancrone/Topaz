namespace Topaz.UI.Razor.Extensibility
{
    public class AppSettingsOptions
    {
        public const string AppSettings = "AppSettings";
        public long? CacheInvalidator { get; set; }
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
    }
}