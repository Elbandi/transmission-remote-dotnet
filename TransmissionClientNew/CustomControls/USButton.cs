using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet.CustomControls
{
    class USButton : Button
    {
        public USButton()
            : base()
        {
            this.SetStyle(ControlStyles.Selectable, false);
        }
    }
}
