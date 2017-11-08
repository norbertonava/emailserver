using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using Merculia.UI.Controls;
using Merculia.UI.Controls.WOutlookBar;

namespace Merculia.UI
{
	#region public class ViewStyle_EventArgs

	/// <summary>
	/// 
	/// </summary>
	public class ViewStyle_EventArgs
	{
		private string m_PropertyName  = "";
		private object m_PropertyValue = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="popertyValue"></param>
		public ViewStyle_EventArgs(string propertyName,object popertyValue)
		{
			m_PropertyName  = propertyName;
			m_PropertyValue = popertyValue;
		}

		#region Properties Implementation

		/// <summary>
		/// 
		/// </summary>
		public string PropertyName
		{
			get{ return m_PropertyName; }
		}

		/// <summary>
		/// 
		/// </summary>
		public object PropertyValue
		{
			get{ return m_PropertyValue; }
		}

		#endregion

	}

	#endregion

	/// <summary>
	/// 
	/// </summary>
	public delegate void ViewStyleChangedEventHandler(object sender,ViewStyle_EventArgs e);


	/// <summary>
	/// Summary description for ViewStyle.
	/// </summary>
	[DesignerSerializer(typeof(WSerializer), typeof(CodeDomSerializer))]
	public class ViewStyle : IWSerializer
	{
		/// <summary>
		/// Occurs when viewStyle has changed.
		/// </summary>
		public event ViewStyleChangedEventHandler StyleChanged = null;
		
		private  static ViewStyle m_ViewStyle = null;

		private Color      m_ControlBackColor         = Color.FromArgb(212,208,200);
		private Color      m_BorderColor              = Color.DarkGray;
		private Color      m_BorderHotColor           = Color.Black;
		private Color      m_ButtonColor              = Color.FromKnownColor(KnownColor.Control);
		private Color      m_ButtonHotColor           = Color.FromArgb(182,193,214);
		private Color      m_ButtonPressedColor       = Color.FromArgb(210,218,232);
		private Color      m_EditColor                = Color.White;
		private Color      m_EditFocusedColor         = Color.Beige;
		private Color      m_EditReadOnlyColor        = Color.FromArgb(228,224,220);
		private Color      m_EditDisabledColor        = Color.Gainsboro;
		private Color      m_FlashColor               = Color.Pink;
		private Color      m_BarColor                 = Color.FromArgb(212,208,200);
		private Color      m_BarHotColor              = Color.FromArgb(182,193,214);
		private Color      m_BarTextColor             = Color.Black;
		private Color      m_BarHotTextColor          = Color.Black;
		private Color      m_BarPressedColor          = Color.FromArgb(210,218,232);
		private Color      m_BarBorderColor           = Color.DarkGray;
		private Color      m_BarHotBorderColor        = Color.Black;
		private Color      m_BarClientAreaColor       = Color.FromArgb(128,128,128);
		private Color      m_BarItemSelectedColor     = Color.Silver;
		private Color      m_BarItemSelectedTextColor = Color.White;
        private Color      m_BarItemDisabledTextColor = Color.LightGray;
//		private Color      m_BarItemSelectedBordrerColor = 
		private Color      m_BarItemHotColor          = Color.FromArgb(182,193,214);
		private Color      m_BarItemPressedColor      = Color.FromArgb(210,218,232);
		private Color      m_BarItemBorderHotColor    = Color.Black;
		private Color      m_BarItemTextColor         = Color.White;
		private Color      m_BarItemHotTextColor      = Color.White;
		private ItemsStyle m_BarItemsStyle            = ItemsStyle.IconSelect;
		private Color      m_TextColor                = Color.Black;
        private Color      m_NegativeValueColor       = Color.Red;
        private Font       m_pGridCellsFont           = new Font("Microsoft Sans Serif",8F,FontStyle.Regular);
		private bool       m_ReadOnly                 = false;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ViewStyle()
		{			
		}

		
		#region method CopyTo

