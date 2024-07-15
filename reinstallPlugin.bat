set on_remote=ssh -t mediaserver@192.168.1.16

dotnet build 

%on_remote% "mkdir -p /tmp/jellyfin-plugins/"
%on_remote% "mkdir -p /var/lib/jellyfin/plugins/local-plugin/"

scp -r .\Jellyfin.Plugin.JellyExplorer\bin\Debug\net6.0\Jellyfin.Plugin.JellyExplorer.dll mediaserver@192.168.1.6:/tmp/jellyfin-plugins/

%on_remote% "sudo install -d -m 0755 -o jellyfin -g jellyfin ./local-plugin/"
%on_remote% "sudo mv /tmp/jellyfin-plugins/*.dll /var/lib/jellyfin/plugins/local-plugin/"
%on_remote% "sudo chown jellyfin:jellyfin ./local-plugin/*.dll "
%on_remote% "sudo service jellyfin restart "
%on_remote% "sudo service jellyfin status | grep Active:"
