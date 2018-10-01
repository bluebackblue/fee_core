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

		/** 矩形。
		*/
		protected NRender2D.Rect2D_R<int> rect;

		/** drawpriority
		*/
		protected long drawpriority;

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

		/** clip_rect
		*/
		protected NRender2D.Rect2D_R<int> clip_rect;

		/** visible_flag
		*/
		protected bool visible_flag;

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

			//rect
			this.rect.Set(0,0,0,0);

			//drawpriority
			this.drawpriority = a_drawpriority;

			//eventplate
			this.eventplate = new NEventPlate.Item(this.deleter,NEventPlate.EventType.Button,this.drawpriority);
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

			//clip_rect
			this.clip_rect.Set(0,0,0,0);

			//visible_flag
			this.visible_flag = true;

			//event_request
			this.event_request = 0;

			//mode
			this.mode = Button_Mode.Normal;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** [Button_Base]コールバック。矩形変更。
		*/
		protected abstract void OnChangeRect();

		/** [Button_Base]コールバック。モード変更。
		*/
		protected abstract void OnChangeMode();

		/** [Button_Base]コールバック。クリップフラグ変更。
		*/
		protected abstract void OnChangeClipFlag();

		/** [Button_Base]コールバック。クリップ矩形変更。
		*/
		protected abstract void OnChangeClipRect();

		/** [Button_Base]コールバック。表示フラグ変更。
		*/
		protected abstract void OnChangeVisibleFlag();

		/** [Slider_Base]コールバック。描画プライオリティ変更。
		*/
		protected abstract void OnChangeDrawPriority();

		/** 削除。
		*/
		public void Delete()
		{
			this.deleter.DeleteAll();

			//ターゲット解除。
			NUi.Ui.GetInstance().UnSetTargetRequest(this);

			//ダウン解除。
			if(Button_Base.s_down_instance == this){
				Button_Base.s_down_instance = null;
			}
		}

		/** モード。設定。
		*/
		private void SetMode(Button_Mode a_mode)
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

				//コールバック。クリップフラグ変更。
				this.OnChangeClipFlag();
			}
		}
		
		/** クリップ矩形。設定。
		*/
		public void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect)
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
		public void SetRect(ref NRender2D.Rect2D_R<int> a_rect)
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

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.rect.x = a_x;
			this.eventplate.SetX(a_x);

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.rect.y = a_y;
			this.eventplate.SetY(a_y);

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.rect.w = a_w;
			this.eventplate.SetW(a_w);

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.rect.h = a_h;
			this.eventplate.SetH(a_h);

			//コールバック。矩形変更。
			this.OnChangeRect();
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

		/** [NEventPlate.OnOverCallBack_Base]OnOverEnter
		*/
		public void OnOverEnter(int a_value)
		{
			Tool.Log("Button_Base","OnOverEnter : " + a_value.ToString());

			this.is_onover = true;

			//ターゲット登録。
			Ui.GetInstance().SetTargetRequest(this);
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
			if((this.lock_flag == false)&&(this.visible_flag == true)){
				//イベントリクエスト。
				this.event_request = 13;

				//ターゲット登録。
				Ui.GetInstance().SetTargetRequest(this);

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
					Ui.GetInstance().UnSetTargetRequest(this);
				}

				//ダウンキャンセル。
				this.down_flag = false;
				if(Button_Base.s_down_instance == this){
					Button_Base.s_down_instance = null;
				}

				//リクエストキャンセル。
				this.event_request = 0;

				this.SetMode(Button_Mode.Lock);
			}else if(this.visible_flag == false){
				//非表示。

				//ターゲット解除。
				if(this.is_onover == false){
					Ui.GetInstance().UnSetTargetRequest(this);
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
					Ui.GetInstance().UnSetTargetRequest(this);

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
				}else if((this.down_flag == true)&&(NInput.Mouse.GetInstance().left.on == false)){
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
					//範囲外ダウン中。
					this.SetMode(Button_Mode.On);
				}else{
					//ターゲット解除。
					Ui.GetInstance().UnSetTargetRequest(this);
					this.SetMode(Button_Mode.Normal);
				}
			}
		}
	}
}

