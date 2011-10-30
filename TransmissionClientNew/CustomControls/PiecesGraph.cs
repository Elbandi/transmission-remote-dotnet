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
        private bool valid = false;
        private Bitmap bmp;
        public PiecesGraph()
        {
            bmp = new Bitmap(this.Width, this.Height);
            len = 0;
            // Set Optimized Double Buffer to reduce flickering
            this.SetStyle(ControlStyles.UserPaint, true);
//            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Redraw when resized
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Invalidated += new InvalidateEventHandler(PiecesGraph_Invalidated);
        }

        void PiecesGraph_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (valid) return;
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(BackColor);
                int c_bit = 0, num_bits, bits_got;
                float bitsperrow = (bmp.Width > 0 ? (float)len / (float)bmp.Width : 0), chunk_done;

                if (bitsperrow > 0)
                {
                    for (int n = 0; n < bmp.Width; n++)
                    {
                        num_bits = (int)(bitsperrow * (n + 1)) - c_bit;
                        bits_got = 0;
                        for (int i = 0; i < num_bits; i++)
                        {
                            if (BitGet(bits, len, c_bit + i))
                                bits_got++;
                        }
                        if (num_bits > 0)
                            chunk_done = (float)bits_got / (float)num_bits;
                        else if (BitGet(bits, len, c_bit))
                            chunk_done = 1;
                        else
                            chunk_done = 0;
                        Color fill = Color.FromArgb((int)(BackColor.R * (1 - chunk_done) + (ForeColor.R) * chunk_done), (int)(BackColor.G * (1 - chunk_done) + (ForeColor.G) * chunk_done), (int)(BackColor.B * (1 - chunk_done) + (ForeColor.B) * chunk_done));

                        g.DrawLine(new Pen(fill), n, 0, n, bmp.Height);

                        c_bit += num_bits;
                    }
                }
            }
            valid = true;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            bmp.Dispose();
            bmp = new Bitmap(this.Width, this.Height);
            valid = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(bmp, 0, 0);
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
            valid = false;
            Invalidate();
        }

        public void ClearBits()
        {
            this.len = 0;
            this.bits = new byte[0];
            valid = false;
            Invalidate();
        }
    }
}
