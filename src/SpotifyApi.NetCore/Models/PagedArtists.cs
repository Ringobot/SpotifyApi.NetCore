using Newtonsoft.Json;
using System.Dynamic;

namespace SpotifyApi.NetCore.Models
{
    /// <summary>
    /// Full Artist Object.
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/reference/object-model/ </remarks>
    public class PagedArtists : Paged<Artist>
    {
    }
}
