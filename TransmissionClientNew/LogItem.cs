using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    class LogItem
    {
        private DateTime date;
        private string title;
        private string body;
        private object updateserial;

        public DateTime Date
        {
            get { return date; }
        }

        public string Title
        {
            get { return title; }
        }

        public string Body
        {
            get { return body; }
        }

        public object updateSerial
        {
            get { return updateserial; }
            set { updateserial = value; }
        }

        public LogItem(DateTime date, string title, string body, object tag)
        {
            this.date = date;
            this.title = title;
            this.body = body;
            this.updateserial = tag;
        }

        public ListViewItem UpdateListviewItem(ListViewItem LVI)
        {
            while (LVI.SubItems.Count < 3)
            {
                LVI.SubItems.Add("");
            }
            LVI.SubItems[0].Text = this.date.ToString();
            LVI.SubItems[1].Text = this.title;
            LVI.SubItems[2].Text = this.body;
            return LVI;
        }
    }
}
