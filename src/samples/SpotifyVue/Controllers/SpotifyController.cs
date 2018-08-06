using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.NetCore;
using SpotifyVue.Models;

namespace SpotifyVue.Controllers
{
    [Route("api/[controller]")]
    public class SpotifyController : Controller
    {
        private readonly IArtistsApi _artists;
        public SpotifyController(IArtistsApi artists)
        {
            _artists = artists;
        }

        // 1. Search artist, list 5 results
        // 2. Play Artist

        [HttpGet("[action]")]
        [Route("api/spotify/searchartists")]
        public async Task<IEnumerable<Models.ArtistItem>> SearchArtists([FromQuery(Name = "query")] string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<Models.ArtistItem>();

            //TODO: Move to a Service
            var artists = await _artists.SearchArtists(query, 5);
            return MapToSearchArtistsModel(artists);
        }

        private IEnumerable<Models.ArtistItem> MapToSearchArtistsModel(SearchResult searchResult)
        {
            var list = new List<SpotifyApi.NetCore.ArtistItem>(searchResult.Artists.Items);
            return list.Select(a => new Models.ArtistItem { Id = a.Id, Name = a.Name });
        }
    }
}