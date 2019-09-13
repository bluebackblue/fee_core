

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。クリップスプライト。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Sprite2D_Clip
	*/
	public class Sprite2D_Clip : Fee.Render2D.Sprite2D_Mapping , Fee.Render2D.OnSprite2DMaterialUpdate_CallBackInterface
	{
		/** is_clip
		*/
		private bool is_clip;

		/** clip_rect
		*/
		private Fee.Geometry.Rect2D_R<int> clip_rect;

		/** constructor
		*/
		public Sprite2D_Clip(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
			:
			base(Fee.Render2D.Render2D.GetInstance().Sprite2D_PoolNew(a_deleter,a_drawpriority))
		{
			//is_clip
			this.is_clip = false;

			//clip_rect
			this.clip_rect.Set(0,0,0,0);

			//マテリアル設定。
			this.sprite.SetMaterialType(Fee.Render2D.Config.MaterialType.AlphaClip);
			this.sprite.SetOnSprite2DMaterialUpdate(this);
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
		public void SetClipRect(in Fee.Geometry.Rect2D_R<int> a_rect)
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

		/** マテリアルアイテムを更新する。

			return == true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public bool OnSprite2DMaterialUpdate(Fee.Render2D.Sprite2D a_sprite2d,Fee.Render2D.Material_Item a_material_item)
		{
			bool t_setpass = false;

			//メインテクスチャー設定。
			if(a_material_item.SetProperty_MainTexture(a_sprite2d.GetTexture()) == true){
				t_setpass = true;
			}

			//クリップ。
			{
				int t_gui_x1;
				int t_gui_y1;
				int t_gui_x2;
				int t_gui_y2;
				Fee.Render2D.Render2D.GetInstance().VirtualScreenToGuiScreen(this.clip_rect.x,this.clip_rect.y + this.clip_rect.h,out t_gui_x1,out t_gui_y1);
				Fee.Render2D.Render2D.GetInstance().VirtualScreenToGuiScreen(this.clip_rect.x + this.clip_rect.w,this.clip_rect.y,out t_gui_x2,out t_gui_y2);
				float t_clip_x1 = t_gui_x1;
				float t_clip_y1 = t_gui_y1;
				float t_clip_x2 = t_gui_x2;
				float t_clip_y2 = t_gui_y2;

				int t_clip_flag = 0;
				if(this.is_clip == true){
					t_clip_flag = 1;
				}

				//clip_flag
				if(a_material_item.SetProperty_ClipFlag(t_clip_flag) == true){
					t_setpass = true;
				}

				if(t_clip_flag > 0){
					if(a_material_item.SetProperty_ClipRectA(t_clip_x1,t_clip_y1,t_clip_x2,t_clip_y2) == true){
						t_setpass = true;
					}
				}
			}

			//SetPass要求。
			return t_setpass;
		}
	}
}

