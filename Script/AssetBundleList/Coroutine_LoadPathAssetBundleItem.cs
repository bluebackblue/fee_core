

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

			//バイナリ。

			byte[] t_result_binary = null;
			{
				AssetBundlePathList_PathItem t_pathitem = AssetBundleList.GetInstance().GetPathItem(a_id);

				if(t_pathitem == null){
					//失敗。
					this.result.errorstring = "Coroutine_LoadAssetBundleItem : Not Found Path : " + a_id;
					yield break;
				}

				Fee.File.Item t_item_bianry = null;
				if(t_pathitem.pathtype == AssetBundlePathList_PathType.AssetsAssetBundle){
					//アセット。アセットバンドル。
					t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadAssetsBinaryFile,t_pathitem.path);
				}else if(t_pathitem.pathtype == AssetBundlePathList_PathType.UrlAssetBundle){
					//ＵＲＬ。アセットバンドル。
					t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadUrlBinaryFile,t_pathitem.path);
				}else{
					//失敗。
					this.result.errorstring = "Coroutine_LoadAssetBundleItem : PathTypeError : " + a_id;
					yield break;
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

				if(t_request.assetBundle != null){
					//■登録。
					Fee.AssetBundleList.AssetBundleList.GetInstance().RegisterAssetBundle(a_id,new AssetBundlePackList_AssetBundleItem(t_request.assetBundle));

					//成功。
					this.result.assetbundleitem = new AssetBundlePackList_AssetBundleItem(t_request.assetBundle);
					yield break;
				}else{
					//失敗。
					this.result.errorstring = "Coroutine_LoadAssetBundleItem : assetbundle == null : " + a_id;
					yield break;
				}
			}
		}
	}
}

