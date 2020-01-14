

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。コルーチン。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** ロードアセットバンドルアイテム。プレハブファイル。

		アセットバンドルアイテムからプレハブファイルを読み込む。

	*/
	public class Coroutine_LoadAssetBundleItemPrefabFile
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
		public System.Collections.IEnumerator CoroutineMain(Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface a_callback_interface,string a_assetbundle_name,string a_asset_name)
		{
			//result
			this.result = new ResultType();

			AssetBundleItem t_assetbundle_item = Fee.AssetBundleList.AssetBundleList.GetInstance().GetAssetBundleItem(a_assetbundle_name);

			if(t_assetbundle_item == null){
				//失敗。
				this.result.errorstring = "Coroutine_LoadAssetBundleItemPrefabFile : assetbundle_item = null : " + a_assetbundle_name + " : " + a_asset_name;
				yield break;
			}

			if(t_assetbundle_item.assetbundle_dummy != null){

				//ダミーアセットバンドルが設定されている。

				string t_path;
				if(t_assetbundle_item.assetbundle_dummy.asset_list.TryGetValue(a_asset_name,out t_path) == true){
					Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadResourcesPrefabFile,new File.Path(t_path));

					do{
						if(a_callback_interface != null){
							a_callback_interface.OnAssetBundleListCoroutine(t_item.GetResultProgress());
						}
						yield return null;
					}while(t_item.IsBusy() == true);

					UnityEngine.GameObject t_prefab = null;

					if(t_item.GetResultAssetType() == Asset.AssetType.Prefab){
						if(t_item.GetResultAssetPrefab() != null){
							t_prefab = t_item.GetResultAssetPrefab();
						}
					}

					if(t_prefab == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadAssetBundleItemPrefabFile : prefab = null : " + a_assetbundle_name + " : " + a_asset_name;
						yield break;
					}

					this.result.asset_file = new Asset.Asset(Asset.AssetType.Prefab,t_prefab);
				}else{
					//失敗。
					this.result.errorstring = "Coroutine_LoadAssetBundleItemPrefabFile : " + a_assetbundle_name + " : " + a_asset_name;
					yield break;
				}

			}else if(t_assetbundle_item.assetbundle_raw != null){

				//アセットバンドル。

				UnityEngine.AssetBundleRequest t_request = t_assetbundle_item.assetbundle_raw.LoadAssetAsync(a_asset_name);

				if(t_request == null){
					//失敗。
					this.result.errorstring = "Coroutine_LoadAssetBundleItemPrefabFile : request = null : " + a_assetbundle_name + " : " + a_asset_name;
					yield break;
				}

				do{
					if(a_callback_interface != null){
						a_callback_interface.OnAssetBundleListCoroutine(t_request.progress);
					}
					yield return null;
				}while(t_request.isDone == false);

				UnityEngine.GameObject t_prefab = t_request.asset as UnityEngine.GameObject;

				if(t_prefab == null){
					//失敗。
					this.result.errorstring = "Coroutine_LoadAssetBundleItemPrefabFile : prefab = null : " + a_assetbundle_name + " : " + a_asset_name;
					yield break;
				}

				this.result.asset_file = new Asset.Asset(Asset.AssetType.Prefab,t_prefab);
			}else{
				Tool.Assert(false);
			}

			//失敗。
			this.result.errorstring = "Coroutine_LoadAssetBundleItemPrefabFile : " + a_assetbundle_name + " : " + a_asset_name;
			yield break;
		}
	}
}

