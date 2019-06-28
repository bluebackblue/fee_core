

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** AssetBundlePackList_AssetBundleItem
	*/
	public class AssetBundlePackList_AssetBundleItem
	{
		/** assetbundle
		*/
		public UnityEngine.AssetBundle assetbundle;

		/** constructor
		*/
		public AssetBundlePackList_AssetBundleItem(UnityEngine.AssetBundle a_assetbundle)
		{
			this.assetbundle = a_assetbundle;
		}

		/** アンロード。
		*/
		public void Unload()
		{
			if(this.assetbundle != null){
				this.assetbundle.Unload(false);
				this.assetbundle = null;
			}
		}
	}

	/** AssetBundlePackList
	*/
	public class AssetBundlePackList
	{
		/** list
		*/
		private System.Collections.Generic.Dictionary<string,AssetBundlePackList_AssetBundleItem> list;

		/** constructor
		*/
		public AssetBundlePackList()
		{
			//list
			this.list = new System.Collections.Generic.Dictionary<string,AssetBundlePackList_AssetBundleItem>();
		}

		/** アセットバンドルアイテム。取得。
		*/
		public AssetBundlePackList_AssetBundleItem GetAssetBundleItem(string a_id)
		{
			if(this.list.TryGetValue(a_id,out AssetBundlePackList_AssetBundleItem t_item) == true){
				return t_item;
			}

			Tool.Assert(false);
			return null;
		}

		/** 削除。
		*/
		public void Delete()
		{
			foreach(System.Collections.Generic.KeyValuePair<string,AssetBundlePackList_AssetBundleItem> t_pair in this.list){
				t_pair.Value.Unload();
			}
			this.list.Clear();
		}

		/** 登録。
		*/
		public void Register(string a_id,AssetBundlePackList_AssetBundleItem a_item)
		{
			if(this.list.ContainsKey(a_id) == false){
				this.list.Add(a_id,a_item);
			}else{
				Tool.Assert(false);
			}			
		}

		/** 解除。
		*/
		public void UnRegister(string a_id)
		{
			if(this.list.TryGetValue(a_id,out AssetBundlePackList_AssetBundleItem t_item) == true){
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

