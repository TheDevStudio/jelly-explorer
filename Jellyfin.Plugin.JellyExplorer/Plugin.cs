using System;
using System.Collections.Generic;
using Jellyfin.Plugin.JellyExplorer.Models;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Jellyfin.Plugin.JellyExplorer
{
    public class JellyExplorerPlugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        public JellyExplorerPlugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override string Name => "JellyExplorer";
        public override Guid Id => Guid.Parse("f1c1b09f-6f1b-4e1c-b9d9-e1f4b4d4f1c1");
        public override string Description => "A plugin for Jellyfin";
        public static JellyExplorerPlugin? Instance { get; private set; }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "jellyexplorer",
                    EmbeddedResourcePath = GetType().Namespace + ".Pages.configPage.html",
                },
                new PluginPageInfo
                {
                    Name = "configPagejs",
                    EmbeddedResourcePath = GetType().Namespace + ".Pages.configPage.js"
                }
            };
        }
    }
}