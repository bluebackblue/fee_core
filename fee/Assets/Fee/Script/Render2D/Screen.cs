using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。スクリーン。
*/


/** Render2D
*/
namespace NRender2D
{
	/** Screen
	*/
	public class Screen
	{
		/** ＧＵＩスクリーン。
		*/
		private Size2D<int> gui_size;
		private bool gui_change = false;

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

		/** ＵＩ再計算フラグ。
		*/
		private bool calc_ui_recalcflag = false;

		/** constructor
		*/
		public Screen()
		{
			//ＧＵＩスクリーン。
			this.gui_size.Set(1,1);
			this.gui_change = false;

			//事前計算。スプライト。
			this.calc_sprite_x = 0.0f;
			this.calc_sprite_y = 0.0f;
			this.calc_sprite_w = 0.0f;
			this.calc_sprite_h = 0.0f;

			//事前計算。ＵＩ。
			this.calc_ui_scale = 0.0f;
			this.calc_ui_x = 0.0f;
			this.calc_ui_y = 0.0f;

			//ＵＩ再計算フラグ。
			this.calc_ui_recalcflag = false;
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

		/** 事前計算。
		*/
		public void CalcScreen(List<Sprite2D> a_sprite_list)
		{
			//ＧＵＩスクリーン座標変更チェック。
			if((this.gui_size.w != UnityEngine.Screen.width)||(this.gui_size.h != UnityEngine.Screen.height)){
				this.gui_size.Set(UnityEngine.Screen.width, UnityEngine.Screen.height);
				this.gui_change = true;
			}else{
				this.gui_change = false;
			}

			//事前計算。
			if(this.gui_change == true){
				if(((float)Config.VIRTUAL_W / Config.VIRTUAL_H) < ((float)this.gui_size.w / this.gui_size.h)){
					//左右に余白。

					this.calc_sprite_w = (Config.VIRTUAL_H * this.gui_size.w) / this.gui_size.h;
					this.calc_sprite_h = Config.VIRTUAL_H;

					this.calc_sprite_x = (this.gui_size.w - ((float)this.gui_size.h * Config.VIRTUAL_W) / Config.VIRTUAL_H) / (2 * this.gui_size.w);
					this.calc_sprite_y = 0.0f;

					this.calc_ui_scale = (float)this.gui_size.h / Config.VIRTUAL_H;

					this.calc_ui_x = (this.calc_sprite_x - 0.5f) * this.gui_size.w;
					this.calc_ui_y = (0.5f - this.calc_sprite_y) * this.gui_size.h;

					this.calc_ui_recalcflag = true;
				}else{
					//上下に余白。

					this.calc_sprite_w = Config.VIRTUAL_W;
					this.calc_sprite_h = (Config.VIRTUAL_W * this.gui_size.h) / this.gui_size.w;

					this.calc_sprite_x = 0.0f;
					this.calc_sprite_y = (this.gui_size.h - ((float)this.gui_size.w * Config.VIRTUAL_H) / Config.VIRTUAL_W) / (2 * this.gui_size.h);

					this.calc_ui_scale = (float)this.gui_size.w / Config.VIRTUAL_W;

					this.calc_ui_x = (this.calc_sprite_x - 0.5f) * this.gui_size.w;
					this.calc_ui_y = (0.5f - this.calc_sprite_y) * this.gui_size.h;

					this.calc_ui_recalcflag = true;
				}

				for(int ii=0;ii<a_sprite_list.Count;ii++){
					a_sprite_list[ii].ResetSpritePositionCache();
				}
			}
		}

		/** ＵＩ再計算フラグ。取得。
		*/
		public bool IsUiReCalcFlag()
		{
			return this.calc_ui_recalcflag;
		}

		/** ＵＩ再計算フラグ。リセット。
		*/
		public void ResetUiReCalcFlag()
		{
			this.calc_ui_recalcflag = false;
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
		public void CalcSpritePosition(Sprite2D a_sprite,float[] a_to_8)
		{
			if(a_sprite.IsRotate() == false){
				a_sprite.CalcSpritePosition(a_to_8);
			}else{
				Vector2 t_center = new Vector2(a_sprite.GetCenterX(),a_sprite.GetCenterY());

				//左上。
				Vector2 t_1 = new Vector2(a_sprite.GetX(),a_sprite.GetY()) - t_center;

				//右上。
				Vector2 t_2 = new Vector2(a_sprite.GetX() + a_sprite.GetW(),a_sprite.GetY()) - t_center;

				//左下。
				Vector2 t_3 = new Vector2(a_sprite.GetX(),a_sprite.GetY() + a_sprite.GetH()) - t_center;

				//右下。
				Vector2 t_4 = new Vector2(a_sprite.GetX() + a_sprite.GetW(),a_sprite.GetY() + a_sprite.GetH()) - t_center;

				//回転。
				Quaternion t_quaternion = a_sprite.GetQuaternion();

				t_1 = t_quaternion * t_1;
				t_2 = t_quaternion * t_2;
				t_3 = t_quaternion * t_3;
				t_4 = t_quaternion * t_4;

				t_1 += t_center;
				t_2 += t_center;
				t_3 += t_center;
				t_4 += t_center;

				//左上。
				a_to_8[0] = t_1.x / this.calc_sprite_w + this.calc_sprite_x;
				a_to_8[1] = 1.0f - (t_1.y / this.calc_sprite_h + this.calc_sprite_y);

				//右上。
				a_to_8[2] = t_2.x / this.calc_sprite_w + this.calc_sprite_x;
				a_to_8[3] = 1.0f - (t_2.y / this.calc_sprite_h + this.calc_sprite_y);

				//左下。
				a_to_8[4] = t_3.x / this.calc_sprite_w + this.calc_sprite_x;
				a_to_8[5] = 1.0f - (t_3.y / this.calc_sprite_h + this.calc_sprite_y);

				//右下。
				a_to_8[6] = t_4.x / this.calc_sprite_w + this.calc_sprite_x;
				a_to_8[7] = 1.0f - (t_4.y / this.calc_sprite_h + this.calc_sprite_y);
			}
		}

		/** 計算。テキスト。
		*/
		public void CalcTextRect(Text2D a_text)
		{		
			float t_w = a_text.GetW();
			float t_h = a_text.GetH();

			if(t_w <= 0.0f){
				t_w = a_text.Raw_GetTextInstance().preferredWidth;
			}else{
				t_w *= this.calc_ui_scale;
			}

			if(t_h <= 0.0f){
				t_h = a_text.Raw_GetTextInstance().preferredHeight;
			}else{
				t_h *= this.calc_ui_scale;
			}

			float t_x = this.calc_ui_x + a_text.GetX() * this.calc_ui_scale;
			float t_y = this.calc_ui_y - a_text.GetY() * this.calc_ui_scale;

			if(a_text.IsCenter() == false){
				t_x += t_w / 2;
				t_y -= t_h / 2;
			}

			a_text.Raw_GetRectTransformInstance().sizeDelta = new Vector2(t_w,t_h);
			a_text.Raw_GetRectTransformInstance().localPosition = new Vector3(t_x,t_y,0.0f);
		}

		/** 計算。入力フィールド。
		*/
		public void CalcInputFieldRect(InputField2D a_inputfield)
		{
			float t_w = a_inputfield.GetW() * this.calc_ui_scale;
			float t_h = a_inputfield.GetH() * this.calc_ui_scale;

			float t_x = this.calc_ui_x + a_inputfield.GetX() * this.calc_ui_scale;
			float t_y = this.calc_ui_y - a_inputfield.GetY() * this.calc_ui_scale;

			if(a_inputfield.IsCenter() == false){
				t_x += t_w / 2;
				t_y -= t_h / 2;
			}

			a_inputfield.Raw_GetRectTransformInstance().sizeDelta = new Vector2(t_w,t_h);
			a_inputfield.Raw_GetRectTransformInstance().localPosition = new Vector3(t_x,t_y,0.0f);
		}
	}
}

