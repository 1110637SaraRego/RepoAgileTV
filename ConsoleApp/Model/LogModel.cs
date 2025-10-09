namespace CandidateTesting.SaraRego.ConsoleApp.Model
{
    internal class LogModel
    {
        public string Provider { get; set; } = "\"MINHA CDN\"";

        public string LogVersion { get; set; } = "1.0";

        public string Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public string[] Fields { get; set; } = ["provider", "http-method", "status-code", "uri-path", "time-taken", "response-size", "cache-status"];

        public enum CacheStatus
        {
            HIT,
            MISS,
            INVALIDATE
        }
    }
}