		/// <summary>
		/// Copies instance viewStyle to destination vieStyle.
		/// If destViewStyle is ReadOnly, values isn't copied !
		/// </summary>
		/// <param name="destViewStyle">ViewStyle where to copy.</param>
		public void CopyTo(ViewStyle destViewStyle)
		{
			destViewStyle.BorderColor              = this.BorderColor;
			destViewStyle.BorderHotColor           = this.BorderHotColor;
			destViewStyle.ButtonColor              = this.ButtonColor;
			destViewStyle.ButtonHotColor           = this.ButtonHotColor;
			destViewStyle.ButtonPressedColor       = this.ButtonPressedColor;
			destViewStyle.ControlBackColor         = this.ControlBackColor;
		//	destViewStyle.ControlForeColor         = this.ControlForeColor;
			destViewStyle.EditColor                = this.EditColor;
			destViewStyle.EditFocusedColor         = this.EditFocusedColor;
			destViewStyle.EditReadOnlyColor        = this.EditReadOnlyColor;
		    destViewStyle.EditDisabledColor        = this.EditDisabledColor;
			destViewStyle.BarColor                 = this.BarColor;   
			destViewStyle.BarHotColor              = this.BarHotColor;   
			destViewStyle.BarTextColor             = this.BarTextColor;  
			destViewStyle.BarHotTextColor          = this.BarHotTextColor;  
         	destViewStyle.BarPressedColor          = this.BarPressedColor;     
			destViewStyle.BarBorderColor           = this.BarBorderColor;     
			destViewStyle.BarHotBorderColor        = this.BarHotBorderColor;    
			destViewStyle.BarClientAreaColor       = this.BarClientAreaColor; 
			destViewStyle.BarItemSelectedColor     = this.BarItemSelectedColor;
			destViewStyle.BarItemSelectedTextColor = this.BarItemSelectedTextColor;
            destViewStyle.BarItemDisabledTextColor = this.BarItemDisabledTextColor;
			destViewStyle.BarItemHotColor          = this.BarItemHotColor;     
			destViewStyle.BarItemPressedColor      = this.BarItemPressedColor;  
			destViewStyle.BarItemBorderHotColor    = this.BarItemBorderHotColor;
			destViewStyle.BarItemTextColor         = this.BarItemTextColor; 
			destViewStyle.BarItemHotTextColor      = this.BarItemHotTextColor;
			destViewStyle.BarItemsStyle            = this.BarItemsStyle;
			destViewStyle.TextColor                = this.TextColor;
            destViewStyle.NegativeValueColor       = this.NegativeValueColor;
            destViewStyle.GridCellsFont            = this.GridCellsFont;
			destViewStyle.FlashColor               = this.FlashColor;
		}

		#endregion

		#region method CopyFrom

		/// <summary>
		/// Copies ViewStyle from source ViewStyle.
		/// Copies values even if, ViewStyle is ReadOnly !
		/// </summary>
		/// <param name="sourceViewStyle"></param>
		public void CopyFrom(ViewStyle sourceViewStyle)
		{
			bool readOnly = m_ReadOnly;

			if(m_ReadOnly){
				m_ReadOnly = false;
			}

			this.BorderColor              = sourceViewStyle.BorderColor; 
			this.BorderHotColor           = sourceViewStyle.BorderHotColor; 
			this.ButtonColor              = sourceViewStyle.ButtonColor; 
			this.ButtonHotColor           = sourceViewStyle.ButtonHotColor; 
			this.ButtonPressedColor       = sourceViewStyle.ButtonPressedColor;
			this.ControlBackColor         = sourceViewStyle.ControlBackColor; 
		//	this.ControlForeColor         = sourceViewStyle.ControlForeColor; 
			this.EditColor                = sourceViewStyle.EditColor; 
			this.EditFocusedColor         = sourceViewStyle.EditFocusedColor;
			this.EditReadOnlyColor        = sourceViewStyle.EditReadOnlyColor;
		    this.EditDisabledColor        = sourceViewStyle.EditDisabledColor;
			this.BarColor                 = sourceViewStyle.BarColor;
			this.BarHotColor              = sourceViewStyle.BarHotColor;
			this.BarTextColor             = sourceViewStyle.BarTextColor;
			this.BarHotTextColor          = sourceViewStyle.BarHotTextColor;
			this.BarPressedColor          = sourceViewStyle.BarPressedColor;
			this.BarBorderColor           = sourceViewStyle.BarBorderColor;
			this.BarHotBorderColor        = sourceViewStyle.BarHotBorderColor;
			this.BarClientAreaColor       = sourceViewStyle.BarClientAreaColor;
			this.BarItemSelectedColor     = sourceViewStyle.BarItemSelectedColor;
			this.BarItemSelectedTextColor = sourceViewStyle.BarItemSelectedTextColor;
            this.BarItemDisabledTextColor = sourceViewStyle.BarItemDisabledTextColor;
			this.BarItemHotColor          = sourceViewStyle.BarItemHotColor; 
			this.BarItemPressedColor      = sourceViewStyle.BarItemPressedColor;
			this.BarItemBorderHotColor    = sourceViewStyle.BarItemBorderHotColor;
			this.BarItemTextColor         = sourceViewStyle.BarItemTextColor;
			this.BarItemHotTextColor      = sourceViewStyle.BarItemHotTextColor;
			this.BarItemsStyle            = sourceViewStyle.BarItemsStyle;
			this.TextColor                = sourceViewStyle.TextColor;
            this.NegativeValueColor       = sourceViewStyle.NegativeValueColor;
            this.GridCellsFont            = sourceViewStyle.GridCellsFont;
			this.FlashColor               = sourceViewStyle.FlashColor; 	
		
			m_ReadOnly = readOnly;
		}

		#endregion


