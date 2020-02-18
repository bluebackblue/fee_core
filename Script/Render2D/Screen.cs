

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。スクリーン。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Screen
	*/
	public class Screen
	{
		/** ＧＵＩスクリーン。
		*/
		private Fee.Geometry.Size2D<int> gui_size;

		/** change_screensize_flag
		*/
		private bool change_screensize_flag;

		/** 事前計算。スプライト。
		*/
		private float calc_sprite_x;
		private float calc_sprite_y;
		private float calc_sprite_w;
		private float calc_sprite_h;

		/** 事前計算。ＵＩ。
		*/
		private float calc_ui_scale;
		private float calc_ui_x;
		private float calc_ui_y;

		/** constructor
		*/
		public Screen()
		{
			//ＧＵＩスクリーン。
			this.gui_size.Set(1,1);

			//change_screensize_flag
			this.change_screensize_flag = true;

			//事前計算。スプライト。
			this.calc_sprite_x = 0.0f;
			this.calc_sprite_y = 0.0f;
			this.calc_sprite_w = 0.0f;
			this.calc_sprite_h = 0.0f;

			//事前計算。ＵＩ。
			this.calc_ui_scale = 0.0f;
			this.calc_ui_x = 0.0f;
			this.calc_ui_y = 0.0f;

			//事前計算。
			this.CalcScreen();
		}

		/** スクリーンサイズ変更フラグ。取得。
		*/
		public bool GetChangeScreenSizeFlag()
		{
			return this.change_screensize_flag;
		}

		/** スクリーンサイズ変更フラグ。設定。
		*/
		public void SetChangeScreenSizeFlag(bool a_flag)
		{
			this.change_screensize_flag = a_flag;
		}

		/** ＧＵＩスクリーン座標　＝＞　仮想スクリーン座標。
		*/
		public void GuiScreenToVirtualScreen(int a_gui_x,int a_gui_y,out int a_virtual_x,out int a_virtual_y)
		{
			a_virtual_x = (int)(((float)a_gui_x / this.gui_size.w - this.calc_sprite_x) * this.calc_sprite_w);
			a_virtual_y = (int)(((float)a_gui_y / this.gui_size.h - this.calc_sprite_y) * this.calc_sprite_h);
		}

		/** 仮想スクリーン座標　＝＞　ＧＵＩスクリーン座標。
		*/
		public void VirtualScreenToGuiScreen(int a_virtual_x,int a_virtual_y,out int a_gui_x,out int a_gui_y)
		{
			float t_xxx = this.calc_sprite_x * this.gui_size.w;
			float t_yyy = this.calc_sprite_y * this.gui_size.h;
			a_gui_x = (int)(t_xxx + a_virtual_x * (this.gui_size.w - t_xxx * 2) / Config.VIRTUAL_W);
			a_gui_y = (int)(t_yyy + a_virtual_y * (this.gui_size.h - t_yyy * 2) / Config.VIRTUAL_H);
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteX()
		{
			return this.calc_sprite_x;
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteY()
		{
			return this.calc_sprite_y;
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteW()
		{
			return this.calc_sprite_w;
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteH()
		{
			return this.calc_sprite_h;
		}

		/**GetScreenWidth
		*/
		public static int GetScreenWidth()
		{
			return UnityEngine.Screen.width;
		}

		/** GetScreenHeight
		*/
		public static int GetScreenHeight()
		{
			return UnityEngine.Screen.height;
		}

		/** 事前計算。
		*/
		public void CalcScreen()
		{
			//ＧＵＩスクリーン座標変更チェック。
			if((this.gui_size.w != Screen.GetScreenWidth())||(this.gui_size.h != Screen.GetScreenHeight())){

				//change_screensize_flag
				this.change_screensize_flag = true;

				//gui_size
				this.gui_size.Set(Screen.GetScreenWidth(),Screen.GetScreenHeight());

				//事前計算。
				if(((float)Config.VIRTUAL_W / Config.VIRTUAL_H) < ((float)this.gui_size.w / this.gui_size.h)){
					//左右に余白。

					this.calc_sprite_w = ((float)Config.VIRTUAL_H * (float)this.gui_size.w) / (float)this.gui_size.h;
					this.calc_sprite_h = Config.VIRTUAL_H;

					this.calc_sprite_x = ((float)this.gui_size.w - ((float)this.gui_size.h * (float)Config.VIRTUAL_W) / (float)Config.VIRTUAL_H) / (2 * (float)this.gui_size.w);
					this.calc_sprite_y = 0.0f;

					this.calc_ui_scale = (float)this.gui_size.h / (float)Config.VIRTUAL_H;

					this.calc_ui_x = (this.calc_sprite_x - 0.5f) * (float)this.gui_size.w;
					this.calc_ui_y = (0.5f - this.calc_sprite_y) * (float)this.gui_size.h;
				}else{
					//上下に余白。

					this.calc_sprite_w = Config.VIRTUAL_W;
					this.calc_sprite_h = ((float)Config.VIRTUAL_W * (float)this.gui_size.h) / (float)this.gui_size.w;

					this.calc_sprite_x = 0.0f;
					this.calc_sprite_y = ((float)this.gui_size.h - ((float)this.gui_size.w * (float)Config.VIRTUAL_H) / (float)Config.VIRTUAL_W) / (2 * (float)this.gui_size.h);

					this.calc_ui_scale = (float)this.gui_size.w / (float)Config.VIRTUAL_W;

					this.calc_ui_x = (this.calc_sprite_x - 0.5f) * (float)this.gui_size.w;
					this.calc_ui_y = (0.5f - this.calc_sprite_y) * (float)this.gui_size.h;
				}
			}
		}

		/** GetGuiW
		*/
		public int GetGuiW()
		{
			return this.gui_size.w;
		}

		/** GetGuiH
		*/
		public int GetGuiH()
		{
			return this.gui_size.h;
		}

		/** 計算。フォントサイズ。
		*/
		public int CalcFontSize(Text2D a_text)
		{
			return (int)(a_text.GetFontSize() * this.calc_ui_scale);
		}

		/** 計算。フォントサイズ。
		*/
		public int CalcFontSize(InputField2D a_inputfield)
		{
			return (int)(a_inputfield.GetFontSize() * this.calc_ui_scale);
		}

		/** 計算。スプライト。
		*/
		public void CalcSprite(Sprite2D a_sprite)
		{
			a_sprite.CalcVertex(this.calc_sprite_x,this.calc_sprite_y,this.calc_sprite_w,this.calc_sprite_h);
		}

		/** 計算。テキスト。
		*/
		public void CalcTextRect(Text2D a_text)
		{
			//サイズ計算。
			if(a_text.Raw_IsCalcSize() == true){
				a_text.Raw_SetCalcSizeFlag(false);

				int t_w = a_text.GetW();
				int t_h = a_text.GetH();

				UnityEngine.Vector2 t_sizedelta;

				if(t_w > 0){
					t_sizedelta.x =	t_w * this.calc_ui_scale;
				}else{
					t_sizedelta.x = Screen.GetScreenWidth();
				}

				if(t_h > 0){
					t_sizedelta.y = t_h * this.calc_ui_scale;
				}else{
					t_sizedelta.y = Screen.GetScreenHeight();
				}

				//自動部分を最大設定。
				a_text.Raw_SetRectTransformSizeDelta(in t_sizedelta);

				if((t_w <= 0)||(t_h <= 0)){

					if(t_w <= 0){
						t_sizedelta.x = a_text.Raw_GetPreferredWidth();
					}

					if(t_h <= 0){
						t_sizedelta.y = a_text.Raw_GetPreferredHeight();
					}

					//自動部分再設定。
					a_text.Raw_SetRectTransformSizeDelta(in t_sizedelta);
				}
			}

			//位置計算。
			UnityEngine.Vector3 t_localposition = new UnityEngine.Vector3(this.calc_ui_x + a_text.GetX() * this.calc_ui_scale,this.calc_ui_y - a_text.GetY() * this.calc_ui_scale,0.0f);
			if((a_text.GetAlignmentTypeX() != Text2D_HorizontalAlignmentType.Center)||(a_text.GetAlignmentTypeY() != Text2D_VerticalAlignmentType.Middle)){
				//計算済みサイズ取得。
				UnityEngine.Vector2 t_sizedelta;
				a_text.Raw_GetRectTransformSizeDelta(out t_sizedelta);

				switch(a_text.GetAlignmentTypeX()){
				case Text2D_HorizontalAlignmentType.Right:
					{
						t_localposition.x -= t_sizedelta.x / 2;
					}break;
				case Text2D_HorizontalAlignmentType.Left:
					{
						t_localposition.x += t_sizedelta.x / 2;
					}break;
				}

				switch(a_text.GetAlignmentTypeY()){
				case Text2D_VerticalAlignmentType.Bottom:
					{
						t_localposition.y += t_sizedelta.y / 2;
					}break;
				case Text2D_VerticalAlignmentType.Top:
					{
						t_localposition.y -= t_sizedelta.y / 2;
					}break;
				}
			}
			a_text.Raw_SetRectTransformLocalPosition(in t_localposition);
		}

		/** 計算。入力フィールド。
		*/
		public void CalcInputFieldRect(InputField2D a_inputfield)
		{
			//サイズ計算。
			{
				UnityEngine.Vector2 t_sizedelta = new UnityEngine.Vector2(a_inputfield.GetW() * this.calc_ui_scale,a_inputfield.GetH() * this.calc_ui_scale);
				a_inputfield.Raw_SetRectTransformSizeDelta(in t_sizedelta);
			}

			//位置計算。
			UnityEngine.Vector3 t_localposition = new UnityEngine.Vector3(this.calc_ui_x + a_inputfield.GetX() * this.calc_ui_scale,this.calc_ui_y - a_inputfield.GetY() * this.calc_ui_scale,0.0f);
			{
				//計算済みサイズ取得。
				UnityEngine.Vector2 t_sizedelta;
				a_inputfield.Raw_GetRectTransformSizeDelta(out t_sizedelta);

				t_localposition.x += t_sizedelta.x / 2;
				t_localposition.y -= t_sizedelta.y / 2;
			}
			a_inputfield.Raw_SetRectTransformLocalPosition(in t_localposition);
		}
	}
}

