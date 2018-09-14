using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。クリップスプライト。
*/


/** NUi
*/
namespace NUi
{
	/** ClipSprite_Sprite2D
	*/
	public class ClipSprite_Sprite2D : NRender2D.Sprite2D
	{
		/** is_clip
		*/
		private bool is_clip;

		/** clip_rect
		*/
		private NRender2D.Rect2D_R<int> clip_rect;

		/** constructor。
		*/
		public ClipSprite_Sprite2D(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority)
			:
			base(a_deleter,a_state,a_drawpriority)
		{
			//is_clip
			this.is_clip = false;

			//clip_rect
			this.clip_rect.Set(0,0,0,0);

			//マテリアル設定。
			this.SetMaterialType(NRender2D.Config.MaterialType.AlphaClip);
		}

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			this.is_clip = a_flag;
		}

		/** クリップ。取得。
		*/
		public bool IsClip()
		{
			return this.is_clip;
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

		/** マテリアルを更新する。

		戻り値 = true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public override bool UpdateMaterial(ref Material a_material)
		{
			bool t_setpass = false;

			//テクスチャ設定。
			Texture2D t_texture = this.GetTexture();
			if(a_material.mainTexture != t_texture){
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

				int t_clip_flag = 0;
				if(this.is_clip == true){
					t_clip_flag = 1;
				}

				if(a_material.GetInt("clip_flag") != t_clip_flag){
					a_material.SetFloat("clip_flag",t_clip_flag);
					t_setpass = true;
				}

				if(t_clip_flag > 0){
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
			}

			//SetPass要求。
			return t_setpass;
		}
	}
}

