using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// An API Wrapper for the Spotify Web API Follow endpoints.
    /// </summary>
    public class FollowApi : SpotifyWebApi, IFollowApi
    {
        public enum ContainsTypes
        {
            Artist,
            User
        }

        protected internal string GetCommaSeperateString(List<string> ids)
        {
            string idsCommaSeparatedList = "";
            foreach (string id in ids)
            {
                idsCommaSeparatedList += id + ",";
            }
            return idsCommaSeparatedList.Substring(0, idsCommaSeparatedList.Length - 1);
        }

        #region constructors

        public FollowApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
            SearchApi = new SearchApi(httpClient, accessTokenProvider);
        }

        public FollowApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
            SearchApi = new SearchApi(httpClient, accessToken);
        }

        public FollowApi(HttpClient httpClient) : base(httpClient)
        {
            SearchApi = new SearchApi(httpClient);
        }

        #endregion
        protected internal virtual ISearchApi SearchApi { get; set; }

        #region GetFollowingContains

        /// <summary>
        /// Check if current user follows artists or users.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="type">The type, either artist or user, the current user follows.</param>
        /// <param name="ids">A comma-separated list of the artist or the user Spotify IDs to check. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>List<bool></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        public async Task<List<bool>> GetFollowingContains(
            string username,
            FollowApi.ContainsTypes type,
            List<string> ids,
            string accessToken = null
            )
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
            
            if (ids?.Count < 1 || ids?.Count > 50) throw new ArgumentNullException("ids");

            string url = $"{BaseUrl}/me/following/contains?" + 
                $"type={(type == ContainsTypes.Artist ? "artist" : "user")}&" +
                $"ids={GetCommaSeperateString(ids)}";

            return await GetModel<List<bool>>(url, accessToken);
        }

        #endregion

    }
}
