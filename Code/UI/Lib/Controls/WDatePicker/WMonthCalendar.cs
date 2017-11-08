using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Merculia.UI.Controls
{
	/// <summary>
	/// Summary description for WMonthCalendar.
	/// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class WMonthCalendar : System.Windows.Forms.MonthCalendar
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 
		/// </summary>
		public WMonthCalendar()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

		}

		#region function Dispose

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// WMonthCalendar
			// 
			this.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.WMonthCalendar_DateChanged);

		}
		#endregion


		#region Events handling

		private void WMonthCalendar_DateChanged(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
			this.SetSelectionRange(this.SelectionRange.Start,this.SelectionRange.Start);
		}

		#endregion


		#region function PostMessage

		/// <summary>
		/// 
		/// </summary>
		/// <param name="m"></param>
		public void PostMessage(ref Message m)
		{
			base.WndProc(ref m);
		}

		#endregion


		#region override WndProc

		/// <summary>
		/// 
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{			
		//	if(m.Msg == (int)Msgs.WM_LBUTTONUP){
		//		WDatePickerPopUp frm = (WDatePickerPopUp)this.FindForm();
		//		frm.RaiseSelectionChanged();
		//		frm.Close();
		//		return;
		//	}

		//	if(m.Msg == (int)Msgs.WM_LBUTTONDOWN){
		//		return;
		//	}

		//	if(m.Msg == (int)Msgs.WM_MBUTTONDOWN){
		//		return;
		//	}


			base.WndProc(ref m);
		}

		#endregion
		
	}
}
