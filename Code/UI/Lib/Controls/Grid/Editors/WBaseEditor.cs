using System;
using System.Drawing;
using System.Windows.Forms;

namespace Merculia.UI.Controls.Grid.Editors
{
	/// <summary>
	/// This base class for grid editors, all gird editors must derrive from this class.
	/// </summary>
	public abstract class WBaseEditor : UserControl
	{
		internal protected WGridTableView m_pGridView = null;
		private  object m_Value = null;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public WBaseEditor()
		{              
        }


        #region method PaintDefault

        /// <summary>
		/// Paints default editor. This is called foreach visible cell what isn't active editor.
		/// </summary>
        /// <param name="view"></param>
		/// <param name="g"></param>
		/// <param name="value"></param>
		/// <param name="cellBounds"></param>
		/// <param name="selected"></param>
		/// <param name="column"></param>
		public virtual void PaintDefault(WGridTableView view,Graphics g,Object value,Rectangle cellBounds,bool selected,WGridColumn column)
		{
            ViewStyle m_ViewStyle = view.Grid.ViewStyle;

            Color textColor = selected ?  Color.White : Color.Black;

            if(value != null && value.GetType() == typeof(Decimal)){
                if((decimal)value < 0){
                    textColor = Color.Red;
                }
            }

			if(column.CellTextFormat.Length == 0 && value != null && value.GetType() == typeof(DateTime)){
                if((DateTime)value == DateTime.MinValue){
                    value = "";
                }
                else{
				    value = ((DateTime)value).ToString("dd.MM.yyyy");
                }
			}            
			else if(value != null && value.GetType() == typeof(bool)){
				Rectangle checkRect = new Rectangle(cellBounds.X + (int)(cellBounds.Width - 12) / 2,cellBounds.Y + (int)(cellBounds.Height - 13) / 2,12,12);
											
				// Fill checkrect
				g.FillRectangle(new SolidBrush(Color.White),checkRect);
				
				if((bool)value){
					// Draw check (Lines from up to down).
					g.DrawLine(new Pen(Color.Black),checkRect.X + 3,checkRect.Y + 5,checkRect.X + 3,checkRect.Y + 5 + 2);
					g.DrawLine(new Pen(Color.Black),checkRect.X + 4,checkRect.Y + 6,checkRect.X + 4,checkRect.Y + 6 + 2);
					g.DrawLine(new Pen(Color.Black),checkRect.X + 5,checkRect.Y + 7,checkRect.X + 5,checkRect.Y + 7 + 2);
					g.DrawLine(new Pen(Color.Black),checkRect.X + 6,checkRect.Y + 6,checkRect.X + 6,checkRect.Y + 6 + 2);
					g.DrawLine(new Pen(Color.Black),checkRect.X + 7,checkRect.Y + 5,checkRect.X + 7,checkRect.Y + 5 + 2);
					g.DrawLine(new Pen(Color.Black),checkRect.X + 8,checkRect.Y + 4,checkRect.X + 8,checkRect.Y + 4 + 2);
					g.DrawLine(new Pen(Color.Black),checkRect.X + 9,checkRect.Y + 3,checkRect.X + 9,checkRect.Y + 3 + 2);
				}
				
				// Draw rect around check
				g.DrawRectangle(new Pen(m_ViewStyle.GetBorderColor(false)),checkRect);
				
				value = null; // Force not to draw text 
			}
			else if(value != null && column.CellTextFormat.Length > 0){
                if(value is IFormattable){
                    value = ((IFormattable)value).ToString(column.CellTextFormat,null);
                }
			}
						
			if(value != null){
                Merculia.UI.Controls.Paint.DrawText(g,textColor,m_ViewStyle.GridCellsFont,value.ToString(),cellBounds,column.CellsTextAlign);
			}
        }

        #endregion


        #region method CalculateHeight

        /// <summary>
		/// Calculates height needed for this editor.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="value"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public int CalculateHeight(Graphics g,Object value,WGridColumn column)
		{
			return 19;
        }

        #endregion

        #region method StartEdit

        /// <summary>
		/// 
		/// </summary>
		/// <param name="view"></param>
		/// <param name="value"></param>
		internal protected virtual void StartEdit(WGridTableView view,object value)
		{
			m_pGridView = view;
			this.Value = value;
			this.SelectAll();
			this.Invalidate();
        }

        #endregion

        #region method SelectAll

        /// <summary>
		/// Selects editor value if editor supports it.
		/// </summary>
		public virtual void SelectAll()
		{
        }

        #endregion

        #region method Undo

        /// <summary>
		/// Restores EditValue with orginal value setted by setValue.
		/// </summary>
		public virtual void Undo()
		{
			if(IsModified){
				this.EditValue = m_Value;
			}
			else{
				m_pGridView.UndoRow();
			}
        }

        #endregion


        #region Properties Implementation

        /// <summary>
		/// Gets or sets value.
		/// </summary>
		public object Value
		{
			get{ return m_Value; }

			set{
				m_Value = value;
				this.EditValue = value;
			}
		}

		/// <summary>
		/// Gets or sets value what is currently visible in editor.
		/// </summary>
		public abstract object EditValue
		{
			get;
			set;
		}

		/// <summary>
		/// Gets if value is modified.
		/// </summary>
		public virtual bool IsModified
		{
			get{ 
				if(this.Value.Equals(this.EditValue)){
					return false;
				}
				else{
					return true;
				} 
			}
        }

        #endregion

        #region Events implementation

        /// <summary>
        /// This even is raised when editor value has changed.
        /// </summary>
        public event EventHandler ValueChanged = null;

        #region method OnValueChanged

        /// <summary>
        /// Raised <b>ValueChanged</b> event.
        /// </summary>
        protected void OnValueChanged()
        {
            if(this.ValueChanged != null){
                this.ValueChanged(this,new EventArgs());
            }
        }

        #endregion

        #endregion

    }
}
