using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Extensions
{
    /// <summary>
    /// Helper extension methods for <see cref="TracksApi" />
    /// </summary>
    public static class TracksApiExtensions
    {
        public static async Task<Track> GetTrackByIsrcCode(this ITracksApi tracksApi, string isrc)
        {
            if (isrc == null || isrc.Length != 12) throw new ArgumentException("12 digit ISRC code expected.");
            return (await tracksApi.SearchTracks($"isrc:{isrc}", null, (1,0)))?.Items.FirstOrDefault();
        }
    }
}