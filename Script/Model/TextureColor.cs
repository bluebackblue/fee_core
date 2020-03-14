

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief モデル。テクスチャーカラー。
*/


/** Fee.Model
*/
namespace Fee.Model
{
	/** TextureColor
	*/
	public class TextureColor
	{
		/** texture
		*/
		public UnityEngine.Texture2D texture;

		/** color_list
		*/
		public UnityEngine.Color[] color_list;

		/** width
		*/
		public int width;

		/** height
		*/
		public int height;

		/** Mode
		*/
		public enum Mode
		{
			/** Get
			*/
			Get,

			/** Set
			*/
			Set,
		}

		/** constructor
		*/
		public TextureColor(UnityEngine.Texture2D a_texture,Mode a_mode)
		{
			//texture
			this.texture = a_texture;

			//width
			this.width = this.texture.width;

			//height
			this.height = this.texture.height;

			//color_list
			if(a_mode == Mode.Get){
				this.color_list = this.texture.GetPixels();
			}else{
				this.color_list = new UnityEngine.Color[this.width * this.height];
			}
		}

		/** ColorToNormal
		*/
		public static UnityEngine.Vector3 ColorToNormal(in UnityEngine.Color a_color)
		{
			UnityEngine.Vector3 t_normal;
			t_normal.x = a_color.a * 2 - 1.0f;
			t_normal.y = a_color.b * 2 - 1.0f;
			t_normal.z = UnityEngine.Mathf.Sqrt(1 - t_normal.x * t_normal.x - t_normal.y * t_normal.y);
			t_normal.Normalize();
			return t_normal;
		}

		/** NormalToColor
		*/
		public static UnityEngine.Color NormalToColor(in UnityEngine.Vector3 a_normal)
		{
			UnityEngine.Color t_color;
			{
				t_color.r = a_normal.x * 0.5f + 0.5f;
				t_color.g = a_normal.y * 0.5f + 0.5f;
				t_color.b = a_normal.z * 0.5f + 0.5f;
				t_color.a = 1.0f;
			}

			return t_color;
		}

		/** SetPixelApply
		*/
		public void SetPixelApply()
		{
			this.texture.SetPixels(this.color_list);
		}

		/** SetColor
		*/
		public void SetColor(int a_x,int a_y,UnityEngine.Color a_color)
		{
			this.color_list[this.width * a_y + a_x] = a_color;
		}

		/** GetColor
		*/
		public UnityEngine.Color GetColor(int a_x,int a_y)
		{
			return this.color_list[this.width * a_y + a_x];
		}

		/** GetColor
		*/
		public UnityEngine.Color GetColor(float a_x_per,float a_y_per)
		{
			int t_x = (int)(a_x_per * this.width);
			int t_y = (int)(a_y_per * this.height);
			t_x = UnityEngine.Mathf.Clamp(t_x,0,this.width - 1);
			t_y = UnityEngine.Mathf.Clamp(t_y,0,this.height - 1);
			return this.GetColor(t_x,t_y);
		}
	}
}

