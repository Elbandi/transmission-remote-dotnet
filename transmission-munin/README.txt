The contents of this folder aren't really related to transmission-remote-dotnet,
but since it's a trivial project which is related to transmission-daemon
I'll keep it here.

It's a transmission-daemon plugin for munin, which is a graphing framework
similar to nagios for system administrators to generate graphs about their
servers.

I may or may not document or develop this.

transmission-parser.php and transmission is a very basic version which uses lynx, shellscript and PHP

transmission-munin.py is a python version based on transmission-remote-cli (the curses one)
authentication is broken in this one for some reason.

This tutorial helped me setup munin:
http://waste.mandragor.org/munin_tutorial/munin.html#node_setup

Here's the munin site: http://munin.projects.linpro.no/