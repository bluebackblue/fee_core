

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

			if(t_assetbundleitem.assetbundle == null){
				//失敗。
				this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : " + a_id;
				yield break;
			}

			UnityEngine.AssetBundleRequest t_request = t_assetbundleitem.assetbundle.LoadAssetAsync(a_assetname);

			if(t_request == null){
				//失敗。
				this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : " + a_id;
				yield break;
			}

			while(t_request.isDone == true){
				yield return null;
			}

			UnityEngine.TextAsset t_textasset = t_request.asset as UnityEngine.TextAsset;

			if(t_textasset == null){
				//失敗。
				this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : " + a_id;
				yield break;
			}

			string t_string = t_textasset.text;

			if(t_string == null){
				//失敗。
				this.result.errorstring = "Coroutine_LoadAssetBundleItemTextFile : " + a_id;
				yield break;
			}

			this.result.asset_file = new Asset.Asset(Asset.AssetType.Text,t_string);
		}
	}
}

