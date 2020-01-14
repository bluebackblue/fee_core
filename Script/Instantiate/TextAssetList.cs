

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。マテリアルリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** TextAssetList
	*/
	public class TextAssetList
	{
		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.TextAsset> list;

		/** constructor
		*/
		public TextAssetList(TextAssetList_MonoBehaviour a_list)
		{
			this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.TextAsset>();
			for(int ii=0;ii<a_list.tag_list.Length;ii++){
				this.list.Add(a_list.tag_list[ii],a_list.textasset_list[ii]);
			}
		}

		/** GetTextAsset
		*/
		public UnityEngine.TextAsset GetTextAsset(string a_tag)
		{
			UnityEngine.TextAsset t_textasset;
			if(this.list.TryGetValue(a_tag,out t_textasset) == true){
				return t_textasset;
			}
			return null;
		}
	}
}

