using System;
using RomM.Games;
using RomM.Models.RomM.Rom;
using Xunit;

namespace RomM.Tests
{
    public class RomMTrailerSourceTests
    {
        private const string Host = "https://romm.example.com";

        [Fact]
        public void ResolveUrl_prefers_normalized_path_over_everything_else()
        {
            var rom = new RomMRom
            {
                SSMetadata = new RomMSSMetadata
                {
                    VideoNormalizedPath = "roms/2/32/video/video-normalized.mp4",
                    VideoPath = "roms/2/32/video/video.mp4",
                    VideoNormalizedUrl = "https://ss.example/normalized.mp4",
                    VideoUrl = "https://ss.example/video.mp4",
                }
            };

            Assert.Equal(Host + "/assets/romm/resources/roms/2/32/video/video-normalized.mp4", RomMTrailerSource.ResolveUrl(rom, Host));
        }

        [Fact]
        public void ResolveUrl_falls_back_to_raw_path_then_normalized_url_then_raw_url()
        {
            var rom = new RomMRom { SSMetadata = new RomMSSMetadata { VideoPath = "roms/2/32/video/video.mp4" } };
            Assert.Equal(Host + "/assets/romm/resources/roms/2/32/video/video.mp4", RomMTrailerSource.ResolveUrl(rom, Host));

            rom = new RomMRom { SSMetadata = new RomMSSMetadata { VideoNormalizedUrl = "https://ss.example/normalized.mp4" } };
            Assert.Equal("https://ss.example/normalized.mp4", RomMTrailerSource.ResolveUrl(rom, Host));

            rom = new RomMRom { SSMetadata = new RomMSSMetadata { VideoUrl = "https://ss.example/video.mp4" } };
            Assert.Equal("https://ss.example/video.mp4", RomMTrailerSource.ResolveUrl(rom, Host));
        }

        [Fact]
        public void ResolveUrl_is_null_without_ss_metadata_or_video()
        {
            Assert.Null(RomMTrailerSource.ResolveUrl(new RomMRom(), Host));
            Assert.Null(RomMTrailerSource.ResolveUrl(new RomMRom { SSMetadata = new RomMSSMetadata() }, Host));
        }

        [Fact]
        public void TargetPath_is_extra_metadata_games_id_video_trailer()
        {
            var gameId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var path = RomMTrailerSource.TargetPath(@"C:\Playnite", gameId);

            Assert.Equal(System.IO.Path.Combine(@"C:\Playnite", "ExtraMetadata", "games", gameId.ToString(), "VideoTrailer.mp4"), path);
        }
    }
}