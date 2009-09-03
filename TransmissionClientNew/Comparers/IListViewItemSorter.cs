using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet.Comparers
{
    interface IListViewItemSorter
    {
        int SortColumn { get; set; }
        SortOrder Order { get; set; }
    }
}
