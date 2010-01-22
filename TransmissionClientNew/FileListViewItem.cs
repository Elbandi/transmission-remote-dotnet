using System;
using System.Collections.Generic;
using System.Text;
using Jayrock.Json;
using System.Windows.Forms;
using Etier.IconHelper;

namespace TransmissionRemoteDotnet
{
    public class FileListViewItem : ListViewItem
    {
        public int FileIndex
        {
            get;
            set;
        }

        private string _extension;
        public string Extension
        {
            get { return this._extension; }
            set
            {
                this._extension = value;
            }
        }

        public string TypeName
        {
            get { return base.SubItems[1].Text; }
            set
            {
                base.SubItems[1].Text = value;
            }
        }

        public long FileSize
        {
            get { return (long)base.SubItems[2].Tag; }
            set
            {
                base.SubItems[2].Tag = value;
                base.SubItems[2].Text = Toolbox.GetFileSize(value);
            }
        }

        public long BytesCompleted
        {
            get { return (long)base.SubItems[3].Tag; }
            set
            {
                base.SubItems[3].Tag = value;
                base.SubItems[3].Text = Toolbox.GetFileSize(value);
                base.SubItems[4].Tag = Toolbox.CalcPercentage(value, this.FileSize);
                base.SubItems[4].Text = this.Progress + "%";
            }
        }

        private bool _wanted;
        public bool Wanted
        {
            get { return this._wanted; }
            set
            {
                this._wanted = value;
                base.SubItems[5].Text = value ? OtherStrings.No : OtherStrings.Yes;
            }
        }

        private int _priority;
        public int Priority
        {
            get { return this._priority; }
            set
            {
                this._priority = value;
                base.SubItems[6].Text = Toolbox.FormatPriority(value);
            }
        }

        public decimal Progress
        {
            get { return (decimal)base.SubItems[4].Tag; }
            set
            {
                base.SubItems[4].Tag = value;
                base.SubItems[4].Text = value + "%";
            }
        }

        public FileListViewItem(JsonObject file, ImageList img, int index, JsonArray wanted, JsonArray priorities)
            : base()
        {
            for (int i = 0; i < 6; i++)
                base.SubItems.Add("");
            string name = (string)file[ProtocolConstants.FIELD_NAME];
            this.FileName = Toolbox.TrimPath(name);
            base.SubItems[0].Tag = name.Length != this.FileName.Length;
            this.FileIndex = index;
            string[] split = this.Name.Split('.');
            if (split.Length > 1)
            {
                this.Extension = split[split.Length - 1].ToLower();
                if (Program.Form.fileIconImageList.Images.ContainsKey(this.Extension) || IconReader.AddToImgList(this.Extension, Program.Form.fileIconImageList))
                {
                    this.TypeName = IconReader.GetTypeName(this.Extension);
                    base.ImageKey = this.Extension;
                }
                else
                    this.TypeName = this.Extension;
            }
            this.FileSize = Toolbox.ToLong(file[ProtocolConstants.FIELD_LENGTH]);
            this.Update(file, wanted, priorities);
        }

        public void Update(JsonObject fileObj, JsonArray wanted, JsonArray priorities)
        {
            this.BytesCompleted = Toolbox.ToLong(fileObj[ProtocolConstants.FIELD_BYTESCOMPLETED]);
            if (wanted != null)
                this.Wanted = Toolbox.ToBool(wanted[this.FileIndex]);
            if (priorities != null)
                this.Priority = Toolbox.ToInt(priorities[this.FileIndex]);
        }

        public string FileName
        {
            get { return base.Name; }
            set
            {
                base.Name = base.Text = base.SubItems[0].Text = base.ToolTipText = value;
            }
        }
    }
}
