using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    public class ListViewDateTimeComparer : IComparer
    {
        int column;

        public ListViewDateTimeComparer(int column)
        {
            this.column = column;
        }

        int IComparer.Compare(object x, object y)
        {
            Torrent tx = (Torrent)x;
            Torrent ty = (Torrent)y;
            DateTime dtx = (DateTime)tx.SubItems[column].Tag;
            DateTime dty = (DateTime)ty.SubItems[column].Tag;
            if (dtx == null)
            {
                return 1;
            }
            else if (dty == null)
            {
                return -1;
            }
            else
            {
                return dtx.CompareTo(dty);
            }
        }
    }
}
