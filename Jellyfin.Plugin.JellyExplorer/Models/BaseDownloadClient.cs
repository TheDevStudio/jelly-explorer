namespace Jellyfin.Plugin.JellyExplorer.Models
{
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
}
