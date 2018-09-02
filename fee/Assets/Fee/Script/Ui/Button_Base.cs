using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。ボタン。
*/


/** NUi
*/
namespace NUi
{
	/** Button_Base
	*/
	public abstract class Button_Base : NDeleter.DeleteItem_Base , NEventPlate.OnOverCallBack_Base , NUi.OnTargetCallBack_Base
	{
		/** クリックコールバック。
		*/
		public delegate void CallBack_Click(int a_value);

		/** s_down_instance
		*/
		protected static Button_Base s_down_instance = null;

		/** deleter
		*/
		protected NDeleter.Deleter deleter;

		/** eventplate
		*/
		protected NEventPlate.Item eventplate;

		/** callback_click
		*/
		protected CallBack_Click callback_click;
		protected int callback_click_value;

		/** is_onover
		*/
		protected bool is_onover;

		/** down_flag
		*/
		protected bool down_flag;

		/** lock_flag
		*/
		protected bool lock_flag;

		/** clip_flag
		*/
		protected bool clip_flag;

		/** visible
		*/
		protected bool visible;

		/** event_request
		*/
		protected int event_request;

		/** mode
		*/
		protected Button_Mode mode;

		/** constructor
		*/
		public Button_Base(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority,CallBack_Click a_callback_click,int a_callback_click_value)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//eventplate
			this.eventplate = new NEventPlate.Item(this.deleter,NEventPlate.EventType.Button,a_drawpriority);
			this.eventplate.SetOnOverCallBack(this);

			//callback_click
			this.callback_click = a_callback_click;
			this.callback_click_value = a_callback_click_value;

			//is_onover
			this.is_onover = false;

			//down_flag
			this.down_flag = false;

			//lock_flag
			this.lock_flag = false;

			//clip_flag
			this.clip_flag = false;

			//visible
			this.visible = true;

			//event_request
			this.event_request = 0;

			//mode
			this.mode = Button_Mode.Normal;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** コールバック。削除。
		*/
		protected abstract void OnDeleteCallBack();

		/** コールバック。矩形。設定。
		*/
		protected abstract void OnSetRectCallBack(int a_x,int a_y,int a_w,int a_h);

		/** コールバック。矩形。設定。
		*/
		protected abstract void OnSetRectCallBack(ref NRender2D.Rect2D_R<int> a_rect);

		/** コールバック。モード。設定。
		*/
		protected abstract void OnSetModeCallBack(Button_Mode a_mode);

		/** 削除。
		*/
		public void Delete()
		{
			this.deleter.DeleteAll();

			//ターゲット解除。
			NUi.Ui.GetInstance().UnSetTarget(this);

			//ダウン解除。
			if(Button_Base.s_down_instance == this){
				Button_Base.s_down_instance = null;
			}

			//コールバック。削除。
			this.OnDeleteCallBack();
		}

		/** モード。設定。
		*/
		private void SetMode(Button_Mode a_mode)
		{
			if(this.mode != a_mode){
				this.mode = a_mode;

				this.OnSetModeCallBack(a_mode);
			}
		}

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			this.eventplate.SetPriority(a_drawpriority);
		}

		/** ロック。設定。
		*/
		public void SetLock(bool a_flag)
		{
			if(this.lock_flag != a_flag){
				this.lock_flag = a_flag;

				if(this.lock_flag == true){
					this.SetMode(Button_Mode.Lock);
				}else{
					if(this.is_onover == true){
						this.SetMode(Button_Mode.On);
					}else{
						this.SetMode(Button_Mode.Normal);
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
			}
		}
		
		/** クリップ矩形。設定。
		*/
		public void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.eventplate.SetClipRect(ref a_rect);
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.eventplate.SetClipRect(a_x,a_y,a_w,a_h);
		}

		/** 矩形。設定。
		*/
		public void SetRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.eventplate.SetRect(ref a_rect);

			//コールバック。矩形。設定。
			this.OnSetRectCallBack(ref a_rect);
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.eventplate.SetRect(a_x,a_y,a_w,a_h);

			//コールバック。矩形。設定。
			this.OnSetRectCallBack(a_x,a_y,a_w,a_h);
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			if(this.visible != a_flag){
				this.visible = a_flag;
				this.eventplate.SetEnable(a_flag);
			}
		}

