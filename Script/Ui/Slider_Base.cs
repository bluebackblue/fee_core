

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。スライダー。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Slider_Base
	*/
	public abstract class Slider_Base : Fee.Deleter.OnDelete_CallBackInterface , Fee.EventPlate.OnEventPlateOver_CallBackInterface<int> , Fee.Ui.OnTarget_CallBackInterface
	{
		/** deleter
		*/
		protected Fee.Deleter.Deleter deleter;

		/** rect
		*/
		protected Fee.Geometry.Rect2D_R<int> rect;

		/** button_rect
		*/
		protected Fee.Geometry.Rect2D_R<int> button_rect;

		/** drawpriority
		*/
		protected long drawpriority;

		/** eventplate
		*/
		protected Fee.EventPlate.Item eventplate;

		/** eventplate_button
		*/
		protected Fee.EventPlate.Item eventplate_button;

		/** callbackparam_changevalue
		*/
		protected OnSliderChangeValue_CallBackParam callbackparam_changevalue;

		/** is_onover
		*/
		protected bool is_onover;

		/** is_onover_button
		*/
		protected bool is_onover_button;

		/** value
		*/
		protected float value;

		/** value_scale
		*/
		protected float value_scale;

		/** clip_flag
		*/
		protected bool clip_flag;

		/** clip_rect
		*/
		protected Fee.Geometry.Rect2D_R<int> clip_rect;

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
		public Slider_Base(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//rect
			this.rect.Set(0,0,0,0);

			//button_rect
			this.button_rect.Set(0,0,0,0);

			//drawpriority
			this.drawpriority = a_drawpriority;

			//eventplate
			this.eventplate = new Fee.EventPlate.Item(this.deleter,Fee.EventPlate.EventType.Button,this.drawpriority);
			this.eventplate.SetOnEventPlateOver(this,0);

			//eventplate_button
			this.eventplate_button = new Fee.EventPlate.Item(this.deleter,Fee.EventPlate.EventType.Button,this.drawpriority + 1);
			this.eventplate_button.SetOnEventPlateOver(this,1);

			//callbackparam_changevalue
			this.callbackparam_changevalue = null;

			//is_onover
			this.is_onover = false;

			//is_onover_button
			this.is_onover_button = false;

			//value
			this.value = 0.0f;

			//value_scale
			this.value_scale = 1.0f;

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

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			this.deleter.DeleteAll();
		}

		/** ボタン矩形。更新。
		*/
		private void UpdateButtonRect()
		{
			this.button_rect.x = this.rect.x + (int)(this.rect.w * (this.value / this.value_scale)) - this.button_rect.w / 2;
			this.button_rect.y = this.rect.y + (this.rect.h - this.button_rect.h) / 2;

			this.eventplate_button.SetRect(in this.button_rect);
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
				this.drawpriority = a_drawpriority;

				this.eventplate.SetPriority(a_drawpriority);
				this.eventplate_button.SetPriority(a_drawpriority + 1);

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
		public void SetClipRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.clip_rect = a_rect;
			this.eventplate.SetClipRect(in a_rect);
			this.eventplate_button.SetClipRect(in a_rect);

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
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.rect = a_rect;
			this.eventplate.SetRect(in a_rect);

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
		public void SetXY(int a_x,int a_y)
		{
			this.rect.x = a_x;
			this.rect.y = a_y;
			this.eventplate.SetXY(a_x,a_y);

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
		public void SetWH(int a_w,int a_h)
		{
			this.rect.w = a_w;
			this.rect.h = a_h;
			this.eventplate.SetWH(a_w,a_h);

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

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートに入場。
		*/
		public void OnEventPlateEnter(int a_id)
		{
			Tool.Log("Slider_Base","OnEventPlateEnter : " + a_id.ToString());

			if((this.is_onover == false)&&(this.is_onover_button == false)){
				//ターゲット登録。
				Ui.GetInstance().SetTargetRequest(this);
			}

			if(a_id == 0){
				this.is_onover = true;
			}else{
				this.is_onover_button = true;
			}
		}

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートから退場。
		*/
		public void OnEventPlateLeave(int a_id)
		{
			Tool.Log("Slider_Base","OnEventPlateLeave : " + a_id.ToString());

			if(a_id == 0){
				this.is_onover = false;
			}else{
				this.is_onover_button = false;
			}
		}

		/** コールバックインターフェイス。設定。
		*/
		public void SetOnSliderChangeValue<T>(Fee.Ui.OnSliderChangeValue_CallBackInterface<T> a_callback_interface,T a_id)
		{
			if(a_callback_interface != null){
				this.callbackparam_changevalue = new Fee.Ui.OnSliderChangeValue_CallBackParam_Generic<T>(a_callback_interface,a_id);
			}else{
				this.callbackparam_changevalue = null;
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
				if(this.value < 0.0f){
					this.value = 0.0f;
				}else if(this.value >= this.value_scale){
					this.value = this.value_scale;
				}

				//ボタン矩形。更新。
				this.UpdateButtonRect();

				//コールバック。
				this.OnChangeRect();

				//コールバック。
				if(this.callbackparam_changevalue != null){
					this.callbackparam_changevalue.Call(this.value);
				}
			}
		}

		/** CallChangeCallBack
		*/
		public void CallChangeCallBack()
		{
			//コールバック。
			if(this.callbackparam_changevalue != null){
				this.callbackparam_changevalue.Call(this.value);
			}
		}

		/** 値。取得。
		*/
		public float GetValue()
		{
			return this.value;
		}

		/** 値スケール値。取得。
		*/
		public float GetValueScale()
		{
			return this.value_scale;
		}

		/** 値スケール値。設定。
		*/
		public void SetValueScale(float a_value_scale)
		{
			this.value_scale = a_value_scale;
		}

		/** [Fee.Ui.OnTarget_CallBackInterface]ターゲット中。
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
			}else if(((this.is_onover == true)||(this.is_onover_button == true))&&(this.down_flag == false)&&(Fee.Input.Mouse.GetInstance().left.down == true)){
				//ダウン。

				//ダウン開始。
				this.down_flag = true;

				{
					float t_value_per = ((float)(Fee.Input.Mouse.GetInstance().cursor.GetX() - this.rect.x)) / this.rect.w;
					if(t_value_per < 0.0f){
						t_value_per = 0.0f;
					}else if(t_value_per > 1.0f){
						t_value_per = 1.0f;
					}
					this.SetValue(t_value_per * this.value_scale);
				}
			}else if((this.down_flag == true)&&(Fee.Input.Mouse.GetInstance().left.on == false)){
				//アップ。

				//ダウンキャンセル。
				this.down_flag = false;
			}else if(((this.is_onover == true)||(this.is_onover_button == true))&&(this.down_flag == true)){
				//ダウン中オーバー中。

				{
					float t_value_per = ((float)(Fee.Input.Mouse.GetInstance().cursor.GetX() - this.rect.x)) / this.rect.w;
					if(t_value_per < 0.0f){
						t_value_per = 0.0f;
					}else if(t_value_per > 1.0f){
						t_value_per = 1.0f;
					}
					this.SetValue(t_value_per * this.value_scale);
				}
			}else if((this.is_onover == true)||(this.is_onover_button == true)){
				//オーバー中。
			}else if(this.down_flag == true){
				//範囲外ダウン中。

				{
					float t_value_per = ((float)(Fee.Input.Mouse.GetInstance().cursor.GetX() - this.rect.x)) / this.rect.w;
					if(t_value_per < 0.0f){
						t_value_per = 0.0f;
					}else if(t_value_per > 1.0f){
						t_value_per = 1.0f;
					}
					this.SetValue(t_value_per * this.value_scale);
				}
			}else{
				//ターゲット解除。
				Ui.GetInstance().UnSetTargetRequest(this);
			}
		}
	}
}

