using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    public partial class TorrentGeneralInfo : UserControl
    {
        public TorrentGeneralInfo()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        public string torrentName
        {
            get { return torrentNameGroupBox.Text; }
            set { torrentNameGroupBox.Text = value; }
        }

        public string timeElapsed
        {
            get { return timeElapsedField.Text; }
            set { timeElapsedField.Text = value; }
        }

        public string remaining
        {
            get { return remainingField.Text; }
            set { remainingField.Text = value; }
        }

        public string timeLabelText
        {
            get { return remainingLabel.Text; }
            set { remainingLabel.Text = value; }
        }

        public string piecesInfo
        {
            get { return piecesInfoField.Text; }
            set { piecesInfoField.Text = value; }
        }

        public string downloaded
        {
            get { return downloadedField.Text; }
            set { downloadedField.Text = value; }
        }

        public string uploaded
        {
            get { return uploadedField.Text; }
            set { uploadedField.Text = value; }
        }

        public string seeders
        {
            get { return seedersField.Text; }
            set { seedersField.Text = value; }
        }

        public string downloadSpeed
        {
            get { return downloadSpeedField.Text; }
            set { downloadSpeedField.Text = value; }
        }

        public string uploadSpeed
        {
            get { return uploadSpeedField.Text; }
            set { uploadSpeedField.Text = value; }
        }

        public string leechers
        {
            get { return leechersField.Text; }
            set { leechersField.Text = value; }
        }

        public string downloadLimit
        {
            get { return downloadLimitField.Text; }
            set { downloadLimitField.Text = value; }
        }

        public string uploadLimit
        {
            get { return uploadLimitField.Text; }
            set { uploadLimitField.Text = value; }
        }

        public string ratio
        {
            get { return ratioField.Text; }
            set { ratioField.Text = value; }
        }

        public string status
        {
            get { return statusField.Text; }
            set { statusField.Text = value; }
        }

        public string startedAt
        {
            get { return startedAtField.Text; }
            set { startedAtField.Text = value; }
        }

        public string location
        {
            get { return locationField.Text; }
            set { locationField.Text = value; }
        }

        public string createdAt
        {
            get { return createdAtField.Text; }
            set { createdAtField.Text = value; }
        }

        public string totalSize
        {
            get { return totalSizeField.Text; }
            set { totalSizeField.Text = value; }
        }

        public string createdBy
        {
            get { return createdByField.Text; }
            set { createdByField.Text = value; }
        }

        public string hash
        {
            get { return hashField.Text; }
            set { hashField.Text = value; }
        }

        public string error
        {
            get { return errorField.Text; }
            set { errorField.Text = value; }
        }

        public bool errorVisible
        {
            get { return errorLabel.Visible; }
            set { errorLabel.Visible = errorField.Visible = value; }
        }

        public string comment
        {
            get { return commentField.Text; }
            set { commentField.Text = value; }
        }

        public void BeginUpdate()
        {
            SetRedraw(0);
        }

        public void EndUpdate()
        {
            SetRedraw(1);
            Refresh();
        }

        private const Int32 WM_SETREDRAW = 0xB;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private void SetRedraw(int Param)
        {
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(Param), IntPtr.Zero);
        }
    }
}
