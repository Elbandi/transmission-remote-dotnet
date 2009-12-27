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
            SortColumn = 0;
            orderOfSort = SortOrder.None;
        }

        public int Compare(object x, object y)
        {
            int compareResult = objectCompare.Compare(x, y);
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

        public void SetupColumn(int RpcVersion)
        {
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
                        objectCompare = new ListViewItemInt64Comparer(columnToSort);
                        break;
                    case 2:
                        objectCompare = new ListViewItemDecimalComparer(columnToSort);
                        break;
                    case 4:
                        objectCompare = new ListViewItemInt32Comparer(columnToSort);
                        break;
                    case 5:
                        objectCompare = new ListViewItemInt32Comparer(columnToSort);
                        break;
                    case 6:
                        objectCompare = new ListViewItemInt64Comparer(columnToSort);
                        break;
                    case 7:
                        objectCompare = new ListViewItemInt64Comparer(columnToSort);
                        break;
                    case 8:
                        objectCompare = new ListViewItemInt64Comparer(columnToSort);
                        break;
                    case 9:
                        objectCompare = new ListViewItemInt64Comparer(columnToSort);
                        break;
                    case 10:
                        objectCompare = new ListViewItemDecimalComparer(columnToSort);
                        break;
                    case 11:
                        objectCompare = new ListViewDateTimeComparer(columnToSort);
                        break;
                    case 12:
                        objectCompare = new ListViewDateTimeComparer(columnToSort);
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
