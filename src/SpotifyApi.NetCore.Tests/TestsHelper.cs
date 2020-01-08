using System.IO;
using Microsoft.Extensions.Configuration;

namespace SpotifyApi.NetCore.Tests 
{
    public static class TestsHelper
    {
        public static IConfiguration GetLocalConfig()
        {
            return new ConfigurationBuilder()
                // Using "..", "..", ".." vs. "..\\..\\.." for system-agnostic path resolution
                // Reference: https://stackoverflow.com/questions/14899422/how-to-navigate-a-few-folders-up#comment97806320_30902714
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."))
                .AddJsonFile("appsettings.local.json", false)
                .Build();
        }
    }
}