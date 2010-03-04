/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PaintDotNet.SystemLayer
{
    /// <summary>
    /// Contains static methods related to the user interface.
    /// </summary>
    public static class UI
    {
        private static bool initScales = false;
        private static float xScale;
        private static float yScale;

        public static void FlashForm(Form form)
        {
            IntPtr hWnd = form.Handle;
            SafeNativeMethods.FlashWindow(hWnd, false);
            SafeNativeMethods.FlashWindow(hWnd, false);
            GC.KeepAlive(form);
        }

        private static void InitScaleFactors(Control c)
        {
            if (c == null)
            {
                xScale = 1.0f;
                yScale = 1.0f;
            }
            else
            {
                using (Graphics g = c.CreateGraphics())
                {
                    xScale = g.DpiX / 96.0f;
                    yScale = g.DpiY / 96.0f;
                }
            }

            initScales = true;
        }

        public static void InitScaling(Control c)
        {
            if (!initScales)
            {
                InitScaleFactors(c);
            }
        }

        public static float ScaleWidth(float width)
        {
            return (float)Math.Round(width * GetXScaleFactor());
        }

        public static int ScaleWidth(int width)
        {
            return (int)Math.Round((float)width * GetXScaleFactor());
        }

        public static int ScaleHeight(int height)
        {
            return (int)Math.Round((float)height * GetYScaleFactor());
        }

        public static float ScaleHeight(float height)
        {
            return (float)Math.Round(height * GetYScaleFactor());
        }

        public static Size ScaleSize(Size size)
        {
            return new Size(ScaleWidth(size.Width), ScaleHeight(size.Height));
        }

        public static Point ScalePoint(Point pt)
        {
            return new Point(ScaleWidth(pt.X), ScaleHeight(pt.Y));
        }

        public static float GetXScaleFactor()
        {
            if (!initScales)
            {
                throw new InvalidOperationException("Must call InitScaling() first");
            }

            return xScale;
        }

        public static float GetYScaleFactor()
        {
            if (!initScales)
            {
                throw new InvalidOperationException("Must call InitScaling() first");
            }

            return yScale;
        }

        /// <summary>
        /// Sets a form's opacity.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="opacity"></param>
        /// <remarks>
        /// Note to implementors: This may be implemented as just "form.Opacity = opacity".
        /// This method works around some visual clumsiness in .NET 2.0 related to
        /// transitioning between opacity == 1.0 and opacity != 1.0.</remarks>
        public static void SetFormOpacity(Form form, double opacity)
        {
            if (opacity < 0.0 || opacity > 1.0)
            {
                throw new ArgumentOutOfRangeException("opacity", "must be in the range [0, 1]");
            }

            uint exStyle = SafeNativeMethods.GetWindowLongW(form.Handle, NativeConstants.GWL_EXSTYLE);

            byte bOldAlpha = 255;

            if ((exStyle & NativeConstants.GWL_EXSTYLE) != 0)
            {
                uint dwOldKey;
                uint dwOldFlags;
                bool result = SafeNativeMethods.GetLayeredWindowAttributes(form.Handle, out dwOldKey, out bOldAlpha, out dwOldFlags);
            }

            byte bNewAlpha = (byte)(opacity * 255.0);
            uint newExStyle = exStyle;

            if (bNewAlpha != 255)
            {
                newExStyle |= NativeConstants.WS_EX_LAYERED;
            }

            if (newExStyle != exStyle || (newExStyle & NativeConstants.WS_EX_LAYERED) != 0)
            {
                if (newExStyle != exStyle)
                {
                    SafeNativeMethods.SetWindowLongW(form.Handle, NativeConstants.GWL_EXSTYLE, newExStyle);
                }

                if ((newExStyle & NativeConstants.WS_EX_LAYERED) != 0)
                {
                    SafeNativeMethods.SetLayeredWindowAttributes(form.Handle, 0, bNewAlpha, NativeConstants.LWA_ALPHA);
                }
            }

            GC.KeepAlive(form);
        }

        public static void EnableShield(Button button, bool enableShield)
        {
            IntPtr hWnd = button.Handle;

            SafeNativeMethods.SendMessageW(
                hWnd,
                NativeConstants.BCM_SETSHIELD,
                IntPtr.Zero,
                enableShield ? new IntPtr(1) : IntPtr.Zero);

            GC.KeepAlive(button);
        }
    }
}
