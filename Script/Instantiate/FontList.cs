

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。フォントリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** FontList
	*/
	public class FontList
	{
		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.Font> list;

		/** constructor
		*/
		public FontList(FontList_MonoBehaviour a_list)
		{
			this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.Font>();
			for(int ii=0;ii<a_list.tag_list.Length;ii++){
				this.list.Add(a_list.tag_list[ii],a_list.font_list[ii]);
			}
		}

		/** GetFont
		*/
		public UnityEngine.Font GetFont(string a_tag)
		{
			UnityEngine.Font t_font;
			if(this.list.TryGetValue(a_tag,out t_font) == true){
				return t_font;
			}
			return null;
		}
	}
}

