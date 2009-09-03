/* http://www.codeproject.com/KB/combobox/glistbox.aspx
 * + some of my fixes. */
 
using System.Windows.Forms;
using System;

namespace TransmissionRemoteDotnet
{
    public class GListBoxItem
    {
        private string _myText;
        private int _myImageIndex;

        private int counter = 0;
        public int Counter
        {
            get { return this.counter; }
            set { this.counter = value; }
        }

        public string Text
        {
            get { return this._myText; }
            set { _myText = value; }
        }

        public string TextWithCounter
        {
            get
            {
                if (counter > 0)
                {
                    return String.Format("{0} ({1})", _myText, counter);
                }
                else
                {
                    return _myText;
                }
            }
        }

        public int ImageIndex
        {
            get { return _myImageIndex; }
            set { _myImageIndex = value; }
        }

        public GListBoxItem(string text, int index)
        {
            _myText = text;
            _myImageIndex = index;
        }

        public GListBoxItem(string text) : this(text, -1) { }
        public GListBoxItem() : this("") { }
        public override string ToString()
        {
            return _myText;
        }
    }
}