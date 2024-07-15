namespace Jellyfin.Plugin.JellyExplorer.Configuration.DownloadClient;

public enum ClientTypes
{
    usenet,
    torrent,
}

public abstract class BaseDownloadClient
{
    public abstract ClientTypes ClientType { get; }
    public abstract bool TestConnection();
    public string Name => GetType().Name;
}
