using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    class LogListViewItem : ListViewItem
    {
        public long UpdateSerial
        {
            get;
            set;
        }

        public bool Debug
        {
            get;
            set;
        }
        public LogListViewItem()
        {
            Debug = false;
            UpdateSerial = -1;
        }
        public LogListViewItem(string text):base(text)
        {
            Debug = false;
            UpdateSerial = -1;
        }
    }
}
