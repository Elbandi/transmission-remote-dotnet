using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace System.Windows.Forms
{
	[ToolboxBitmap( typeof( ListBox ) )]
	public class RefreshingListBox : ListBox
	{
		public new void RefreshItem( int index )
		{
			base.RefreshItem( index );
		}

		public new void RefreshItems()
		{
			base.RefreshItems();
		}
	}
}
