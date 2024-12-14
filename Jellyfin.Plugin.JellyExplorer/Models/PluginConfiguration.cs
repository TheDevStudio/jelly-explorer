using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.JellyExplorer.Models
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string TmdbApiKey { get; set; }

        public PluginConfiguration()
        {
            TmdbApiKey = string.Empty;
        }
    }
}
