#!/usr/bin/python
########################################################################
# This is transmission-remote-cli, whereas 'cli' stands for 'Curses    #
# Luminous Interface', a client for the daemon of the BitTorrent       #
# client Transmission.                                                 #
#                                                                      #
# This program is free software: you can redistribute it and/or modify #
# it under the terms of the GNU General Public License as published by #
# the Free Software Foundation, either version 3 of the License, or    #
# (at your option) any later version.                                  #
#                                                                      #
# This program is distributed in the hope that it will be useful,      #
# but WITHOUT ANY WARRANTY; without even the implied warranty of       #
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the        #
# GNU General Public License for more details:                         #
# http://www.gnu.org/licenses/gpl-3.0.txt                              #
########################################################################

USERNAME = ''
PASSWORD = ''
HOST = 'localhost'
PORT = 9091

import time
import simplejson as json
import urllib2

import os
import signal
import locale
import sys
locale.setlocale(locale.LC_ALL, '')

from optparse import OptionParser


parser = OptionParser(usage="Usage: %prog [[USERNAME:PASSWORD@]HOST[:PORT]]")
(options, connection) = parser.parse_args()


# parse connection data
if connection:
    if connection[0].find('@') >= 0:
        auth, connection[0] = connection[0].split('@')
        if auth.find(':') >= 0:
            USERNAME, PASSWORD = auth.split(':')

    if connection[0].find(':') >= 0:
        HOST, PORT = connection[0].split(':')
        PORT = int(PORT)
    else:
        HOST = connection[0]


# Handle communication with Transmission server.
class TransmissionRequest:
    def __init__(self, host, port, method=None, arguments=None):
        self.url = 'http://%s:%d/transmission/rpc' % (host, port)
        self.open_request  = None
        self.last_update   = 0
        if method:
            self.set_request_data(method, arguments)

    def set_request_data(self, method, arguments=None):
        request_data = {'method':method}
        if arguments: request_data['arguments'] = arguments
        self.http_request = urllib2.Request(url=self.url, data=json.dumps(request_data))

    def send_request(self):
        """Ask for information from server OR submit command."""

        try:
            self.open_request = urllib2.urlopen(self.http_request)
        except AttributeError:
            return
        except urllib2.HTTPError, msg:
            print msg
        except urllib2.URLError, msg:
            if msg.reason[0] == 4:
                return
            else:
                print("Cannot connect to %s: %s" % (self.http_request.host, msg.reason[1]))

    def get_response(self):
        """Get response to previously sent request."""

        if self.open_request == None:
            return {'result': 'no open request'}

        response = self.open_request.read()
        try:
            data = json.loads(response)
        except ValueError:
            print("Cannot not parse response: %s" % response)
        self.open_request = None
        return data


# End of Class TransmissionRequest


# Higher level of data exchange
class Transmission:
    LIST_FIELDS = [ 'rateDownload', 'rateUpload' ]

    def __init__(self, host, port, username, password):
        self.host  = host
        self.port  = port
        self.username = username
        self.password = password

        if username and password:
            url = 'http://%s:%d/transmission/rpc' % (host, port)
            authhandler = urllib2.HTTPDigestAuthHandler()
            authhandler.add_password('Transmission RPC Server', url, username, password)
            opener = urllib2.build_opener(authhandler)
            urllib2.install_opener(opener)

        self.request = TransmissionRequest(host, port, 'torrent-get', {'fields': self.LIST_FIELDS})


    def get_speeds(self):
		self.request.send_request()
                response = self.request.get_response()

                if response['result'] == 'success':
		    totalDown, totalUp = 0, 0
        	    for t in response['arguments']['torrents']:
		        totalDown += t['rateDownload']
		        totalUp += t['rateUpload']
		    return (totalDown, totalUp)

if len(sys.argv) > 1 and sys.argv[1] == "config":
    print "graph_title Transmission up/down rate\ngraph_vlabel Rate (KB/s)\ngraph_category Transmission\ndownRate.label Download rate\nupRate.label Upload rate"
    exit(0)

t = Transmission(HOST, PORT, USERNAME, PASSWORD)
(totalDownload, totalUpload) = t.get_speeds()

print "downRate.value %s\nupRate.value %s" % (round(totalDownload/1024,3), round(totalUpload/1024,3))
