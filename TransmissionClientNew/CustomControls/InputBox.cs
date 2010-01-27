using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    public class InputBox
    {
        private class InputBoxForm : Form
        {
            private Label lblPrompt;
            private Button btnOK;
            private Button btnCancel;
            private TextBox txtInput;

            public InputBoxForm()
            {
                InitializeComponent();
            }

            private void InitializeComponent()
            {
                this.lblPrompt = new System.Windows.Forms.Label();
                this.btnOK = new System.Windows.Forms.Button();
                this.btnCancel = new System.Windows.Forms.Button();
                this.txtInput = new System.Windows.Forms.TextBox();
                this.SuspendLayout();
                // 
                // lblPrompt
                // 
                this.lblPrompt.Location = new System.Drawing.Point(12, 9);
                this.lblPrompt.Name = "lblPrompt";
                this.lblPrompt.Size = new System.Drawing.Size(302, 13);
                this.lblPrompt.TabIndex = 0;
                // 
                // btnOK
                // 
                this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.btnOK.Location = new System.Drawing.Point(226, 70);
                this.btnOK.Name = "btnOK";
                this.btnOK.Size = new System.Drawing.Size(64, 24);
                this.btnOK.TabIndex = 2;
                this.btnOK.Text = "&OK";
                // 
                // btnCancel
                // 
                this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.btnCancel.Location = new System.Drawing.Point(326, 70);
                this.btnCancel.Name = "btnCancel";
                this.btnCancel.Size = new System.Drawing.Size(64, 24);
                this.btnCancel.TabIndex = 3;
                this.btnCancel.Text = OtherStrings.Cancel;
                // 
                // txtInput
                // 
                this.txtInput.Location = new System.Drawing.Point(16, 30);
                this.txtInput.Name = "txtInput";
                this.txtInput.Size = new System.Drawing.Size(369, 20);
                this.txtInput.TabIndex = 1;
                // 
                // InputBox
                // 
                this.AcceptButton = this.btnOK;
                this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
                this.CancelButton = this.btnCancel;
                this.ClientSize = new System.Drawing.Size(398, 108);
                this.Controls.Add(this.txtInput);
                this.Controls.Add(this.btnCancel);
                this.Controls.Add(this.btnOK);
                this.Controls.Add(this.lblPrompt);
                this.DoubleBuffered = true;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "InputBox";
                this.Text = "InputBox";
                this.Load += new System.EventHandler(this.InputBox_Load);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.ShowInTaskbar = false;
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            private void InputBox_Load(object sender, EventArgs e)
            {
                this.BringToFront();
                txtInput.Focus();
                txtInput.SelectionLength = txtInput.Text.Length;
            }

            #region Private Properties

            internal string FormCaption
            {
                set
                {
                    this.Text = value;
                }
            } // property FormCaption

            internal string FormPrompt
            {
                set
                {
                    lblPrompt.Text = value;
                }
            } // property FormPrompt

            internal string Value
            {
                get
                {
                    return txtInput.Text;
                }
                set
                {
                    txtInput.Text = value;
                }
            } // property Value

            internal bool UseSystemPasswordChar
            {
                set
                {
                    txtInput.UseSystemPasswordChar = value;
                }
            } // property UseSystemPasswordChar

            #endregion

        }

        static public string Show(string Prompt)
        {
            return Show(Prompt, string.Empty);
        }

        static public string Show(string Prompt, string Title)
        {
            return Show(Prompt, Title, string.Empty);
        }

        static public string Show(string Prompt, string Title, bool UseSystemPasswordChar)
        {
            return Show(Prompt, Title, string.Empty, UseSystemPasswordChar);
        }

        static public string Show(string Prompt, string Title, string Default)
        {
            return Show(Prompt, Title, Default, false);
        }

        static public string Show(string Prompt, string Title, string Default, bool UseSystemPasswordChar)
        {
            InputBoxForm frmInputDialog = new InputBoxForm();
            frmInputDialog.FormCaption = Title;
            frmInputDialog.FormPrompt = Prompt;
            frmInputDialog.Value = Default;
            frmInputDialog.UseSystemPasswordChar = UseSystemPasswordChar;

            return frmInputDialog.ShowDialog() == DialogResult.OK ? frmInputDialog.Value : null;
        }
    }
}
