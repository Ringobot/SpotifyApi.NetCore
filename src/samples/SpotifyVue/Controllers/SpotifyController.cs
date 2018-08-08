using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Authorization;
using SpotifyVue.Data;
using SpotifyVue.Models;

namespace SpotifyVue.Controllers
{
    [Route("api/[controller]")]
    public class SpotifyController : Controller
    {
        private readonly IArtistsApi _artists;
        private readonly IUserAccountsService _userAccounts;
        
        //TODO: Move to UserAuthService
        private readonly UserAuthStorage _userAuthStorage; 
        

        /// A dictionary of state, userHash for verification of authorization callback
        /// IRL this would be Redis, Table Storage or Cosmos DB
        //TODO: Move to UserAuthService
        private readonly ConcurrentDictionary<string, string> _states = new ConcurrentDictionary<string, string>();

        public SpotifyController(IArtistsApi artists, UserAuthStorage userAuthStorage, IUserAccountsService userAccounts)
        {
            _artists = artists;
            _userAccounts = userAccounts;
            _userAuthStorage = userAuthStorage;
        }

        // 1. Search artist, list 3 results
        // 2. Play Artist
        // 3. Authorise user

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
        public async Task<SpotifyAuthorization> Authorize([FromBody] SpotifyAuthorization auth)
        {
            if (string.IsNullOrWhiteSpace(auth.SpotifyUsername)) throw new ArgumentNullException("SpotifyUsername");

            //TODO: Move to a Service
            string userHash = StateHelper.UserHash(auth.SpotifyUsername);
            var userAuth = _userAuthStorage.Get(auth.SpotifyUsername);
            if (userAuth != null && userAuth.Authorized) return MapToSpotifyAuthorization(userAuth);

            // create a state value and persist it until the callback
            string state = Guid.NewGuid().ToString("N");
            _states.TryAdd(state, userHash);

            // encode the userHash and state into one "state" string and generate an Authorization URL for the read and modify playback scopes
            string stateEncoded = StateHelper.EncodeState((userHash, state));
            string url = _userAccounts.AuthorizeUrl(stateEncoded, new[] { "user-read-playback-state", "user-modify-playback-state" });

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
        public async Task<SpotifyAuthorization> Authorize([FromQuery(Name="state")] string state, [FromQuery(Name="code")] string code = null, 
            [FromQuery(Name="error")] string error = null)
        {
            // decode the state param
            var userState = StateHelper.DecodeState(state);

            // do the userHash and state match what was persisted by the POST method (above)?
            string userHash = null;
            if (!_states.TryGetValue(userState.state, out userHash)) throw new InvalidOperationException("state param is invalid");
            if (userHash != userState.userHash) throw new InvalidOperationException("state param is invalid");
            
            // if Spotify returned an error, throw it
            if (error != null) throw new SpotifyApiErrorException(error);

            // Use the code to request a token
            var tokens = await _userAccounts.RequestAccessRefreshToken(userHash, code);
            
            var userAuth = _userAuthStorage.GetByUserHash(userHash);
            userAuth.Authorized = true;
            userAuth.RefreshToken = tokens.RefreshToken;
            userAuth.Scopes = tokens.Scope;

            _userAuthStorage.Update(userAuth.SpotifyUserName, userAuth);

            return MapToSpotifyAuthorization(userAuth);
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