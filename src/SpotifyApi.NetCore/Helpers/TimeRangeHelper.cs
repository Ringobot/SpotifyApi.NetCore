using SpotifyApi.NetCore.Models;

namespace SpotifyApi.NetCore.Helpers
{
    public static class TimeRangeHelper
    {
        internal static string TimeRangeString(TimeRange timeRange) => timeRange == TimeRange.ShortTerm ? "short_term" :
            (timeRange == TimeRange.LongTerm ? "long_term" : "medium_term");
    }
}
