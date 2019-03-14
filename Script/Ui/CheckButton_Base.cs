using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。チェックボタン。
*/


/** NUi
*/
namespace NUi
{
	/** CheckButton_Base
	*/
	public abstract class CheckButton_Base : NDeleter.DeleteItem_Base , NEventPlate.OnOverCallBack_Base , NUi.OnTargetCallBack_Base
	{
		/** [CheckButton_Base]コールバック。変更。
		*/
		public delegate void CallBack_Change(int a_id,bool a_flag);

		/** deleter
		*/
		protected NDeleter.Deleter deleter;

		/** eventplate
		*/
		protected NEventPlate.Item eventplate;

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

		/** visible
		*/
		protected bool visible;

		/** mode
		*/
		protected CheckButton_Mode mode;

		/** check_flag
		*/
		protected bool check_flag;

		/** constructor
		*/
		public CheckButton_Base(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority,CallBack_Change a_callback_change,int a_callback_id)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//eventplate
			this.eventplate = new NEventPlate.Item(this.deleter,NEventPlate.EventType.Button,a_drawpriority);
			this.eventplate.SetOnOverCallBack(this);

			//callback_change
			this.callback_change = a_callback_change;
			this.callback_id = a_callback_id;

			//is_onover
			this.is_onover = false;

			//lock_flag
			this.lock_flag = false;

			//clip_flag
			this.clip_flag = false;

			//visible
			this.visible = true;

			//mode
			this.mode = CheckButton_Mode.Normal;

			//check_flag
			this.check_flag = false;

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
		protected abstract void OnSetModeCallBack(CheckButton_Mode a_mode);

		/** コールバック。チェック。設定。
		*/
		protected abstract void OnSetCheckCallBack(bool a_flag);

		/** コールバック。クリップ。設定。
		*/
		protected abstract void OnSetClipCallBack(bool a_flag);

		/** コールバック。クリップ矩形。設定。
		*/
		protected abstract void OnSetClipRectCallBack(int a_x,int a_y,int a_w,int a_h);

		/** コールバック。クリップ矩形。設定。
		*/
		protected abstract void OnSetClipRectCallBack(ref NRender2D.Rect2D_R<int> a_rect);

		/** 削除。
		*/
		public void Delete()
		{
			this.deleter.DeleteAll();

			//ターゲット解除。
			NUi.Ui.GetInstance().UnSetTargetRequest(this);

			//コールバック。削除。
			this.OnDeleteCallBack();
		}

		/** モード。設定。
		*/
		private void SetMode(CheckButton_Mode a_mode)
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

				//コールバック。クリップ。設定。
				this.OnSetClipCallBack(a_flag);
			}
		}
		
		/** クリップ矩形。設定。
		*/
		public void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.eventplate.SetClipRect(ref a_rect);

			//コールバック。クリップ矩形。設定。
			this.OnSetClipRectCallBack(ref a_rect);
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.eventplate.SetClipRect(a_x,a_y,a_w,a_h);

			//コールバック。クリップ矩形。設定。
			this.OnSetClipRectCallBack(a_x,a_y,a_w,a_h);
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
			Tool.Log("CheckButton_Base","OnOverEnter : " + a_value.ToString());

			this.is_onover = true;

			//ターゲット登録。
			Ui.GetInstance().SetTargetRequest(this);
		}

		/** [NEventPlate.OnOverCallBack_Base]OnOverLeave
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

				//コールバック。チェック。設定。
				this.OnSetCheckCallBack(this.check_flag);

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

				this.SetMode(CheckButton_Mode.Lock);
			}else if(this.visible == false){
				//非表示。

				//ターゲット解除。
				if(this.is_onover == false){
					Ui.GetInstance().UnSetTargetRequest(this);
				}

				this.SetMode(CheckButton_Mode.Normal);
			}else if(this.is_onover == true){
				//オーバー中。
				this.SetMode(CheckButton_Mode.On);

				if(NInput.Mouse.GetInstance().left.down == true){
					this.check_flag = !this.check_flag;

					//コールバック。チェック。設定。
					this.OnSetCheckCallBack(this.check_flag);

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

