using Playnite.SDK;
using Playnite.SDK.Models;
using RomM.Models.RomM.Rom;
using System;
using System.IO;

namespace RomM.Games
{
    // Fetches a rom's trailer (if any) into Playnite's ExtraMetadata folder. See RomMTrailerSource
    // for how the source URL and target path are resolved.
    internal static class RomMTrailerDownloader
    {
        // No-op when a trailer already exists on disk or RomM has none for this rom. Never throws:
        // a failed trailer fetch must not fail game import/metadata refresh.
        public static void DownloadIfMissing(IPlayniteAPI playnite, ILogger logger, Game game, RomMRom rom, string romMHost)
        {
            var target = RomMTrailerSource.TargetPath(playnite.Paths.ConfigurationPath, game.Id);
            if (File.Exists(target))
                return;

            var sourceUrl = RomMTrailerSource.ResolveUrl(rom, romMHost);
            if (sourceUrl == null)
                return;

            var tempPath = target + ".tmp";
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(target));

                using (var response = HttpClientSingleton.Instance.GetAsync(sourceUrl).GetAwaiter().GetResult())
                {
                    response.EnsureSuccessStatusCode();
                    using (var content = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                    using (var file = File.Create(tempPath))
                    {
                        content.CopyTo(file);
                    }
                }

                File.Move(tempPath, target);
            }
            catch (Exception e)
            {
                logger.Error(e, $"[Trailer] Failed to download trailer for {game.Name}");
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }
    }
}