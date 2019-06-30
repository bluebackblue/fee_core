

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
	/** ロードパス。アセットバンドルアイテム。。
	*/
	public class Coroutine_LoadPathAssetBundleItem
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** アセットバンドルアイテム。
			*/
			public AssetBundlePackList_AssetBundleItem assetbundleitem;

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
				//assetbundleitem
				this.assetbundleitem = null;

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
		public System.Collections.IEnumerator CoroutineMain(Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface a_callback_interface,string a_id)
		{
			//result
			this.result = new ResultType();

			{
				AssetBundlePackList_AssetBundleItem t_item_assetbundle = Fee.AssetBundleList.AssetBundleList.GetInstance().GetAssetBundleItem(a_id);

				if(t_item_assetbundle != null){
					//成功。
					this.result.assetbundleitem = t_item_assetbundle;
					yield break;
				}
			}

			AssetBundlePathList_PathItem t_pathitem = AssetBundleList.GetInstance().GetPathItem(a_id);

			if(t_pathitem == null){
				//失敗。
				this.result.errorstring = "Coroutine_LoadAssetBundleItem : Not Found Path : " + a_id;
				yield break;
			}

			if(t_pathitem.pathtype == AssetBundlePathList_PathType.AssetsDummyAssetBundle){

				//ダミーアセットバンドル。

				//ＪＳＯＮ。

				string t_result_string = null;
				{
					Fee.File.Item t_item_json = null;

					switch(t_pathitem.pathtype){
					case AssetBundlePathList_PathType.AssetsDummyAssetBundle:
						{
							t_item_json = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadAssetsTextFile,t_pathitem.path);
						}break;
					default:
						{
							//失敗。
							this.result.errorstring = "Coroutine_LoadAssetBundleItem : PathTypeError : " + a_id;
							yield break;
						}break;
					}

					if(t_item_json == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadAssetBundleItem : item_json = null : " + a_id;
						yield break;
					}

					while(t_item_json.IsBusy() == true){
						yield return null;
					}

					if(t_item_json.GetResultAssetType() == Asset.AssetType.Text){
						t_result_string = t_item_json.GetResultAssetText();
					}

					if(t_result_string == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadAssetBundleItem : result_string == null : " + a_id;
						yield break;
					}
				}

				{
					//DummryAssetBundle
					Fee.AssetBundleList.DummryAssetBundle t_dummyassetbundle = Fee.JsonItem.Convert.JsonStringToObject<Fee.AssetBundleList.DummryAssetBundle>(t_result_string);

					if(t_dummyassetbundle == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadAssetBundleItem : dummyassetbundle = null : " + a_id;
						yield break;
					}

					{
						AssetBundlePackList_AssetBundleItem t_assetbundleitem = new AssetBundlePackList_AssetBundleItem(t_dummyassetbundle);

						//■登録。
						Fee.AssetBundleList.AssetBundleList.GetInstance().RegisterAssetBundle(a_id,t_assetbundleitem);

						//成功。
						this.result.assetbundleitem = t_assetbundleitem;
						yield break;
					}
				}

			}else{

				//アセットバンドル。

				//バイナリ。

				byte[] t_result_binary = null;
				{
					Fee.File.Item t_item_bianry = null;

					switch(t_pathitem.pathtype){
					#if(UNITY_EDITOR)
					case AssetBundlePathList_PathType.AssetsAssetBundle:
						{
							//アセット。アセットバンドル。
							t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadAssetsBinaryFile,t_pathitem.path);
						}break;
					#endif
					case  AssetBundlePathList_PathType.UrlAssetBundle:
						{
							//ＵＲＬ。アセットバンドル。
							t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadUrlBinaryFile,t_pathitem.path);
						}break;
					default:
						{
							//失敗。
							this.result.errorstring = "Coroutine_LoadAssetBundleItem : PathTypeError : " + a_id;
							yield break;
						}break;
					}

					if(t_item_bianry == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadAssetBundleItem : item_bianry = null : " + a_id;
						yield break;
					}

					while(t_item_bianry.IsBusy() == true){
						yield return null;
					}

					if(t_item_bianry.GetResultAssetType() == Asset.AssetType.Binary){
						t_result_binary = t_item_bianry.GetResultAssetBinary();
					}

					if(t_result_binary == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadAssetBundleItem : result_binary == null : " + a_id;
						yield break;
					}
				}

				//アセットバンドル。

				{
					UnityEngine.AssetBundleCreateRequest t_request = null;

					//LoadFromMemoryAsync
					try{
						t_request = UnityEngine.AssetBundle.LoadFromMemoryAsync(t_result_binary);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}
					if(t_request == null){
						this.result.errorstring = "Coroutine_LoadAssetBundleItem : request == null : " + a_id;
						yield break;
					}
				
					while(t_request.isDone == true){
						yield return null;
					}

					if(t_request.assetBundle == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadAssetBundleItem : assetbundle == null : " + a_id;
						yield break;
					}

					{
						AssetBundlePackList_AssetBundleItem t_assetbundleitem = new AssetBundlePackList_AssetBundleItem(t_request.assetBundle);

						//■登録。
						Fee.AssetBundleList.AssetBundleList.GetInstance().RegisterAssetBundle(a_id,t_assetbundleitem);

						//成功。
						this.result.assetbundleitem = t_assetbundleitem;
						yield break;
					}
				}
			}
		}
	}
}

