// transmission-remote-dotnet
// http://code.google.com/p/transmission-remote-dotnet/
// Copyright (C) 2009 Alan F
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Text;

namespace TransmissionRemoteDotnet
{
    partial class AboutDialog : CultureForm
    {
        Dictionary<string, string> coders = new Dictionary<string, string>()
        {
            {"Alan F", "YWxhbkBldGgwLm9yZy51aw=="},
            {"András Első", "YW5kcmFzLmVsc29AZ21haWwuY29t"},
        };
        public AboutDialog()
        {
            InitializeComponent();
            Version version = AssemblyVersion;
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyTitle;
            this.labelVersion.Text = String.Format("v{0}.{1} ({2} {3})", version.Major, version.Minor, "build" /* OtherStrings.Build.ToLower() */, version.Build);
            this.labelCopyright.Text = String.Format("{0} {1}", AssemblyCopyright, Encoding.ASCII.GetString(Convert.FromBase64String("")));
            this.tableLayoutPanel.ColumnStyles[0].Width = this.labelProductName.Width + 8;
            LinkLabel.Link hl = new LinkLabel.Link();
            hl.Description = "desc";
            hl.LinkData = hl.Name = "http://code.google.com/p/transmission-remote-dotnet";
            hl.Start = labelHomepageLink.Text.Length + 1;
            hl.Length = hl.Name.Length;
            labelHomepageLink.Text += " " + hl.Name;
            this.labelHomepageLink.Links.Add(hl);
            bool first = true;
            foreach (KeyValuePair<string, string> c in coders)
            {
                LinkLabel.Link l = new LinkLabel.Link();
                l.Description = "desc";
                l.LinkData = Encoding.ASCII.GetString(Convert.FromBase64String(c.Value));
                l.Name = string.Format("{0} <{1}>", c.Key, l.LinkData);
                if (!first)
                {
                    labelDevelopers.Text += ",";
                }
                l.Start = labelDevelopers.Text.Length + 1;
                l.Length = l.Name.Length;
                labelDevelopers.Text += " " + l.Name;
                labelDevelopers.Links.Add(l);
                first = false;
            }
        }

        private void labelHomepageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            labelHomepageLink.LinkVisited = true;

            System.Diagnostics.Process.Start(labelHomepageLink.Text.Substring(e.Link.Start, e.Link.Length));
        }

        private void labelCoders_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                e.Link.Visited = true;
                System.Diagnostics.Process.Start("mailto:" + e.Link.LinkData.ToString());
            }
            catch (Exception)
            {
            }
        }

        #region Assembly Attribute Accessors

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyLocation
        {
            get
            {
                return Assembly.GetExecutingAssembly().Location;
            }
        }

        public static Version AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
}
