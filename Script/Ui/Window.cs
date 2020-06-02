

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。ウィンドウ。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Window
	*/
	public class Window : Window_Base , Fee.Deleter.OnDelete_CallBackInterface , Fee.EventPlate.OnEventPlateOver_CallBackInterface<int> , Fee.Ui.OnTarget_CallBackInterface
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
		private Fee.Geometry.Pos2D<int> downpos;

		/** constructor
		*/
		public static Window Create(Fee.Deleter.Deleter a_deleter,OnWindow_CallBackInterface a_callback_interface)
		{
			//Window t_this = Fee.Ui.Ui.GetInstance().GetPoolList_Window().PoolNew();
			Window t_this = new Window();
			{
				//プールから作成。
				t_this.InitializeFromPool(a_callback_interface);

				//bg_sprite
				t_this.bg_sprite = Fee.Render2D.Sprite2D.Create(null,0);
				t_this.bg_sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_this.bg_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
				t_this.bg_sprite.SetColor(0.0f,0.0f,0.0f,1.0f);

				//titlebar
				t_this.titlebar = Fee.Render2D.Sprite2D.Create(null,0);
				t_this.titlebar.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_this.titlebar.SetTexture(UnityEngine.Texture2D.whiteTexture);
				t_this.titlebar.SetColor(0.2f,0.2f,0.2f,1.0f);

				//titlebar_h
				t_this.titlebar_h = 20;

				//blockitem
				t_this.blockitem = new Fee.EventPlate.BlockItem(null,0,EventPlate.EventTypeMask.NotWindow);

				//bg_eventplate
				t_this.bg_eventplate = new EventPlate.Item(null,EventPlate.EventType.Window,0);
				t_this.bg_eventplate.SetOnEventPlateOver(t_this,0);

				//titlebar_eventplate
				t_this.titlebar_eventplate = new Fee.EventPlate.Item(null,Fee.EventPlate.EventType.Button,0);
				t_this.titlebar_eventplate.SetOnEventPlateOver(t_this,1);

				//is_onover_bg
				t_this.is_onover_bg = false;

				//is_onover_titlebar
				t_this.is_onover_titlebar = false;

				//is_drag
				t_this.is_drag = false;

				//downpos
				t_this.downpos.Set(0,0);

				if(a_deleter != null){
					a_deleter.Regist(t_this);
				}
			}
			return t_this;
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			//OnDelete
			this.bg_sprite.OnDelete();
			this.titlebar.OnDelete();
			this.blockitem.OnDelete();
			this.bg_eventplate.OnDelete();
			this.titlebar_eventplate.OnDelete();

			//プールへ削除。
			this.DeleteToPool();

			//プールに変換。
			//Fee.Ui.Ui.GetInstance().GetPoolList_Window().PoolDelete(this);
		}

		/** 色。設定。
		*/
		public void SetBgColor(in UnityEngine.Color a_color)
		{
			this.bg_sprite.SetColor(in a_color);
		}

		/** マテリアルタイプ。設定。
		*/
		public void SetBgMaterialType(Render2D.Config.MaterialType a_material_type)
		{
			this.bg_sprite.SetMaterialType(a_material_type);
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

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートに入場。
		*/
		public void OnEventPlateEnter(int a_id)
		{
			//ターゲット登録。
			Ui.GetInstance().SetTargetRequest(this);

			if(a_id == 0){
				this.is_onover_bg = true;
			}else{
				this.is_onover_titlebar = true;
			}
		}

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートから退場。
		*/
		public void OnEventPlateLeave(int a_id)
		{
			if(a_id == 0){
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
			long t_drawpriority = this.layerindex * Fee.Render2D.Config.DRAWPRIORITY_STEP;

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
			this.bg_sprite.SetRect(in this.rect);

			//blockitem
			this.blockitem.SetRect(in this.rect);

			//bg_eventplate
			this.bg_eventplate.SetRect(in this.rect);

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

		/** [Fee.Ui.OnTarget_CallBackInterface]ターゲット中。
		*/
		public void OnTarget()
		{
			if((this.is_onover_titlebar == true)&&(this.is_drag == false)&&(Fee.Input.Input.GetInstance().mouse.left.down == true)){
				//ドラッグ開始。

				//ウィンドウを最前面にする。
				Fee.Ui.Ui.GetInstance().SetWindowPriorityTopMost(this);

				int t_x = Fee.Input.Input.GetInstance().mouse.cursor.GetX() - this.rect.x;
				int t_y = Fee.Input.Input.GetInstance().mouse.cursor.GetY() - this.rect.y;
				this.downpos.Set(t_x,t_y);

				this.is_drag = true;
			}else if((this.is_drag == true)&&(Fee.Input.Input.GetInstance().mouse.left.on == false)){
				//ドラッグ終了。
				this.is_drag = false;
			}else if(this.is_drag == true){
				//ドラッグ中。

				int t_x = Fee.Input.Input.GetInstance().mouse.cursor.GetX() - this.downpos.x;
				int t_y = Fee.Input.Input.GetInstance().mouse.cursor.GetY() - this.downpos.y;
				this.SetXY(t_x,t_y);
			}else if(this.is_onover_titlebar == true){
				//ドラッグ開始待ち。
			}else if(this.is_onover_bg == true){
				if(Fee.Input.Input.GetInstance().mouse.left.down == true){
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

