/*
 * Copyright � 2006 Stefan Trosch�tz (stefan@troschuetz.de)
 * 
 * This file is part of UICultureChanger Class Library.
 * 
 * UICultureChanger is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or any later version.
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 * 
 * UICultureChanger.cs, 20.08.2006
 * 
 */

// If using .NET Framework versions prior 2.0, remove comment delimiters in the following line 
//   or define the Prior2 symbol using project settings
//#define Prior2

using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
#if Prior2
using System.Collections;
#else
using System.Collections.Generic;
#endif
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Troschuetz
{
    /// <summary>
    /// Enables changes of the UI culture of a collection of <see cref="Form"/> objects at runtime.
    /// </summary>
    [DesignerSerializer(typeof(UICultureChanger.UICultureChangerCodeDomSerializer), typeof(CodeDomSerializer))]
    public class UICultureChanger : Component
    {
        #region UICultureChangerCodeDomSerializer class
        /// <summary>
        /// Serializes an object graph of <see cref="UICultureChanger"/> class to a series of CodeDOM statements.
        /// </summary>
        /// <remarks>
        /// The <see cref="Serialize"/> method is customized, so CodeStatement for the object construction 
        ///   doesn't use the default constructor of <see cref="UICultureChanger"/> class.
        /// </remarks>
        internal class UICultureChangerCodeDomSerializer : CodeDomSerializer
        {
            /// <summary>
            /// Deserializes the specified serialized CodeDOM object into an object.
            /// </summary>
            /// <param name="manager">
            /// A serialization manager interface that is used during the deserialization process.
            /// </param>
            /// <param name="codeObject">A serialized CodeDOM object to deserialize.</param>
            /// <returns>The deserialized CodeDOM object.</returns>
            public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
            {
                CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.GetSerializer(
                    typeof(UICultureChanger).BaseType, typeof(CodeDomSerializer));

                return baseClassSerializer.Deserialize(manager, codeObject);
            }

            /// <summary>
            /// Serializes the specified object into a CodeDOM object.
            /// </summary>
            /// <param name="manager">The serialization manager to use during serialization.</param>
            /// <param name="value">The object to serialize.</param>
            /// <returns>A CodeDOM object representing the object that has been serialized.</returns>
            public override object Serialize(IDesignerSerializationManager manager, object value)
            {
                CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.GetSerializer(
                    typeof(UICultureChanger).BaseType, typeof(CodeDomSerializer));
                CodeStatementCollection codeStatementCollection = (CodeStatementCollection)baseClassSerializer.Serialize(
                    manager, value);
                string variableName = ((Component)value).Site.Name;

                // If the CodeStatementCollection only contains the constructor and/or member variable definition, 
                //   add comment-block with name of the member variable.
                if (codeStatementCollection.Count <= 2)
                {
                    codeStatementCollection.Add(new CodeCommentStatement(""));
                    codeStatementCollection.Add(new CodeCommentStatement(variableName));
                    codeStatementCollection.Add(new CodeCommentStatement(""));
                }

                // Add a call to the UICultureChanger.AddForm method.
                CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(
                        new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), variableName),
                        "AddForm", new CodeExpression[] { new CodeThisReferenceExpression() });
                codeStatementCollection.Add(codeMethodInvokeExpression);

                return codeStatementCollection;
            }
        }
        #endregion

        #region ChangeInfo class
        /// <summary>
        /// Encapsulates all information needed to apply localized resources to a form or field.
        /// </summary>
        private class ChangeInfo
        {
            #region instance fields
            /// <summary>
            /// Gets the name of the form or field.
            /// </summary>
            public string Name
            {
                get { return name; }
            }

            /// <summary>
            /// Stores the name of the form or field.
            /// </summary>
            private string name;

            /// <summary>
            /// Gets the instance of the form or field.
            /// </summary>
            public object Value
            {
                get { return this.value; }
            }

            /// <summary>
            /// Stores the instance of the form or field.
            /// </summary>
            private object value;

            /// <summary>
            /// Gets the <see cref="Type"/> object of the form or field.
            /// </summary>
            public Type Type
            {
                get { return type; }
            }

            /// <summary>
            /// Stores the <see cref="Type"/> object of the form or field.
            /// </summary>
            private Type type;
            #endregion

            #region construction
            /// <summary>
            /// Initializes a new instance of the <see cref="ChangeInfo"/> class.
            /// </summary>
            /// <param name="name">The name of the form or field.</param>
            /// <param name="value">The instance of the form or field.</param>
            /// <param name="type">The <see cref="Type"/> object of the form or field.</param>
            public ChangeInfo(string name, object value, Type type)
            {
                this.name = name;
                this.value = value;
                this.type = type;
            }
            #endregion
        }
        #endregion

