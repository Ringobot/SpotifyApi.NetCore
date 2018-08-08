namespace SpotifyVue.Models
{
    public class UserAuth
    {
        public string SpotifyUserName { get; set; }
        public string UserHash { get; set; }
        public string RefreshToken { get; set; }
        public bool Authorized { get; set; }
        public string Scopes { get; set; }
    }
}