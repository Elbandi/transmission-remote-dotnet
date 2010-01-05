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
            ListViewItem tx = (ListViewItem)x;
            ListViewItem ty = (ListViewItem)y;
            if (tx.SubItems[column].Tag == null && ty.SubItems[column].Tag == null)
            {
                return 0;
            }
            else if (tx.SubItems[column].Tag == null)
            {
                return 1;
            }
            else if (ty.SubItems[column].Tag == null)
            {
                return -1;
            }
            else
            {
                DateTime dtx = (DateTime)tx.SubItems[column].Tag;
                DateTime dty = (DateTime)ty.SubItems[column].Tag;
                return dtx.CompareTo(dty);
            }
        }
    }
}
