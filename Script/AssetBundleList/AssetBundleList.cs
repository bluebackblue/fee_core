

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドル。
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

		/** PathType
		*/
		public enum PathType
		{
			#if(UNITY_EDITOR)
			AssetsPath,
			#endif

			UrlPath,
		}

		/** PathItem
		*/
		public class PathItem
		{
			//pathtype
			public PathType pathtype;

			//path
			public Fee.File.Path path;

			/**
			*/
			public PathItem(PathType a_pathtype,Fee.File.Path a_path)
			{
				//pathtype
				this.pathtype = a_pathtype;

				//path
				this.path = a_path;
			}
		}

		/** work_list
		*/
		private System.Collections.Generic.List<Work> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<Work> add_list;

		/** main_file
		*/
		private Main_File main_file;

		/** list
		*/
		private System.Collections.Generic.Dictionary<string,UnityEngine.AssetBundle> assetbundle_list;

		/** path_list
		*/
		private System.Collections.Generic.Dictionary<string,PathItem> path_list;

		/** アセットバンドル。登録。
		*/
		public void RegisterAssetBundle(string a_id,UnityEngine.AssetBundle a_assetbundle)
		{
			this.assetbundle_list.Add(a_id,a_assetbundle);
		}

		/** アセットバンドル。アンロード。
		*/
		public void UnloadAssetBundle(string a_id)
		{
			if(this.assetbundle_list.TryGetValue(a_id,out UnityEngine.AssetBundle t_assetbundle) == true){
				this.assetbundle_list.Remove(a_id);
				if(t_assetbundle != null){
					t_assetbundle.Unload(false);
				}
			}
		}

		/** 全アセットバンドル。アンロード。
		*/
		public void UnloadAllAssetBundle()
		{
			foreach(System.Collections.Generic.KeyValuePair<string,UnityEngine.AssetBundle> t_pair in this.assetbundle_list){
				t_pair.Value.Unload(false);
			}
			this.assetbundle_list.Clear();
		}

		/** パス追加。
		*/
		public void AddPath(string a_id,PathType a_pathtype,Fee.File.Path a_path)
		{
			this.path_list.Add(a_id,new PathItem(a_pathtype,a_path));
		}

		/** RequestLoad
		*/
		public Item RequestLoad(string a_id)
		{
			Work t_work = new Work();
			t_work.RequestFile(a_id);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** アセットバンドル。取得。
		*/
		public UnityEngine.AssetBundle GetAssetBundle(string a_id)
		{
			if(this.assetbundle_list.TryGetValue(a_id,out UnityEngine.AssetBundle t_assetbundle) == true){
				return t_assetbundle;
			}
			return null;
		}

		/** パスアイテム。取得。
		*/
		public PathItem GetPathItem(string a_id)
		{
			if(this.path_list.TryGetValue(a_id,out PathItem t_pathitem) == true){
				return t_pathitem;
			}
			return null;
		}

		/** [シングルトン]constructor
		*/
		private AssetBundleList()
		{
			//work_list
			this.work_list = new System.Collections.Generic.List<Work>();

			//add_list
			this.add_list = new System.Collections.Generic.List<Work>();

			//main_file
			this.main_file = new Main_File();

			//assetbundle_list
			this.assetbundle_list = new System.Collections.Generic.Dictionary<string,UnityEngine.AssetBundle>();

			//path_list
			this.path_list = new System.Collections.Generic.Dictionary<string,PathItem>();

		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//全アセットバンドル。アンロード。
			this.UnloadAllAssetBundle();

			this.main_file.Delete();
		}

		/** メイン。取得。
		*/
		public Main_File GetMainFile()
		{
			return this.main_file;
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