#if Prior2
        #region ChangeInfoList class
        /// <summary>
        /// A strongly typed collection of <see cref="ChangeInfoList"/> objects.
        /// </summary>
        private class ChangeInfoList : CollectionBase
		{
			#region instance fields
            /// <value>
            /// Gets or sets the <see cref="ChangeInfo"/>object at the specified index.<br />
            /// In C#, this property is the indexer for the <see cref="ChangeInfoList"/> class.
            /// </value>
            /// <remarks>
            /// <see cref="ChangeInfoList"/> accepts a null reference (<see langword="Nothing"/> in Visual Basic) as a valid value 
            ///   and allows duplicate elements.<br />
            /// This property provides the ability to access a specific element in the collection by using the following syntax: 
            ///   <c>myCollection[index]</c>.
            /// </remarks>
            /// <param name="index">The zero-based index of the element to get or set.</param>
            /// <exception cref="ArgumentOutOfRangeException">
            /// <paramref name="index"/> is less than zero.<br />
            /// -or-<br />
            /// <paramref name="index"/>  is equal to or greater than <see cref="CollectionBase.Count"/>.
            /// </exception>
            public ChangeInfo this[int index]  
			{
				get  
				{
					return (ChangeInfo) this.InnerList[index];
				}
				set  
				{
					this.InnerList[index] = value;
				}
			}
			#endregion

			#region construction
			public ChangeInfoList(int capacity)
			{
				base.InnerList.Capacity = capacity;
			}
			#endregion

			#region instance methods
            /// <summary>
            /// Adds a <see cref="ChangeInfo"/> object to the end of the <see cref="ChangeInfoList"/>.
            /// </summary>
            /// <remarks>
            /// <see cref="ChangeInfoList"/> accepts a null reference (<see langword="Nothing"/> in Visual Basic) as a valid value 
            ///   and allows duplicate elements.
            /// </remarks>
            /// <param name="changeInfo">
            /// The <see cref="ChangeInfo"/> object to be added to the end of the <see cref="ChangeInfoList"/>. 
            /// The value can be a null reference (<see langword="Nothing"/> in Visual Basic).
            /// </param>
            /// <returns>The <see cref="ChangeInfoList"/> index at which the <paramref name="changeInfo"/> has been added.</returns>
            public int Add(ChangeInfo changeInfo)  
			{
				return this.InnerList.Add(changeInfo);
			}

            /// <summary>
            /// Removes the first occurrence of a specific <see cref="ChangeInfo"/> object from the 
            ///   <see cref="ChangeInfoList"/>.
            /// </summary>
            /// <param name="changeInfo">
            /// The <see cref="ChangeInfo"/> object to remove from the <see cref="ChangeInfoList"/>. 
            /// The value can be a null reference (<see langword="Nothing"/> in Visual Basic).
            /// </param>
			public void Remove(ChangeInfo changeInfo)
			{
				this.InnerList.Remove(changeInfo);
			}

            /// <summary>
            /// Sets the capacity to the actual number of elements in the <see cref="ChangeInfoList"/>.
            /// </summary>
            /// <remarks>
            /// This method can be used to minimize a list's memory overhead if no new elements will be added to the list.<br />
            ///	To completely clear all elements in a list, call the <see cref="CollectionBase.Clear"/> method before calling 
            ///	  <see cref="TrimToSize"/>. Trimming an empty <see cref="ChangeInfoList"/> sets the capacity of the 
            ///	  <see cref="ChangeInfoList"/> to the default capacity, not zero.
            /// </remarks>
            public void TrimToSize()
			{
				this.InnerList.TrimToSize();
			}
			#endregion
   		}
		#endregion
#endif

        #region instance fields
		/// <summary>
		/// Stores a collection of <see cref="Form"/> objects whose UI culture will be changed.
		/// </summary>
#if Prior2
		private ArrayList forms;
#else
        private List<Form> forms;
