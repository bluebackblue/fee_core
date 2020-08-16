

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
	public class AssetBundleList
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

		/** work
		*/
		private Fee.List.NodePool<WorkItem> work_pool;
		private System.Collections.Generic.LinkedList<WorkItem> work_add;
		private System.Collections.Generic.LinkedList<WorkItem> work_list;

		/** playerloop_flag
		*/
		private bool playerloop_flag;

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
			//work
			this.work_pool = new List.NodePool<WorkItem>(16);
			this.work_add = new System.Collections.Generic.LinkedList<WorkItem>();
			this.work_list = new System.Collections.Generic.LinkedList<WorkItem>();

			//main_assetbundle
			this.main_assetbundle = new Main_AssetBundle();

			//main_asset
			this.main_asset = new Main_Asset();

			//pathlist
			this.pathlist = new PathList();

			//assetbundleitem_list
			this.assetbundleitem_list = new AssetBundleItem_List();

			//PlayerLoopType
			this.playerloop_flag = true;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopType.Fee_AssetBundleList_Main),this.Main);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.pathlist.Delete();
			this.assetbundleitem_list.Delete();

			this.main_assetbundle.Delete();

			//PlayerLoopType
			this.playerloop_flag = false;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopType.Fee_AssetBundleList_Main));
		}

		/** 更新。
		*/
		private void Main()
		{
			try{
				if(this.playerloop_flag == true){
					//追加。
					{
						System.Collections.Generic.LinkedListNode<WorkItem> t_node = this.work_add.Last;
						while(t_node != null){
							this.work_add.Remove(t_node);
							this.work_list.AddLast(t_node);
							t_node = this.work_add.Last;
						}
					}

					//更新。
					{
						System.Collections.Generic.LinkedListNode<WorkItem> t_node = this.work_list.First;
						while(t_node != null){
							System.Collections.Generic.LinkedListNode<WorkItem> t_node_next = t_node.Next;
							if(t_node.Value.Main() == true){
								this.work_list.Remove(t_node);
								this.work_pool.Free(t_node);
							}
							t_node = t_node_next;
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
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
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestLoadPathItemAssetBundleItem(a_assetbundle_name);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** アンロード。アセットバンドルアイテム。

			アセットバンドルアイテムをアンロード。

		*/
		public Item RequestUnLoadAssetBundleItem(string a_assetbundle_name)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestUnLoadAssetBundleItem(a_assetbundle_name);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** ロードアセットバンドルアイテム。テキストファイル。

			ロード済みのアセットバンドルアイテムからテキストファイルをロード。

		*/
		public Item RequestLoadAssetBundleItemTextFile(string a_assetbundle_name,string a_asset_name)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestLoadAssetBundleItemTextFile(a_assetbundle_name,a_asset_name);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** ロードアセットバンドルアイテム。テクスチャファイル。

			ロード済みのアセットバンドルアイテムからテクスチャーファイルをロード。

		*/
		public Item RequestLoadAssetBundleItemTextureFile(string a_assetbundle_name,string a_asset_name)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestLoadAssetBundleItemTextureFile(a_assetbundle_name,a_asset_name);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** ロードアセットバンドルアイテム。プレハブファイル。

			ロード済みのアセットバンドルアイテムからプレハブファイルをロード。

		*/
		public Item RequestLoadAssetBundleItemPrefabFile(string a_assetbundle_name,string a_asset_name)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestLoadAssetBundleItemPrefabFile(a_assetbundle_name,a_asset_name);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
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
			if((this.work_list.Count > 0)||(this.work_add.Count > 0)){
				return true;
			}
			return false;
		}
	}
}

