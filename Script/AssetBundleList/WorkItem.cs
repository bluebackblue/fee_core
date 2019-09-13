

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

			/** ロードパス。パックアイテム。

				パスアイテムからパックアイテムをロード。

			*/
			LoadPathItemPackItem,

			/** アンロード。パックアイテム。
			*/
			UnLoadPackItem,

			/** パックアイテム。テキストファイル。
			*/
			LoadPackItemTextFile,

			/** パックアイテム。テクスチャファイル。
			*/
			LoadPackItemTextureFile,

			/** パックアイテム。プレハブファイル。
			*/
			LoadPackItemPrefabFile,
		};

		/** mode
		*/
		private Mode mode;

		/** request_type
		*/
		private RequestType request_type;

		/** request_id
		*/
		private string request_id;

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

			//request_id
			this.request_id = null;

			//item
			this.item = new Item();
		}

		/** リクエスト。ロードパスアイテム。パックアイテム。

			パスアイテムからパックアイテムをロード。

		*/
		public void RequestLoadPathItemPackItem(string a_id)
		{
			this.request_type = RequestType.LoadPathItemPackItem;
			this.request_id = a_id;
		}

		/** リクエスト。アンロード。パックアイテム。
		*/
		public void RequestUnLoadPackItem(string a_id)
		{
			this.request_type = RequestType.UnLoadPackItem;
			this.request_id = a_id;
		}

		/** リクエスト。ロードパックアイテム。テキストファイル。
		*/
		public void RequestLoadPackItemTextFile(string a_id,string a_assetname)
		{
			this.request_type = RequestType.LoadPackItemTextFile;
			this.request_id = a_id;
			this.request_assetname = a_assetname; 
		}

		/** リクエスト。ロードパックアイテム。テクスチャファイル。
		*/
		public void RequestLoadPackItemTextureFile(string a_id,string a_assetname)
		{
			this.request_type = RequestType.LoadPackItemTextureFile;
			this.request_id = a_id;
			this.request_assetname = a_assetname; 
		}

		/** リクエスト。ロードパックアイテム。プレハブファイル。
		*/
		public void RequestLoadPackItemPrefabFile(string a_id,string a_assetname)
		{
			this.request_type = RequestType.LoadPackItemPrefabFile;
			this.request_id = a_id;
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
					case RequestType.LoadPathItemPackItem:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainPack().RequestLoadPathItemPackItem(this.request_id) == true){
								this.mode = Mode.Do_AssetBundle;
							}
						}break;
					case RequestType.UnLoadPackItem:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainPack().RequestUnLoadPackItem(this.request_id) == true){
								this.mode = Mode.Do_AssetBundle;
							}
						}break;
					case RequestType.LoadPackItemTextFile:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAsset().RequestLoadPackItemTextFile(this.request_id,this.request_assetname) == true){
								this.mode = Mode.Do_Asset;
							}
						}break;
					case RequestType.LoadPackItemTextureFile:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAsset().RequestLoadPackItemTextureFile(this.request_id,this.request_assetname) == true){
								this.mode = Mode.Do_Asset;
							}
						}break;
					case RequestType.LoadPackItemPrefabFile:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAsset().RequestLoadPackItemPrefabFile(this.request_id,this.request_assetname) == true){
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
					Main_Pack t_main = Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainPack();

					this.item.SetResultProgress(t_main.GetResultProgress());

					if(t_main.GetResultType() != Main_Pack.ResultType.None){
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_Pack.ResultType.LoadPathItemPackItem:
							{
								//ロードパスアイテム。パックアイテム。

								if(t_main.GetResultPackItem() != null){
									this.item.SetResultPackItem(t_main.GetResultPackItem());
									t_success = true;
								}
							}break;
						case Main_Pack.ResultType.UnLoadPackItem:
							{
								//アンロード。パックアイテム。

								this.item.SetResultUnLoadPackItem();
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

