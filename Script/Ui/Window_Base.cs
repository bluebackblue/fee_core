

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。ウィンドウ。ベース。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Window_Base
	*/
	public abstract class Window_Base : Fee.Deleter.DeleteItem_Base
	{
		/** deleter
		*/
		protected Fee.Deleter.Deleter deleter;

		/** 矩形。
		*/
		protected Fee.Render2D.Rect2D_R<int> rect;

		/** blockitem
		*/
		private Fee.EventPlate.BlockItem blockitem;

		/** layerindex
		*/
		protected int layerindex;

		/** コールバック。
		*/
		private OnWindowCallBack_Base callback;

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
		public Window_Base(Fee.Deleter.Deleter a_deleter,Fee.Ui.OnWindowCallBack_Base a_callback)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//rect
			this.rect.Set(0,0,0,0);

			//blockitem
			this.blockitem = new Fee.EventPlate.BlockItem(this.deleter,0);

			//layerindex
			this.layerindex = 0;

			//callback
			this.callback = a_callback;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}

			//ウィンドウ登録。
			Fee.Ui.Ui.GetInstance().RegisterWindow(this);

			//ウィンドウを最前面にする。
			Fee.Ui.Ui.GetInstance().SetWindowPriorityTopMost(this);
		}

		/** 削除。
		*/
		public void Delete()
		{
			//ウィンドウ解除。
			Fee.Ui.Ui.GetInstance().UnRegisterWindow(this);

			//[Window_Base]コールバック。削除。
			this.OnDelete_FromBase();

			this.deleter.DeleteAll();
		}

		/** レイヤーインデックス。変更。
		*/
		public void ChangeLayerIndex(int a_layerindex)
		{
			this.layerindex = a_layerindex;

			//drawpriority
			long t_drawpriority = this.layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;

			//blockitem
			this.blockitem.SetPriority(t_drawpriority);

			//[Window_Base]コールバック。レイヤーインデックス変更。
			this.OnChangeLayerIndex_FromBase();

			//[Fee.Ui.OnWindowCallBack_Base]レイヤーインデックス変更。
			if(this.callback != null){
				this.callback.OnChangeLayerIndex(this.layerindex);
			}
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.rect.Set(a_x,a_y,a_w,a_h);
			this.blockitem.SetRect(ref this.rect);

			//[Window_Base]コールバック。矩形変更。
			this.OnChangeRect_FromBase();

			//[Fee.Ui.OnWindowCallBack_Base]矩形変更。
			if(this.callback != null){
				this.callback.OnChangeRect(ref this.rect);
			}
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.rect.x = a_x;
			this.rect.y = a_y;

			this.blockitem.SetXY(a_x,a_y);

			//[Window_Base]コールバック。矩形変更。
			this.OnChangeXY_FromBase();

			//[Fee.Ui.OnWindowCallBack_Base]矩形変更。
			if(this.callback != null){
				this.callback.OnChangeXY(this.rect.x,this.rect.y);
			}
		}
	}
}

