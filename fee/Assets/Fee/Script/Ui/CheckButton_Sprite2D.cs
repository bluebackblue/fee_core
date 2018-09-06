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
	/** CheckButton_Sprite2D
	*/
	public class CheckButton_Sprite2D : NRender2D.Sprite2D
	{
		/** clip_rect
		*/
		NRender2D.Rect2D_R<int> clip_rect;

		/** mode
		*/
		CheckButton_Mode mode;

		/** constructor。
		*/
		public CheckButton_Sprite2D(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority)
			:
			base(a_deleter,a_state,a_drawpriority)
		{
			this.SetMaterialType(NRender2D.Config.MaterialType.Button);
			this.clip_rect.Set(-NRender2D.Render2D.VIRTUAL_W,-NRender2D.Render2D.VIRTUAL_H,NRender2D.Render2D.VIRTUAL_W*2,NRender2D.Render2D.VIRTUAL_H*2);
			this.mode = CheckButton_Mode.Normal;
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.clip_rect = a_rect;
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.clip_rect.x = a_x;
			this.clip_rect.y = a_y;
			this.clip_rect.w = a_w;
			this.clip_rect.h = a_h;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipX()
		{
			return this.clip_rect.x;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipY()
		{
			return this.clip_rect.y;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipW()
		{
			return this.clip_rect.w;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipH()
		{
			return this.clip_rect.h;
		}

		/** モード。設定。
		*/
		public void SetMode(CheckButton_Mode a_mode)
		{
			this.mode = a_mode;
		}

		/** マテリアルを更新する。

		戻り値 = true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public override bool UpdateMaterial(ref Material a_material)
		{
			bool t_setpass = false;

			Texture2D t_texture = this.GetTexture();

			if(a_material.mainTexture != t_texture){
				//テクスチャー設定。
				a_material.mainTexture = this.GetTexture();
				t_setpass = true;
			}

			//クリップ。
			{
				int t_gui_x1;
				int t_gui_y1;
				int t_gui_x2;
				int t_gui_y2;
				NRender2D.Render2D.GetInstance().VirtualScreenToGuiScreen(this.clip_rect.x,this.clip_rect.y + this.clip_rect.h,out t_gui_x1,out t_gui_y1);
				NRender2D.Render2D.GetInstance().VirtualScreenToGuiScreen(this.clip_rect.x + this.clip_rect.w,this.clip_rect.y,out t_gui_x2,out t_gui_y2);
				float t_clip_x1 = t_gui_x1;
				float t_clip_y1 = NRender2D.Render2D.GetInstance().GetGuiH() - t_gui_y1;
				float t_clip_x2 = t_gui_x2;
				float t_clip_y2 = NRender2D.Render2D.GetInstance().GetGuiH() - t_gui_y2;

				if(a_material.GetFloat("clip_x1") != t_clip_x1){
					a_material.SetFloat("clip_x1",t_clip_x1);
					t_setpass = true;
				}

				if(a_material.GetFloat("clip_y1") != t_clip_y1){
					a_material.SetFloat("clip_y1",t_clip_y1);
					t_setpass = true;
				}

				if(a_material.GetFloat("clip_x2") != t_clip_x2){
					a_material.SetFloat("clip_x2",t_clip_x2);
					t_setpass = true;
				}

				if(a_material.GetFloat("clip_y2") != t_clip_y2){
					a_material.SetFloat("clip_y2",t_clip_y2);
					t_setpass = true;
				}
			}

			//モード。
			{
				if(a_material.GetInt("mode") != (int)this.mode){
					a_material.SetInt("mode",(int)this.mode);
					t_setpass = true;
				}
			}

			//矩形サイズ。
			{
				int t_rect_x1;
				int t_rect_y1;
				int t_rect_x2;
				int t_rect_y2;
				NRender2D.Render2D.GetInstance().VirtualScreenToGuiScreen(this.GetX(),this.GetY(),out t_rect_x1,out t_rect_y1);
				NRender2D.Render2D.GetInstance().VirtualScreenToGuiScreen(this.GetX() + this.GetW(),this.GetY() + this.GetH(),out t_rect_x2,out t_rect_y2);
				float t_clip_w = t_rect_x2 - t_rect_x1;
				float t_clip_h = t_rect_y2 - t_rect_y1;

				if(a_material.GetFloat("rect_w") != t_clip_w){
					a_material.SetFloat("rect_w",t_clip_w);
					t_setpass = true;
				}

				if(a_material.GetFloat("rect_h") != t_clip_h){
					a_material.SetFloat("rect_h",t_clip_h);
					t_setpass = true;
				}
			}

			//SetPass要求。
			return t_setpass;
		}
	}
}

