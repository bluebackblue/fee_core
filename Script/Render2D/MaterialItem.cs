

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

		/** status
		*/
		private Fee.Material.Status material_status;

		/** main_texture
		*/
		private Fee.Material.Property_Texture main_texture;

		/** clip
		*/
		private Fee.Material.Property_Int clip_flag;
		private Fee.Material.Property_Float clip_x1;
		private Fee.Material.Property_Float clip_y1;
		private Fee.Material.Property_Float clip_x2;
		private Fee.Material.Property_Float clip_y2;

		/** corner
		*/
		private Fee.Material.Property_Int corner_size;

		/** texture
		*/
		private Fee.Material.Property_Float texture_x;
		private Fee.Material.Property_Float texture_y;
		private Fee.Material.Property_Float texture_w;
		private Fee.Material.Property_Float texture_h;

		/** rect
		*/
		private Fee.Material.Property_Float rect_w;
		private Fee.Material.Property_Float rect_h;

		/** constructor
		*/
		public MaterialItem(UnityEngine.Material a_material_raw,Fee.Material.Status a_material_status,bool a_duplicate)
		{
			//material_status
			this.material_status = a_material_status;

			//material
			if(a_duplicate == true){
				//複製。
				this.material = new UnityEngine.Material(a_material_raw);
			}else{
				//参照。
				this.material = a_material_raw;
			}

			//プロパティ初期化。
			string[] t_property_list = this.material_status.property_list;
			for(int ii=0;ii<t_property_list.Length;ii++){
				switch(t_property_list[ii]){
				case "clip_flag":
					{
						this.clip_flag.Initialize(this.material,t_property_list[ii]);
					}break;
				case "clip_x1":
					{
						this.clip_x1.Initialize(this.material,t_property_list[ii]);
					}break;
				case "clip_y1":
					{
						this.clip_y1.Initialize(this.material,t_property_list[ii]);
					}break;
				case "clip_x2":
					{
						this.clip_x2.Initialize(this.material,t_property_list[ii]);
					}break;
				case "clip_y2":
					{
						this.clip_y2.Initialize(this.material,t_property_list[ii]);
					}break;
				case "corner_size":
					{
						this.corner_size.Initialize(this.material,t_property_list[ii]);
					}break;
				case "texture_x":
					{
						this.texture_x.Initialize(this.material,t_property_list[ii]);
					}break;
				case "texture_y":
					{
						this.texture_y.Initialize(this.material,t_property_list[ii]);
					}break;
				case "texture_w":
					{
						this.texture_w.Initialize(this.material,t_property_list[ii]);
					}break;
				case "texture_h":
					{
						this.texture_h.Initialize(this.material,t_property_list[ii]);
					}break;
				case "rect_w":
					{
						this.rect_w.Initialize(this.material,t_property_list[ii]);
					}break;
				case "rect_h":
					{
						this.rect_h.Initialize(this.material,t_property_list[ii]);
					}break;
				case "_MainTex":
					{
						this.main_texture.Initialize(this.material,t_property_list[ii]);
					}break;
				default:
					{
						Tool.Log("Material_Item",this.material.name + " : " + t_property_list[ii]);
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
			MaterialItem t_material_item = new MaterialItem(this.material,this.material_status,true);
			return t_material_item;
		}

		/** マテリアルを設定する。、
		*/
		public void SetMaterialToInstance(UnityEngine.UI.Graphic a_instance)
		{
			a_instance.material = this.material;
		}

		/** マインテクスチャーインスタンス。比較。
		*/
		public bool CompareMainTextureInstance(UnityEngine.Texture2D a_texture)
		{
			if(a_texture == this.main_texture.GetValue()){
				return true;
			}
			return false;
		}

		/** SetPass
		*/
		public void SetPass(int a_pass)
		{
			this.material.SetPass(a_pass);
		}

		/** プロパティ。設定。
		*/
		public bool SetProperty_MainTexture(UnityEngine.Texture a_texture)
		{
			bool t_change = false;

			t_change |= this.main_texture.SetValue(a_texture);

			return t_change;
		}

		/** プロパティ。設定。

			return == true : 変更あり。

		*/
		public bool SetProperty_ClipFlag(int a_clip_flag)
		{
			bool t_change = false;

			t_change |= this.clip_flag.SetValue(a_clip_flag);

			return t_change;
		}

		/** プロパティ。設定。

			return == true : 変更あり。

		*/
		public bool SetProperty_ClipRectA(float a_clip_x1,float a_clip_y1,float a_clip_x2,float a_clip_y2)
		{
			bool t_change = false;

			//clip_x1
			t_change |= this.clip_x1.SetValue(a_clip_x1);

			//clip_y1
			t_change |= this.clip_y1.SetValue(a_clip_y1);

			//clip_x2
			t_change |= this.clip_x2.SetValue(a_clip_x2);

			//clip_y2
			t_change |= this.clip_y2.SetValue(a_clip_y2);

			return t_change;
		}

		/** プロパティ。設定。

			return == true : 変更あり。

		*/
		public bool SetProperty_CornerSize(int a_corner_size)
		{
			bool t_change = false;

			//corner_size
			t_change |= this.corner_size.SetValue(a_corner_size);

			return t_change;
		}

		/** プロパティ。設定。

			return == true : 変更あり。

		*/
		public bool SetProperty_TextureRerctR(float a_texture_x,float a_texture_y,float a_texture_w,float a_texture_h)
		{
			bool t_change = false;

			//texture_x
			t_change |= this.texture_x.SetValue(a_texture_x);

			//texture_y
			t_change |= this.texture_y.SetValue(a_texture_y);
			
			//texture_w
			t_change |= this.texture_w.SetValue(a_texture_w);
			
			//texture_h
			t_change |= this.texture_h.SetValue(a_texture_h);

			return t_change;
		}

		/** プロパティ。設定。

			return == true : 変更あり。

		*/
		public bool SetProperty_RerctWH(float a_rect_w,float a_rect_h)
		{
			bool t_change = false;

			//rect_w
			t_change |= this.rect_w.SetValue(a_rect_w);

			//rect_h
			t_change |= this.rect_h.SetValue(a_rect_h);

			return t_change;
		}
	}
}

