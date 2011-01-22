
// Copyright © 2009 by Christoph Richner. All rights are reserved.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//
// website http://www.raccoom.net, email support@raccoom.net, msn chrisdarebell@msn.com

using System;
using System.Text.RegularExpressions;

namespace Raccoom.Xml
{
	/// <summary>
	/// XmlSerializationUtil gets the xml data and write it's through reflection to the strong typed classes.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	internal class XmlSerializationUtil
	{
		#region fields
		#endregion
		
		internal static bool DecodeXmlTextReaderValue (object instance, System.Xml.XmlReader xmlTextReader)
		{
			return DecodeXmlTextReaderValue(instance,null, xmlTextReader);
		}
		
		internal static bool DecodeXmlTextReaderValue (object instance, System.Reflection.PropertyInfo propertyInfo, System.Xml.XmlReader xmlTextReader)
		{
			try
			{
				// find related property by name if not provided as parameter
				if(propertyInfo==null) propertyInfo = instance.GetType().GetProperty(xmlTextReader.Name, System.Reflection.BindingFlags.IgnoreCase |  System.Reflection.BindingFlags.Public |  System.Reflection.BindingFlags.Instance);
				// 
				if(propertyInfo==null) return false;	
				// unescaped characters <>&
				if(propertyInfo.PropertyType.Equals(typeof(string)))
				{
                    if (xmlTextReader.NodeType == System.Xml.XmlNodeType.Element)
                    {
                        xmlTextReader.ReadStartElement();
                        propertyInfo.SetValue(instance, Decode(xmlTextReader.ReadString().Trim()), null);
                        xmlTextReader.ReadEndElement();
                    }
                    else
                    {
                        propertyInfo.SetValue(instance, Decode(xmlTextReader.Value.Trim()), null);
                    }

				} 
				else if(propertyInfo.PropertyType.Equals(typeof(DateTime)))
				{
					propertyInfo.SetValue(instance, ParseRfc822DateTime(xmlTextReader.ReadInnerXml().Trim()),null);
				}
				else
				{				
					propertyInfo.SetValue(instance, System.ComponentModel.TypeDescriptor.GetConverter(propertyInfo.PropertyType).ConvertFromString(xmlTextReader.ReadInnerXml().Trim()), null);				
				}
			} 
			catch(System.Exception e)
			{
				System.Diagnostics.Debug.WriteLine(propertyInfo.Name +", " + propertyInfo.PropertyType.Name +" / " + instance.ToString() + " " + e.Message);
				return false;
			}
			return true;	
		}
		
		internal static string Decode (string value)
		{
			return System.Web.HttpUtility.HtmlDecode(value);
		}

        #region rfs datetime

        /// <summary>
        /// <remarks>
        ///Copyright (c)  vendredi13@007.freesurf.fr
        ///All rights reserved.
        /// </remarks>
        /// </summary>
        /// <param name="adate">rfc date time</param>
        /// <returns>Rfc date as DateTime</returns>
        internal static System.DateTime ParseRfc822DateTime(string adate)
        {
            System.DateTime dt = System.DateTime.Now;
            if (DateTime.TryParse(adate, out dt)) return dt;
            //
            string tmp;
            string[] resp;
            string dayName;
            string dpart;
            string hour, minute;
            string timeZone;

            //--- strip comments
            //--- XXX : FIXME : how to handle nested comments ?
            tmp = Regex.Replace(adate, "(\\([^(].*\\))", "");

            // strip extra white spaces
            tmp = Regex.Replace(tmp, "\\s+", " ");
            tmp = Regex.Replace(tmp, "^\\s+", "");
            tmp = Regex.Replace(tmp, "\\s+$", "");

            // extract week name part
            resp = tmp.Split(new char[] { ',' }, 2);
            if (resp.Length == 2)
            {
                // there's week name
                dayName = resp[0];
                tmp = resp[1];
            }
            else dayName = "";

            try
            {
                // extract date and time
                int pos = tmp.LastIndexOf(" ");
                if (pos < 1) throw new FormatException("probably not a date");
                dpart = tmp.Substring(0, pos - 1);
                timeZone = tmp.Substring(pos + 1);
                dt = Convert.ToDateTime(dpart);

                // check weekDay name
                // this must be done befor convert to GMT 
                if (!string.IsNullOrEmpty(dayName))
                {
                    if ((dt.DayOfWeek == DayOfWeek.Friday && dayName != "Fri") ||
                        (dt.DayOfWeek == DayOfWeek.Monday && dayName != "Mon") ||
                        (dt.DayOfWeek == DayOfWeek.Saturday && dayName != "Sat") ||
                        (dt.DayOfWeek == DayOfWeek.Sunday && dayName != "Sun") ||
                        (dt.DayOfWeek == DayOfWeek.Thursday && dayName != "Thu") ||
                        (dt.DayOfWeek == DayOfWeek.Tuesday && dayName != "Tue") ||
                        (dt.DayOfWeek == DayOfWeek.Wednesday && dayName != "Wed")
                        )
                        throw new FormatException("invalide week of day");
                }

                // adjust to localtime
                if (Regex.IsMatch(timeZone, "[+\\-][0-9][0-9][0-9][0-9]"))
                {
                    // it's a modern ANSI style timezone
                    int factor = 0;
                    hour = timeZone.Substring(1, 2);
                    minute = timeZone.Substring(3, 2);
                    if (timeZone.Substring(0, 1) == "+") factor = 1;
                    else if (timeZone.Substring(0, 1) == "-") factor = -1;
                    else throw new FormatException("incorrect tiem zone");
                    dt = dt.AddHours(factor * Convert.ToInt32(hour));
                    dt = dt.AddMinutes(factor * Convert.ToInt32(minute));
                }
                else
                {
                    // it's a old style military time zone ?
                    switch (timeZone)
                    {
                        case "A": dt = dt.AddHours(1); break;
                        case "B": dt = dt.AddHours(2); break;
                        case "C": dt = dt.AddHours(3); break;
                        case "D": dt = dt.AddHours(4); break;
                        case "E": dt = dt.AddHours(5); break;
                        case "F": dt = dt.AddHours(6); break;
                        case "G": dt = dt.AddHours(7); break;
                        case "H": dt = dt.AddHours(8); break;
                        case "I": dt = dt.AddHours(9); break;
                        case "K": dt = dt.AddHours(10); break;
                        case "L": dt = dt.AddHours(11); break;
                        case "M": dt = dt.AddHours(12); break;
                        case "N": dt = dt.AddHours(-1); break;
                        case "O": dt = dt.AddHours(-2); break;
                        case "P": dt = dt.AddHours(-3); break;
                        case "Q": dt = dt.AddHours(-4); break;
                        case "R": dt = dt.AddHours(-5); break;
                        case "S": dt = dt.AddHours(-6); break;
                        case "T": dt = dt.AddHours(-7); break;
                        case "U": dt = dt.AddHours(-8); break;
                        case "V": dt = dt.AddHours(-9); break;
                        case "W": dt = dt.AddHours(-10); break;
                        case "X": dt = dt.AddHours(-11); break;
                        case "Y": dt = dt.AddHours(-12); break;
                        case "Z":
                        case "UT":
                        case "GMT": break;    // It's UTC
                        case "EST": dt = dt.AddHours(5); break;
                        case "EDT": dt = dt.AddHours(4); break;
                        case "CST": dt = dt.AddHours(6); break;
                        case "CDT": dt = dt.AddHours(5); break;
                        case "MST": dt = dt.AddHours(7); break;
                        case "MDT": dt = dt.AddHours(6); break;
                        case "PST": dt = dt.AddHours(8); break;
                        case "PDT": dt = dt.AddHours(7); break;
                        default: throw new FormatException("invalide time zone");
                    }
                }
            }
            catch (Exception e)
            {
                throw new FormatException(string.Format("Invalide date:{0}:{1}", e.Message, adate));
            }
            return dt;
        }
        #endregion	
		
	}
}
