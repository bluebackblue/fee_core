

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。アセットバンドルリスト。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** AssetBundleItem_List
	*/
	public class AssetBundleItem_List
	{
		/** list
		*/
		private System.Collections.Generic.Dictionary<string,AssetBundleItem> list;

		/** constructor
		*/
		public AssetBundleItem_List()
		{
			//list
			this.list = new System.Collections.Generic.Dictionary<string,AssetBundleItem>();
		}

		/** 削除。
		*/
		public void Delete()
		{
			foreach(System.Collections.Generic.KeyValuePair<string,AssetBundleItem> t_pair in this.list){
				t_pair.Value.Unload();
			}
			this.list.Clear();
		}

		/** アセットバンドルアイテム。取得。
		*/
		public AssetBundleItem GetItem(string a_assetbundle_name)
		{
			AssetBundleItem t_assetbundle_item;
			if(this.list.TryGetValue(a_assetbundle_name,out t_assetbundle_item) == true){
				return t_assetbundle_item;
			}

			return null;
		}

		/** アセットバンドルアイテム。登録。
		*/
		public void Regist(string a_assetbundle_name,AssetBundleItem a_assetbundle_item)
		{
			if(this.list.ContainsKey(a_assetbundle_name) == false){
				this.list.Add(a_assetbundle_name,a_assetbundle_item);
			}else{
				Tool.Assert(false);
			}			
		}

		/** アセットバンドルアイテム。解除。
		*/
		public bool UnRegist(string a_assetbundle_name)
		{
			AssetBundleItem t_item;

			if(this.list.TryGetValue(a_assetbundle_name,out t_item) == true){
				if(t_item != null){
					t_item.Unload();
					this.list.Remove(a_assetbundle_name);
					return true;
				}
			}

			return false;
		}
	}
}

