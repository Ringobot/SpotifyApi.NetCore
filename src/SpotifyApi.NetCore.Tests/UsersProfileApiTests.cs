using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class UsersProfileApiTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetUsersProfile_NoUserId_FullProfile()
        {
            // arrange
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new UsersProfileApi(http, accounts);

            // act
            var response = await api.GetCurrentUsersProfile();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetUsersProfile_UserId_PublicProfile()
        {
            // arrange
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());
            const string userId = "daniellarsennz";

            var api = new UsersProfileApi(http, accounts);

            // act
            var response = await api.GetUsersProfile(userId);
        }
    }
}
