using Newtonsoft.Json;

namespace SpotifyApi.NetCore.Helpers
{
    /// <summary>
    /// Helper for sending data via PUT and POST requests.
    /// </summary>
    public class SpotifyRequestDataWrappersHelper
    {
        /// <summary>
        /// Helper struct for sending string[] which has to be in Json  called ids for PUT and POST requests.
        /// </summary>
        public struct PassStringArrayIdsRequestDataJsonWrapperClass
        {
            [JsonProperty("ids")]
            public string[] Ids { get; set; }

            public PassStringArrayIdsRequestDataJsonWrapperClass(string[] ids)
            {
                this.Ids = ids;
            }
        }
    }
}
