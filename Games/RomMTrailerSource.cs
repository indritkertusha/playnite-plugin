using RomM.Models.RomM.Rom;
using System;
using System.IO;

namespace RomM.Games
{
    // Pure helpers for locating a rom's trailer and where it belongs on disk. Playnite has no
    // native trailer field; themes (e.g. Stardust) and the ExtraMetadataLoader extension instead
    // look for a video file under Playnite's ExtraMetadata folder by convention.
    internal static class RomMTrailerSource
    {
        public const string FileName = "VideoTrailer.mp4";

        // {ConfigurationPath}/ExtraMetadata/games/{game.Id}/VideoTrailer.mp4
        public static string TargetPath(string configurationPath, Guid gameId)
            => Path.Combine(configurationPath, "ExtraMetadata", "games", gameId.ToString(), FileName);

        // The normalized re-encode plays back more reliably than the raw Screenscraper capture, and
        // the locally-cached *_path avoids hitting Screenscraper directly. Null when RomM has no
        // trailer for this rom.
        public static string ResolveUrl(RomMRom rom, string romMHost)
        {
            var ss = rom.SSMetadata;
            if (ss == null)
                return null;

            if (!string.IsNullOrEmpty(ss.VideoNormalizedPath))
                return RomMUrl.Resource(romMHost, ss.VideoNormalizedPath);
            if (!string.IsNullOrEmpty(ss.VideoPath))
                return RomMUrl.Resource(romMHost, ss.VideoPath);
            if (!string.IsNullOrEmpty(ss.VideoNormalizedUrl))
                return ss.VideoNormalizedUrl;
            if (!string.IsNullOrEmpty(ss.VideoUrl))
                return ss.VideoUrl;
            return null;
        }
    }
}