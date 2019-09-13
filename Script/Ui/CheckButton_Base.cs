

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。チェックボタン。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** CheckButton_Base
	*/
	public abstract class CheckButton_Base : Fee.Deleter.OnDelete_CallBackInterface , Fee.EventPlate.OnEventPlateOver_CallBackInterface<int> , Fee.Ui.OnTarget_CallBackInterface
	{
		/** deleter
		*/
		protected Fee.Deleter.Deleter deleter;

		/** 矩形。
		*/
		protected Fee.Geometry.Rect2D_R<int> rect;

		/** drawpriority
		*/
		protected long drawpriority;

		/** eventplate
		*/
		protected Fee.EventPlate.Item eventplate;

		/** callbackparam_changecheck
		*/
		protected Fee.Ui.OnCheckButtonChangekCheck_CallBackParam callbackparam_changecheck;

		/** is_onover
		*/
		protected bool is_onover;

		/** lock_flag
		*/
		protected bool lock_flag;

		/** clip_flag
		*/
		protected bool clip_flag;

		/** clip_rect
		*/
		protected Fee.Geometry.Rect2D_R<int> clip_rect;

		/** visible_flag
		*/
		protected bool visible_flag;

		/** mode
		*/
		protected CheckButton_Mode mode;

		/** check_flag
		*/
		protected bool check_flag;

		/** constructor
		*/
		public CheckButton_Base(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//rect
			this.rect.Set(0,0,0,0);

			//drawpriority
			this.drawpriority = a_drawpriority;

			//eventplate
			this.eventplate = new Fee.EventPlate.Item(this.deleter,Fee.EventPlate.EventType.Button,this.drawpriority);
			this.eventplate.SetOnEventPlateOver(this,-1);

			//callbackparam_changecheck
			this.callbackparam_changecheck = null;

			//is_onover
			this.is_onover = false;

			//lock_flag
			this.lock_flag = false;

			//clip_flag
			this.clip_flag = false;

			//clip_rect
			this.clip_rect.Set(0,0,0,0);

			//visible_flag
			this.visible_flag = true;

			//mode
			this.mode = CheckButton_Mode.Normal;

			//check_flag
			this.check_flag = false;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Regist(this);
			}
		}

		/** [CheckButton_Base]コールバック。矩形変更。
		*/
		protected abstract void OnChangeRect();

		/** [CheckButton_Base]コールバック。クリップフラグ変更。
		*/
		protected abstract void OnChangeClipFlag();

		/** [CheckButton_Base]コールバック。クリップ矩形変更。
		*/
		protected abstract void OnChangeClipRect();

		/** [CheckButton_Base]コールバック。モード変更。
		*/
		protected abstract void OnChangeMode();

		/** [CheckButton_Base]コールバック。チェックフラグ変更。
		*/
		protected abstract void OnChangeCheckFlag();

		/** [Slider_Base]コールバック。表示フラグ変更。
		*/
		protected abstract void OnChangeVisibleFlag();

		/** [Slider_Base]コールバック。描画プライオリティ変更。
		*/
		protected abstract void OnChangeDrawPriority();

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			this.deleter.DeleteAll();

			//コールバック解除。
			this.callbackparam_changecheck = null;

			//ターゲット解除。
			Fee.Ui.Ui.GetInstance().UnSetTargetRequest(this);
		}

		/** モード。設定。
		*/
		private void SetMode(CheckButton_Mode a_mode)
		{
			if(this.mode != a_mode){
				this.mode = a_mode;

				//コールバック。モード変更。
				this.OnChangeMode();
			}
		}

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			if(this.drawpriority != a_drawpriority){
				this.drawpriority = a_drawpriority;

				this.eventplate.SetPriority(a_drawpriority);

				//コールバック。描画プライオリティ変更。
				this.OnChangeDrawPriority();
			}
		}

		/** ロック。設定。
		*/
		public void SetLock(bool a_flag)
		{
			if(this.lock_flag != a_flag){
				this.lock_flag = a_flag;

				if(this.lock_flag == true){
					this.SetMode(CheckButton_Mode.Lock);
				}else{
					if(this.is_onover == true){
						this.SetMode(CheckButton_Mode.On);
					}else{
						this.SetMode(CheckButton_Mode.Normal);
					}
				}
			}
		}

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			if(this.clip_flag != a_flag){
				this.clip_flag = a_flag;
				this.eventplate.SetClip(a_flag);

				//コールバック。クリップフラグ変更。
				this.OnChangeClipFlag();
			}
		}
		
		/** クリップ矩形。設定。
		*/
		public void SetClipRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.clip_rect = a_rect;
			this.eventplate.SetClipRect(in a_rect);

			//コールバック。クリップ矩形変更。
			this.OnChangeClipRect();
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.clip_rect.Set(a_x,a_y,a_w,a_h);
			this.eventplate.SetClipRect(a_x,a_y,a_w,a_h);

			//コールバック。クリップ矩形変更。
			this.OnChangeClipRect();
		}

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.rect = a_rect;
			this.eventplate.SetRect(in a_rect);

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.rect.Set(a_x,a_y,a_w,a_h);
			this.eventplate.SetRect(a_x,a_y,a_w,a_h);

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			if(this.visible_flag != a_flag){
				this.visible_flag = a_flag;
				this.eventplate.SetEnable(a_flag);

				//コールバック。表示フラグ変更。
				this.OnChangeVisibleFlag();
			}
		}

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートに入場。
		*/
		public void OnEventPlateEnter(int a_id)
		{
			Tool.Log("CheckButton_Base","OnEventPlateEnter : " + a_id.ToString());

			this.is_onover = true;

			//ターゲット登録。
			Ui.GetInstance().SetTargetRequest(this);
		}

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートから退場。
		*/
		public void OnEventPlateLeave(int a_id)
		{
			Tool.Log("CheckButton_Base","OnEventPlateLeave : " + a_id.ToString());

			this.is_onover = false;
		}

		/** コールバックインターフェイス。設定。
		*/
		public void SetOnCheckButtonChangekCheck<T>(Fee.Ui.OnCheckButtonChangekCheck_CallBackInterface<T> a_callback_interface,T a_id)
		{
			if(a_callback_interface != null){
				this.callbackparam_changecheck = new Fee.Ui.OnCheckButtonChangekCheck_CallBackParam_Generic<T>(a_callback_interface,a_id);
			}else{
				this.callbackparam_changecheck = null;
			}
		}

		/** オンオーバー。取得。
		*/
		public bool IsOnOver()
		{
			return this.is_onover;
		}

		/** チェック。設定。
		*/
		public void SetCheck(bool a_flag)
		{
			if(this.check_flag != a_flag){
				this.check_flag = a_flag;

				//コールバック。チェックフラグ変更。
				this.OnChangeCheckFlag();

				//コールバック。
				if(this.callbackparam_changecheck != null){
					this.callbackparam_changecheck.Call(this.check_flag);
				}
			}
		}

		/** チェック。取得。
		*/
		public bool IsCheck()
		{
			return this.check_flag;
		}

		/** [Fee.Ui.OnTarget_CallBackInterface]ターゲット中。
		*/
		public void OnTarget()
		{
			if(this.lock_flag == true){
				//ロック中。

				//ターゲット解除。
				if(this.is_onover == false){
					Ui.GetInstance().UnSetTargetRequest(this);
				}

				this.SetMode(CheckButton_Mode.Lock);
			}else if(this.visible_flag == false){
				//非表示。

				//ターゲット解除。
				if(this.is_onover == false){
					Ui.GetInstance().UnSetTargetRequest(this);
				}

				this.SetMode(CheckButton_Mode.Normal);
			}else if(this.is_onover == true){
				//オーバー中。
				this.SetMode(CheckButton_Mode.On);

				if(Fee.Input.Mouse.GetInstance().left.down == true){
					this.check_flag = !this.check_flag;

					//コールバック。チェックフラグ変更。
					this.OnChangeCheckFlag();

					//コールバック。
					if(this.callbackparam_changecheck != null){
						this.callbackparam_changecheck.Call(this.check_flag);
					}
				}
			}else{
				//ターゲット解除。
				Ui.GetInstance().UnSetTargetRequest(this);

				this.SetMode(CheckButton_Mode.Normal);
			}
		}
	}
}