#endif

        /// <summary>
        /// Gets or sets a value indicating whether localized Text values are applied when changing the UI culture.
        /// </summary>
        [Browsable(true), DefaultValue(true)]
        [Description("Indicates whether localized Text values are applied when changing the UI culture."),
         Category("Behavior")]
        public bool ApplyText
        {
            get { return this.applyText; }
            set { this.applyText = value; }
        }

        /// <summary>
        /// Stores a value indicating whether localized Text values are applied when changing the UI culture.
        /// </summary>
        private bool applyText;

        /// <summary>
        /// Gets or sets a value indicating whether localized Size values are applied when changing the UI culture.
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Description("Indicates whether localized Size values are applied when changing the UI culture."), 
         Category("Behavior")]
        public bool ApplySize
        {
            get { return this.applySize; }
            set { this.applySize = value; }
        }

        /// <summary>
        /// Stores a value indicating whether localized Size values are applied when changing the UI culture.
        /// </summary>
        private bool applySize;

        /// <summary>
        /// Gets or sets a value indicating whether localized Location values are applied when changing the UI culture.
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Description("Indicates whether localized Location values are applied when changing the UI culture."), 
         Category("Behavior")]
        public bool ApplyLocation
        {
            get { return this.applyLocation; }
            set { this.applyLocation = value; }
        }

        /// <summary>
        /// Stores a value indicating whether localized Location values are applied when changing the UI culture.
        /// </summary>
        private bool applyLocation;

        /// <summary>
        /// Gets or sets a value indicating whether localized RightToLeft values are applied when changing the UI culture.
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Description("Indicates whether localized RightToLeft values are applied when changing the UI culture."),
         Category("Behavior")]
        public bool ApplyRightToLeft
        {
            get { return this.applyRightToLeft; }
            set { this.applyRightToLeft = value; }
        }

        /// <summary>
        /// Stores a value indicating whether localized RightToLeft values are applied when changing the UI culture.
        /// </summary>
        private bool applyRightToLeft;

#if !Prior2
        /// <summary>
        /// Gets or sets a value indicating whether localized RightToLeftLayout values are applied when changing the UI culture.
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Description("Indicates whether localized RightToLeftLayout values are applied when changing the UI culture."),
         Category("Behavior")]
        public bool ApplyRightToLeftLayout
        {
            get { return this.applyRightToLeftLayout; }
            set { this.applyRightToLeftLayout = value; }
        }

        /// <summary>
        /// Stores a value indicating whether localized RightToLeftLayout values are applied when changing the UI culture.
        /// </summary>
        private bool applyRightToLeftLayout;
