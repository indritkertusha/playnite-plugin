using Newtonsoft.Json;

namespace RomM.Models.RomM.Rom
{
    // Screenscraper metadata/media on a rom. Only the media we consume (icon, background, trailer)
    // are modelled here; Newtonsoft ignores the rest of the object.
    public class RomMSSMetadata
    {
        // Composite image with consistent dimensions. _path is served by RomM (relative), _url is external.
        [JsonProperty("miximage_path")]
        public string MiximagePath { get; set; }
        [JsonProperty("miximage_url")]
        public string MiximageUrl { get; set; }

        // Background art. _path is served by RomM (relative), _url is external.
        [JsonProperty("fanart_path")]
        public string FanartPath { get; set; }
        [JsonProperty("fanart_url")]
        public string FanartUrl { get; set; }

        // Gameplay video. _normalized_ is a re-encoded copy with consistent codec/format; _path is
        // served by RomM (relative), _url is external.
        [JsonProperty("video_path")]
        public string VideoPath { get; set; }
        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }
        [JsonProperty("video_normalized_path")]
        public string VideoNormalizedPath { get; set; }
        [JsonProperty("video_normalized_url")]
        public string VideoNormalizedUrl { get; set; }
    }
}
