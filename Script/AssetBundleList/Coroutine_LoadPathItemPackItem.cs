

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
	/** ロードパス。パックアイテム。

		パスアイテムからパックアイテムをロード。

	*/
	public class Coroutine_LoadPathItemPackItem
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** パックアイテム。
			*/
			public PackItem pack_item;

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
				//pack_item
				this.pack_item = null;

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
				PackItem t_pack_item = Fee.AssetBundleList.AssetBundleList.GetInstance().GetPackItem(a_assetbundle_name);

				if(t_pack_item != null){
					//成功。
					this.result.pack_item = t_pack_item;
					yield break;
				}
			}

			PathItem t_pathitem = AssetBundleList.GetInstance().GetPathItem(a_assetbundle_name);

			if(t_pathitem == null){
				//失敗。
				this.result.errorstring = "Coroutine_LoadPathItemPackItem : Not Found Path : " + a_assetbundle_name;
				yield break;
			}

			//ダミーアセットバンドルの読み込み。
			#if(UNITY_EDITOR)
			if(t_pathitem.assetbundle_pathtype == AssetBundlePathType.AssetsDummyAssetBundle){
				string t_result_string = null;
				{
					Fee.File.Item t_item_dummy_assetbundle_json = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadAssetsTextFile,t_pathitem.assetbundle_path);
					if(t_item_dummy_assetbundle_json == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadPathItemPackItem : item_json = null : " + a_assetbundle_name;
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
						this.result.errorstring = "Coroutine_LoadPathItemPackItem : result_string == null : " + a_assetbundle_name;
						yield break;
					}
				}

				//DummryAssetBundle
				Fee.AssetBundleList.DummryAssetBundle t_dummyassetbundle = Fee.JsonItem.Convert.JsonStringToObject<Fee.AssetBundleList.DummryAssetBundle>(t_result_string);

				if(t_dummyassetbundle == null){
					//失敗。
					this.result.errorstring = "Coroutine_LoadPathItemPackItem : dummyassetbundle = null : " + a_assetbundle_name;
					yield break;
				}

				PackItem t_pack_item = new PackItem(t_dummyassetbundle);

				//登録。
				Fee.AssetBundleList.AssetBundleList.GetInstance().RegistPackItem(a_assetbundle_name,t_pack_item);

				//成功。
				this.result.pack_item = t_pack_item;
				yield break;
			}
			#endif
			
			//アセットバンドルの読み込み。
			{
				//アセットバンドル。
				Progress t_progress = new Progress(new float[]{
					0.5f,	
					0.5f
				});

				//アセットバンドルのバイナリ読み込み。
				byte[] t_result_binary = null;
				{
					Fee.File.Item t_item_bianry = null;

					switch(t_pathitem.assetbundle_pathtype){
					#if(UNITY_EDITOR)
					case AssetBundlePathType.AssetsAssetBundle:
						{
							//アセットフォルダにあるアセットバンドルの相対パス。
							t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadAssetsBinaryFile,t_pathitem.assetbundle_path);
						}break;
					#endif
					case  AssetBundlePathType.UrlAssetBundle:
						{
							//アセットバンドルのＵＲＬ。
							t_item_bianry = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadUrlBinaryFile,t_pathitem.assetbundle_path);
						}break;
					default:
						{
							//失敗。
							this.result.errorstring = "Coroutine_LoadPathItemPackItem : PathTypeError : " + a_assetbundle_name;
							yield break;
						}break;
					}

					if(t_item_bianry == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadPathItemPackItem : item_bianry = null : " + a_assetbundle_name;
						yield break;
					}

					do{
						//■ステップ０。
						if(a_callback_interface != null){
							t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_0_LoadBinary,(int)Progress_MainStep.Max,0,1);
							a_callback_interface.OnAssetBundleListCoroutine(t_progress.CalcProgress(t_item_bianry.GetResultProgress()));
						}
						yield return null;
					}while(t_item_bianry.IsBusy() == true);

					if(t_item_bianry.GetResultAssetType() == Asset.AssetType.Binary){
						t_result_binary = t_item_bianry.GetResultAssetBinary();
					}

					if(t_result_binary == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadPathItemPackItem : result_binary == null : " + a_assetbundle_name;
						yield break;
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
						this.result.errorstring = "Coroutine_LoadPathItemPackItem : request == null : " + a_assetbundle_name;
						yield break;
					}
				
					do{
						//■ステップ１。
						if(a_callback_interface != null){
							t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_1_LoadFromMemoryAsync,(int)Progress_MainStep.Max,0,1);
							a_callback_interface.OnAssetBundleListCoroutine(t_progress.CalcProgress(t_request.progress));
						}
						yield return null;
					}while(t_request.isDone == true);

					if(t_request.assetBundle == null){
						//失敗。
						this.result.errorstring = "Coroutine_LoadPathItemPackItem : assetbundle == null : " + a_assetbundle_name;
						yield break;
					}

					{
						PackItem t_pack_item = new PackItem(t_request.assetBundle);

						//登録。
						Fee.AssetBundleList.AssetBundleList.GetInstance().RegistPackItem(a_assetbundle_name,t_pack_item);

						//成功。
						this.result.pack_item = t_pack_item;
						yield break;
					}
				}
			}
		}
	}
}

