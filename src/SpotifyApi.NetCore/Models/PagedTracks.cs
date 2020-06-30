using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyApi.NetCore.Models
{
    /// <summary>
    /// Paged Full Track Objects.
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/reference/object-model/ </remarks>
    public class PagedTracks : Paged<Track>
    {
    }
}