#endif

        /// <summary>
        /// Gets or sets a value indicating whether localized ToolTip values are applied when changing the UI culture.
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Description("Indicates whether localized tooltips are applied when changing the UI culture."), 
         Category("Behavior")]
        public bool ApplyToolTip
        {
            get { return this.applyToolTip; }
            set { this.applyToolTip = value; }
        }

        /// <summary>
        /// Stores a value indicating whether localized ToolTip values are applied when changing the UI culture.
        /// </summary>
        private bool applyToolTip;

        /// <summary>
        /// Gets or sets a value indicating whether localized Help values are applied when changing the UI culture.
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Description("Indicates whether localized help contents are applied when changing the UI culture."),
         Category("Behavior")]
        public bool ApplyHelp
        {
            get { return this.applyHelp; }
            set { this.applyHelp = value; }
        }

        /// <summary>
        /// Stores a value indicating whether localized Help values are applied when changing the UI culture.
        /// </summary>
        private bool applyHelp;

        /// <summary>
        /// Gets or sets a value indicating whether the Size values of forms remain unchanged when changing the UI culture.
        /// </summary>
        /// <remarks>
        /// This property has no effect unless <see cref="ApplySize"/> is <see langword="true"/>.
        /// </remarks>
        [Browsable(true), DefaultValue(true)]
        [Description("Indicates whether the Size values of forms are preserved when changing the UI culture."),
         Category("Behavior")]
        public bool PreserveFormSize
        {
            get { return this.preserveFormSize; }
            set { this.preserveFormSize = value; }
        }

        /// <summary>
        /// Stores a value indicating whether the Size values of forms remain unchanged when changing the UI culture.
        /// </summary>
        private bool preserveFormSize;

        /// <summary>
        /// Gets or sets a value indicating whether the Location values of forms remain unchanged when changing the UI culture.
        /// </summary>
        /// <remarks>
        /// This property has no effect unless <see cref="ApplyLocation"/> is <see langword="true"/>.
        /// </remarks>
        [Browsable(true), DefaultValue(true)]
        [Description("Indicates whether the Location values of forms are preserved when changing the UI culture."),
         Category("Behavior")]
        public bool PreserveFormLocation
        {
            get { return this.preserveFormLocation; }
            set { this.preserveFormLocation = value; }
        }

        /// <summary>
        /// Stores a value indicating whether the Location values of forms remain unchanged when changing the UI culture.
        /// </summary>
        private bool preserveFormLocation;
        #endregion

        #region construction, deconstruction
        /// <summary>
        /// Initializes a new instance of the <see cref="UICultureChanger"/> class.
        /// </summary>
        public UICultureChanger()
        {
            // Create List with size 1, so it can take the container form of the component without resizing.
#if Prior2
			this.forms = new ArrayList(1);
#else
			this.forms = new List<Form>(1);
#endif

            this.applyText = true;
            this.applySize = false;
            this.applyLocation = false;
            this.applyRightToLeft = false;
#if !Prior2
            this.applyRightToLeftLayout = false;
#endif
            this.applyToolTip = false;
            this.applyHelp = false;
            this.preserveFormSize = true;
            this.preserveFormLocation = true;
        }

        /// <summary> 
        /// Releases the unmanaged resources used by the <see cref="UICultureChanger"/> and optionally releases the managed 
        ///   resources.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only 
        ///   unmanaged resources. 
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (int index = 0; index < this.forms.Count; index++)
                {
#if Prior2
					this.RemoveForm((Form) this.forms[index]);
#else
					this.RemoveForm(this.forms[index]);
#endif
		        }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region instance methods
        /// <summary>
        /// Adds the specified <see cref="Form"/> object to the collection of forms whose UI cultures will be changed.
        /// </summary>
        /// <remarks>
        /// The <see cref="UICultureChanger"/> component registers to the <see cref="Form.FormClosed"/> event of the specified
        ///   <see cref="Form"/> object, so after being closed it can automatically be removed from the form collection.
        /// </remarks>
        /// <param name="form">The <see cref="Form"/> object to add to the end of the form collection.</param>
        public void AddForm(Form form)
        {
            if (form != null)
            {
                this.forms.Add(form);

#if Prior2
				form.Closed += new EventHandler(this.Form_Closed);
#else
				form.FormClosed += new FormClosedEventHandler(this.Form_FormClosed);
#endif
            }
        }

        /// <summary>
        /// Removes the first occurrence of the specified <see cref="Form"/> object from the collection of forms 
        ///   whose UI cultures will be changed.
        /// </summary>
        /// <param name="form">The <see cref="Form"/> object to remove from the form collection.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="form"/> is successfully removed; otherwise, <see langword="false"/>. 
        /// This method also returns <see langword="false"/> if <paramref name="form"/> is a null reference 
        ///   (<see langword="Nothing"/> in Visual Basic) or was not found in the form collection. 
        /// </returns>
        public bool RemoveForm(Form form)
        {
            if (form == null)
            {
                return false;
            }
            else
            {
#if Prior2
				form.Closed -= new EventHandler(this.Form_Closed);
				this.forms.Remove(form);
				return true;
#else
				form.FormClosed -= new FormClosedEventHandler(this.Form_FormClosed);
                return this.forms.Remove(form);
#endif
            }
        }

#if Prior2
		/// <summary>
		/// Removes the specified sender object from the collection of forms whose UI cultures will be changed, if it is a 
		///   <see cref="Form"/> object
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="EventArgs"/> object that contains the event data.</param>
		private void Form_Closed(object sender, EventArgs e)
		{
			Form form = sender as Form;
			if (form != null)
			{
				this.RemoveForm(form);
			}
		}
#else
		/// <summary>
        /// Removes the specified sender object from the collection of forms whose UI cultures will be changed, if it is a 
        ///   <see cref="Form"/> object
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="FormClosedEventArgs"/> object that contains the event data.</param>
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form form = sender as Form;
            if (form != null)
            {
                this.RemoveForm(form);
            }
        }
