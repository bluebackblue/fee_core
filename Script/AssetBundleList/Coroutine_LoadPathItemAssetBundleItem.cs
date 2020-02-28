

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
	/** ロードパス。アセットバンドルアイテム。

		パスアイテムからアセットバンドルアイテムをロード。

	*/
	public class Coroutine_LoadPathItemAssetBundleItem
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** アセットバンドルアイテム。
			*/
			public AssetBundleItem assetbundle_item;

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
				//assetbundle_item
				this.assetbundle_item = null;

				//errorstring
				this.errorstring = null;

				//responseheader
				this.responseheader = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** Progress_MainStep
		*/
		private enum Progress_MainStep
		{
			Progress_MainStep_0_LoadBinary = 0,
			Progress_MainStep_1_LoadFromMemoryAsync = 1,

			Max,
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface a_callback_interface,string a_assetbundle_name)
		{
			//result
			this.result = new ResultType();

			{
				AssetBundleItem t_assetbundle_item = Fee.AssetBundleList.AssetBundleList.GetInstance().GetAssetBundleItem(a_assetbundle_name);

				if(t_assetbundle_item != null){
					//成功。
					this.result.assetbundle_item = t_assetbundle_item;
					yield break;
				}
			}

			PathItem t_pathitem = AssetBundleList.GetInstance().GetPathItem(a_assetbundle_name);

			if(t_pathitem == null){
				//失敗。
				this.result.errorstring = "Coroutine_LoadPathItemAssetBundleItem : Not Found Path : " + a_assetbundle_name;
				yield break;
			}

			//ダミーアセットバンドルの読み込み。
			#if(UNITY_EDITOR)

			if(t_pathitem.assetbundle_pathtype == AssetBundlePathType.AssetsPathDummyAssetBundle){
				string t_result_string = null;
				{
					Fee.File.Item t_item_dummy_assetbundle_json = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadAssetsPathTextFile,t_pathitem.assetbundle_path);
					if(t_item_dummy_assetbundle_json == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadPathItemAssetBundleItem : item_json = null : " + a_assetbundle_name;
						yield break;
					}

					do{
						if(a_callback_interface != null){
							a_callback_interface.OnAssetBundleListCoroutine(t_item_dummy_assetbundle_json.GetResultProgress());
						}
						yield return null;
					}while(t_item_dummy_assetbundle_json.IsBusy() == true);

					if(t_item_dummy_assetbundle_json.GetResultAssetType() == Asset.AssetType.Text){
						t_result_string = t_item_dummy_assetbundle_json.GetResultAssetText();
					}

					if(t_result_string == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadPathItemAssetBundleItem : result_string == null : " + a_assetbundle_name;
						yield break;
					}
				}

				//DummryAssetBundle
				Fee.AssetBundleList.DummryAssetBundle t_dummyassetbundle = Fee.JsonItem.Convert.JsonStringToObject<Fee.AssetBundleList.DummryAssetBundle>(t_result_string);

				if(t_dummyassetbundle == null){
					//失敗。
					this.result.errorstring = "Coroutine_LoadPathItemAssetBundleItem : dummyassetbundle = null : " + a_assetbundle_name;
					yield break;
				}

				AssetBundleItem t_assetbundle_item = new AssetBundleItem(t_dummyassetbundle,t_pathitem);

				//登録。
				Fee.AssetBundleList.AssetBundleList.GetInstance().RegistAssetBundleItem(a_assetbundle_name,t_assetbundle_item);

				//成功。
				this.result.assetbundle_item = t_assetbundle_item;
				yield break;
			}

			#endif
			
			//アセットバンドルの読み込み。
			{
				//アセットバンドル。
				Fee.Pattern.Progress t_progress = new Fee.Pattern.Progress(new float[]{
					0.5f,	
					0.5f
				});

				//アセットバンドルのバイナリ読み込み。
				byte[] t_result_binary = null;
				{
					Fee.File.Item t_item_bianry = null;

					switch(t_pathitem.assetbundle_pathtype){

					#if(UNITY_EDITOR)

					case AssetBundlePathType.AssetsPathAssetBundle:
						{
							//アセットフォルダにあるアセットバンドルの相対パス。
							t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadAssetsPathBinaryFile,t_pathitem.assetbundle_path);
						}break;

					#endif

					case AssetBundlePathType.UrlAssetBundle:
						{
							//ＵＲＬ。
							t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadUrlBinaryFile,t_pathitem.assetbundle_path);
						}break;
					case AssetBundlePathType.StreamingAssetsAssetBundle:
						{
							//ストリーミングアセット。
							t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadStreamingAssetsBinaryFile,t_pathitem.assetbundle_path);
						}break;
					case AssetBundlePathType.LocalAssetBundle:
						{
							//ローカル。
							t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadLocalBinaryFile,t_pathitem.assetbundle_path);
						}break;
					case AssetBundlePathType.FullPathAssetBundle:
						{
							//フルパス。
							t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadFullPathBinaryFile,t_pathitem.assetbundle_path);
						}break;

					default:
						{
							//失敗。
							this.result.errorstring = "Coroutine_LoadPathItemAssetBundleItem : PathTypeError : " + a_assetbundle_name;
							yield break;
						}break;
					}

					if(t_item_bianry == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadPathItemAssetBundleItem : item_bianry = null : " + a_assetbundle_name;
						yield break;
					}

					do{
						//■ステップ０。
						if(a_callback_interface != null){
							t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_0_LoadBinary,0,1);
							a_callback_interface.OnAssetBundleListCoroutine(t_progress.CalcProgress(t_item_bianry.GetResultProgress()));
						}
						yield return null;
					}while(t_item_bianry.IsBusy() == true);

					if(t_item_bianry.GetResultAssetType() == Asset.AssetType.Binary){
						t_result_binary = t_item_bianry.GetResultAssetBinary();
					}

					if(t_result_binary == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadPathItemAssetBundleItem : result_binary == null : " + a_assetbundle_name;
						yield break;
					}else{
						System.Collections.Generic.Dictionary<string,string> t_response_header = t_item_bianry.GetResultResponseHeader();
						if(t_response_header != null){
							string t_response_code;
							if(t_item_bianry.GetResultResponseHeader().TryGetValue(Fee.File.Config.RESPONSECODE_KEY,out t_response_code) == true){
								if(t_response_code != "200"){
									//失敗。
									this.result.errorstring = "Coroutine_LoadPathItemAssetBundleItem : t_response_code = " + t_response_code;
									yield break;
								}
							}
						}
					}
				}

				//バイナリーのアセットバンドル化。
				{
					UnityEngine.AssetBundleCreateRequest t_request = null;

					//LoadFromMemoryAsync
					try{
						t_request = UnityEngine.AssetBundle.LoadFromMemoryAsync(t_result_binary);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}
					if(t_request == null){
						this.result.errorstring = "Coroutine_LoadPathItemAssetBundleItem : request == null : " + a_assetbundle_name;
						yield break;
					}
				
					do{
						//■ステップ１。
						if(a_callback_interface != null){
							t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_1_LoadFromMemoryAsync,0,1);
							a_callback_interface.OnAssetBundleListCoroutine(t_progress.CalcProgress(t_request.progress));
						}
						yield return null;
					}while(t_request.isDone == false);

					if(t_request.assetBundle == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadPathItemAssetBundleItem : assetbundle == null : " + a_assetbundle_name;
						yield break;
					}

					{
						AssetBundleItem t_assetbundle_item = new AssetBundleItem(t_request.assetBundle,t_pathitem);

						#if(UNITY_EDITOR)
						{
							UnityEngine.Object[] t_object = t_request.assetBundle.LoadAllAssets();
							for(int ii=0;ii<t_object.Length;ii++){
								Tool.Log(a_assetbundle_name,t_object[ii].name);
							}
						}
						#endif



						//登録。
						Fee.AssetBundleList.AssetBundleList.GetInstance().RegistAssetBundleItem(a_assetbundle_name,t_assetbundle_item);

						//成功。
						this.result.assetbundle_item = t_assetbundle_item;
						yield break;
					}
				}
			}
		}
	}
}

