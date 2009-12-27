using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jayrock.Json;
using System.Windows.Forms;
using Etier.IconHelper;

namespace TransmissionRemoteDotnet
{
    public class FileItem
    {
        /* this field need a private data, to make it read only from public */
        public int FileIndex
        {
            get;
            set;
        }

        private string _extension;
        public string Extension
        {
            get { return this._extension; }
        }

        public string TypeName
        {
            get;
            set;
        }

        public long FileSize
        {
            get;
            set;
        }

        public long BytesCompleted
        {
            get;
            set;
        }

        private bool _wanted;
        public bool Wanted
        {
            get { return this._wanted; }
        }

        private int _priority;
        public int Priority
        {
            get { return this._priority; }
        }

        public decimal Progress
        {
            get;
            set;
        }


        public ListViewItem UpdateListviewItem(ListViewItem LVI)
        {
            while (LVI.SubItems.Count < 7)
            {
                LVI.SubItems.Add("");
            }
            LVI.SubItems[0].Text = LVI.ToolTipText = LVI.Name = this.FileName;
            LVI.SubItems[1].Text = this.TypeName;
            LVI.SubItems[2].Text = Toolbox.GetFileSize(FileSize);
            LVI.SubItems[3].Text = Toolbox.GetFileSize(BytesCompleted);
            LVI.SubItems[4].Text = this.Progress + "%";
            LVI.SubItems[5].Text = this.Wanted ? OtherStrings.No : OtherStrings.Yes;
            LVI.SubItems[6].Text = Toolbox.FormatPriority(this.Priority);
            LVI.ImageKey = this.Extension;
            return LVI;
        }

        public FileItem(JsonObject file, ImageList img, int index, JsonArray wanted, JsonArray priorities)
        {
            string name = (string)file[ProtocolConstants.FIELD_NAME];
            this.FileName = Toolbox.TrimPath(name);
            this.FileIndex = index;
            string[] split = this.FileName.Split('.');
            if (split.Length > 1)
            {
                this._extension = split[split.Length - 1].ToLower();
                if (Program.Form.fileIconImageList.Images.ContainsKey(this.Extension) || IconReader.AddToImgList(this.Extension, Program.Form.fileIconImageList))
                {
                    this.TypeName = IconReader.GetTypeName(this._extension);
                }
                else
                    this.TypeName = this._extension;
            }
            this.FileSize = Toolbox.ToLong(file[ProtocolConstants.FIELD_LENGTH]);
            this.Update(file, wanted, priorities);
        }

        public void Update(JsonObject fileObj, JsonArray wanted, JsonArray priorities)
        {
            this.BytesCompleted = Toolbox.ToLong(fileObj[ProtocolConstants.FIELD_BYTESCOMPLETED]);
            if (wanted != null)
                this._wanted = Toolbox.ToBool(wanted[this.FileIndex]);
            if (priorities != null)
                this._priority = Toolbox.ToInt(priorities[this.FileIndex]);
        }

        public string FileName
        {
            get;
            set;
        }
    }
}
