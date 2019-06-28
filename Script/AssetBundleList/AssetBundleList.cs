

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
		private System.Collections.Generic.List<Work> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<Work> add_list;

		/** main_assetbundle
		*/
		private Main_AssetBundle main_assetbundle;

		/** main_asset
		*/
		private Main_Asset main_asset;

		/** assetbundlepathlist
		*/
		private AssetBundlePathList assetbundlepathlist;

		/** assetbundlepacklist
		*/
		private AssetBundlePackList assetbundlepacklist;

		/** [シングルトン]constructor
		*/
		private AssetBundleList()
		{
			//work_list
			this.work_list = new System.Collections.Generic.List<Work>();

			//add_list
			this.add_list = new System.Collections.Generic.List<Work>();

			//main_assetbundle
			this.main_assetbundle = new Main_AssetBundle();

			//main_asset
			this.main_asset = new Main_Asset();

			//assetbundlepathlist
			this.assetbundlepathlist = new AssetBundlePathList();

			//assetbundlepacklist
			this.assetbundlepacklist = new AssetBundlePackList();
		}

		/** [パス]パス。登録。
		*/
		public void RegisterPath(string a_id,AssetBundlePathList_PathType a_pathtype,Fee.File.Path a_path)
		{
			this.assetbundlepathlist.Register(a_id,a_pathtype,a_path);
		}

		/** [パス]パス。解除。
		*/
		public void UnRegisterPath(string a_id)
		{
			this.assetbundlepathlist.UnRegister(a_id);
		}

		/** [パス]パスアイテム。取得。
		*/
		public AssetBundlePathList_PathItem GetPathItem(string a_id)
		{
			return this.assetbundlepathlist.GetPathItem(a_id);
		}

		/** [アセットバンドル]アセットバンドルアイテム。取得。
		*/
		public AssetBundlePackList_AssetBundleItem GetAssetBundleItem(string a_id)
		{
			return this.assetbundlepacklist.GetAssetBundleItem(a_id);
		}

		/** [アセットバンドル]アセットバンドルアイテム。登録。
		*/
		public void RegisterAssetBundle(string a_id,AssetBundlePackList_AssetBundleItem a_item)
		{
			this.assetbundlepacklist.Register(a_id,a_item);
		}

		/** [アセットバンドル]アセットバンドルアイテム。解除。
		*/
		public void UnRegisterAssetBundle(string a_id)
		{
			this.assetbundlepacklist.UnRegister(a_id);
		}

		/** ロードパス。アセットバンドルアイテム。
		*/
		public Item RequestLoadPathAssetBundleItem(string a_id)
		{
			Work t_work = new Work();
			t_work.RequestLoadPathAssetBundleItem(a_id);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** アンロードパス。アセットバンドルアイテム。
		*/
		public Item RequestUnLoadPathAssetBundleItem(string a_id)
		{
			Work t_work = new Work();
			t_work.RequestUnLoadPathAssetBundleItem(a_id);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** ロードアセットバンドルアイテム。テキストファイル。
		*/
		public Item RequestLoadAssetBundleItemTextFile(string a_id,string a_assetname)
		{
			Work t_work = new Work();
			t_work.RequestLoadAssetBundleItemTextFile(a_id,a_assetname);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** ロードアセットバンドルアイテム。テクスチャファイル。
		*/
		public Item RequestLoadAssetBundleItemTextureFile(string a_id,string a_assetname)
		{
			Work t_work = new Work();
			t_work.RequestLoadAssetBundleItemTextureFile(a_id,a_assetname);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** ロードアセットバンドルアイテム。プレハブファイル。
		*/
		public Item RequestLoadAssetBundleItemPrefabFile(string a_id,string a_assetname)
		{
			Work t_work = new Work();
			t_work.RequestLoadAssetBundleItemPrefabFile(a_id,a_assetname);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.assetbundlepathlist.Delete();
			this.assetbundlepacklist.Delete();

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

