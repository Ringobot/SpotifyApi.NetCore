using SpotifyApi.NetCore.Models;

namespace SpotifyApi.NetCore.Helpers
{
    /// <summary>
    /// Helper for TimeRange
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/#spotify-uris-and-ids </remarks>
    public static class TimeRangeHelper
    {
        /// <summary>
        /// Return the string value for the time range.
        /// </summary>
        /// <param name="timeRange">Required. The enum <see cref="TimeRange"/> time range to be resolved to the string value.</param>
        /// <returns>string Over what time frame the affinities are computed. Valid values: long_term (calculated from several years of data and including all new data as it becomes available), medium_term (approximately last 6 months), short_term (approximately last 4 weeks). Default: medium_term.</returns>
        internal static string TimeRangeString(TimeRange timeRange) => timeRange == TimeRange.ShortTerm ? "short_term" :
            (timeRange == TimeRange.LongTerm ? "long_term" : "medium_term");
    }
}
