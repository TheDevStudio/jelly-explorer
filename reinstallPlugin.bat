set remote_user=mediaserver
set remote_host=mediaservervm
set on_remote=ssh -t %remote_user%@%remote_host%

dotnet clean 
dotnet build 

%on_remote% "mkdir -p /tmp/jellyfin-plugins/"
scp -r .\Jellyfin.Plugin.JellyExplorer\bin\Debug\net6.0\Jellyfin.Plugin.JellyExplorer.dll %remote_user%@%remote_host%:/tmp/jellyfin-plugins/

%on_remote% "sudo -ujellyfin mkdir -p /var/lib/jellyfin/plugins/local-plugin/"
%on_remote% "sudo mv /tmp/jellyfin-plugins/*.dll /var/lib/jellyfin/plugins/local-plugin/"
%on_remote% "sudo chown jellyfin:jellyfin ./local-plugin/*.dll "
%on_remote% "sudo service jellyfin restart "
%on_remote% "sudo service jellyfin status | grep Active:"
