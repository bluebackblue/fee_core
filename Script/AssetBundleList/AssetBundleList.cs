

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
	/** AssetBundleList
	*/
	public class AssetBundleList : Config
	{
		/** [シングルトン]s_instance
		*/
		private static AssetBundleList s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new AssetBundleList();
			}
		}

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static AssetBundleList GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** work_list
		*/
		private System.Collections.Generic.List<WorkItem> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<WorkItem> add_list;

		/** main_assetbundle
		*/
		private Main_AssetBundle main_assetbundle;

		/** main_asset
		*/
		private Main_Asset main_asset;

		/** pathlist
		*/
		private PathList pathlist;

		/** assetbundleitem_list
		*/
		private AssetBundleItem_List assetbundleitem_list;

		/** [シングルトン]constructor
		*/
		private AssetBundleList()
		{
			//work_list
			this.work_list = new System.Collections.Generic.List<WorkItem>();

			//add_list
			this.add_list = new System.Collections.Generic.List<WorkItem>();

			//main_assetbundle
			this.main_assetbundle = new Main_AssetBundle();

			//main_asset
			this.main_asset = new Main_Asset();

			//pathlist
			this.pathlist = new PathList();

			//assetbundleitem_list
			this.assetbundleitem_list = new AssetBundleItem_List();
		}

		/** パスアイテム。登録。

			アセットバンドルのパス。アセットバンドル名を設定する。

		*/
		public void RegistPathItem(string a_assetbundle_name,AssetBundlePathType a_assetbundle_pathtype,Fee.File.Path a_path)
		{
			this.pathlist.Regist(a_assetbundle_name,a_assetbundle_pathtype,a_path);
		}

		/** パスアイテム。解除。
		*/
		public bool UnRegistPathItem(string a_assetbundle_name)
		{
			return this.pathlist.UnRegist(a_assetbundle_name);
		}

		/** パスアイテム。取得。
		*/
		public PathItem GetPathItem(string a_assetbundle_name)
		{
			return this.pathlist.GetItem(a_assetbundle_name);
		}

		/** アセットバンドルアイテム。取得。
		*/
		public AssetBundleItem GetAssetBundleItem(string a_assetbundle_name)
		{
			return this.assetbundleitem_list.GetItem(a_assetbundle_name);
		}

		/** アセットバンドルアイテム。登録。
		*/
		public void RegistAssetBundleItem(string a_assetbundle_name,AssetBundleItem a_item)
		{
			this.assetbundleitem_list.Regist(a_assetbundle_name,a_item);
		}

		/** アセットバンドルアイテム。解除。
		*/
		public void UnRegistAssetBundleItem(string a_assetbundle_name)
		{
			this.assetbundleitem_list.UnRegist(a_assetbundle_name);
		}

		/** ロードパス。アセットバンドルアイテム。

			パスアイテムからアセットバンドルアイテムをロード。

		*/
		public Item RequestLoadPathItemAssetBundleItem(string a_assetbundle_name)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestLoadPathItemAssetBundleItem(a_assetbundle_name);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** アンロード。アセットバンドルアイテム。

			アセットバンドルアイテムをアンロード。

		*/
		public Item RequestUnLoadAssetBundleItem(string a_assetbundle_name)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestUnLoadAssetBundleItem(a_assetbundle_name);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** ロードアセットバンドルアイテム。テキストファイル。

			ロード済みのアセットバンドルアイテムからテキストファイルをロード。

		*/
		public Item RequestLoadAssetBundleItemTextFile(string a_assetbundle_name,string a_asset_name)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestLoadAssetBundleItemTextFile(a_assetbundle_name,a_asset_name);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** ロードアセットバンドルアイテム。テクスチャファイル。

			ロード済みのアセットバンドルアイテムからテクスチャーファイルをロード。

		*/
		public Item RequestLoadAssetBundleItemTextureFile(string a_assetbundle_name,string a_asset_name)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestLoadAssetBundleItemTextureFile(a_assetbundle_name,a_asset_name);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** ロードアセットバンドルアイテム。プレハブファイル。

			ロード済みのアセットバンドルアイテムからプレハブファイルをロード。

		*/
		public Item RequestLoadAssetBundleItemPrefabFile(string a_assetbundle_name,string a_asset_name)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestLoadAssetBundleItemPrefabFile(a_assetbundle_name,a_asset_name);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.pathlist.Delete();
			this.assetbundleitem_list.Delete();

			this.main_assetbundle.Delete();
		}

		/** メイン。取得。
		*/
		public Main_AssetBundle GetMainAssetBundle()
		{
			return this.main_assetbundle;
		}

		/** メイン。取得。
		*/
		public Main_Asset GetMainAsset()
		{
			return this.main_asset;
		}

		/** 処理中。チェック。
		*/
		public bool IsBusy()
		{
			if((this.work_list.Count > 0)||(this.add_list.Count > 0)){
				return true;
			}
			return false;
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				//追加。
				if(this.add_list.Count > 0){
					for(int ii=0;ii<this.add_list.Count;ii++){
						this.work_list.Add(this.add_list[ii]);
					}
					this.add_list.Clear();
				}

				int t_index = 0;
				while(t_index < this.work_list.Count){
					if(this.work_list[t_index].Main() == true){
						this.work_list.RemoveAt(t_index);
					}else{
						t_index++;
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

