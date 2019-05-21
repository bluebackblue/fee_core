

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。ウィンドウ。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Window
	*/
	public class Window : Window_Base , Fee.Deleter.DeleteItem_Base , Fee.EventPlate.OnOverCallBack_Base , Fee.Ui.OnTargetCallBack_Base
	{
		/** bg_sprite
		*/
		private Fee.Render2D.Sprite2D bg_sprite;

		/** titlebar
		*/
		private Fee.Render2D.Sprite2D titlebar;

		/** titlebar_h
		*/
		private int titlebar_h;

		/** blockitem
		*/
		private Fee.EventPlate.BlockItem blockitem;

		/** bg_eventplate
		*/
		private Fee.EventPlate.Item bg_eventplate;

		/** eventplate_titlebar
		*/
		private Fee.EventPlate.Item titlebar_eventplate;

		/** is_onover_bg
		*/
		private bool is_onover_bg;

		/** is_onover_titlebar
		*/
		private bool is_onover_titlebar;

		/** is_drag
		*/
		private bool is_drag;

		/** downpos
		*/
		private Fee.Render2D.Pos2D<int> downpos;

		/** constructor
		*/
		public Window(Fee.Deleter.Deleter a_deleter,OnWindowCallBack_Base a_callback)
			:
			base(a_deleter,a_callback)
		{
			//bg_sprite
			this.bg_sprite = new Fee.Render2D.Sprite2D(this.deleter,0);
			this.bg_sprite.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.bg_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg_sprite.SetColor(0.0f,0.0f,0.0f,1.0f);

			//titlebar
			this.titlebar = new Fee.Render2D.Sprite2D(this.deleter,0);
			this.titlebar.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.titlebar.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.titlebar.SetColor(0.2f,0.2f,0.2f,1.0f);

			//titlebar_h
			this.titlebar_h = 20;

			//blockitem
			this.blockitem = new Fee.EventPlate.BlockItem(this.deleter,0,EventPlate.EventTypeMask.NotWindow);

			//bg_eventplate
			this.bg_eventplate = new EventPlate.Item(this.deleter,EventPlate.EventType.Window,0);
			this.bg_eventplate.SetOnOverCallBack(this);
			this.bg_eventplate.SetOnOverCallBackValue(0);

			//titlebar_eventplate
			this.titlebar_eventplate = new Fee.EventPlate.Item(this.deleter,Fee.EventPlate.EventType.Button,0);
			this.titlebar_eventplate.SetOnOverCallBack(this);
			this.titlebar_eventplate.SetOnOverCallBackValue(1);

			//is_onover_bg
			this.is_onover_bg = false;

			//is_onover_titlebar
			this.is_onover_titlebar = false;

			//is_drag
			this.is_drag = false;

			//downpos
			this.downpos.Set(0,0);
		}

		/** 色。設定。
		*/
		public void SetBgColor(ref UnityEngine.Color a_color)
		{
			this.bg_sprite.SetColor(ref a_color);
		}

		/** マテリアルタイプ。設定。
		*/
		public void SetBgMaterialType(Render2D.Config.MaterialType a_material)
		{
			this.bg_sprite.SetMaterialType(a_material);
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

		/** [Fee.EventPlateOnOverCallBack_Base]イベントプレートに入場。
		*/
		public void OnOverEnter(int a_value)
		{
			//ターゲット登録。
			Ui.GetInstance().SetTargetRequest(this);

			if(a_value == 0){
				this.is_onover_bg = true;
			}else{
				this.is_onover_titlebar = true;
			}
		}

		/** [Fee.EventPlateOnOverCallBack_Base]イベントプレートから退場。
		*/
		public void OnOverLeave(int a_value)
		{
			if(a_value == 0){
				this.is_onover_bg = false;
			}else{
				this.is_onover_titlebar = false;
			}
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
			long t_drawpriority = this.layerindex * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;

			//bg_sprite
			this.bg_sprite.SetDrawPriority(t_drawpriority);

			//blockitem
			this.blockitem.SetPriority(t_drawpriority);

			//bg_eventplate
			this.bg_eventplate.SetPriority(t_drawpriority);

			//titlebar
			this.titlebar.SetDrawPriority(t_drawpriority + this.GetTitleBarDrawPriorityOffset());

			//titlebar_eventplate
			this.titlebar_eventplate.SetPriority(t_drawpriority + this.GetTitleBarDrawPriorityOffset());
		}

		/** [WindowBase]コールバック。矩形変更。
		*/
		protected override void OnChangeRect_FromBase()
		{
			//bg_sprite
			this.bg_sprite.SetRect(ref this.rect);

			//blockitem
			this.blockitem.SetRect(ref this.rect);

			//bg_eventplate
			this.bg_eventplate.SetRect(ref this.rect);

			//titlebar
			this.titlebar.SetRect(this.rect.x,this.rect.y,this.rect.w,this.titlebar_h);

			//titlebar_eventplate
			this.titlebar_eventplate.SetRect(this.rect.x,this.rect.y,this.rect.w,this.titlebar_h);
		}

		/** [WindowBase]コールバック。矩形変更。
		*/
		protected override void OnChangeXY_FromBase()
		{
			//bg_sprite
			this.bg_sprite.SetXY(this.rect.x,this.rect.y);

			//blockitem
			this.blockitem.SetXY(this.rect.x,this.rect.y);

			//bg_eventplate
			this.bg_eventplate.SetXY(this.rect.x,this.rect.y);

			//titlebar
			this.titlebar.SetXY(this.rect.x,this.rect.y);

			//titlebar_eventplate
			this.titlebar_eventplate.SetXY(this.rect.x,this.rect.y);
		}

		/** IsOnOver
		*/
		public bool IsOnOver()
		{
			return (this.is_onover_bg || this.is_onover_titlebar);
		}

		/** [Fee.Ui.OnTargetCallBack_Base]OnTarget
		*/
		public void OnTarget()
		{
			if((this.is_onover_titlebar == true)&&(this.is_drag == false)&&(Fee.Input.Mouse.GetInstance().left.down == true)){
				//ドラッグ開始。

				//ウィンドウを最前面にする。
				Fee.Ui.Ui.GetInstance().SetWindowPriorityTopMost(this);

				int t_x = Fee.Input.Mouse.GetInstance().pos.x - this.rect.x;
				int t_y = Fee.Input.Mouse.GetInstance().pos.y - this.rect.y;
				this.downpos.Set(t_x,t_y);

				this.is_drag = true;
			}else if((this.is_drag == true)&&(Fee.Input.Mouse.GetInstance().left.on == false)){
				//ドラッグ終了。
				this.is_drag = false;
			}else if(this.is_drag == true){
				//ドラッグ中。

				int t_x = Fee.Input.Mouse.GetInstance().pos.x - this.downpos.x;
				int t_y = Fee.Input.Mouse.GetInstance().pos.y - this.downpos.y;
				this.SetXY(t_x,t_y);
			}else if(this.is_onover_titlebar == true){
				//ドラッグ開始待ち。
			}else if(this.is_onover_bg == true){
				if(Fee.Input.Mouse.GetInstance().left.down == true){
					//ウィンドウを最前面にする。
					Fee.Ui.Ui.GetInstance().SetWindowPriorityTopMost(this);
				}
			}else{
				//ターゲット解除。
				Ui.GetInstance().UnSetTargetRequest(this);
			}
		}
	}
}

