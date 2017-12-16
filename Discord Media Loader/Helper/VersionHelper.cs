using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Octokit;

namespace Discord_Media_Loader.Helper
{
    internal class VersionHelper
    {
        internal static Version CurrentVersion => Assembly.GetExecutingAssembly().GetName().Version;

        internal static Version AppVersion => AssemblyName.GetAssemblyName("Discord Media Loader.Application.dll").Version;

        internal static async Task<Version> GetReleaseVersion()
        {
            var github = new GitHubClient(new ProductHeaderValue("DiscordMedialLoader"));

            var tag =
               (await github.Repository.Release.GetAll("Serraniel", "DiscordMediaLoader")).OrderByDescending(x => x.CreatedAt).First().TagName.Replace("v", "") ?? "";

            var version = new Version(tag);
            return version;
        }

        internal static async Task<string> DownloadLatestReleaseVersion()
        {
            return await DownloadVersion(await GetReleaseVersion());
        }

        internal static async Task<string> DownloadVersion(Version version)
        {
            var github = new GitHubClient(new ProductHeaderValue("DiscordMediaLoader"));
            var releaseVersion = (from release in (await github.Repository.Release.GetAll("Serraniel", "DiscordMediaLoader")) where release.TagName == $"v{version.Major}.{version.Minor}.{version.Build}.{version.Revision}" select release).First();

            return releaseVersion.Assets.FirstOrDefault()?.BrowserDownloadUrl;
        }
    }
}