		#region function GetEditColor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="readOnly"></param>
		/// <param name="enabled"></param>
		/// <returns></returns>
		public Color GetEditColor(bool readOnly,bool enabled)
		{
			return GetEditColor(readOnly,enabled,false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="readOnly"></param>
		/// <param name="enabled"></param>
		/// <param name="focused"></param>
		/// <returns></returns>
		public Color GetEditColor(bool readOnly,bool enabled,bool focused)
		{
			// Normal edit color wanted
			if(!readOnly && enabled){
				if(focused){
					return this.EditFocusedColor;
				}
				return this.EditColor;
			}
			else{
				// ReadOnly edit color wanted
				if(readOnly && enabled){
					return this.EditReadOnlyColor;
				}
				else{ // Disabled edit color wanted
					return this.EditDisabledColor;
				}
			}
		}

		#endregion

		#region function GetBorderColor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hot"></param>
		/// <returns></returns>
		public Color GetBorderColor(bool hot)
		{
			if(hot){
				return m_BorderHotColor;
			}
			else{
				return m_BorderColor;
			}
		}

		#endregion

		#region function GetButtonColor

		/// <summary>
		/// Gets button color from current viewStyle settings.
		/// </summary>
		/// <param name="hot">True if hot color wanted.</param>
		/// <param name="pressed">True if button pressed color wanted.</param>
		/// <returns>Returns requested button color from current viewStyle settings.</returns>
		public Color GetButtonColor(bool hot,bool pressed)
		{
			if(hot){
				if(pressed){
					return m_ViewStyle.ButtonPressedColor;
				}
				else{
					return m_ViewStyle.ButtonHotColor;
				}
			}
			else{
				return m_ViewStyle.ButtonColor;
			}
		}

		#endregion

		
		#region method RestoreDefault

		/// <summary>
		/// Restores default viewStyle settings.
		/// </summary>
		public void RestoreDefault()
		{
			this.BorderColor              = Color.DarkGray;     
			this.BorderHotColor           = Color.Black;
			this.ButtonColor              = Color.FromArgb(212,208,200);
			this.ButtonHotColor           = Color.FromArgb(182,193,214);
			this.ButtonPressedColor       = Color.FromArgb(210,218,232);
			this.ControlBackColor         = Color.FromKnownColor(KnownColor.Control);
	//		this.ControlForeColor         =;
			this.EditColor                = Color.White;
			this.EditFocusedColor         = Color.Beige;
			this.EditReadOnlyColor        = Color.FromArgb(228,224,220);
			this.EditDisabledColor        = Color.Gainsboro;
			this.FlashColor               = Color.Pink;
			this.BarColor                 = Color.FromArgb(212,208,200);
			this.BarHotColor              = Color.FromArgb(182,193,214);
			this.BarTextColor             = Color.Black;
			this.BarHotTextColor          = Color.Black;
			this.BarPressedColor          = Color.FromArgb(210,218,232);
			this.BarBorderColor           = Color.DarkGray;
			this.BarHotBorderColor        = Color.Black;
			this.BarClientAreaColor       = Color.FromArgb(128,128,128);
			this.BarItemSelectedColor     = Color.Silver;
			this.BarItemSelectedTextColor = Color.White;
            this.BarItemDisabledTextColor = Color.LightGray;
		//	this.BarItemSelectedBordrerColor = 
			this.BarItemHotColor          = Color.FromArgb(182,193,214);
			this.BarItemPressedColor      = Color.FromArgb(210,218,232);
			this.BarItemBorderHotColor    = Color.Black;
			this.BarItemTextColor         = Color.White;
			this.BarItemHotTextColor      = Color.White;
			this.BarItemsStyle            = ItemsStyle.IconSelect;
            this.GridCellsFont            = new Font("Microsoft Sans Serif",8F,FontStyle.Regular);
            this.TextColor                = Color.Black;
		}

		#endregion

		#region method SaveToXml

		/// <summary>
		/// Saves current viewStyle to xml.
		/// </summary>
		/// <returns>Returns byte xml data.</returns>
		public byte[] SaveToXml()
		{
			byte[] retVal = null;

			DataSet ds = new DataSet("dsViewStyle");
			DataTable dt = ds.Tables.Add("ViewStyle");

			dt.Columns.Add("BorderColor",Type.GetType("System.String"));
			dt.Columns.Add("BorderHotColor",Type.GetType("System.String"));
			dt.Columns.Add("ButtonColor",Type.GetType("System.String"));
			dt.Columns.Add("ButtonHotColor",Type.GetType("System.String"));
			dt.Columns.Add("ButtonPressedColor",Type.GetType("System.String"));
			dt.Columns.Add("ControlBackColor",Type.GetType("System.String"));
			dt.Columns.Add("ControlForeColor",Type.GetType("System.String"));
			dt.Columns.Add("EditColor",Type.GetType("System.String"));
			dt.Columns.Add("EditFocusedColor",Type.GetType("System.String"));
			dt.Columns.Add("EditReadOnlyColor",Type.GetType("System.String"));
			//
			dt.Columns.Add("FlashColor",Type.GetType("System.String"));
			dt.Columns.Add("BarColor",Type.GetType("System.String"));
			dt.Columns.Add("BarHotColor",Type.GetType("System.String"));
			dt.Columns.Add("BarTextColor",Type.GetType("System.String"));
			dt.Columns.Add("BarHotTextColor",Type.GetType("System.String"));
			dt.Columns.Add("BarPressedColor",Type.GetType("System.String"));
			dt.Columns.Add("BarBorderColor",Type.GetType("System.String"));
			dt.Columns.Add("BarHotBorderColor",Type.GetType("System.String"));
			dt.Columns.Add("BarClientAreaColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemSelectedColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemSelectedTextColor",Type.GetType("System.String"));
            dt.Columns.Add("BarItemDisabledTextColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemSelectedBorder",Type.GetType("System.String"));
			dt.Columns.Add("BarItemHotColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemPressedColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemBorderHotColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemTextColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemHotTextColor",Type.GetType("System.String"));
			dt.Columns.Add("BarItemsStyle",Type.GetType("System.String"));
			dt.Columns.Add("TextColor",Type.GetType("System.String"));
			dt.Columns.Add("Text3DColor",Type.GetType("System.String"));
			dt.Columns.Add("TextShadowColor",Type.GetType("System.String"));
		//	dt.Columns.Add("TextStyle",Type.GetType("System.String"));
            dt.Columns.Add("NegativeValueColor",Type.GetType("System.String"));
            dt.Columns.Add("GridCellsFont",Type.GetType("System.String"));
		

			DataRow dr = dt.NewRow();
			dr["BorderColor"]              = this.BorderColor.ToArgb();
			dr["BorderHotColor"]           = this.BorderHotColor.ToArgb();
			dr["ButtonColor"]              = this.ButtonColor.ToArgb();
			dr["ButtonHotColor"]           = this.ButtonHotColor.ToArgb();
			dr["ButtonPressedColor"]       = this.ButtonPressedColor.ToArgb();
			dr["ControlBackColor"]         = this.ControlBackColor.ToArgb();
			dr["ControlForeColor"]         = this.ControlForeColor.ToArgb();
			dr["EditColor"]                = this.EditColor.ToArgb();
			dr["EditFocusedColor"]         = this.EditFocusedColor.ToArgb();
			dr["EditReadOnlyColor"]        = this.EditReadOnlyColor.ToArgb();
			//
			dr["FlashColor"]               = this.FlashColor.ToArgb();
			dr["BarColor"]                 = this.BarColor.ToArgb();
			dr["BarHotColor"]              = this.BarHotColor.ToArgb();
			dr["BarTextColor"]             = this.BarTextColor.ToArgb();
			dr["BarHotTextColor"]          = this.BarHotTextColor.ToArgb();
			dr["BarPressedColor"]          = this.BarPressedColor.ToArgb();
			dr["BarBorderColor"]           = this.BarBorderColor.ToArgb();
			dr["BarHotBorderColor"]        = this.BarHotBorderColor.ToArgb();
			dr["BarClientAreaColor"]       = this.BarClientAreaColor.ToArgb();
			dr["BarItemSelectedColor"]     = this.BarItemSelectedColor.ToArgb();
			dr["BarItemSelectedTextColor"] = this.BarItemSelectedTextColor.ToArgb();
            dr["BarItemDisabledTextColor"] = this.BarItemDisabledTextColor.ToArgb();
	//		dr["BarItemSelectedBorder"]    = this.BarItemSelectedBorder.ToArgb();
			dr["BarItemHotColor"]          = this.BarItemHotColor.ToArgb();
			dr["BarItemPressedColor"]      = this.BarItemPressedColor.ToArgb();
			dr["BarItemBorderHotColor"]    = this.BarItemBorderHotColor.ToArgb();
			dr["BarItemTextColor"]         = this.BarItemTextColor.ToArgb();
			dr["BarItemHotTextColor"]      = this.BarItemHotTextColor.ToArgb();
			dr["BarItemsStyle"]            = Convert.ToInt32(this.BarItemsStyle);
			dr["FlashColor"]               = this.FlashColor.ToArgb();
			dr["TextColor"]                = this.TextColor.ToArgb();
            dr["NegativeValueColor"]       = this.NegativeValueColor.ToArgb();
		//	dr["TextStyle"]                = this.TextStyle;
            //
            dr["GridCellsFont"]            = UI_Utils.FontToString(this.GridCellsFont);

			dt.Rows.Add(dr);

			MemoryStream strm = new MemoryStream();
			ds.WriteXml(strm,XmlWriteMode.IgnoreSchema);
			retVal = strm.ToArray();
			strm.Close();

			ds.Dispose();

			return retVal;
		}

		#endregion

		#region method LoadFromXml

		/// <summary>
		/// Loads viewStyle from xml data.
		/// </summary>
		/// <param name="viewStyleXML">ViewStyle data.(This must be result of SaveToXml())</param>
		public void LoadFromXml(byte[] viewStyleXML)
		{
			try{
				MemoryStream strm = new MemoryStream(viewStyleXML);
				DataSet ds = new DataSet();
				ds.ReadXml(strm);
				strm.Close();

				DataRow dr = ds.Tables["ViewStyle"].Rows[0];

                if(dr.Table.Columns.Contains("BorderColor")){
				    this.BorderColor = Color.FromArgb(Convert.ToInt32(dr["BorderColor"]));
                }
                if(dr.Table.Columns.Contains("BorderHotColor")){
				    this.BorderHotColor = Color.FromArgb(Convert.ToInt32(dr["BorderHotColor"]));
                }
                if(dr.Table.Columns.Contains("ButtonColor")){
				    this.ButtonColor = Color.FromArgb(Convert.ToInt32(dr["ButtonColor"]));
                }
                if(dr.Table.Columns.Contains("ButtonHotColor")){
				    this.ButtonHotColor = Color.FromArgb(Convert.ToInt32(dr["ButtonHotColor"]));
                }
                if(dr.Table.Columns.Contains("ButtonPressedColor")){
				    this.ButtonPressedColor = Color.FromArgb(Convert.ToInt32(dr["ButtonPressedColor"]));
                }
                if(dr.Table.Columns.Contains("ControlBackColor")){
				    this.ControlBackColor = Color.FromArgb(Convert.ToInt32(dr["ControlBackColor"]));
                }
                if(dr.Table.Columns.Contains("EditColor")){			
				    this.EditColor = Color.FromArgb(Convert.ToInt32(dr["EditColor"]));
                }
                if(dr.Table.Columns.Contains("EditFocusedColor")){
				    this.EditFocusedColor = Color.FromArgb(Convert.ToInt32(dr["EditFocusedColor"]));
                }
                if(dr.Table.Columns.Contains("EditReadOnlyColor")){
				    this.EditReadOnlyColor = Color.FromArgb(Convert.ToInt32(dr["EditReadOnlyColor"]));
                }

                if(dr.Table.Columns.Contains("BarColor")){				
				    this.BarColor = Color.FromArgb(Convert.ToInt32(dr["BarColor"]));
                }
                if(dr.Table.Columns.Contains("BarHotColor")){
				    this.BarHotColor = Color.FromArgb(Convert.ToInt32(dr["BarHotColor"]));
                }
                if(dr.Table.Columns.Contains("BarTextColor")){
				    this.BarTextColor = Color.FromArgb(Convert.ToInt32(dr["BarTextColor"]));
                }
                if(dr.Table.Columns.Contains("BarHotTextColor")){
				    this.BarHotTextColor = Color.FromArgb(Convert.ToInt32(dr["BarHotTextColor"]));
                }
                if(dr.Table.Columns.Contains("BarPressedColor")){
				    this.BarPressedColor = Color.FromArgb(Convert.ToInt32(dr["BarPressedColor"]));
                }
                if(dr.Table.Columns.Contains("BarBorderColor")){
				    this.BarBorderColor = Color.FromArgb(Convert.ToInt32(dr["BarBorderColor"]));
                }
                if(dr.Table.Columns.Contains("BarHotBorderColor")){
				    this.BarHotBorderColor = Color.FromArgb(Convert.ToInt32(dr["BarHotBorderColor"]));
                }
                if(dr.Table.Columns.Contains("BarClientAreaColor")){
				    this.BarClientAreaColor = Color.FromArgb(Convert.ToInt32(dr["BarClientAreaColor"]));
                }
                if(dr.Table.Columns.Contains("BarItemSelectedColor")){
				    this.BarItemSelectedColor = Color.FromArgb(Convert.ToInt32(dr["BarItemSelectedColor"]));
                }
                if(dr.Table.Columns.Contains("BarItemSelectedTextColor")){
				    this.BarItemSelectedTextColor = Color.FromArgb(Convert.ToInt32(dr["BarItemSelectedTextColor"]));
                }
                if(dr.Table.Columns.Contains("BarItemDisabledTextColor")){
                    this.BarItemDisabledTextColor = Color.FromArgb(Convert.ToInt32(dr["BarItemDisabledTextColor"]));
                }
                if(dr.Table.Columns.Contains("BarItemHotColor")){
				    this.BarItemHotColor = Color.FromArgb(Convert.ToInt32(dr["BarItemHotColor"]));
                }
                if(dr.Table.Columns.Contains("BarItemPressedColor")){
				    this.BarItemPressedColor = Color.FromArgb(Convert.ToInt32(dr["BarItemPressedColor"]));
                }
                if(dr.Table.Columns.Contains("BarItemBorderHotColor")){
				    this.BarItemBorderHotColor = Color.FromArgb(Convert.ToInt32(dr["BarItemBorderHotColor"]));
                }
                if(dr.Table.Columns.Contains("BarItemTextColor")){
				    this.BarItemTextColor = Color.FromArgb(Convert.ToInt32(dr["BarItemTextColor"]));
                }
                if(dr.Table.Columns.Contains("BarItemHotTextColor")){
				    this.BarItemHotTextColor = Color.FromArgb(Convert.ToInt32(dr["BarItemHotTextColor"]));
                }
                if(dr.Table.Columns.Contains("BarItemsStyle")){
				    this.BarItemsStyle = (ItemsStyle)Convert.ToInt32(dr["BarItemsStyle"]);
                }
                if(dr.Table.Columns.Contains("FlashColor")){
				    this.FlashColor = Color.FromArgb(Convert.ToInt32(dr["FlashColor"]));
                }
                if(dr.Table.Columns.Contains("TextColor")){
				    this.TextColor = Color.FromArgb(Convert.ToInt32(dr["TextColor"]));
                }
                if(dr.Table.Columns.Contains("NegativeValueColor")){
				    this.NegativeValueColor = Color.FromArgb(Convert.ToInt32(dr["NegativeValueColor"]));
                }
                
                if(dr.Table.Columns.Contains("GridCellsFont")){
				    this.GridCellsFont = UI_Utils.ParseFont(dr["GridCellsFont"].ToString());
                }

				ds.Dispose();
			}
			catch(Exception x){
				MessageBox.Show(x.Message);
				RestoreDefault();
			}
		}

		#endregion


		#region function ShouldSerialize

		/// <summary>
		/// Checks if specified property must be serialized.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		/// <returns></returns>
		public bool ShouldSerialize(string propertyName)
		{
			bool retVal = false;

			switch(propertyName)
			{
				case "BorderColor":
					if(ViewStyle.staticViewStyle.BorderColor != this.BorderColor){
						retVal = true;
					}
					break;
				case "BorderHotColor":
					if(ViewStyle.staticViewStyle.BorderHotColor != this.BorderHotColor){
						retVal = true;
					}
					break;
				case "ButtonColor":
					if(ViewStyle.staticViewStyle.ButtonColor != this.ButtonColor){
						retVal = true;
					}
					break;
				case "ButtonHotColor":
					if(ViewStyle.staticViewStyle.ButtonHotColor != this.ButtonHotColor){
						retVal = true;
					}
					break;
				case "ButtonPressedColor":
					if(ViewStyle.staticViewStyle.ButtonPressedColor != this.ButtonPressedColor){
						retVal = true;
					}
					break;
				case "ControlBackColor":
					if(ViewStyle.staticViewStyle.ControlBackColor != this.ControlBackColor){
						retVal = true;
					}
					break;
				case "ControlForeColor":
					if(ViewStyle.staticViewStyle.ControlForeColor != this.ControlForeColor){
						retVal = true;
					}
					break;
				case "EditColor":
					if(ViewStyle.staticViewStyle.EditColor != this.EditColor){
						retVal = true;
					}
					break;
				case "EditFocusedColor":
					if(ViewStyle.staticViewStyle.EditFocusedColor != this.EditFocusedColor){
						retVal = true;
					}
					break;
				case "EditReadOnlyColor":
					if(ViewStyle.staticViewStyle.EditReadOnlyColor != this.EditReadOnlyColor){
						retVal = true;
					}
					break;
				case "EditDisabledColor":
					if(ViewStyle.staticViewStyle.EditDisabledColor != this.EditDisabledColor){
						retVal = true;
					}
					break;
				case "BarColor":
					if(ViewStyle.staticViewStyle.BarColor != this.BarColor){
						retVal = true;
					}   
					break;
				case "BarHotColor":
					if(ViewStyle.staticViewStyle.BarHotColor != this.BarHotColor){
						retVal = true;
					}   
					break;
				case "BarTextColor":
					if(ViewStyle.staticViewStyle.BarTextColor != this.BarTextColor){
						retVal = true;
					}  
					break;
				case "BarHotTextColor":
					if(ViewStyle.staticViewStyle.BarHotTextColor != this.BarHotTextColor){
						retVal = true;
					} 
					break;
				case "BarPressedColor":
					if(ViewStyle.staticViewStyle.BarPressedColor != this.BarPressedColor){
						retVal = true;
					}     
					break;
				case "BarBorderColor":
					if(ViewStyle.staticViewStyle.BarBorderColor != this.BarBorderColor){
						retVal = true;
					}    
					break;
				case "BarHotBorderColor":
					if(ViewStyle.staticViewStyle.BarHotBorderColor != this.BarHotBorderColor){
						retVal = true;
					}    
					break;
				case "BarClientAreaColor":
					if(ViewStyle.staticViewStyle.BarClientAreaColor != this.BarClientAreaColor){
						retVal = true;
					} 
					break;
				case "BarItemSelectedColor":
					if(ViewStyle.staticViewStyle.BarItemSelectedColor != this.BarItemSelectedColor){
						retVal = true;
					}
					break;
				case "BarItemSelectedTextColor":
					if(ViewStyle.staticViewStyle.BarItemSelectedTextColor != this.BarItemSelectedTextColor){
						retVal = true;
					}
					break;
				case "BarItemHotColor":
					if(ViewStyle.staticViewStyle.BarItemHotColor != this.BarItemHotColor){
						retVal = true;
					}     
					break;
				case "BarItemPressedColor":
					if(ViewStyle.staticViewStyle.BarItemPressedColor != this.BarItemPressedColor){
						retVal = true;
					}  
					break;
				case "BarItemBorderHotColor":
					if(ViewStyle.staticViewStyle.BarItemBorderHotColor != this.BarItemBorderHotColor){
						retVal = true;
					}
					break;
				case "BarItemTextColor":
					if(ViewStyle.staticViewStyle.BarItemTextColor != this.BarItemTextColor){
						retVal = true;
					}
					break;
				case "BarItemHotTextColor":
					if(ViewStyle.staticViewStyle.BarItemHotTextColor != this.BarItemHotTextColor){
						retVal = true;
					}
					break;
				case "BarItemsStyle":
					if(ViewStyle.staticViewStyle.BarItemsStyle != this.BarItemsStyle){
						retVal = true;
					}
					break;
				case "FlashColor":
					if(ViewStyle.staticViewStyle.FlashColor != this.FlashColor){
						retVal = true;
					}
					break;
				case "TextColor":
					if(ViewStyle.staticViewStyle.TextColor != this.TextColor){
						retVal = true;
					}
					break;
			}

			return retVal;
		}

		#endregion


		#region Properties Implementation

		#region Border stuff

		/// <summary>
		/// Gets or sets BorderColor.
		/// </summary>
		public Color BorderColor
		{
			get{ return m_BorderColor; }

			set{ 
				if(!m_ReadOnly && m_BorderColor != value){
					m_BorderColor = value;
					OnStyleChanged("BorderColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BorderHotColor.
		/// </summary>
		public Color BorderHotColor
		{
			get{ return m_BorderHotColor; }

			set{ 
				if(!m_ReadOnly && m_BorderHotColor != value){
					m_BorderHotColor = value; 
					OnStyleChanged("BorderHotColor",value);
				}
			}
		}

		#endregion
		
		#region Button stuff

		/// <summary>
		/// Gets or sets ButtonColor.
		/// </summary>
		public Color ButtonColor
		{
			get{ return m_ButtonColor; }

			set{ 
				if(!m_ReadOnly && m_ButtonColor != value){
					m_ButtonColor = value; 
					OnStyleChanged("ButtonColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets ButtonHotColor.
		/// </summary>
		public Color ButtonHotColor
		{
			get{ return m_ButtonHotColor; }

			set{ 
				if(!m_ReadOnly && m_ButtonHotColor != value){
					m_ButtonHotColor = value; 
					OnStyleChanged("ButtonHotColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets ButtonPressedColor.
		/// </summary>
		public Color ButtonPressedColor
		{
			get{ return m_ButtonPressedColor; }

			set{ 
				if(!m_ReadOnly && m_ButtonPressedColor != value){
					m_ButtonPressedColor = value; 
					OnStyleChanged("ButtonPressedColor",value);
				}
			}
		}

		#endregion

		#region Control stuff

		/// <summary>
		/// Gets or sets ControlBackColor.
		/// </summary>
		public Color ControlBackColor
		{
			get{ return m_ControlBackColor; }

			set{
				if(!m_ReadOnly && m_ControlBackColor != value){
					m_ControlBackColor = value; 
					OnStyleChanged("ControlBackColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets ControlForeColor.
		/// </summary>
		public Color ControlForeColor
		{
			get{ return Color.AliceBlue; }
		}

		/// <summary>
		/// Gets or sets FlashColor.
		/// </summary>
		public Color FlashColor
		{
			get{ return m_FlashColor; }

			set{ 
				if(!m_ReadOnly && m_FlashColor != value){
					m_FlashColor = value; 
					OnStyleChanged("FlashColor",value);
				}
			}
		}

		#endregion

		#region Edit stuff

		/// <summary>
		/// Gets or sets EditColor.
		/// </summary>
		public Color EditColor
		{
			get{ return m_EditColor; }

			set{ 
				if(!m_ReadOnly && m_EditColor != value){
					m_EditColor = value; 
					OnStyleChanged("EditColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets EditFocusedColor.
		/// </summary>
		public Color EditFocusedColor
		{
			get{ return m_EditFocusedColor; }

			set{ 
				if(!m_ReadOnly && m_EditFocusedColor != value){
					m_EditFocusedColor = value; 
					OnStyleChanged("EditFocusedColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets EditReadOnlyColor.
		/// </summary>
		public Color EditReadOnlyColor
		{
			get{ return m_EditReadOnlyColor; }

			set{ 
				if(!m_ReadOnly && m_EditReadOnlyColor != value){
					m_EditReadOnlyColor = value; 
					OnStyleChanged("EditReadOnlyColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets EditDisabledColor.
		/// </summary>
		public Color EditDisabledColor
		{
			get{ return m_EditDisabledColor; }

			set{ 
				if(!m_ReadOnly && m_EditDisabledColor != value){
					m_EditDisabledColor = value; 
					OnStyleChanged("EditDisabledColor",value);
				}
			}
		}

		#endregion

		#region Bar stuff

		/// <summary>
		/// Gets or sets BarColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarColor
		{
			get{ return m_BarColor; }

			set{ 
				if(!m_ReadOnly && m_BarColor != value){
					m_BarColor = value; 
					OnStyleChanged("BarColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarHotColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarHotColor
		{
			get{ return m_BarHotColor; }

			set{ 
				if(!m_ReadOnly && m_BarHotColor != value){
					m_BarHotColor = value; 
					OnStyleChanged("BarHotColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarTextColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarTextColor
		{
			get{ return m_BarTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarTextColor != value){
					m_BarTextColor = value; 
					OnStyleChanged("BarTextColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarHotTextColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarHotTextColor
		{
			get{ return m_BarHotTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarHotTextColor != value){
					m_BarHotTextColor = value; 
					OnStyleChanged("BarHotTextColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarPressedColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarPressedColor
		{
			get{ return m_BarPressedColor; }

			set{ 
				if(!m_ReadOnly && m_BarPressedColor != value){
					m_BarPressedColor = value; 
					OnStyleChanged("BarPressedColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarBorderColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarBorderColor
		{
			get{ return m_BarBorderColor; }

			set{ 
				if(!m_ReadOnly && m_BarBorderColor != value){
					m_BarBorderColor = value; 
					OnStyleChanged("BarBorderColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarHotBorderColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarHotBorderColor
		{
			get{ return m_BarHotBorderColor; }

			set{ 
				if(!m_ReadOnly && m_BarHotBorderColor != value){
					m_BarHotBorderColor = value; 
					OnStyleChanged("BarHotBorderColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarClientAreaColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarClientAreaColor
		{
			get{ return m_BarClientAreaColor; }

			set{ 
				if(!m_ReadOnly && m_BarClientAreaColor != value){
					m_BarClientAreaColor = value; 
					OnStyleChanged("BarClientAreaColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarItemSelectedColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarItemSelectedColor
		{
			get{ return m_BarItemSelectedColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemSelectedColor != value){
					m_BarItemSelectedColor = value; 
					OnStyleChanged("BarItemSelectedColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarItemSelectedTextColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarItemSelectedTextColor
		{
			get{ return m_BarItemSelectedTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemSelectedTextColor != value){
					m_BarItemSelectedTextColor = value; 
					OnStyleChanged("BarItemSelectedTextColor",value);
				}
			}
		}

        /// <summary>
		/// Gets or sets BarItemDisabledTextColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarItemDisabledTextColor
		{
			get{ return m_BarItemDisabledTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemDisabledTextColor != value){
					m_BarItemDisabledTextColor = value; 
					OnStyleChanged("BarItemDisabledTextColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarItemHotColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarItemHotColor
		{
			get{ return m_BarItemHotColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemHotColor != value){
					m_BarItemHotColor = value; 
					OnStyleChanged("BarItemHotColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarItemPressedColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarItemPressedColor
		{
			get{ return m_BarItemPressedColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemPressedColor != value){
					m_BarItemPressedColor = value; 
					OnStyleChanged("BarItemPressedColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarItemBorderHotColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarItemBorderHotColor
		{
			get{ return m_BarItemBorderHotColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemBorderHotColor != value){
					m_BarItemBorderHotColor = value; 
					OnStyleChanged("BarItemBorderHotColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarItemTextColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarItemTextColor
		{
			get{ return m_BarItemTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemTextColor != value){
					m_BarItemTextColor = value; 
					OnStyleChanged("BarItemTextColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarItemHotTextColor.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public Color BarItemHotTextColor
		{
			get{ return m_BarItemHotTextColor; }

			set{ 
				if(!m_ReadOnly && m_BarItemHotTextColor != value){
					m_BarItemHotTextColor = value; 
					OnStyleChanged("BarItemHotTextColor",value);
				}
			}
		}

		/// <summary>
		/// Gets or sets BarItemsStyle.
		/// </summary>
		[
		Category("OutlookBar"),
		]
		public ItemsStyle BarItemsStyle
		{
			get{ return m_BarItemsStyle; }

			set{ 
				if(!m_ReadOnly && m_BarItemsStyle != value){
					m_BarItemsStyle = value; 
					OnStyleChanged("BarItemsStyle",value);
				}
			}
		}

		#endregion

		#region Text stuff

		/// <summary>
		/// Gets or sets TextColor.
		/// </summary>
		[
		Category("Text"),
		]
		public Color TextColor
		{
			get{ return m_TextColor; }

			set{
				if(!m_ReadOnly && m_TextColor != value){
					m_TextColor = value;
					OnStyleChanged("TextColor",value);
				}
			}
		}	
	
        /// <summary>
		/// Gets or sets negative value color.
		/// </summary>
		public Color NegativeValueColor
		{
			get{ return m_NegativeValueColor; }

			set{
				if(!m_ReadOnly && m_NegativeValueColor != value){
					m_NegativeValueColor = value;
					OnStyleChanged("NegativeValueColor",value);
				}
			}
		}

		#endregion
        
		#region View stuff (this)

		/// <summary>
		/// 
		/// </summary>
		[
		Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public bool ReadOnly
		{
			get{ return m_ReadOnly; }

			set{ m_ReadOnly = value; }
		}

		#endregion

        #region Grid

        /// <summary>
        /// Gets or sets grid cells font.
        /// </summary>
        [Category("Grid")]
        public Font GridCellsFont
        {
            get{ return m_pGridCellsFont; }

            set{
                if(!m_ReadOnly && m_pGridCellsFont != value){
					m_pGridCellsFont = value;
					OnStyleChanged("GridCellsFont",value);
				}
            }
        }

        #endregion

        #endregion

        #region Events Implementation

        /// <summary>
		/// 
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="popertyValue"></param>
		protected void OnStyleChanged(string propertyName,object popertyValue)
		{
			// Raises the event; 	
			ViewStyle_EventArgs oArg = new ViewStyle_EventArgs(propertyName,popertyValue);

			if(this.StyleChanged != null){
				this.StyleChanged(this, oArg);
			}	
		}

		#endregion


		//------ Static functions / properties ----------//
		
		#region Static Properties

		/// <summary>
		/// Gets static viewStyle.
		/// </summary>
		public static ViewStyle staticViewStyle
		{
			get{
				if(m_ViewStyle == null){
					m_ViewStyle = new ViewStyle();
				}
				
				return m_ViewStyle;				
			}
		}

		#endregion

		//-----------------------------------------------//

	}
}
