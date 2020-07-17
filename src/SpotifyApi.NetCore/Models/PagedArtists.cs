using Newtonsoft.Json;
using System.Dynamic;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Paged Full Artist Objects.
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/reference/object-model/ </remarks>
    public class PagedArtists : Paged<Artist>
    {
    }
}
