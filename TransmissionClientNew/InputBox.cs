using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    public partial class InputBox : Form
    {
        private Label lblPrompt;
        private Button btnOK;
        private Button btnCancel;
        private TextBox txtInput;

        public InputBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox));
            this.lblPrompt = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblPrompt
            // 
            resources.ApplyResources(this.lblPrompt, "lblPrompt");
            this.lblPrompt.Name = "lblPrompt";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            // 
            // txtInput
            // 
            resources.ApplyResources(this.txtInput, "txtInput");
            this.txtInput.Name = "txtInput";
            // 
            // InputBox
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblPrompt);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.Load += new System.EventHandler(this.InputBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            txtInput.Focus();
            txtInput.SelectionLength = txtInput.Text.Length;
        }

        #region Public Static Show functions

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
            InputBox frmInputDialog = new InputBox();
            frmInputDialog.FormCaption = Title;
            frmInputDialog.FormPrompt = Prompt;
            frmInputDialog.Value = Default;
            frmInputDialog.UseSystemPasswordChar = UseSystemPasswordChar;

            return frmInputDialog.ShowDialog() == DialogResult.OK ? frmInputDialog.Value : null;
        }

        #endregion

        #region Private Properties

        private string FormCaption
        {
            set
            {
                this.Text = value;
            }
        } // property FormCaption

        private string FormPrompt
        {
            set
            {
                lblPrompt.Text = value;
            }
        } // property FormPrompt

        private string Value
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

        private bool UseSystemPasswordChar
        {
            set
            {
                txtInput.UseSystemPasswordChar = value;
            }
        } // property UseSystemPasswordChar

        #endregion

    }
}
