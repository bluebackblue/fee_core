using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。スライダー。
*/


/** NUi
*/
namespace NUi
{
	/** Slider_Base
	*/
	public abstract class Slider_Base : NDeleter.DeleteItem_Base , NEventPlate.OnOverCallBack_Base , NUi.OnTargetCallBack_Base
	{
		/** チェンジコールバック。
		*/
		public delegate void CallBack_Change(int a_index,float a_value);

		/** deleter
		*/
		protected NDeleter.Deleter deleter;

		/** rect
		*/
		protected NRender2D.Rect2D_R<int> rect;

		/** button_rect
		*/
		protected NRender2D.Rect2D_R<int> button_rect;

		/** drawpriority
		*/
		protected long drawpriority;

		/** eventplate
		*/
		protected NEventPlate.Item eventplate;

		/** eventplate_button
		*/
		protected NEventPlate.Item eventplate_button;

		/** CallBack_Change
		*/
		protected CallBack_Change callback_change;
		protected int callback_change_index;

		/** is_onover
		*/
		protected bool is_onover;

		/** is_onover_button
		*/
		protected bool is_onover_button;

		/** value
		*/
		protected float value;

		/** clip_flag
		*/
		protected bool clip_flag;

		/** clip_rect
		*/
		protected NRender2D.Rect2D_R<int> clip_rect;

		/** visible_flag
		*/
		protected bool visible_flag;

		/** down_flag
		*/
		protected bool down_flag;

		/** lock_flag
		*/
		protected bool lock_flag;

		/** constructor
		*/
		public Slider_Base(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority,CallBack_Change a_callback_change,int a_callback_change_index)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//rect
			this.rect.Set(0,0,0,0);

			//button_rect
			this.button_rect.Set(0,0,0,0);

			//drawpriority
			this.drawpriority = a_drawpriority;

			//eventplate
			this.eventplate = new NEventPlate.Item(this.deleter,NEventPlate.EventType.Button,this.drawpriority);
			this.eventplate.SetOnOverCallBack(this);
			this.eventplate.SetOnOverCallBackValue(0);

			//eventplate_button
			this.eventplate_button = new NEventPlate.Item(this.deleter,NEventPlate.EventType.Button,this.drawpriority + 1);
			this.eventplate_button.SetOnOverCallBack(this);
			this.eventplate_button.SetOnOverCallBackValue(1);

			//callback_change
			this.callback_change = a_callback_change;
			this.callback_change_index = a_callback_change_index;

			//is_onover
			this.is_onover = false;

			//is_onover_button
			this.is_onover_button = false;

			//value
			this.value = 0.0f;

			//clip_flag
			this.clip_flag = false;

			//clip_rect
			this.clip_rect.Set(0,0,0,0);

			//visible_flag
			this.visible_flag = true;

			//down_flag
			this.down_flag = false;

			//lock_flag
			this.lock_flag = false;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** [Slider_Base]コールバック。ロックフラグ変更。
		*/
		protected abstract void OnChangeLockFlag();

		/** [Slider_Base]コールバック。矩形変更。
		*/
		protected abstract void OnChangeRect();

		/** [Slider_Base]コールバック。クリップフラグ変更。
		*/
		protected abstract void OnChangeClipFlag();

		/** [Slider_Base]コールバック。クリップ矩形変更。
		*/
		protected abstract void OnChangeClipRect();

		/** [Slider_Base]コールバック。表示フラグ変更。
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
		}

		/** ボタン矩形。更新。
		*/
		private void UpdateButtonRect()
		{
			int t_w = this.button_rect.w;
			int t_h = this.button_rect.h;

			if(t_w <= 0){
				t_w = this.rect.h;
			}

			if(t_h <= 0){
				t_h = this.rect.h;
			}

			int t_x = this.rect.x + (int)(this.rect.w * this.value) - t_w / 2;
			int t_y = this.rect.y + (this.rect.h - t_h) / 2;

			this.eventplate_button.SetRect(t_x,t_y,t_w,t_h);
		}

