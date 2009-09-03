// transmission-remote-dotnet
// http://code.google.com/p/transmission-remote-dotnet/
// Copyright (C) 2009 Alan F
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using TransmissionRemoteDotnet.Comparers;

namespace TransmissionRemoteDotnet.Comparers
{
    public class ListViewItemSorter : IComparer, IListViewItemSorter
    {
        private int columnToSort;
        private SortOrder orderOfSort;
        private IComparer objectCompare;
        
        public ListViewItemSorter()
        {
            columnToSort = 0;
            orderOfSort = SortOrder.None;
            objectCompare = new ListViewTextComparer(0, false);
        }

        public int Compare(object x, object y)
        {
            int compareResult;
            compareResult = objectCompare.Compare(x, y);
            if (orderOfSort == SortOrder.Ascending)
            {
                return compareResult;
            }
            else if (orderOfSort == SortOrder.Descending)
            {
                return (-compareResult);
            }
            else
            {
                return 0;
            }
        }

        /* Set the column and choose the best IComparer implementation */

        public int SortColumn
        {
            set
            {
                columnToSort = value;
                switch (columnToSort)
                {
                    case 0:
                        objectCompare = new ListViewTextComparer(0, false);
                        break;
                    case 1:
                        objectCompare = new ListViewTorrentInt64Comparer(ProtocolConstants.FIELD_TOTALSIZE);
                        break;
                    case 2:
                        objectCompare = new ListViewItemDecimalComparer(value);
                        break;
                    case 4:
                        objectCompare = new ListViewTorrentInt32Comparer(ProtocolConstants.FIELD_SEEDERS);
                        break;
                    case 5:
                        objectCompare = new ListViewTorrentInt32Comparer(ProtocolConstants.FIELD_LEECHERS);
                        break;
                    case 6:
                        objectCompare = new ListViewTorrentInt32Comparer(ProtocolConstants.FIELD_RATEDOWNLOAD);
                        break;
                    case 7:
                        objectCompare = new ListViewTorrentInt32Comparer(ProtocolConstants.FIELD_RATEUPLOAD);
                        break;
                    case 8:
                        objectCompare = new ListViewTorrentInt64Comparer(ProtocolConstants.FIELD_ETA);
                        break;
                    case 9:
                        objectCompare = new ListViewTorrentInt64Comparer(ProtocolConstants.FIELD_UPLOADEDEVER);
                        break;
                    case 10:
                        objectCompare = new ListViewTorrentRatioComparer();
                        break;
                    case 11:
                        objectCompare = new ListViewTorrentInt64Comparer(ProtocolConstants.FIELD_ADDEDDATE);
                        break;
                    case 12:
                        objectCompare = new ListViewDoneDateComparer();
                        break;
                    default:
                        objectCompare = new ListViewTextComparer(columnToSort, true);
                        break;
                }
            }
            get
            {
                return columnToSort;
            }
        }

        public SortOrder Order
        {
            set
            {
                orderOfSort = value;
            }
            get
            {
                return orderOfSort;
            }
        }
    }
}
