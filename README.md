# JellyExplorer plugin

```
clear && 
dotnet build &&
scp .\Jellyfin.Plugin.JellyExplorer\bin\Debug\net6.0\Jellyfin.Plugin.JellyExplorer.dll 'mediaserver@192.168.1.6:/tmp/jellyfin-plugins/' &&
ssh -t mediaserver@192.168.1.6 'cd /var/lib/jellyfin/plugins/local-plugin/ && sudo mv /tmp/jellyfin-plugins/*.dll . && sudo chown jellyfin:jellyfin ./*.dll && sudo service jellyfin restart && sudo service jellyfin status | grep Active:'
```



                            