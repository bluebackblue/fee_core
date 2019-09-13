

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。コルーチン。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.Data
*/
namespace Fee.Data
{
	/** ロード。
	*/
	public class Coroutine_Load
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** アセットファイル。
			*/
			public Fee.Asset.Asset asset_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** レスポンスヘッダー。
			*/
			public System.Collections.Generic.Dictionary<string,string> responseheader;

			/** constructor
			*/
			public ResultType()
			{
				//asset_file
				this.asset_file = null;

				//errorstring
				this.errorstring = null;

				//responseheader
				this.responseheader = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.Data.OnDataCoroutine_CallBackInterface a_callback_interface,ListItem a_listitem)
		{
			//result
			this.result = new ResultType();

			AssetBundleList.Item t_assetbundlelist_item = null;
			Fee.File.Item t_file_item = null;

			switch(a_listitem.path_type){
			case PathType.AssetBundle_Prefab:
				{
					t_assetbundlelist_item = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadPackItemPrefabFile(a_listitem.assetbundle_name,a_listitem.id);
					if(t_assetbundlelist_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.assetbundle_name + " : " + a_listitem.id;
						yield break;
					}					
				}break;
			case PathType.AssetBundle_Texture:
				{
					t_assetbundlelist_item = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadPackItemTextureFile(a_listitem.assetbundle_name,a_listitem.id);
					if(t_assetbundlelist_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.assetbundle_name + " : " + a_listitem.id;
						yield break;
					}	
				}break;
			case PathType.AssetBundle_Text:
				{
					t_assetbundlelist_item = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadPackItemTextFile(a_listitem.assetbundle_name,a_listitem.id);
					if(t_assetbundlelist_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.assetbundle_name + " : " + a_listitem.id;
						yield break;
					}	
				}break;
			case PathType.Resources_Prefab:
				{
					//リソース。プレハブ。
					t_file_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadResourcesPrefabFile,a_listitem.path);
					if(t_file_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.path;
						yield break;
					}	
				}break;
			case PathType.Resources_Texture:
				{
					//リソース。テクスチャ。
					t_file_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadResourcesTextureFile,a_listitem.path);
					if(t_file_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.path;
						yield break;
					}	
				}break;
			case PathType.Resources_Text:
				{
					//リソース。テキスト。
					t_file_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadResourcesTextFile,a_listitem.path);
					if(t_file_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.path;
						yield break;
					}	
				}break;
			case PathType.StreamingAssets_Texture:
				{
					//ストリーミングアセット。テクスチャ。
					t_file_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadStreamingAssetsTextureFile,a_listitem.path);
					if(t_file_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.path;
						yield break;
					}	
				}break;
			case PathType.StreamingAssets_Text:
				{
					//ストリーミングアセット。テキスト。
					t_file_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadStreamingAssetsTextFile,a_listitem.path);
					if(t_file_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.path;
						yield break;
					}	
				}break;
			case PathType.StreamingAssets_Binary:
				{
					//ストリーミングアセット。バイナリ。
					t_file_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadStreamingAssetsBinaryFile,a_listitem.path);
					if(t_file_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.path;
						yield break;
					}	
				}break;
			case PathType.Url_Texture:
				{
					//ＵＲＬ。テクスチャ。
					t_file_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadUrlTextureFile,a_listitem.path);
					if(t_file_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.path;
						yield break;
					}	
				}break;
			case PathType.Url_Text:
				{
					//ＵＲＬ。テキスト。
					t_file_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadUrlTextFile,a_listitem.path);
					if(t_file_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.path;
						yield break;
					}	
				}break;
			case PathType.Url_Binary:
				{
					//ＵＲＬ。バイナリ。
					t_file_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadUrlBinaryFile,a_listitem.path);
					if(t_file_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_Load : " + a_listitem.path;
						yield break;
					}	
				}break;
			default:
				{
					Tool.Assert(false);
				}break;
			}

			if(t_assetbundlelist_item != null){
				//ロード中。
				do{
					if(a_callback_interface != null){
						a_callback_interface.OnDataCoroutine(t_assetbundlelist_item.GetResultProgress());
					}
					yield return null;
				}while(t_assetbundlelist_item.IsBusy() == true);

				switch(a_listitem.path_type){
				case PathType.AssetBundle_Prefab:
					{
						if(t_assetbundlelist_item.GetResultAssetType() == Asset.AssetType.Prefab){
							if(t_assetbundlelist_item.GetResultAssetPrefab() != null){
								this.result.asset_file = t_assetbundlelist_item.GetResultAsset();
								yield break;
							}
						}
					}break;
				case PathType.AssetBundle_Texture:
					{
						if(t_assetbundlelist_item.GetResultAssetType() == Asset.AssetType.Texture){
							if(t_assetbundlelist_item.GetResultAssetTexture() != null){
								this.result.asset_file = t_assetbundlelist_item.GetResultAsset();
								yield break;
							}
						}
					}break;
				case PathType.AssetBundle_Text:
					{
						if(t_assetbundlelist_item.GetResultAssetType() == Asset.AssetType.Text){
							if(t_assetbundlelist_item.GetResultAssetText() != null){
								this.result.asset_file = t_assetbundlelist_item.GetResultAsset();
								yield break;
							}
						}
					}break;
				default:
					{
						Tool.Assert(false);
					}break;
				}

				//失敗。
				this.result.errorstring = "Coroutine_Load : " + t_assetbundlelist_item.GetResultErrorString();
				yield break;
			}

			if(t_file_item != null){
				//ロード中。
				do{
					if(a_callback_interface != null){
						a_callback_interface.OnDataCoroutine(t_file_item.GetResultProgress());
					}
					yield return null;
				}while(t_file_item.IsBusy() == true);

				switch(a_listitem.path_type){
				case PathType.Resources_Prefab:
					{
						//リソース。プレハブ。
						if(t_file_item.GetResultAssetType() == Asset.AssetType.Prefab){
							if(t_file_item.GetResultAssetPrefab() != null){
								this.result.asset_file = t_file_item.GetResultAsset();
								yield break;
							}
						}
					}break;
				case PathType.Resources_Texture:
				case PathType.StreamingAssets_Texture:
				case PathType.Url_Texture:
					{
						//リソース。テクスチャ。
						//ストリーミングアセット。テクスチャ。
						//ＵＲＬ。テクスチャ。

						if(t_file_item.GetResultAssetType() == Asset.AssetType.Texture){
							if(t_file_item.GetResultAssetTexture() != null){
								this.result.asset_file = t_file_item.GetResultAsset();
								yield break;
							}
						}
					}break;
				case PathType.Resources_Text:
				case PathType.StreamingAssets_Text:
				case PathType.Url_Text:
					{
						//リソース。テキスト。
						//ストリーミングアセット。テキスト。
						//ＵＲＬ。テキスト。

						if(t_file_item.GetResultAssetType() == Asset.AssetType.Text){
							if(t_file_item.GetResultAssetText() != null){
								this.result.asset_file = t_file_item.GetResultAsset();
								yield break;
							}
						}
					}break;
				case PathType.StreamingAssets_Binary:
				case PathType.Url_Binary:
					{
						//ストリーミングアセット。バイナリ。
						//ＵＲＬ。バイナリ。

						if(t_file_item.GetResultAssetType() == Asset.AssetType.Binary){
							if(t_file_item.GetResultAssetBinary() != null){
								this.result.asset_file = t_file_item.GetResultAsset();
								yield break;
							}
						}
					}break;
				default:
					{
						Tool.Assert(false);
					}break;
				}
				
				//失敗。
				this.result.errorstring = "Coroutine_Load : " + t_file_item.GetResultErrorString();
				yield break;
			}

			//不明。
			Tool.Assert(false);
			this.result.errorstring = "Coroutine_Load : " + "Unknown";
			yield break;
		}
	}
}

