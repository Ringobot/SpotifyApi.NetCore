using Newtonsoft.Json;
using System;

namespace SpotifyApi.NetCore
{
    public class PlayHistory
    {
        /// <summary>
        /// The track the user listened to.
        /// </summary>
        [JsonProperty("track")]
        public Track Track { get; set; }

        /// <summary>
        /// The date and time the track was played.
        /// </summary>
        [JsonProperty("played_at")]
        public string PlayedAt { get; set; }

        /// <summary>
        /// The context the track was played from.
        /// </summary>
        [JsonProperty("context")]
        public Context Context { get; set; }

        /// <summary>
        /// The context the track was played from as <see cref="DateTimeOffset"/>.
        /// </summary>
        public DateTimeOffset? PlayedAtDateTime 
        { 
            get
            {
                if (PlayedAt == null) return null;
                
                if (DateTimeOffset.TryParse(PlayedAt, out var result))
                {
                    return result;
                }

                return null;
            } 
        }
    }
}
