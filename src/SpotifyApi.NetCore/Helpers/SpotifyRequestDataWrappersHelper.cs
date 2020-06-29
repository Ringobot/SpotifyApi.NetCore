using Newtonsoft.Json;

namespace SpotifyApi.NetCore.Helpers
{
    public class SpotifyRequestDataWrappersHelper
    {
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
