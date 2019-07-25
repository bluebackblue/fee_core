

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。アセットバンドルパスリスト。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** AssetBundlePathList
	*/
	public class AssetBundlePathList
	{
		/** list
		*/
		private System.Collections.Generic.Dictionary<string,AssetBundlePathList_PathItem> list;

		/** constructor
		*/
		public AssetBundlePathList()
		{
			this.list = new System.Collections.Generic.Dictionary<string,AssetBundlePathList_PathItem>();
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.list.Clear();
		}

		/** 登録。
		*/
		public void Register(string a_assetbundle_name,AssetBundlePathList_PathType a_pathtype,Fee.File.Path a_path)
		{
			if(this.list.ContainsKey(a_assetbundle_name) == false){
				this.list.Add(a_assetbundle_name,new AssetBundlePathList_PathItem(a_pathtype,a_path));
			}else{
				Tool.Assert(false);
			}
		}

		/** 解除。
		*/
		public void UnRegister(string a_assetbundle_name)
		{
			if(this.list.ContainsKey(a_assetbundle_name) == true){
				this.list.Remove(a_assetbundle_name);
			}else{
				Tool.Assert(false);
			}
		}

		/** パスアイテム。取得。
		*/
		public AssetBundlePathList_PathItem GetPathItem(string a_assetbundle_name)
		{
			AssetBundlePathList_PathItem t_item;
			if(this.list.TryGetValue(a_assetbundle_name,out t_item) == true){
				return t_item;
			}

			Tool.Assert(false);
			return null;
		}
	}
}