#endif

        /// <summary>
        /// Applies the specified <see cref="CultureInfo"/> object to the <see cref="Thread.CurrentUICulture"/> field and 
        ///   corresponding localized resources to all collected forms.
        /// </summary>
        /// <param name="cultureInfo">A <see cref="CultureInfo"/> object representing the wanted UI culture.</param>
        public void ApplyCulture(CultureInfo cultureInfo)
        {
            // Applies culture to current Thread.
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            for (int index = 0; index < this.forms.Count; index++)
            {
#if Prior2
				this.ApplyCultureToForm((Form)this.forms[index]);
#else
				this.ApplyCultureToForm(this.forms[index]);
#endif
            }
        }

        /// <summary>
        /// Applies localized resources to the specified <see cref="Form"/> object according to the 
        ///   <see cref="Thread.CurrentUICulture"/>.
        /// </summary>
        /// <param name="form">The <see cref="Form"/> object to which changed UI culture should be applied.</param>
        private void ApplyCultureToForm(Form form)
        {
            // Create a resource manager for the form and determine its fields via reflection.
            // Create and fill a collection, containing all infos needed to apply localized resources.
            ComponentResourceManager resources = new ComponentResourceManager(form.GetType());
            FieldInfo[] fields = form.GetType().GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly |
                BindingFlags.NonPublic);
#if Prior2
			ChangeInfoList changeInfos = new ChangeInfoList(fields.Length + 1);
#else
			List<ChangeInfo> changeInfos = new List<ChangeInfo>(fields.Length + 1);
#endif
            changeInfos.Add(new ChangeInfo("$this", form, form.GetType()));
            for (int index = 0; index < fields.Length; index++)
            {
                changeInfos.Add(new ChangeInfo(fields[index].Name, fields[index].GetValue(form), fields[index].FieldType));
            }
#if Prior2
			changeInfos.TrimToSize();
#else
			changeInfos.TrimExcess();
