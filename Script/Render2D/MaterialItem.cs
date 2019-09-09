

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。マテリアルアイテム。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** MaterialItem
	*/
	public class MaterialItem
	{
		/** material
		*/
		private UnityEngine.Material material;

		/** cache_texture
		*/
		private UnityEngine.Texture cache_texture;

		/** property_clip
		*/
		private int property_clip_flag;
		private float property_clip_x1;
		private float property_clip_y1;
		private float property_clip_x2;
		private float property_clip_y2;

		/** property_corner_size
		*/
		private int property_corner_size;

		/** property_texture
		*/
		private float property_texture_x;
		private float property_texture_y;
		private float property_texture_w;
		private float property_texture_h;

		/** property_rect
		*/
		private float property_rect_w;
		private float property_rect_h;

		/** constructor
		*/
		public MaterialItem(UnityEngine.Material a_material,bool a_duplicate)
		{
			//material
			if(a_duplicate == true){
				//複製。
				this.material = new UnityEngine.Material(a_material);
			}else{
				//参照。
				this.material = a_material;
			}

			//cache_texture
			this.cache_texture = this.material.mainTexture;

			//プロパティ初期値。取得。
			string[] t_property_list = this.material.GetTexturePropertyNames();
			for(int ii=0;ii<t_property_list.Length;ii++){
				switch(t_property_list[ii]){
				case "clip_flag":
					{
						this.property_clip_flag = this.material.GetInt(t_property_list[ii]);
					}break;
				case "clip_x1":
					{
						this.property_clip_x1 = this.material.GetFloat(t_property_list[ii]);
					}break;
				case "clip_y1":
					{
						this.property_clip_y1 = this.material.GetFloat(t_property_list[ii]);
					}break;
				case "clip_x2":
					{
						this.property_clip_x2 = this.material.GetFloat(t_property_list[ii]);
					}break;
				case "clip_y2":
					{
						this.property_clip_y2 = this.material.GetFloat(t_property_list[ii]);
					}break;
				case "corner_size":
					{
						this.property_corner_size = this.material.GetInt(t_property_list[ii]);
					}break;
				case "texture_x":
					{
						this.property_texture_x = this.material.GetFloat(t_property_list[ii]);
					}break;
				case "texture_y":
					{
						this.property_texture_y = this.material.GetFloat(t_property_list[ii]);
					}break;
				case "texture_w":
					{
						this.property_texture_w = this.material.GetFloat(t_property_list[ii]);
					}break;
				case "texture_h":
					{
						this.property_texture_h = this.material.GetFloat(t_property_list[ii]);
					}break;
				case "rect_w":
					{
						this.property_rect_w = this.material.GetFloat(t_property_list[ii]);
					}break;
				case "rect_h":
					{
						this.property_rect_h = this.material.GetFloat(t_property_list[ii]);
					}break;
				default:
					{
					}break;
				}
			}
		}

		/** 削除。
		*/
		public void DestroyImmediate()
		{
			UnityEngine.GameObject.DestroyImmediate(this.material);
			this.material = null;
		}

		/** マテリアルアイテム。複製。
		*/
		public MaterialItem DuplicateMaterialItem()
		{
			MaterialItem t_material_item = new MaterialItem(this.material,true);
			return t_material_item;
		}

		/** マテリアルを設定する。、
		*/
		public void SetMaterialToInstance(UnityEngine.UI.Graphic a_instance)
		{
			a_instance.material = this.material;
		}

		/** テクスチャインスタンス。比較。
		*/
		public bool CompareTextureInstance(UnityEngine.Texture2D a_texture)
		{
			if(a_texture == this.cache_texture){
				return true;
			}
			return false;
		}

		/** テクスチャー。設定。
		*/
		public void SetTexture(UnityEngine.Texture2D a_texture)
		{
			this.cache_texture = a_texture;
			this.material.mainTexture = a_texture;
		}

		/** SetPass
		*/
		public void SetPass(int a_pass)
		{
			this.material.SetPass(a_pass);
		}

		/** クリップフラグ。プロパティ設定。

			return == true : 変更あり。

		*/
		public bool SetProperty_ClipFlag(int a_value)
		{
			bool t_change = false;

			//clip_flag
			if(this.property_clip_flag != a_value){
				this.property_clip_flag = a_value;
				this.material.SetInt("clip_flag",a_value);
				t_change = true;
			}

			return t_change;
		}

		/** クリップ矩形。
		*/
		public bool SetProperty_ClipRect(float a_x1,float a_y1,float a_x2,float a_y2)
		{
			bool t_change = false;

			//clip_x1
			if(this.property_clip_x1 != a_x1){
				this.property_clip_x1 = a_x1;
				this.material.SetFloat("clip_x1",a_x1);
				t_change = true;
			}

			//clip_y1
			if(this.property_clip_y1 != a_y1){
				this.property_clip_y1 = a_y1;
				this.material.SetFloat("clip_y1",a_y1);
				t_change = true;
			}

			//clip_x2
			if(this.property_clip_x2 != a_x2){
				this.property_clip_x2 = a_x2;
				this.material.SetFloat("clip_x2",a_x2);
				t_change = true;
			}

			//clip_y2
			if(this.property_clip_y2 != a_y2){
				this.property_clip_y2 = a_y2;
				this.material.SetFloat("clip_y2",a_y2);
				t_change = true;
			}

			return t_change;
		}

		/** SetProperty_CornerSize
		*/
		public bool SetProperty_CornerSize(int a_corner_size)
		{
			bool t_change = false;

			//corner_size
			if(this.property_corner_size != a_corner_size){
				this.property_corner_size = a_corner_size;
				this.material.SetInt("corner_size",a_corner_size);
				t_change = true;
			}

			return t_change;
		}

		/** テクスチャー矩形。
		*/
		public bool SetProperty_TextureRerct(float a_texture_x,float a_texture_y,float a_texture_w,float a_texture_h)
		{
			bool t_change = false;

			//texture_x
			if(this.property_texture_x != a_texture_x){
				this.property_texture_x = a_texture_x;
				this.material.SetFloat("texture_x",a_texture_x);
				t_change = true;
			}

			//texture_y
			if(this.property_texture_y != a_texture_y){
				this.property_texture_y = a_texture_y;
				this.material.SetFloat("texture_y",a_texture_y);
				t_change = true;
			}

			//texture_w
			if(this.property_texture_w != a_texture_w){
				this.property_texture_w = a_texture_w;
				this.material.SetFloat("texture_w",a_texture_w);
				t_change = true;
			}

			//texture_h
			if(this.property_texture_h != a_texture_h){
				this.property_texture_h = a_texture_h;
				this.material.SetFloat("texture_h",a_texture_h);
				t_change = true;
			}

			return t_change;
		}

		/** 矩形。
		*/
		public bool SetProperty_RerctWH(float a_rect_w,float a_rect_h)
		{
			bool t_change = false;

			//rect_w
			if(this.property_rect_w != a_rect_w){
				this.property_rect_w = a_rect_w;
				this.material.SetFloat("rect_w",a_rect_w);
				t_change = true;
			}

			//rect_h
			if(this.property_rect_h != a_rect_h){
				this.property_rect_h = a_rect_h;
				this.material.SetFloat("rect_h",a_rect_h);
				t_change = true;
			}

			return t_change;
		}
	}
}

