set remote_user=mediaserver
set remote_host=mediaservervm
set on_remote=ssh -t %remote_user%@%remote_host%

dotnet clean 
dotnet build --configuration Release

scp -r .\Jellyfin.Plugin.JellyExplorer\bin\Release\net6.0 %remote_user%@%remote_host%:/tmp/JellyExplorer
%on_remote% "sudo -ujellyfin mkdir -p /var/lib/jellyfin/plugins/JellyExplorer/"
%on_remote% "sudo mv /tmp/JellyExplorer/* /var/lib/jellyfin/plugins/JellyExplorer/"
%on_remote% "sudo chown jellyfin:jellyfin /var/lib/jellyfin/plugins/JellyExplorer/* "
%on_remote% "sudo service jellyfin restart "
%on_remote% "sudo service jellyfin status | grep Active:"
