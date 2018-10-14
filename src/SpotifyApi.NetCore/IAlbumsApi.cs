using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IAlbumsApi
    {
        #region GetAlbum

        Task<Album> GetAlbum(string albumId);
        Task<Album> GetAlbum(string albumId, string market);

        #endregion

        #region GetAlbumTracks
        //TODO
        #endregion

        #region GetAlbums
        //TODO
        #endregion

    }
}