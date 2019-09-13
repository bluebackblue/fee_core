

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。アセットバンドルパックリスト。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** PackList
	*/
	public class PackList
	{
		/** pack_list
		*/
		private System.Collections.Generic.Dictionary<string,PackItem> pack_list;

		/** constructor
		*/
		public PackList()
		{
			//pack_list
			this.pack_list = new System.Collections.Generic.Dictionary<string,PackItem>();
		}

		/** 削除。
		*/
		public void Delete()
		{
			foreach(System.Collections.Generic.KeyValuePair<string,PackItem> t_pair in this.pack_list){
				t_pair.Value.Unload();
			}
			this.pack_list.Clear();
		}

		/** パックアイテム。取得。
		*/
		public PackItem GetItem(string a_assetbundle_name)
		{
			PackItem t_pack_item;
			if(this.pack_list.TryGetValue(a_assetbundle_name,out t_pack_item) == true){
				return t_pack_item;
			}

			return null;
		}

		/** パックアイテム。登録。
		*/
		public void Regist(string a_assetbundle_name,PackItem a_pack_item)
		{
			if(this.pack_list.ContainsKey(a_assetbundle_name) == false){
				this.pack_list.Add(a_assetbundle_name,a_pack_item);
			}else{
				Tool.Assert(false);
			}			
		}

		/** パックアイテム。解除。
		*/
		public void UnRegist(string a_assetbundle_name)
		{
			PackItem t_item;
			if(this.pack_list.TryGetValue(a_assetbundle_name,out t_item) == true){
				if(t_item != null){
					t_item.Unload();
				}else{
					Tool.Assert(false);
				}
			}else{
				Tool.Assert(false);
			}
		}
	}
}

