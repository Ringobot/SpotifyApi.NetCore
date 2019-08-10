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
        /// <summary>
        /// Get a single track by its ISRC code.
        /// </summary>
        /// <param name="tracksApi">This instance of <see cref="ITracksApi"/>.</param>
        /// <param name="isrc">A valid 12 digit ISRC code.</param>
        /// <returns></returns>
        public static async Task<Track> GetTrackByIsrcCode(this ITracksApi tracksApi, string isrc)
        {
            if (isrc == null || isrc.Length != 12) throw new ArgumentException("12 digit ISRC code expected.");
            return (await tracksApi.SearchTracks($"isrc:{isrc}", limit: 1))?.Items.FirstOrDefault();
        }
    }
}