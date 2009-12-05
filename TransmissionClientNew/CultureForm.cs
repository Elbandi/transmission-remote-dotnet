using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    public class CultureForm : Form
    {
        public CultureForm()
        {
            Program.CultureChanger.AddForm(this);
        }
    }
}
