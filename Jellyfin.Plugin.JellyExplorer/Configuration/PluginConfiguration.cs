using Jellyfin.Plugin.JellyExplorer.Configuration.DownloadClient;
using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.JellyExplorer.Configuration;

public enum ConnProtocols
{
    http,
    https
}

public class PluginConfiguration : BasePluginConfiguration
{
    public PluginConfiguration()
    {
        TmdbApiKey = "";
        TransmissionClient = new TransmissionClient();
    }

    public string TmdbApiKey { get; set; }

    public TransmissionClient TransmissionClient { get; set; }

}
