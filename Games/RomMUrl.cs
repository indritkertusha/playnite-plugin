namespace RomM.Games
{
    // Pure url joining used across the plugin (RomM.CombineUrl delegates here). Trims a trailing
    // slash off the base and a leading slash off the relative part so the two always join with exactly
    // one separator, even when the payload omits or doubles them.
    internal static class RomMUrl
    {
        public static string Combine(string baseUrl, string relativePath)
            => $"{baseUrl?.TrimEnd('/')}/{relativePath?.TrimStart('/') ?? ""}";

        // Most per-rom media (miximage, fanart, manuals, video) comes back as a path relative to
        // RomM's resource mount rather than a full URL.
        public static string Resource(string baseUrl, string relativePath)
            => Combine(baseUrl, $"assets/romm/resources/{relativePath?.TrimStart('/')}");
    }
}
