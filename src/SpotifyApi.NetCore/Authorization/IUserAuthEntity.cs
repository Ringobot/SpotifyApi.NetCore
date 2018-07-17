using System;

namespace SpotifyApi.NetCore
{
    public interface IUserAuthEntity
    {
        string UserHash { get; }
        DateTime? Expiry { get; set; }
        string RefreshToken { get; set; }
        string AccessToken { get; set; }
        string State { get; set; }
        string Code { get; set; }
        string AuthUrl { get; set; }
        string TokenType { get; set; }
        string Scope { get; set; }
    }
}