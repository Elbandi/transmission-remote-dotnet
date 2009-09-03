/* http://www.codeproject.com/KB/combobox/glistbox.aspx
 * + some of my fixes. */
 
using System.Windows.Forms;
using System.Drawing;
using System;

namespace TransmissionRemoteDotnet
{
    public class GListBox : ListBox
    {
        private ImageList _myImageList;
        public ImageList ImageList
        {
            get { return _myImageList; }
            set { _myImageList = value; }
        }

        public GListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.SetStyle(  
                ControlStyles.OptimizedDoubleBuffer |  
                ControlStyles.ResizeRedraw |  
                ControlStyles.UserPaint,  
                true);  
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Region iRegion = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(new SolidBrush(this.BackColor), iRegion);
            if (this.Items.Count > 0)
            {
                for (int i = 0; i < this.Items.Count; ++i)
                {
                    System.Drawing.Rectangle irect = this.GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(irect))
                    {
                        if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i)
                          || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i))
                          || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Selected, this.ForeColor,
                                this.BackColor));
                        }
                        else
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Default, this.ForeColor,
                                this.BackColor));
                        }
#if !MONO
                        iRegion.Complement(irect);
#endif
                    }
                }
            }
            base.OnPaint(e);
        }

        public GListBoxItem FindItem(string key)
        {
            foreach (object o in Items)
            {
                if (o.GetType().Equals(typeof(GListBoxItem)))
                {
                    GListBoxItem gi = (GListBoxItem)o;
                    if (gi.Text.Equals(key))
                        return gi;
                }
            }
            return null;
        }

        public void RemoveItem(string key)
        {
            object toRemove = null;
            foreach (object o in Items)
            {
                if (o.GetType().Equals(typeof(GListBoxItem)))
                {
                    if (((GListBoxItem)o).Text.Equals(key))
                    {
                        toRemove = o;
                        break;
                    }
                }
            }
            Items.Remove(toRemove);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            if (Items[e.Index].GetType() == typeof(GListBoxItem))
            {
                try
                {
                    Rectangle bounds = e.Bounds;
                    GListBoxItem item = (GListBoxItem)Items[e.Index];
                    if (item.ImageIndex != -1)
                    {
                        Size imageSize = _myImageList.ImageSize;
                        _myImageList.Draw(e.Graphics, bounds.Left, bounds.Top, item.ImageIndex);
                        e.Graphics.DrawString(item.TextWithCounter, e.Font, new SolidBrush(e.ForeColor),
                            bounds.Left + imageSize.Width, bounds.Top);
                    }
                    else
                    {
                        e.Graphics.DrawString(item.TextWithCounter, e.Font, new SolidBrush(e.ForeColor),
                            bounds.Left, bounds.Top);
                    }
                }
                catch
                {
                    DrawStringItem(e);
                }
            }
            else
            {
                DrawStringItem(e);
            }
            base.OnDrawItem(e);
        }

        private void DrawStringItem(DrawItemEventArgs e)
        {
            Rectangle bounds = e.Bounds;
            if (Items.Count > e.Index)
            {
                e.Graphics.DrawString(Items[e.Index].ToString(), e.Font,
                    new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
            }
            else
            {
                e.Graphics.DrawString(Text, e.Font, new SolidBrush(e.ForeColor),
                    bounds.Left, bounds.Top);
            }
        }
    }
}