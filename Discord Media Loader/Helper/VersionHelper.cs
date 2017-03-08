using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Octokit;

namespace Discord_Media_Loader.Helper
{
    internal class VersionHelper
    {
        internal static Version CurrentVersion => Assembly.GetExecutingAssembly().GetName().Version;

        internal static async Task<Version> GetLatestReleaseVersion()
        {
            var github = new GitHubClient(new ProductHeaderValue("DiscordMediaLoader"));
            var tag =
                (await github.Repository.Release.GetAll("Serraniel", "DiscordMediaLoader")).OrderBy(x => x.CreatedAt).First().TagName.Replace("v", "") ?? "";
            var version = new Version(tag);
            return version;
        }

        internal static async Task<string> DownloadLatestReleaseVersion()
        {
            return await DownloadReleaseVersion(await GetLatestReleaseVersion());
        }

        internal static async Task<string> DownloadReleaseVersion(Version version)
        {
            var github = new GitHubClient(new ProductHeaderValue("DiscordMediaLoader"));
            var releaseVersion = (from release in (await github.Repository.Release.GetAll("Serraniel", "DiscordMediaLoader")) where release.TagName == $"v{version.Major}.{version.Minor}.{version.Build}.{version.Revision}" select release).First();
            //where release.TagName == $"v{version.Major}.{version.Minor}.{version.Revision}.{version.Build}"
            /*var r = releases.ElementAt(0);
            if (r.TagName == $"v{version.Major}.{version.Minor}.{version.Build}.{version.Revision}")
            {
                var releaseVersion = releases.First();
                return releaseVersion.Url;
            }*/
            return releaseVersion.ZipballUrl;
        }
    }
}
