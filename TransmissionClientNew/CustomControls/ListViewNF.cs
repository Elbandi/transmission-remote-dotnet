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

/* This class should prevent ListView's flickering. */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    public class ListViewNF : ListView
    {
        public ListViewNF()
        {
            //Activate double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }

#if !MONO
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct LVCOLUMN
        {
            public Int32 mask;
            public Int32 cx;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)]
            public string pszText;
            public IntPtr hbm;
            public Int32 cchTextMax;
            public Int32 fmt;
            public Int32 iSubItem;
            public Int32 iImage;
            public Int32 iOrder;
        }

        private const Int32 HDI_FORMAT = 0x4;
        private const Int32 HDF_SORTUP = 0x400;
        private const Int32 HDF_SORTDOWN = 0x200;
        private const Int32 LVM_GETHEADER = 0x101f;
        private const Int32 HDM_GETITEM = 0x120b;
        private const Int32 HDM_SETITEM = 0x120c;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageLVCOLUMN(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref LVCOLUMN lPLVCOLUMN);

        public void SetSortIcon(int ColumnIndex, System.Windows.Forms.SortOrder Order)
        {
            IntPtr ColumnHeader = SendMessage(this.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);

            for (int ColumnNumber = 0; ColumnNumber <= this.Columns.Count - 1; ColumnNumber++)
            {
                IntPtr ColumnPtr = new IntPtr(ColumnNumber);
                LVCOLUMN lvColumn = new LVCOLUMN();
                lvColumn.mask = HDI_FORMAT;
                SendMessageLVCOLUMN(ColumnHeader, HDM_GETITEM, ColumnPtr, ref lvColumn);

                if (!(Order == System.Windows.Forms.SortOrder.None) && ColumnNumber == ColumnIndex)
                {
                    switch (Order)
                    {
                        case System.Windows.Forms.SortOrder.Ascending:
                            lvColumn.fmt &= ~HDF_SORTDOWN;
                            lvColumn.fmt |= HDF_SORTUP;
                            break;
                        case System.Windows.Forms.SortOrder.Descending:
                            lvColumn.fmt &= ~HDF_SORTUP;
                            lvColumn.fmt |= HDF_SORTDOWN;
                            break;
                    }
                }
                else
                {
                    lvColumn.fmt &= ~HDF_SORTDOWN & ~HDF_SORTUP;
                }

                SendMessageLVCOLUMN(ColumnHeader, HDM_SETITEM, ColumnPtr, ref lvColumn);
            }
        }
#endif
    }
}
