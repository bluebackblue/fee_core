

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
	/** MaterialList
	*/
	public class MaterialList
	{
		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.Material> list;

		/** constructor
		*/
		public MaterialList(MaterialList_MonoBehaviour a_list)
		{
			this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.Material>();
			for(int ii=0;ii<a_list.tag_list.Length;ii++){
				this.list.Add(a_list.tag_list[ii],a_list.material_list[ii]);
			}
		}

		/** GetMaterial
		*/
		public UnityEngine.Material GetMaterial(string a_tag)
		{
			UnityEngine.Material t_material;
			if(this.list.TryGetValue(a_tag,out t_material) == true){
				return t_material;
			}
			return null;
		}
	}
}

