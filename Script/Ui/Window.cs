using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。ウィンドウ。
*/


/** NUi
*/
namespace NUi
{
	/** Window
	*/
	public class Window : Window_Base , NDeleter.DeleteItem_Base , NEventPlate.OnOverCallBack_Base , NUi.OnTargetCallBack_Base
	{
		/** bg_sprite
		*/
		private NRender2D.Sprite2D bg_sprite;

		/** titlebar
		*/
		private NRender2D.Sprite2D titlebar;

		/** titlebar_h
		*/
		private int titlebar_h;

		/** eventplate_titlebar
		*/
		private NEventPlate.Item eventplate_titlebar;

		/** is_onover
		*/
		private bool is_onover;

		/** is_drag
		*/
		private bool is_drag;

		/** downpos
		*/
		private NRender2D.Pos2D<int> downpos;

		/** constructor
		*/
		public Window(NDeleter.Deleter a_deleter,OnWindowCallBack_Base a_callback)
			:
			base(a_deleter,a_callback)
		{
			//bg_sprite
			this.bg_sprite = new NRender2D.Sprite2D(this.deleter,null,0);
			this.bg_sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.bg_sprite.SetTexture(Texture2D.whiteTexture);
			this.bg_sprite.SetColor(0.0f,0.0f,0.0f,1.0f);

			//titlebar
			this.titlebar = new NRender2D.Sprite2D(this.deleter,null,0);
			this.titlebar.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.titlebar.SetTexture(Texture2D.whiteTexture);
			this.titlebar.SetColor(0.2f,0.2f,0.2f,1.0f);

			//titlebar_h
			this.titlebar_h = 20;

			//eventplate_titlebar
			this.eventplate_titlebar = new NEventPlate.Item(this.deleter,NEventPlate.EventType.Button,0);
			this.eventplate_titlebar.SetOnOverCallBack(this);

			//is_onover
			this.is_onover = false;

			//is_drag
			this.is_drag = false;

			//downpos
			this.downpos.Set(0,0);
		}

		/** GetTitleBarDrawPriorityOffset
		*/
		public long GetTitleBarDrawPriorityOffset()
		{
			return 1;
		}

		/** GetTitleBarH
		*/
		public int GetTitleBarH()
		{
			return this.titlebar_h;
		}

		/** [NEventPlate.OnOverCallBack_Base]イベントプレートに入場。
		*/
		public void OnOverEnter(int a_value)
		{
			//ターゲット登録。
			Ui.GetInstance().SetTargetRequest(this);

			this.is_onover = true;
		}

		/** [NEventPlate.OnOverCallBack_Base]イベントプレートから退場。
		*/
		public void OnOverLeave(int a_value)
		{
			this.is_onover = false;
		}

		/** [Window_Base]コールバック。削除。
		*/
		protected override void OnDelete_FromBase()
		{
			//ターゲット解除。
			Ui.GetInstance().UnSetTargetRequest(this);
		}

		/** [Window_Base]コールバック。レイヤーインデックス変更。
		*/
		protected override void OnChangeLayerIndex_FromBase()
		{
			//drawpriority
			long t_drawpriority = this.layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

			//bg_sprite
			this.bg_sprite.SetDrawPriority(t_drawpriority);

			//titlebar
			this.titlebar.SetDrawPriority(t_drawpriority + this.GetTitleBarDrawPriorityOffset());

			//eventplate_titlebar
			this.eventplate_titlebar.SetPriority(t_drawpriority + this.GetTitleBarDrawPriorityOffset());
		}

		/** [WindowBase]コールバック。矩形変更。
		*/
		protected override void OnChangeRect_FromBase()
		{
			//bg_sprite
			this.bg_sprite.SetRect(ref this.rect);

			//titlebar
			this.titlebar.SetRect(this.rect.x,this.rect.y,this.rect.w,this.titlebar_h);

			//eventplate_titlebar
			this.eventplate_titlebar.SetRect(this.rect.x,this.rect.y,this.rect.w,this.titlebar_h);
		}

		/** [WindowBase]コールバック。矩形変更。
		*/
		protected override void OnChangeXY_FromBase()
		{
			//bg_sprite
			this.bg_sprite.SetXY(this.rect.x,this.rect.y);

			//titlebar
			this.titlebar.SetXY(this.rect.x,this.rect.y);

			//eventplate_titlebar
			this.eventplate_titlebar.SetXY(this.rect.x,this.rect.y);
		}

		/** [NUi.OnTargetCallBack_Base]OnTarget
		*/
		public void OnTarget()
		{
			if((this.is_onover == true)&&(this.is_drag == false)&&(NInput.Mouse.GetInstance().left.down == true)){
				//ドラッグ開始。

				//ウィンドウを最前面にする。
				NUi.Ui.GetInstance().SetWindowPriorityTopMost(this);

				int t_x = NInput.Mouse.GetInstance().pos.x - this.rect.x;
				int t_y = NInput.Mouse.GetInstance().pos.y - this.rect.y;
				this.downpos.Set(t_x,t_y);

				this.is_drag = true;
			}else if((this.is_drag == true)&&(NInput.Mouse.GetInstance().left.on == false)){
				//ドラッグ終了。
				this.is_drag = false;
			}else if(this.is_drag == true){
				//ドラッグ中。

				int t_x = NInput.Mouse.GetInstance().pos.x - this.downpos.x;
				int t_y = NInput.Mouse.GetInstance().pos.y - this.downpos.y;
				this.SetXY(t_x,t_y);
			}else if(this.is_onover == true){
				//ドラッグ開始待ち。
			}else{
				//ターゲット解除。
				Ui.GetInstance().UnSetTargetRequest(this);
			}
		}
	}
}

