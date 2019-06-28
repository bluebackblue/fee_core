

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。ワーク。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** Work
	*/
	public class Work
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
			*/
			LoadPathAssetBundleItem,

			/** アンロードパス。アセットバンドルアイテム。
			*/
			UnLoadPathAssetBundleItem,

			/** ロードアセットバンドルアイテム。テキストファイル。
			*/
			LoadAssetBundleItemTextFile,

			/** ロードアセットバンドルアイテム。テクスチャファイル。
			*/
			LoadAssetBundleItemTextureFile,

			/** ロードアセットバンドルアイテム。プレハブファイル。
			*/
			LoadAssetBundleItemPrefabFile,
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
		public Work()
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

		/** リクエスト。ロードパス。アセットバンドルアイテム。
		*/
		public void RequestLoadPathAssetBundleItem(string a_id)
		{
			this.request_type = RequestType.LoadPathAssetBundleItem;
			this.request_id = a_id;
		}

		/** リクエスト。アンロードパス。アセットバンドルアイテム。
		*/
		public void RequestUnLoadPathAssetBundleItem(string a_id)
		{
			this.request_type = RequestType.UnLoadPathAssetBundleItem;
			this.request_id = a_id;
		}

		/** リクエスト。ロードアセットバンドルアイテム。テキストファイル。
		*/
		public void RequestLoadAssetBundleItemTextFile(string a_id,string a_assetname)
		{
			this.request_type = RequestType.LoadAssetBundleItemTextFile;
			this.request_id = a_id;
			this.request_assetname = a_assetname; 
		}

		/** リクエスト。ロードアセットバンドルアイテム。テクスチャファイル。
		*/
		public void RequestLoadAssetBundleItemTextureFile(string a_id,string a_assetname)
		{
			this.request_type = RequestType.LoadAssetBundleItemTextureFile;
			this.request_id = a_id;
			this.request_assetname = a_assetname; 
		}

		/** リクエスト。ロードアセットバンドルアイテム。プレハブファイル。
		*/
		public void RequestLoadAssetBundleItemPrefabFile(string a_id,string a_assetname)
		{
			this.request_type = RequestType.LoadAssetBundleItemPrefabFile;
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
					case RequestType.LoadPathAssetBundleItem:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAssetBundle().RequestLoadPathAssetBundleItem(this.request_id) == true){
								this.mode = Mode.Do_AssetBundle;
							}
						}break;
					case RequestType.UnLoadPathAssetBundleItem:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAssetBundle().RequestUnLoadPathAssetBundleItem(this.request_id) == true){
								this.mode = Mode.Do_AssetBundle;
							}
						}break;
					case RequestType.LoadAssetBundleItemTextFile:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAsset().RequestLoadAssetBundleItemTextFile(this.request_id,this.request_assetname) == true){
								this.mode = Mode.Do_Asset;
							}
						}break;
					case RequestType.LoadAssetBundleItemTextureFile:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAsset().RequestLoadAssetBundleItemTextureFile(this.request_id,this.request_assetname) == true){
								this.mode = Mode.Do_Asset;
							}
						}break;
					case RequestType.LoadAssetBundleItemPrefabFile:
						{
							if(Fee.AssetBundleList.AssetBundleList.GetInstance().GetMainAsset().RequestLoadAssetBundleItemPrefabFile(this.request_id,this.request_assetname) == true){
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
						case Main_AssetBundle.ResultType.LoadPathAssetBundleItem:
							{
								//ロードパス。アセットバンドルアイテム。

								if(t_main.GetResultAssetBundleItem() != null){
									this.item.SetResultAssetBundleItem(t_main.GetResultAssetBundleItem());
									t_success = true;
								}
							}break;
						case Main_AssetBundle.ResultType.UnLoadPathAssetBundleItem:
							{
								//アンロードパス。アセットバンドルアイテム。

								this.item.SetResultUnLoadAssetBundleItem();
								t_success = true;
							}break;
						default:
							{
								Tool.Assert(false);
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
						default:
							{
								Tool.Assert(false);
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

