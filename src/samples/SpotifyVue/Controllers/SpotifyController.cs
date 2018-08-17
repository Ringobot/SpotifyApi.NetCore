using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Authorization;
using SpotifyVue.Models;
using SpotifyVue.Services;

namespace SpotifyVue.Controllers
{
    [Route("api/[controller]")]
    public class SpotifyController : Controller
    {
        private readonly IArtistsApi _artists;
        private readonly IUserAccountsService _userAccounts;
        private readonly SpotifyAuthService _authService;

        public SpotifyController(IArtistsApi artists, IUserAccountsService userAccounts, SpotifyAuthService authService)
        {
            _artists = artists;
            _userAccounts = userAccounts;
            _authService = authService;
        }

        // 1. Search artist, list 3 results
        // 2. Authorise user
        // 3. Play Artist
        // 4. Get recommendations

        [HttpGet("[action]")]
        [Route("api/spotify/searchartists")]
        public async Task<IEnumerable<Models.ArtistItem>> SearchArtists([FromQuery(Name = "query")] string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<Models.ArtistItem>();

            //TODO: Move to a Service
            var artists = await _artists.SearchArtists(query, 3);
            return MapToSearchArtistsModel(artists);
        }

        [HttpPost("[action]")]
        [Route("api/spotify/authorize")]
        public SpotifyAuthorization Authorize([FromBody] SpotifyAuthorization auth)
        {
            if (string.IsNullOrWhiteSpace(auth.SpotifyUsername)) throw new ArgumentNullException("SpotifyUsername");

            var userAuth = _authService.GetUserAuth(auth.SpotifyUsername);
            if (userAuth != null && userAuth.Authorized) return MapToSpotifyAuthorization(userAuth);

            // create a state value and persist it until the callback
            string state = _authService.CreateAndStoreState(auth.SpotifyUsername);
            
            // generate an Authorization URL for the read and modify playback scopes
            string url = _userAccounts.AuthorizeUrl(state, new[] { "user-read-playback-state", "user-modify-playback-state" });

            return new SpotifyAuthorization 
            {
                SpotifyUsername = auth.SpotifyUsername, 
                Authorized = false, 
                AuthorizationUrl = url 
            };
        }

        /// Authorization callback from Spotify
        [HttpGet("[action]")]
        [Route("api/spotify/authorize")]
        public async Task<ContentResult> Authorize([FromQuery(Name="state")] string state, [FromQuery(Name="code")] string code = null, 
            [FromQuery(Name="error")] string error = null)
        {
            // do the userHash and state match what was persisted by the POST method (above)?
            string userHash = _authService.DecodeAndValidateState(state);

            // if Spotify returned an error, throw it
            if (error != null) throw new SpotifyApiErrorException(error);

            // Use the code to request a token
            var tokens = await _userAccounts.RequestAccessRefreshToken(userHash, code);

            var userAuth = _authService.SetUserAuthRefreshToken(userHash, tokens);

            // return an HTML result that posts a message back to the opening window and then closes itself.
            return new ContentResult 
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = $"<html><body><script>window.opener.postMessage(true, \"*\");window.close()</script>Spotify Authorization successful. You can close this window now</body></html>"
            };
        }

        private IEnumerable<Models.ArtistItem> MapToSearchArtistsModel(SearchResult searchResult)
        {
            var list = new List<SpotifyApi.NetCore.ArtistItem>(searchResult.Artists.Items);
            return list.Select(a => new Models.ArtistItem { Id = a.Id, Name = a.Name });
        }

        private SpotifyAuthorization MapToSpotifyAuthorization(UserAuth userAuth)
        {
            return new SpotifyAuthorization
            {
                Authorized = userAuth.Authorized,
                SpotifyUsername = userAuth.SpotifyUserName
            };
        }
    }

    public class SpotifyAuthorization
    {
        public string SpotifyUsername { get; set; }
        public bool Authorized { get; set; }
        public string AuthorizationUrl { get; set; }
    }

}