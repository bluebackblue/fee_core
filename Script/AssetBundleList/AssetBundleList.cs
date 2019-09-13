

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

		/** main_pack
		*/
		private Main_Pack main_pack;

		/** main_asset
		*/
		private Main_Asset main_asset;

		/** pathlist
		*/
		private PathList pathlist;

		/** packlist
		*/
		private PackList packlist;

		/** [シングルトン]constructor
		*/
		private AssetBundleList()
		{
			//work_list
			this.work_list = new System.Collections.Generic.List<Work>();

			//add_list
			this.add_list = new System.Collections.Generic.List<Work>();

			//main_pack
			this.main_pack = new Main_Pack();

			//main_asset
			this.main_asset = new Main_Asset();

			//pathlist
			this.pathlist = new PathList();

			//packlist
			this.packlist = new PackList();
		}

		/** パスアイテム。登録。
		*/
		public void RegistPathItem(string a_assetbundle_name,AssetBundlePathType a_assetbundle_pathtype,Fee.File.Path a_path)
		{
			this.pathlist.Regist(a_assetbundle_name,a_assetbundle_pathtype,a_path);
		}

		/** パスアイテム。解除。
		*/
		public void UnRegistPathItem(string a_assetbundle_name)
		{
			this.pathlist.UnRegist(a_assetbundle_name);
		}

		/** パスアイテム。取得。
		*/
		public PathItem GetPathItem(string a_assetbundle_name)
		{
			return this.pathlist.GetItem(a_assetbundle_name);
		}

		/** パックアイテム。取得。
		*/
		public PackItem GetPackItem(string a_assetbundle_name)
		{
			return this.packlist.GetItem(a_assetbundle_name);
		}

		/** パックアイテム。登録。
		*/
		public void RegistPackItem(string a_assetbundle_name,PackItem a_item)
		{
			this.packlist.Regist(a_assetbundle_name,a_item);
		}

		/** パックアイテム。解除。
		*/
		public void UnRegistPackItem(string a_assetbundle_name)
		{
			this.packlist.UnRegist(a_assetbundle_name);
		}

		/** ロードパス。パックアイテム。

			パスアイテムからパックアイテムをロード。

		*/
		public Item RequestLoadPathItemPackItem(string a_id)
		{
			Work t_work = new Work();
			t_work.RequestLoadPathItemPackItem(a_id);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** アンロード。パックアイテム。

			パックアイテムをアンロード。

		*/
		public Item RequestUnLoadPackItem(string a_id)
		{
			Work t_work = new Work();
			t_work.RequestUnLoadPackItem(a_id);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** ロードパックアイテム。テキストファイル。

			ロード済みのパックアイテムからテキストファイルをロード。

		*/
		public Item RequestLoadPackItemTextFile(string a_id,string a_asset_name)
		{
			Work t_work = new Work();
			t_work.RequestLoadPackItemTextFile(a_id,a_asset_name);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** ロードパックアイテム。テクスチャファイル。

			ロード済みのパックアイテムからテクスチャーファイルをロード。

		*/
		public Item RequestLoadPackItemTextureFile(string a_id,string a_asset_name)
		{
			Work t_work = new Work();
			t_work.RequestLoadPackItemTextureFile(a_id,a_asset_name);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** ロードパックアイテム。プレハブファイル。

			ロード済みのパックアイテムからプレハブファイルをロード。

		*/
		public Item RequestLoadPackItemPrefabFile(string a_id,string a_asset_name)
		{
			Work t_work = new Work();
			t_work.RequestLoadPackItemPrefabFile(a_id,a_asset_name);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.pathlist.Delete();
			this.packlist.Delete();

			this.main_pack.Delete();
		}

		/** メイン。取得。
		*/
		public Main_Pack GetMainPack()
		{
			return this.main_pack;
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