		/** ボタンサイズ。設定。
		*/
		public void SetButtonSize(int a_w,int a_h)
		{
			this.button_rect.w = a_w;
			this.button_rect.h = a_h;

			//ボタン矩形。更新。
			UpdateButtonRect();

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** ロックフラグ。設定。
		*/
		public void SetLock(bool a_flag)
		{
			if(this.lock_flag != a_flag){
				this.lock_flag = a_flag;

				//コールバック。ロックフラグ変更。
				this.OnChangeLockFlag();
			}
		}

		/** ロックフラグ。取得。
		*/
		public bool IsLock()
		{
			return this.lock_flag;
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

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			if(this.clip_flag != a_flag){
				this.clip_flag = a_flag;
				this.eventplate.SetClip(a_flag);
				this.eventplate_button.SetClip(a_flag);

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
			this.eventplate_button.SetClipRect(ref a_rect);

			//コールバック。クリップ矩形変更。
			this.OnChangeClipRect();
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.clip_rect.Set(a_x,a_y,a_w,a_h);
			this.eventplate.SetClipRect(a_x,a_y,a_w,a_h);
			this.eventplate_button.SetClipRect(a_x,a_y,a_w,a_h);

			//コールバック。クリップ矩形変更。
			this.OnChangeClipRect();
		}

		/** 矩形。設定。
		*/
		public void SetRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.rect = a_rect;
			this.eventplate.SetRect(ref a_rect);

			//ボタン矩形。更新。
			this.UpdateButtonRect();

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.rect.Set(a_x,a_y,a_w,a_h);
			this.eventplate.SetRect(a_x,a_y,a_w,a_h);

			//ボタン矩形。更新。
			this.UpdateButtonRect();

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.rect.x = a_x;
			this.eventplate.SetX(a_x);

			//ボタン矩形。更新。
			this.UpdateButtonRect();

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.rect.y = a_y;
			this.eventplate.SetY(a_y);

			//ボタン矩形。更新。
			this.UpdateButtonRect();

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.rect.w = a_w;
			this.eventplate.SetW(a_w);

			//ボタン矩形。更新。
			this.UpdateButtonRect();

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.rect.h = a_h;
			this.eventplate.SetH(a_h);

			//ボタン矩形。更新。
			this.UpdateButtonRect();

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
				this.eventplate_button.SetEnable(a_flag);

				//コールバック。表示フラグ変更。
				this.OnChangeVisibleFlag();
			}
		}

		/** [NEventPlate.OnOverCallBack_Base]OnOverEnter
		*/
		public void OnOverEnter(int a_value)
		{
			Tool.Log("Slider_Base","OnOverEnter : " + a_value.ToString());

			if((this.is_onover == false)&&(this.is_onover_button == false)){
				//ターゲット登録。
				Ui.GetInstance().SetTargetRequest(this);
			}

			if(a_value == 0){
				this.is_onover = true;
			}else{
				this.is_onover_button = true;
			}
		}

		/** [NEventPlate.OnOverCallBack_Base]OnOverLeave
		*/
		public void OnOverLeave(int a_value)
		{
			Tool.Log("Slider_Base","OnOverLeave : " + a_value.ToString());

			if(a_value == 0){
				this.is_onover = false;
			}else{
				this.is_onover_button = false;
			}
		}

		/** オンオーバー。取得。
		*/
		public bool IsOnOver()
		{
			return (this.is_onover | this.is_onover_button);
		}

		/** 値。設定。
		*/
		public void SetValue(float a_value)
		{
			if(this.value != a_value){
				this.value = a_value;

				//ボタン矩形。更新。
				this.UpdateButtonRect();

				//コールバック。
				this.OnChangeRect();

				//コールバック。
				if(this.callback_change != null){
					this.callback_change(this.callback_change_index,this.value);
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
				if((this.is_onover == false)&&(this.is_onover_button == false)){
					Ui.GetInstance().UnSetTargetRequest(this);
				}

				//ダウンキャンセル。
				this.down_flag = false;
			}else if(((this.is_onover == true)||(this.is_onover_button == true))&&(this.down_flag == false)&&(NInput.Mouse.GetInstance().left.down == true)){
				//ダウン。

				//ダウン開始。
				this.down_flag = true;

				{
					float t_value = ((float)(NInput.Mouse.GetInstance().pos.x - this.rect.x)) / this.rect.w;
					if(t_value < 0.0f){
						t_value = 0.0f;
					}else if(t_value > 1.0f){
						t_value = 1.0f;
					}
					this.SetValue(t_value);
				}
			}else if((this.down_flag == true)&&(NInput.Mouse.GetInstance().left.on == false)){
				//アップ。

				//ダウンキャンセル。
				this.down_flag = false;
			}else if(((this.is_onover == true)||(this.is_onover_button == true))&&(this.down_flag == true)){
				//ダウン中オーバー中。

				{
					float t_value = ((float)(NInput.Mouse.GetInstance().pos.x - this.rect.x)) / this.rect.w;
					if(t_value < 0.0f){
						t_value = 0.0f;
					}else if(t_value > 1.0f){
						t_value = 1.0f;
					}
					this.SetValue(t_value);
				}
			}else if((this.is_onover == true)||(this.is_onover_button == true)){
				//オーバー中。
			}else if(this.down_flag == true){
				//範囲外ダウン中。

				{
					float t_value = ((float)(NInput.Mouse.GetInstance().pos.x - this.rect.x)) / this.rect.w;
					if(t_value < 0.0f){
						t_value = 0.0f;
					}else if(t_value > 1.0f){
						t_value = 1.0f;
					}
					this.SetValue(t_value);
				}
			}else{
				//ターゲット解除。
				Ui.GetInstance().UnSetTargetRequest(this);
			}
		}
	}
}

