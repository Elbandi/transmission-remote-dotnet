using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace TransmissionRemoteDotnet
{
    class ListViewHeader : NativeWindow
    {
        ListView listView;
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr windowHandle, uint command);
        private const int GetWindow_Child = 5;
        private const int Msg_SetCursor = 0x0020, Msg_LButtonDown = 0x0201,
            Msg_LButtonDoubleClick = 0x0203;
        private const int HDM_FIRST = 0x1200;
        private const int HDM_HITTEST = HDM_FIRST + 6;

        private const int HHT_ONDIVIDER = 4;
        private const int HHT_ONDIVOPEN = 8;

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;
        private const int SB_CTL = 2;
        private const int SB_BOTH = 3;

        private const int SIF_POS = 0x0004;
        private const int SIF_DISABLENOSCROLL = 0x0008;
        private const int SIF_TRACKPOS = 0x0010;
        //        private const int SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);

        [StructLayout(LayoutKind.Sequential)]
        public class SCROLLINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(SCROLLINFO));
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetScrollInfo(IntPtr hWnd, int fnBar, SCROLLINFO si);

        [StructLayout(LayoutKind.Sequential)]
        public class HDHITTESTINFO
        {
            public int pt_x;
            public int pt_y;
            public int flags;
            public int iItem;
        }
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageHDHITTESTINFO(IntPtr hWnd, int Msg, IntPtr wParam, [In, Out] HDHITTESTINFO lParam);

        private int HeaderControlHitTest(IntPtr handle, Point pt, int flag)
        {
            HDHITTESTINFO testInfo = new HDHITTESTINFO();
            testInfo.pt_x = pt.X;
            testInfo.pt_y = pt.Y;
            IntPtr result = SendMessageHDHITTESTINFO(handle, HDM_HITTEST, IntPtr.Zero, testInfo);

            if ((testInfo.flags & flag) != 0)
                return result.ToInt32();
            else
                return -1;
        }

        public static int GetScrollPosition(ListView lv, bool horizontalBar)
        {
            int fnBar = (horizontalBar ? SB_HORZ : SB_VERT);

            SCROLLINFO si = new SCROLLINFO();
            si.fMask = SIF_POS;
            if (GetScrollInfo(lv.Handle, fnBar, si))
                return si.nPos;
            else
                return -1;
        }

        public int GetDividerUnderPoint(IntPtr handle, Point pt)
        {
            return HeaderControlHitTest(handle, pt, HHT_ONDIVIDER + HHT_ONDIVOPEN);
        }

        protected bool IsCursorOverLockedDivider
        {
            get
            {
                Point pt = this.listView.PointToClient(Cursor.Position);
                pt.X += GetScrollPosition(this.listView, true);
                int dividerIndex = GetDividerUnderPoint(this.Handle, pt);
                if (dividerIndex >= 0 && dividerIndex < this.listView.Columns.Count)
                {
                    ColumnHeader column = this.listView.Columns[dividerIndex];
                    return column.Width == 0;
                }
                else
                    return false;
            }
        }

        public ListViewHeader(ListView listView)
        {
            //            m = (Form1)listView.Parent;
            this.listView = listView;
            listView.HandleCreated += delegate(object sender, EventArgs args)
            {
                IntPtr handle = GetWindow(listView.Handle, GetWindow_Child);
                AssignHandle(handle);
            };
        }
        ~ListViewHeader() { ReleaseHandle(); }
        protected override void WndProc(ref Message message)
        {
            switch (message.Msg)
            {
                case Msg_SetCursor:
                case Msg_LButtonDown:
                    if (this.IsCursorOverLockedDivider)
                    {
                        message.Msg = 0;
                    }
                    break;
            }
            base.WndProc(ref message);
        }

    }
}
