using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.NetCore;

namespace SpotifyVue.Controllers
{
    [Route("api/[controller]")]
    public class SpotifyController : Controller
    {
        private readonly IArtistsApi _artists;
        public SpotifyController (IArtistsApi artists)
        {
            _artists = artists;
        }

        // 1. Search artist, list 3 results
        // 2. Play Artist

        [HttpGet("[action]")]
        [Route("api/spotify/searchartists")]
        public async Task<Artist[]> SearchArtists([FromQuery(Name="query")] string query)
        {
            var artists = await _artists.SearchArtists(query, 3);
            return artists;
        }
    }
}