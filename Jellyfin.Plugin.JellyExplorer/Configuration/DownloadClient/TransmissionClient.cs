using Transmission.API.RPC;

namespace Jellyfin.Plugin.JellyExplorer.Configuration.DownloadClient;

public class TransmissionClient : BaseDownloadClient
{
    public override ClientTypes ClientType => ClientTypes.torrent;

    public TransmissionClient()
    {
        IPAddress = "";
        ConnProtocol = ConnProtocols.http;
        Port = 0;
        SessionId = "";
        AuthEnabled = false;
        Username = "";
        Password = "";
    }

    public ConnProtocols ConnProtocol { get; set; }
    public string IPAddress { get; set; }
    public int Port { get; set; }

    // TODO: Add support for optional Auth instead of required Auth
    public bool AuthEnabled { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string SessionId { get; set; }

    public override bool TestConnection()
    {
        var url = ConnProtocol.ToString() + IPAddress.ToString() + ":" + Port.ToString();
        var client = new Client(url, SessionId, Username, Password);

        return client.PortTest();
    }
}
