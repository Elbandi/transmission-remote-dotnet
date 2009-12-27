using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    class SplitContainerFix : SplitContainer
    {
        public SplitContainerFix()
        {
            panel2MinSize = base.Panel2MinSize;
        }

        public new int Panel2MinSize
        {
            get
            {
                if (isSized)
                    return base.Panel2MinSize;
                return panel2MinSize;
            }
            set
            {
                panel2MinSize = value;
                if (isSized)
                    base.Panel2MinSize = panel2MinSize;
            }
        }

        public new System.Drawing.Size Size
        {
            get { return base.Size; }
            set
            {
                base.Size = value;
                if (!isSized)
                {
                    isSized = true;
                    if (panel2MinSize != 0)
                        Panel2MinSize = panel2MinSize;
                }
            }
        }

        private int panel2MinSize;
        private bool isSized = false;
    }
}