		/** [NEventPlate.OnOverCallBack_Base]OnOverEnter
		*/
		public void OnOverEnter(int a_value)
		{
			Tool.Log("Button_Base","OnOverEnter : " + a_value.ToString());

			this.is_onover = true;

			//ターゲット登録。
			Ui.GetInstance().SetTarget(this);
		}

		/** [NEventPlate.OnOverCallBack_Base]OnOverLeave
		*/
		public void OnOverLeave(int a_value)
		{
			Tool.Log("Button_Base","OnOverLeave : " + a_value.ToString());

			this.is_onover = false;
		}

		/** オンオーバー。取得。
		*/
		public bool IsOnOver()
		{
			return this.is_onover;
		}

		/** クリックイベント。発行。
		*/
		public void ClickEventRequest()
		{
			if((this.lock_flag == false)&&(this.visible == true)){
				//イベントリクエスト。
				this.event_request = 13;

				//ターゲット登録。
				Ui.GetInstance().SetTarget(this);

				//ダウンキャンセル。
				this.down_flag = false;
				if(Button_Base.s_down_instance == this){
					Button_Base.s_down_instance = null;
				}
			}
		}

		/** [NUi.OnTargetCallBack_Base]OnTarget
		*/
		public void OnTarget()
		{
			if(this.lock_flag == true){
				//ロック中。

				//ターゲット解除。
				if(this.is_onover == false){
					Ui.GetInstance().UnSetTarget(this);
				}

				//ダウンキャンセル。
				this.down_flag = false;
				if(Button_Base.s_down_instance == this){
					Button_Base.s_down_instance = null;
				}

				//リクエストキャンセル。
				this.event_request = 0;

				this.SetMode(Button_Mode.Lock);
			}else if(this.visible == false){
				//非表示。

				//ターゲット解除。
				if(this.is_onover == false){
					Ui.GetInstance().UnSetTarget(this);
				}

				//ダウンキャンセル。
				this.down_flag = false;
				if(Button_Base.s_down_instance == this){
					Button_Base.s_down_instance = null;
				}

				//リクエストキャンセル。
				this.event_request = 0;

				this.SetMode(Button_Mode.Normal);
			}else if(this.event_request > 0){
				//イベント中。

				//ダウンキャンセル。
				this.down_flag = false;
				if(Button_Base.s_down_instance == this){
					Button_Base.s_down_instance = null;
				}

				this.event_request--;
				if(this.event_request == 0){

					//ターゲット解除。
					Ui.GetInstance().UnSetTarget(this);

					//コールバック。
					if(this.callback_click != null){
						this.callback_click(this.callback_click_value);
					}

					if(this.is_onover == true){
						this.SetMode(Button_Mode.On);
					}else{
						this.SetMode(Button_Mode.Normal);
					}
				}else{
					this.SetMode(Button_Mode.Down);
				}
			}else{
				if((this.is_onover == true)&&(this.down_flag == false)&&(NInput.Mouse.GetInstance().left.down == true)){
					//ダウン。

					//ダウン開始。
					this.down_flag = true;
					Button_Base.s_down_instance = this;

					this.SetMode(Button_Mode.Down);
				}else if((this.down_flag == true)&&(NInput.Mouse.GetInstance().left.up == true)){
					//アップ。

					//ダウンキャンセル。
					this.down_flag = false;
					if(Button_Base.s_down_instance == this){
						Button_Base.s_down_instance = null;
					}

					//コールバック。
					if(this.is_onover == true){
						if(this.callback_click != null){
							this.callback_click(this.callback_click_value);
						}
					}

					if(this.is_onover == true){
						this.SetMode(Button_Mode.On);
					}else{
						this.SetMode(Button_Mode.Normal);
					}
				}else if((this.is_onover == true)&&(this.down_flag == true)){
					//ダウン中オーバー中。

					this.SetMode(Button_Mode.Down);
				}else if(this.is_onover == true){
					//オーバー中。

					if(Button_Base.s_down_instance == null){
						this.SetMode(Button_Mode.On);
					}else{
						this.SetMode(Button_Mode.Normal);
					}
				}else if(this.down_flag == true){
					//ダウン中。
					this.SetMode(Button_Mode.On);
				}else{
					//ターゲット解除。
					Ui.GetInstance().UnSetTarget(this);
					this.SetMode(Button_Mode.Normal);
				}
			}
		}
	}
}

