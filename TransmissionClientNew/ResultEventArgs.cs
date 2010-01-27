using System;
using System.Collections.Generic;
using System.Text;

namespace TransmissionRemoteDotnet
{
    public class ResultEventArgs : EventArgs
    {
        public ICommand Result { get; set; }
    }
}
