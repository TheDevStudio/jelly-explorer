using System;
using System.Collections.Generic;
using Jellyfin.Plugin.JellyExplorer.Models;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Jellyfin.Plugin.JellyExplorer
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        public override string Name => "JellyExplorer";
        public override Guid Id => Guid.Parse("53b691fd-9675-fb3e-c72a-7855259d1ddc");
        public override string Description => "Search movie details using TMDB API";
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public static Plugin Instance { get; private set; }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "jellyexplorer",
                    EmbeddedResourcePath = GetType().Namespace + ".Pages.configpage.html"
                }
            };
        }
    }
}

