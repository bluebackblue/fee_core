

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
	/** AssetBundlePathList_PathType
	*/
	public enum AssetBundlePathList_PathType
	{
		/** アセット。アセットバンドル。
		*/
		#if(UNITY_EDITOR)
		AssetsAssetBundle,
		#endif

		/** ＵＲＬ。アセットバンドル。
		*/
		UrlAssetBundle,

		/** アセット。ダミーアセットバンドル。
		*/
		AssetsDummyAssetBundle,
	}

	/** AssetBundlePathList_PathItem
	*/
	public class AssetBundlePathList_PathItem
	{
		/** pathtype
		*/
		public AssetBundlePathList_PathType pathtype;

		/** path
		*/
		public Fee.File.Path path;

		/** constructor
		*/
		public AssetBundlePathList_PathItem(AssetBundlePathList_PathType a_pathtype,Fee.File.Path a_path)
		{
			//pathtype
			this.pathtype = a_pathtype;

			//path
			this.path = a_path;
		}
	}

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
		public void Register(string a_id,AssetBundlePathList_PathType a_pathtype,Fee.File.Path a_path)
		{
			if(this.list.ContainsKey(a_id) == false){
				this.list.Add(a_id,new AssetBundlePathList_PathItem(a_pathtype,a_path));
			}else{
				Tool.Assert(false);
			}
		}

		/** 解除。
		*/
		public void UnRegister(string a_id)
		{
			if(this.list.ContainsKey(a_id) == true){
				this.list.Remove(a_id);
			}else{
				Tool.Assert(false);
			}
		}

		/** パスアイテム。取得。
		*/
		public AssetBundlePathList_PathItem GetPathItem(string a_id)
		{
			if(this.list.TryGetValue(a_id,out AssetBundlePathList_PathItem t_item) == true){
				return t_item;
			}

			Tool.Assert(false);
			return null;
		}
	}
}

