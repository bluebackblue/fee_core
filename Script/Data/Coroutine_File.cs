

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
	/** ファイル。
	*/
	public class Coroutine_File
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

			//LoadRequestType
			Fee.File.File.LoadRequestType t_loadrequest_type = File.File.LoadRequestType.None;
			switch(a_listitem.path_type){
			case PathType.AssetBundle_Prefab:
				{
					//アセットバンドル。プレハブ。

					AssetBundleList.Item t_item = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadAssetBundleItemPrefabFile(a_listitem.assetbundle_name,a_listitem.id);
					if(t_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_File : " + a_listitem.assetbundle_name + " : " + a_listitem.id;
						yield break;
					}
					while(t_item.IsBusy() == true){
						yield return null;
					}
					if(t_item.GetResultAssetType() == Asset.AssetType.Prefab){
						this.result.asset_file = t_item.GetResultAsset();
						yield break;
					}

					//失敗。
					this.result.errorstring = "Coroutine_File : " + a_listitem.assetbundle_name;
					yield break;
				}break;
			case PathType.AssetBundle_Texture:
				{
					//アセットバンドル。テクスチャ。

					AssetBundleList.Item t_item = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadAssetBundleItemTextureFile(a_listitem.assetbundle_name,a_listitem.id);
					if(t_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_File : " + a_listitem.assetbundle_name + " : " + a_listitem.id;
						yield break;
					}
					while(t_item.IsBusy() == true){
						yield return null;
					}
					if(t_item.GetResultAssetType() == Asset.AssetType.Texture){
						this.result.asset_file = t_item.GetResultAsset();
						yield break;
					}

					//失敗。
					this.result.errorstring = "Coroutine_File : " + a_listitem.assetbundle_name;
					yield break;
				}break;
			case PathType.AssetBundle_Text:
				{
					//アセットバンドル。テキスト。

					AssetBundleList.Item t_item = Fee.AssetBundleList.AssetBundleList.GetInstance().RequestLoadAssetBundleItemTextFile(a_listitem.assetbundle_name,a_listitem.id);
					if(t_item == null){
						//失敗。
						this.result.errorstring = "Coroutine_File : " + a_listitem.assetbundle_name + " : " + a_listitem.id;
						yield break;
					}
					while(t_item.IsBusy() == true){
						yield return null;
					}
					if(t_item.GetResultAssetType() == Asset.AssetType.Text){
						this.result.asset_file = t_item.GetResultAsset();
						yield break;
					}

					//失敗。
					this.result.errorstring = "Coroutine_File : " + a_listitem.assetbundle_name;
					yield break;
				}break;
			case PathType.Resources_Prefab:
				{
					//リソース。プレハブ。
					t_loadrequest_type = File.File.LoadRequestType.LoadResourcesPrefabFile;
				}break;
			case PathType.Resources_Texture:
				{
					//リソース。テクスチャ。
					t_loadrequest_type = File.File.LoadRequestType.LoadResourcesTextureFile;
				}break;
			case PathType.Resources_Text:
				{
					//リソース。テキスト。
					t_loadrequest_type = File.File.LoadRequestType.LoadResourcesTextFile;
				}break;
			case PathType.StreamingAssets_Texture:
				{
					//ストリーミングアセット。テクスチャ。
					t_loadrequest_type = File.File.LoadRequestType.LoadStreamingAssetsTextureFile;
				}break;
			case PathType.StreamingAssets_Text:
				{
					//ストリーミングアセット。テキスト。
					t_loadrequest_type = File.File.LoadRequestType.LoadStreamingAssetsTextFile;
				}break;
			case PathType.StreamingAssets_Binary:
				{
					//ストリーミングアセット。バイナリ。
					t_loadrequest_type = File.File.LoadRequestType.LoadStreamingAssetsBinaryFile;
				}break;
			case PathType.Url_Texture:
				{
					//ＵＲＬ。テクスチャ。
					t_loadrequest_type = File.File.LoadRequestType.LoadUrlTextureFile;
				}break;
			case PathType.Url_Text:
				{
					//ＵＲＬ。テキスト。
					t_loadrequest_type = File.File.LoadRequestType.LoadUrlTextFile;
				}break;
			case PathType.Url_Binary:
				{
					//ＵＲＬ。バイナリ。
					t_loadrequest_type = File.File.LoadRequestType.LoadUrlBinaryFile;
				}break;
			default:
				{
					Tool.Assert(false);
				}break;
			}

			{
				//RequestLoad
				Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoad(t_loadrequest_type,a_listitem.path);

				while(true){
					if(t_item.GetResultType() != File.Item.ResultType.None){
						if(t_item.GetResultType() == File.Item.ResultType.Asset){
							//成功。
							this.result.asset_file = t_item.GetResultAsset();
							yield break;
						}else if(t_item.GetResultType() == File.Item.ResultType.Error){
							//失敗。
							this.result.errorstring = "Coroutine_File : " + t_item.GetResultErrorString();
							yield break;
						}else{
							break;
						}
					}

					if(a_callback_interface != null){
						a_callback_interface.OnDataCoroutine(t_item.GetResultProgress());
					}

					yield return null;
				}
			}

			//不明。
			Tool.Assert(false);
			this.result.errorstring = "Coroutine_File : " + "Unknown";
			yield break;
		}
	}
}

