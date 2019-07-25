

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
	/** ロードアセットバンドルアイテム。テキストファイル。
	*/
	public class Coroutine_LoadAssetBundleItemTextFile
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
		public System.Collections.IEnumerator CoroutineMain(Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface a_callback_interface,string a_id,string a_assetname)
		{
			//result
			this.result = new ResultType();

			AssetBundlePackList_AssetBundleItem t_assetbundleitem = Fee.AssetBundleList.AssetBundleList.GetInstance().GetAssetBundleItem(a_id);

			if(t_assetbundleitem == null){
				//失敗。
				this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : " + a_id;
				yield break;
			}

			if(t_assetbundleitem.assetbundle_dummy != null){

				//ダミーアセットバンドル。

				string t_path;
				if(t_assetbundleitem.assetbundle_dummy.asset_list.TryGetValue(a_assetname,out t_path) == true){
					Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadResourcesTextFile,new File.Path(t_path));

					do{
						if(a_callback_interface != null){
							a_callback_interface.OnAssetBundleListCoroutine(t_item.GetResultProgress());
						}
						yield return null;
					}while(t_item.IsBusy() == true);

					string t_string = null;

					if(t_item.GetResultAssetType() == Asset.AssetType.Text){
						if(t_item.GetResultAssetText() != null){
							t_string = t_item.GetResultAssetText();
						}
					}

					if(t_string == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : string = null : " + a_id + " : " + a_assetname;
						yield break;
					}

					this.result.asset_file = new Asset.Asset(Asset.AssetType.Text,t_string);
				}else{
					//失敗。
					this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : " + a_id;
					yield break;
				}

			}else if(t_assetbundleitem.assetbundle != null){

				//アセットバンドル。

				UnityEngine.AssetBundleRequest t_request = t_assetbundleitem.assetbundle.LoadAssetAsync(a_assetname);

				if(t_request == null){
					//失敗。
					this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : " + a_id;
					yield break;
				}

				do{
					if(a_callback_interface != null){
						a_callback_interface.OnAssetBundleListCoroutine(t_request.progress);
					}
					yield return null;
				}while(t_request.isDone == true);

				UnityEngine.TextAsset t_text = t_request.asset as UnityEngine.TextAsset;

				if(t_text == null){
					//失敗。
					this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : " + a_id;
					yield break;
				}

				string t_string = t_text.text;

				if(t_string == null){
					//失敗。
					this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : " + a_id;
					yield break;
				}

				this.result.asset_file = new Asset.Asset(Asset.AssetType.Text,t_string);
			}else{
				Tool.Assert(false);
			}

			//失敗。
			this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : " + a_id;
			yield break;
		}
	}
}

