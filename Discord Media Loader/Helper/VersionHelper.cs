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

        internal static async Task<Version> GetLatestReleaseVersion(string owner, string repository)
        {
            var github = new GitHubClient(new ProductHeaderValue("DiscordMediaLoader"));
            var tag =
                (await github.Repository.Release.GetAll("Serraniel", "DiscordMediaLoader")).OrderBy(x => x.CreatedAt).First().TagName.Replace("v", "") ?? "";
            var version = new Version(tag);
            return version;
        }
    }
}
