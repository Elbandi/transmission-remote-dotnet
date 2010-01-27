using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace TransmissionRemoteDotnet
{
    class SelectableLabel : TextBox
    {
        public SelectableLabel()
        {
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;
            base.ReadOnly = true;
            base.Text = "";
            base.Visible = false;
#if !MONO
            base.MouseUp += new MouseEventHandler(
                delegate(object sender, MouseEventArgs e) { HideCaret((sender as Control).Handle); }
            );
#endif
        }

#if !MONO
        [DllImport("User32.dll")]
        static extern Boolean HideCaret(IntPtr hWnd);

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                base.Visible = value.Length > 0;
            }
        }
#endif
    }
}
