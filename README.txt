ABOUT
----------------------------

transmission-remote-dotnet is a Windows remote client to the RPC interface of transmission-daemon, which is
part of the Transmission BitTorrent client. The application is quite like µTorrent in appearance and
currently supports almost all the RPC specification.

FEATURES
----------------------------

    * Supports all or almost all the remote functionality of Transmission.
    * Adding torrents by handling .torrent files, drag-n-drop, and browsing.
    * Start, stop, remove, delete, recheck torrents.
    * Authentication support.
    * Limiting upload/download/peer limits globally or for specific torrents. Set alternate global limits for certain times.
    * Prioritisation of files and torrents.
    * Remotely update the blocklist and test the incoming BitTorrent port.
    * Filter by tracker or state.
    * Proxy support.
    * Minimise/close to tray option, torrent complete/started popup.
    * Country of peers displayed by text and flag using GeoIP.
    * SSL support.
    * Multiple settings profiles.
    * Backwards compatible with older versions of Transmission.
    * Samba and SSH integration.
    * More!

CREDITS AND ACKNOWLEDGEMENTS
----------------------------

 * András Első, co-developer of transmission-remote-dotnet, for his many excellent contributions to the project.
 * charles and the other Transmission developers for their great work.
 * Most icons are the creation of David Vignoni in his Nuvola theme. http://www.icon-king.com/
 * Some icons were kindly contributed by liuzhiyi and benketom.
 * The code for reading .torrent files was taken from Alan McGovern's MonoTorrent project.
 * Thank you to all the following users kind enough to translate this application:
    * Romaric R. for the French translation.
    * benba for the German translation.
    * drphrozen for the Danish translation.
    * András Első for the Hungarian translation.
    * liuzhiyi for the Chinese translation.
    * Woodzu and cichy for the Polish translation.
    * RayTracer for the Turkish translation.
    * Alexey Komarov for the Russian translation.
    * huexxx for the Spanish translation.
    * twohertz for the Korean translation.
    * zeal.tsai for the Taiwanese translation.
    * ikeblaster for the Czech translation.

SOURCE CODE
----------------------------

The source is GPLv3 and can be obtained with the command:

svn co http://transmission-remote-dotnet.googlecode.com/svn/trunk/ transmission-remote-dotnet/	

Patches, suggestions and bug reports (with as much info as possible) are always welcome. I will look at
them, but please be patient. Please send them through the google code site (below).

http://code.google.com/p/transmission-remote-dotnet/