#endif
            
            // Call SuspendLayout for Form and all fields derived from Control, so assignment of 
            //   localized resources doesn't change layout immediately.
            for (int index = 0; index < changeInfos.Count; index++)
            {
                if (changeInfos[index].Type.IsSubclassOf(typeof(Control)))
                {
                    changeInfos[index].Type.InvokeMember("SuspendLayout", BindingFlags.InvokeMethod, null,
                        changeInfos[index].Value, null);
                }
            }

            if (this.applyText)
            {
                // If available, assign localized text to Form and fields.
                String text;
                for (int index = 0; index < changeInfos.Count; index++)
                {
                    if (changeInfos[index].Type.GetProperty("Text", typeof(String)) != null &&
                        changeInfos[index].Type.GetProperty("Text", typeof(String)).CanWrite)
                    {
                        text = resources.GetString(changeInfos[index].Name + ".Text");
                        if (text != null && text != "")
                        {
                            changeInfos[index].Type.InvokeMember("Text", BindingFlags.SetProperty, null,
                                changeInfos[index].Value, new object[] { text });
                        }
                    }
                }
            }

            if (this.applySize)
            {
                // If available, assign localized sizes to Form and fields.
                object size;
                Control control;
                int index = 0;
                if (this.preserveFormSize)
                {
                    // Skip the form entry in changeInfos collection.
                    index = 1;
                }
                for (; index < changeInfos.Count; index++)
                {
                    if (changeInfos[index].Type.GetProperty("Size", typeof(Size)) != null &&
                        changeInfos[index].Type.GetProperty("Size", typeof(Size)).CanWrite)
                    {
                        size = resources.GetObject(changeInfos[index].Name + ".Size");
                        if (size != null && size.GetType() == typeof(Size))
                        {
                            if (changeInfos[index].Type.IsSubclassOf(typeof(Control)))
                            {// In case of an inheritor of Control take into account the Anchor property.
                                control = (Control)changeInfos[index].Value;
                                if ((control.Anchor & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right))
                                {
                                    // Control is bound to the left and right edge, so preserve its width.
                                    size = new Size(control.Width, ((Size)size).Height);
                                }
                                if ((control.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom))== (AnchorStyles.Top | AnchorStyles.Bottom))
                                {
                                    // Control is bound to the top and bottom edge, so preserve its height.
                                    size = new Size(((Size)size).Width, control.Height);
                                }
                                control.Size = (Size)size;
                            }
                            else
                            {
                                changeInfos[index].Type.InvokeMember("Size", BindingFlags.SetProperty, null,
                                    changeInfos[index].Value, new object[] { size });
                            }
                        }
                    }

                    if (changeInfos[index].Type.GetProperty("ClientSize", typeof(Size)) != null &&
                        changeInfos[index].Type.GetProperty("ClientSize", typeof(Size)).CanWrite)
                    {
                        size = resources.GetObject(changeInfos[index].Name + ".ClientSize");
                        if (size != null && size.GetType() == typeof(Size))
                        {
                            if (changeInfos[index].Type.IsSubclassOf(typeof(Control)))
                            {// In case of an inheritor of Control take into account the Anchor property.
                                control = (Control)changeInfos[index].Value;
                                if ((control.Anchor & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right))
                                {
                                    // Control is bound to the left and right edge, so preserve the width of its client area.
                                    size = new Size(control.ClientSize.Width, ((Size)size).Height);
                                }
                                if ((control.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom))
                                {
                                    // Control is bound to the top and bottom edge, so preserve the height of its client area.
                                    size = new Size(((Size)size).Width, control.ClientSize.Height);
                                }
                                control.ClientSize = (Size)size;
                            }
                            else
                            {
                                changeInfos[index].Type.InvokeMember("ClientSize", BindingFlags.SetProperty, null,
                                    changeInfos[index].Value, new object[] { size });
                            }
                        }
                    }
                }
            }

            if (this.applyLocation)
            {
                // If available, assign localized locations to Form and fields.
                object location;
                Control control;
                int index = 0;
                if (this.preserveFormLocation)
                {
                    // Skip the form entry in changeInfos collection.
                    index = 1;
                }
                for (; index < changeInfos.Count; index++)
                {
                    if (changeInfos[index].Type.GetProperty("Location", typeof(Point)) != null &&
                        changeInfos[index].Type.GetProperty("Location", typeof(Point)).CanWrite)
                    {
                        location = resources.GetObject(changeInfos[index].Name + ".Location");
                        if (location != null && location.GetType() == typeof(Point))
                        {
                            if (changeInfos[index].Type.IsSubclassOf(typeof(Control)))
                            {// In case of an inheritor of Control take into account the Anchor property.
                                control = (Control)changeInfos[index].Value;
                                if ((control.Anchor & (AnchorStyles.Left | AnchorStyles.Right)) == AnchorStyles.Right)
                                {
                                    // Control is bound to the right but not the left edge, so preserve its x-coordinate.
                                    location = new Point(control.Left, ((Point)location).Y);
                                }
                                if ((control.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == AnchorStyles.Bottom)
                                {
                                    // Control is bound to the bottom but not the top edge, so preserve its y-coordinate.
                                    location = new Point(((Point)location).X, control.Top);
                                }
                                control.Location = (Point)location;
                            }
                            else
                            {
                                changeInfos[index].Type.InvokeMember("Location", BindingFlags.SetProperty, null,
                                    changeInfos[index].Value, new object[] { location });
                            }
                        }
                    }
                }
            }

            if (this.applyRightToLeft)
            {
                // If available, assign localized RightToLeft values to Form and fields.
                object rightToLeft;
                for (int index = 0; index < changeInfos.Count; index++)
                {
                    if (changeInfos[index].Type.GetProperty("RightToLeft", typeof(RightToLeft)) != null &&
                        changeInfos[index].Type.GetProperty("RightToLeft", typeof(RightToLeft)).CanWrite)
                    {
                        rightToLeft = resources.GetObject(changeInfos[index].Name + ".RightToLeft");
                        if (rightToLeft != null && rightToLeft.GetType() == typeof(RightToLeft))
                        {
                            changeInfos[index].Type.InvokeMember("RightToLeft", BindingFlags.SetProperty, null,
                                changeInfos[index].Value, new object[] { rightToLeft });
                        }
                    }
                }
            }

#if !Prior2
            if (this.applyRightToLeftLayout)
            {
                // If available, assign localized RightToLeftLayout values to Form and fields.
                object rightToLeftLayout;
                for (int index = 0; index < changeInfos.Count; index++)
                {
                    if (changeInfos[index].Type.GetProperty("RightToLeftLayout", typeof(bool)) != null &&
                        changeInfos[index].Type.GetProperty("RightToLeftLayout", typeof(bool)).CanWrite)
                    {
                        rightToLeftLayout = resources.GetObject(changeInfos[index].Name + ".RightToLeftLayout");
                        if (rightToLeftLayout != null && rightToLeftLayout.GetType() == typeof(bool))
                        {
                            changeInfos[index].Type.InvokeMember("RightToLeftLayout", BindingFlags.SetProperty, null,
                                changeInfos[index].Value, new object[] { rightToLeftLayout });
                        }
                    }
                }
            }
#endif

            if (this.applyToolTip)
            {
                // If available, assign localized ToolTipText to fields.
                // Also search for a ToolTip component in the current form.
                ToolTip toolTip = null;
                for (int index = 1; index < changeInfos.Count; index++)
                {
                    if (changeInfos[index].Type == typeof(ToolTip))
                    {
                        toolTip = (ToolTip)changeInfos[index].Value;
                        resources.ApplyResources(toolTip, changeInfos[index].Name);
                        changeInfos.Remove(changeInfos[index]);
                    }
#if !Prior2
                    String text;
                    if (changeInfos[index].Type.GetProperty("ToolTipText", typeof(String)) != null &&
                        changeInfos[index].Type.GetProperty("ToolTipText", typeof(String)).CanWrite)
                    {
                        text = resources.GetString(changeInfos[index].Name + ".ToolTipText");
                        if (text != null)
                        {
                            changeInfos[index].Type.InvokeMember("ToolTipText", BindingFlags.SetProperty, null,
                                changeInfos[index].Value, new object[] { text });
                        }
                    }
#endif
                }

                if (toolTip != null)
                {// Form contains a ToolTip component.
                    // If available, assign localized tooltips to Form and fields.
                    String text;
                    for (int index = 0; index < changeInfos.Count; index++)
                    {
                        if (changeInfos[index].Type.IsSubclassOf(typeof(Control)))
                        {
                            text = resources.GetString(changeInfos[index].Name + ".ToolTip");
                            if (text != null)
                            {
                                toolTip.SetToolTip((Control)changeInfos[index].Value, text);
                            }
                        }
                    }
                }
            }

            if (this.applyHelp)
            {
                // Search for a HelpProvider component in the current form.
                HelpProvider helpProvider = null;
                for (int index = 1; index < changeInfos.Count; index++)
                {
                    if (changeInfos[index].Type == typeof(HelpProvider))
                    {
                        helpProvider = (HelpProvider)changeInfos[index].Value;
                        resources.ApplyResources(helpProvider, changeInfos[index].Name);
                        changeInfos.Remove(changeInfos[index]);
                        break;
                    }
                }

                if (helpProvider != null)
                {
                    // If available, assign localized help to Form and fields.
                    String text;
                    object help;
                    for (int index = 0; index < changeInfos.Count; index++)
                    {
                        if (changeInfos[index].Type.IsSubclassOf(typeof(Control)))
                        {
                            text = resources.GetString(changeInfos[index].Name + ".HelpKeyword");
                            if (text != null)
                            {
                                helpProvider.SetHelpKeyword((Control)changeInfos[index].Value, text);
                            }
                            help = resources.GetObject(changeInfos[index].Name + ".HelpNavigator");
                            if (help != null && help.GetType() == typeof(HelpNavigator))
                            {
                                helpProvider.SetHelpNavigator((Control)changeInfos[index].Value, (HelpNavigator)help);
                            }
                            text = resources.GetString(changeInfos[index].Name + ".HelpString");
                            if (text != null)
                            {
                                helpProvider.SetHelpString((Control)changeInfos[index].Value, text);
                            }
                            help = resources.GetObject(changeInfos[index].Name + ".ShowHelp");
                            if (help != null && help.GetType() == typeof(bool))
                            {
                                helpProvider.SetShowHelp((Control)changeInfos[index].Value, (bool)help);
                            }
                        }
                    }
                }
            }

            // Call ResumeLayout for Form and all fields derived from Control to resume layout logic.
            for (int index = changeInfos.Count - 1; index >= 0; index--)
            {
                if (changeInfos[index].Type.IsSubclassOf(typeof(Control)))
                {
                    changeInfos[index].Type.InvokeMember("ResumeLayout", BindingFlags.InvokeMethod, null,
                        changeInfos[index].Value, new object[] { true });
                }
            }
        }
        #endregion
    }
}
