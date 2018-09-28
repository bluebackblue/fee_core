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
		/** deleter
		*/
		protected NDeleter.Deleter deleter;

		/** rect
		*/
		protected NRender2D.Rect2D_R<int> rect;

		/** eventplate
		*/
		protected NEventPlate.Item eventplate;

		/** is_onover
		*/
		protected bool is_onover;

		/** value
		*/
		protected float value;

		/** lock_flag
		*/
		/*
		protected bool lock_flag;
		*/

		/** clip_flag
		*/
		/*
		protected bool clip_flag;
		*/

		/** visible
		*/
		/*
		protected bool visible;
		*/

		/** constructor
		*/
		public Slider_Base(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//rect
			this.rect = new NRender2D.Rect2D_R<int>(0,0,0,0);

			//eventplate
			this.eventplate = new NEventPlate.Item(this.deleter,NEventPlate.EventType.Button,a_drawpriority);
			this.eventplate.SetOnOverCallBack(this);

			//is_onover
			this.is_onover = false;

			//value
			this.value = 0.5f;

			//lock_flag
			/*
			this.lock_flag = false;
			*/

			//clip_flag
			/*
			this.clip_flag = false;
			*/

			//visible
			/*
			this.visible = true;
			*/

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** [Slider_Base]コールバック。削除。
		*/
		protected abstract void OnDeleteCallBack();

		/** [Slider_Base]コールバック。矩形変更。
		*/
		protected abstract void OnChangeRect();

		/** [Slider_Base]コールバック。クリップ。設定。
		*/
		/*
		protected abstract void OnSetClipCallBack(bool a_flag);
		*/

		/** [Slider_Base]コールバック。クリップ矩形。設定。
		*/
		/*
		protected abstract void OnSetClipRectCallBack(int a_x,int a_y,int a_w,int a_h);
		*/

		/** [Slider_Base]コールバック。クリップ矩形。設定。
		*/
		/*
		protected abstract void OnSetClipRectCallBack(ref NRender2D.Rect2D_R<int> a_rect);
		*/

		/** [Slider_Base]コールバック。表示。設定。
		*/
		/*
		protected abstract void OnSetVisibleCallBack(bool a_flag);
		*/

		/** [Slider_Base]コールバック。描画プライオリティ。設定。
		*/
		protected abstract void OnSetDrawPriority(long a_drawpriority);

		/** コールバック。値変更。
		*/
		protected abstract void OnChangeValue();

		/** 削除。
		*/
		public void Delete()
		{
			this.deleter.DeleteAll();

			//コールバック。削除。
			this.OnDeleteCallBack();
		}

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			this.eventplate.SetPriority(a_drawpriority);

			//コールバック。描画プライオリティ。設定。
			this.OnSetDrawPriority(a_drawpriority);
		}

		/** ロック。設定。
		*/
		/*
		public void SetLock(bool a_flag)
		{
			if(this.lock_flag != a_flag){
				this.lock_flag = a_flag;
			}
		}
		*/

		/** クリップ。設定。
		*/
		/*
		public void SetClip(bool a_flag)
		{
			if(this.clip_flag != a_flag){
				this.clip_flag = a_flag;
				this.eventplate.SetClip(a_flag);

				//コールバック。クリップ。設定。
				this.OnSetClipCallBack(a_flag);
			}
		}
		*/

		/** クリップ矩形。設定。
		*/
		/*
		public void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.eventplate.SetClipRect(ref a_rect);

			//コールバック。クリップ矩形。設定。
			this.OnSetClipRectCallBack(ref a_rect);
		}
		*/

		/** クリップ矩形。設定。
		*/
		/*
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.eventplate.SetClipRect(a_x,a_y,a_w,a_h);

			//コールバック。クリップ矩形。設定。
			this.OnSetClipRectCallBack(a_x,a_y,a_w,a_h);
		}
		*/

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
		/*
		public void SetVisible(bool a_flag)
		{
			if(this.visible != a_flag){
				this.visible = a_flag;
				this.eventplate.SetEnable(a_flag);

				//コールバック。表示。設定。
				this.OnSetVisibleCallBack(a_flag);
			}
		}
		*/

		/** [NEventPlate.OnOverCallBack_Base]OnOverEnter
		*/
		public void OnOverEnter(int a_value)
		{
			Tool.Log("Slider_Base","OnOverEnter : " + a_value.ToString());

			this.is_onover = true;

			//ターゲット登録。
			Ui.GetInstance().SetTargetRequest(this);
		}

		/** [NEventPlate.OnOverCallBack_Base]OnOverLeave
		*/
		public void OnOverLeave(int a_value)
		{
			Tool.Log("Slider_Base","OnOverLeave : " + a_value.ToString());

			this.is_onover = false;
		}

		/** オンオーバー。取得。
		*/
		public bool IsOnOver()
		{
			return this.is_onover;
		}

		/** [NUi.OnTargetCallBack_Base]OnTarget
		*/
		public void OnTarget()
		{
			if(this.is_onover == false){
				//ターゲット解除。
				Ui.GetInstance().UnSetTargetRequest(this);
			}else{
				if(NInput.Mouse.GetInstance().left.down == true){
					float t_value = ((float)(NInput.Mouse.GetInstance().pos.x - this.rect.x)) / this.rect.w;
					if(t_value < 0.0f){
						t_value = 0.0f;
					}else if(t_value > 1.0f){
						t_value = 1.0f;
					}

					if(this.value != t_value){
						this.value = t_value;

						//コールバック。
						this.OnChangeValue();
					}
				}
			}
		}
	}
}

