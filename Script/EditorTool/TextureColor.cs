

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。
*/


/** Fee.EditorTool
*/
#if(UNITY_EDITOR)
namespace Fee.EditorTool
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

			//color_list
			if(a_mode == Mode.Get){
				this.color_list = this.texture.GetPixels();
			}else{
				this.color_list = new UnityEngine.Color[this.texture.width * this.texture.height];
			}
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
			this.color_list[this.texture.width * a_y + a_x] = a_color;
		}

		/** GetColor
		*/
		public UnityEngine.Color GetColor(int a_x,int a_y)
		{
			return this.color_list[this.texture.width * a_y + a_x];
		}

		/** GetColor
		*/
		public UnityEngine.Color GetColor(float a_x_per,float a_y_per)
		{
			int t_x = (int)(a_x_per * this.texture.width);
			int t_y = (int)(a_y_per * this.texture.height);
			t_x = UnityEngine.Mathf.Clamp(t_x,0,this.texture.width - 1);
			t_y = UnityEngine.Mathf.Clamp(t_y,0,this.texture.height - 1);
			return this.GetColor(t_x,t_y);
		}

		/** SetColor
		*/
		public void SetColor(float a_x_per,float a_y_per,UnityEngine.Color a_color)
		{
			int t_x = (int)(a_x_per * this.texture.width);
			int t_y = (int)(a_y_per * this.texture.height);
			t_x = UnityEngine.Mathf.Clamp(t_x,0,this.texture.width - 1);
			t_y = UnityEngine.Mathf.Clamp(t_y,0,this.texture.height - 1);
			this.SetColor(t_x,t_y,a_color);
		}
	}
}
#endif

