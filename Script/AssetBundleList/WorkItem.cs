

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。ワークアイテム。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** WorkItem
	*/
	public class WorkItem
	{
		/** Mode
		*/
		private enum Mode
		{
			/** 開始。
			*/
			Start,

			/** Do_AssetBundle
			*/
			Do_AssetBundle,

			/** Do_Asset
			*/
			Do_Asset,

			/** 完了。
			*/
			End
		};

		/** RequestType
		*/
		private enum RequestType
		{
			/** None
			*/
			None,

			/** ロードパス。アセットバンドルアイテム。

				パスアイテムからアセットバンドルアイテムをロード。

			*/
			LoadPathItemAssetBundleItem,

			/** アンロード。アセットバンドルアイテム。
			*/
			UnLoadAssetBundleItem,



			/** アセットバンドルアイテム。テキストファイル。
			*/
			LoadAssetBundleItemTextFile,

			/** アセットバンドルアイテム。テクスチャファイル。
			*/
			LoadAssetBundleItemTextureFile,

			/** アセットバンドルアイテム。プレハブファイル。
			*/
			LoadAssetBundleItemPrefabFile,
		};

		/** mode
		*/
		private Mode mode;

		/** request_type
		*/
		private RequestType request_type;

		/** request_assetbundle_name
		*/
		private string request_assetbundle_name;

		/** request_assetname
		*/
		private string request_assetname;

		/** item
		*/
		private Item item;

		/** constructor
		*/
		public WorkItem()
		{
			//mode
			this.mode = Mode.Start;

			//request_type
			this.request_type = RequestType.None;

			//request_assetbundle_name
			this.request_assetbundle_name = null;

			//item
			this.item = new Item();
		}

		/** リセット。
		*/
		public void Reset()
		{
			//mode
			this.mode = Mode.Start;

			//request_type
			this.request_type = RequestType.None;

			//request_assetbundle_name
			this.request_assetbundle_name = null;

			//item
			this.item.Reset();
		}

		/** リクエスト。ロードパスアイテム。アセットバンドルアイテム。

			パスアイテムからアセットバンドルアイテムをロード。

		*/
		public void RequestLoadPathItemAssetBundleItem(string a_assetbundle_name)
		{
			this.request_type = RequestType.LoadPathItemAssetBundleItem;
			this.request_assetbundle_name = a_assetbundle_name;
		}

		/** リクエスト。アンロード。アセットバンドルアイテム。
		*/
		public void RequestUnLoadAssetBundleItem(string a_assetbundle_name)
		{
			this.request_type = RequestType.UnLoadAssetBundleItem;
			this.request_assetbundle_name = a_assetbundle_name;
		}

		/** リクエスト。ロードアセットバンドルアイテム。テキストファイル。
		*/
		public void RequestLoadAssetBundleItemTextFile(string a_assetbundle_name,string a_assetname)
		{
			this.request_type = RequestType.LoadAssetBundleItemTextFile;
			this.request_assetbundle_name = a_assetbundle_name;
			this.request_assetname = a_assetname; 
		}

		/** リクエスト。ロードアセットバンドルアイテム。テクスチャファイル。
		*/
		public void RequestLoadAssetBundleItemTextureFile(string a_assetbundle_name,string a_assetname)
		{
			this.request_type = RequestType.LoadAssetBundleItemTextureFile;
			this.request_assetbundle_name = a_assetbundle_name;
			this.request_assetname = a_assetname; 
		}

		/** リクエスト。ロードアセットバンドルアイテム。プレハブファイル。
		*/
		public void RequestLoadAssetBundleItemPrefabFile(string a_assetbundle_name,string a_assetname)
		{
			this.request_type = RequestType.LoadAssetBundleItemPrefabFile;
			this.request_assetbundle_name = a_assetbundle_name;
			this.request_assetname = a_assetname; 
		}

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** 更新。

			return == true : 完了。

		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Start:
				{
					switch(this.request_type){
					case RequestType.LoadPathItemAssetBundleItem:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAssetBundle().RequestLoadPathItemAssetBundleItem(this.request_assetbundle_name) == true){
								this.mode = Mode.Do_AssetBundle;
							}
						}break;
					case RequestType.UnLoadAssetBundleItem:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAssetBundle().RequestUnLoadAssetBundleItem(this.request_assetbundle_name) == true){
								this.mode = Mode.Do_AssetBundle;
							}
						}break;
					case RequestType.LoadAssetBundleItemTextFile:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAsset().RequestLoadAssetBundleItemTextFile(this.request_assetbundle_name,this.request_assetname) == true){
								this.mode = Mode.Do_Asset;
							}
						}break;
					case RequestType.LoadAssetBundleItemTextureFile:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAsset().RequestLoadAssetBundleItemTextureFile(this.request_assetbundle_name,this.request_assetname) == true){
								this.mode = Mode.Do_Asset;
							}
						}break;
					case RequestType.LoadAssetBundleItemPrefabFile:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAsset().RequestLoadAssetBundleItemPrefabFile(this.request_assetbundle_name,this.request_assetname) == true){
								this.mode = Mode.Do_Asset;
							}
						}break;
					default:
						{
							Tool.Assert(false);
						}break;
					}
				}break;
			case Mode.End:
				{
				}return true;
			case Mode.Do_AssetBundle:
				{
					Main_AssetBundle t_main = Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAssetBundle();

					this.item.SetResultProgress(t_main.GetResultProgress());

					if(t_main.GetResultType() != Main_AssetBundle.ResultType.None){
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_AssetBundle.ResultType.LoadPathItemAssetBundleItem:
							{
								//ロードパスアイテム。アセットバンドルアイテム。

								if(t_main.GetResultAssetBundleItem() != null){
									this.item.SetResultAssetBundleItem(t_main.GetResultAssetBundleItem());
									t_success = true;
								}
							}break;
						case Main_AssetBundle.ResultType.UnLoadAssetBundleItem:
							{
								//アンロード。アセットバンドルアイテム。

								this.item.SetResultUnLoadAssetBundleItem();
								t_success = true;
							}break;
						}

						if(t_success == false){
							this.item.SetResultErrorString(t_main.GetResultErrorString());
						}

						//完了。
						t_main.Fix();

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_main.Cancel();
					}
				}break;
			case Mode.Do_Asset:
				{
					Main_Asset t_main = Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAsset();

					this.item.SetResultProgress(t_main.GetResultProgress());

					if(t_main.GetResultType() != Main_Asset.ResultType.None){
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_Asset.ResultType.Asset:
							{
								if(t_main.GetResultAsset() != null){
									this.item.SetResultAsset(t_main.GetResultAsset());
									t_success = true;
								}
							}break;
						}

						if(t_success == false){
							this.item.SetResultErrorString(t_main.GetResultErrorString());
						}

						//完了。
						t_main.Fix();

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_main.Cancel();
					}
				}break;
			}

			return false;
		}
	}
}

