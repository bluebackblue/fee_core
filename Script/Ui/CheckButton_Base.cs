

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
	public abstract class CheckButton_Base : Fee.Deleter.OnDelete_CallBackInterface , Fee.EventPlate.OnOver_CallBackInterface , Fee.Ui.OnTargetCallBack_Base
	{
		/** [CheckButton_Base]コールバック。変更。
		*/
		public delegate void CallBack_Change(int a_id,bool a_flag);

		/** deleter
		*/
		protected Fee.Deleter.Deleter deleter;

		/** 矩形。
		*/
		protected Fee.Render2D.Rect2D_R<int> rect;

		/** drawpriority
		*/
		protected long drawpriority;

		/** eventplate
		*/
		protected Fee.EventPlate.Item eventplate;

		/** callback_change
		*/
		protected CallBack_Change callback_change;
		protected int callback_id;

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
		protected Fee.Render2D.Rect2D_R<int> clip_rect;

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
		public CheckButton_Base(Fee.Deleter.Deleter a_deleter,long a_drawpriority,CallBack_Change a_callback_change,int a_callback_id)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//rect
			this.rect.Set(0,0,0,0);

			//drawpriority
			this.drawpriority = a_drawpriority;

			//eventplate
			this.eventplate = new Fee.EventPlate.Item(this.deleter,Fee.EventPlate.EventType.Button,this.drawpriority);
			this.eventplate.SetOnOverCallBackInterface(this);

			//callback_change
			this.callback_change = a_callback_change;
			this.callback_id = a_callback_id;

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
				a_deleter.Register(this);
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
		public void SetClipRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.clip_rect = a_rect;
			this.eventplate.SetClipRect(ref a_rect);

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
		public void SetRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.rect = a_rect;
			this.eventplate.SetRect(ref a_rect);

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

		/** [Fee.EventPlate.OnOver_CallBackInterface]イベントプレートに入場。
		*/
		public void OnOverEnter(int a_value)
		{
			Tool.Log("CheckButton_Base","OnOverEnter : " + a_value.ToString());

			this.is_onover = true;

			//ターゲット登録。
			Ui.GetInstance().SetTargetRequest(this);
		}

		/** [Fee.EventPlate.OnOver_CallBackInterface]イベントプレートから退場。
		*/
		public void OnOverLeave(int a_value)
		{
			Tool.Log("CheckButton_Base","OnOverLeave : " + a_value.ToString());

			this.is_onover = false;
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
				if(this.callback_change != null){
					this.callback_change(this.callback_id,this.check_flag);
				}
			}
		}

		/** チェック。取得。
		*/
		public bool IsCheck()
		{
			return this.check_flag;
		}

		/** [Fee.Ui.OnTargetCallBack_Base]OnTarget
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
					if(this.callback_change != null){
						this.callback_change(this.callback_id,this.check_flag);
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

