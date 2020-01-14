

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
	/** PathList
	*/
	public class PathList
	{
		/** path_list
		*/
		private System.Collections.Generic.Dictionary<string,PathItem> path_list;

		/** constructor
		*/
		public PathList()
		{
			//path_list
			this.path_list = new System.Collections.Generic.Dictionary<string,PathItem>();
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.path_list.Clear();
		}

		/** パスアイテム。登録。
		*/
		public void Regist(string a_assetbundle_name,AssetBundlePathType a_assetbundle_pathtype,Fee.File.Path a_assetbundle_path)
		{
			if(this.path_list.ContainsKey(a_assetbundle_name) == false){
				this.path_list.Add(a_assetbundle_name,new PathItem(a_assetbundle_pathtype,a_assetbundle_path));
			}else{
				Tool.Assert(false);
			}
		}

		/** パスアイテム。解除。
		*/
		public bool UnRegist(string a_assetbundle_name)
		{
			if(this.path_list.Remove(a_assetbundle_name) == true){
				return true;
			}
			return false;
		}

		/** パスアイテム。取得。
		*/
		public PathItem GetItem(string a_assetbundle_name)
		{
			PathItem t_item;
			if(this.path_list.TryGetValue(a_assetbundle_name,out t_item) == true){
				return t_item;
			}
			return null;
		}
	}
}

