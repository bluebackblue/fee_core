

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。ウィンドウ。ベース。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Window_Base
	*/
	public abstract class Window_Base : Fee.Deleter.OnDelete_CallBackInterface
	{
		/** deleter
		*/
		protected Fee.Deleter.Deleter deleter;

		/** 矩形。
		*/
		protected Fee.Geometry.Rect2D_R<int> rect;

		/** layerindex
		*/
		protected int layerindex;

		/** コールバックインターフェイス。
		*/
		private Fee.Ui.OnWindow_CallBackInterface callback_interface;

		/** windowresumeitem
		*/
		private WindowResumeItem windowresumeitem;

		/** [Window_Base]コールバック。削除。
		*/
		protected abstract void OnDelete_FromBase();

		/** [Window_Base]コールバック。レイヤーインデックス変更。
		*/
		protected abstract void OnChangeLayerIndex_FromBase();

		/** [Window_Base]コールバック。矩形変更。
		*/
		protected abstract void OnChangeRect_FromBase();

		/** [Window_Base]コールバック。矩形変更。
		*/
		protected abstract void OnChangeXY_FromBase();

		/** constructor
		*/
		public Window_Base(Fee.Deleter.Deleter a_deleter,Fee.Ui.OnWindow_CallBackInterface a_callback_interface)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//rect
			this.rect.Set(0,0,0,0);

			//layerindex
			this.layerindex = 0;

			//callback_interface
			this.callback_interface = a_callback_interface;

			//windowresumeitem
			this.windowresumeitem = null;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}

			//ウィンドウ登録。
			Fee.Ui.Ui.GetInstance().RegisterWindow(this);

			//ウィンドウを最前面にする。
			Fee.Ui.Ui.GetInstance().SetWindowPriorityTopMost(this);
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			//ウィンドウ解除。
			Fee.Ui.Ui.GetInstance().UnRegisterWindow(this);

			//[Window_Base]コールバック。削除。
			this.OnDelete_FromBase();

			//削除。
			this.deleter.DeleteAll();
		}

		/** 描画プライオリティ。取得。
		*/
		public int GetLayerIndex()
		{
			return this.layerindex;
		}

		/** レイヤーインデックス。変更。
		*/
		public void ChangeLayerIndex(int a_layerindex)
		{
			this.layerindex = a_layerindex;

			//[Window_Base]コールバック。レイヤーインデックス変更。
			this.OnChangeLayerIndex_FromBase();

			//[Fee.Ui.OnDelete_CallBackInterface]レイヤーインデックス変更。
			if(this.callback_interface != null){
				this.callback_interface.OnWindowChangeLayerIndex(this.layerindex);
			}
		}

		/** 矩形。取得。
		*/
		public int GetX()
		{
			return this.rect.x;
		}

		/** 矩形。取得。
		*/
		public int GetY()
		{
			return this.rect.y;
		}

		/** 矩形。取得。
		*/
		public int GetW()
		{
			return this.rect.w;
		}

		/** 矩形。取得。
		*/
		public int GetH()
		{
			return this.rect.h;
		}

		/** ウィンドウレジューム。登録。
		*/
		public void RegisterWindowResume(string a_label,in Fee.Geometry.Rect2D_R<int> t_new_rect)
		{
			this.windowresumeitem = Fee.Ui.Ui.GetInstance().RegisterWindowResume(a_label,in t_new_rect);
		}

		/** ウィンドウレジューム。解除。
		*/
		public void UnRegisterWindowResume(string a_label)
		{
			Fee.Ui.Ui.GetInstance().UnRegisterWindowResume(a_label);
			this.windowresumeitem = null;
		}

		/** ウィンドウレジューム。アンセット。
		*/
		public void UnSetWindowResume()
		{
			this.windowresumeitem = null;
		}

		/** 矩形。設定。
		*/
		public void SetRectFromWindowResumeItem()
		{
			//rect
			this.rect = this.windowresumeitem.rect;

			//rect
			if(this.windowresumeitem != null){
				this.windowresumeitem.rect = this.rect;
			}

			//[Window_Base]コールバック。矩形変更。
			this.OnChangeRect_FromBase();

			//[Fee.Ui.OnDelete_CallBackInterface]矩形変更。
			if(this.callback_interface != null){
				this.callback_interface.OnWindowChangeRect(in this.rect);
			}
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			//rect
			this.rect.Set(a_x,a_y,a_w,a_h);

			//rect
			if(this.windowresumeitem != null){
				this.windowresumeitem.rect = this.rect;
			}

			//[Window_Base]コールバック。矩形変更。
			this.OnChangeRect_FromBase();

			//[Fee.Ui.OnDelete_CallBackInterface]矩形変更。
			if(this.callback_interface != null){
				this.callback_interface.OnWindowChangeRect(in this.rect);
			}
		}

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			//rect
			this.rect = a_rect;

			//rect
			if(this.windowresumeitem != null){
				this.windowresumeitem.rect = this.rect;
			}

			//[Window_Base]コールバック。矩形変更。
			this.OnChangeRect_FromBase();

			//[Fee.Ui.OnDelete_CallBackInterface]矩形変更。
			if(this.callback_interface != null){
				this.callback_interface.OnWindowChangeRect(in this.rect);
			}
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.rect.x = a_x;
			this.rect.y = a_y;

			//rect
			if(this.windowresumeitem != null){
				this.windowresumeitem.rect = this.rect;
			}

			//[Window_Base]コールバック。矩形変更。
			this.OnChangeXY_FromBase();

			//[Fee.Ui.OnDelete_CallBackInterface]矩形変更。
			if(this.callback_interface != null){
				this.callback_interface.OnWindowChangeXY(this.rect.x,this.rect.y);
			}
		}
	}
}

