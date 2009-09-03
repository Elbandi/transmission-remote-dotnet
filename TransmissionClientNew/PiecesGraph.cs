// transmission-remote-dotnet
// http://code.google.com/p/transmission-remote-dotnet/
// Copyright (C) 2009 Első András
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    public partial class PiecesGraph : UserControl
    {
        private byte[] bits;
        private int len;
        public PiecesGraph()
        {
            len = 0;
            // Set Optimized Double Buffer to reduce flickering
            this.SetStyle(ControlStyles.UserPaint, true);
//            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Redraw when resized
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (len > 0)
            {
                decimal arany = (decimal)len / Width;
                for (int n = 0; n < Width; n++)
                {
                    if (BitGet(bits, len, (int)(n * arany)))
                    {
                        e.Graphics.DrawLine(new Pen(ForeColor), n, 0, n, Height);
                    }
                }
            }
        }

        private bool BitGet(byte[] array, int len, int index)
        {
            if (index < 0 || index >= len)
                throw new ArgumentOutOfRangeException();
            return (array[index >> 3] & (1 << ((7-index) & 7))) != 0;
        }

        public void ApplyBits(byte[] b, int len)
        {
            this.len = len;
            this.bits = b;
            Invalidate();
        }

        public void ClearBits()
        {
            this.len = 0;
            this.bits = new byte[0];
            Invalidate();
        }
    }
}
