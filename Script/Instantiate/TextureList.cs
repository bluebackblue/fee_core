

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。テクスチャーリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** TextureList
	*/
	public class TextureList
	{
		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.Texture2D> list;

		/** constructor
		*/
		public TextureList(TextureList_MonoBehaviour a_list)
		{
			this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.Texture2D>();
			for(int ii=0;ii<a_list.tag_list.Length;ii++){
				this.list.Add(a_list.tag_list[ii],a_list.texture_list[ii]);
			}
		}

		/** GetTexture
		*/
		public UnityEngine.Texture2D GetTexture(string a_tag)
		{
			UnityEngine.Texture2D t_texture;
			if(this.list.TryGetValue(a_tag,out t_texture) == true){
				return t_texture;
			}
			return null;
		}
	}
}

