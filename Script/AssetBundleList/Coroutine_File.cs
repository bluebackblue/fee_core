

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドル。コルーチン。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** ファイル。
	*/
	public class Coroutine_File
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** アセットバンドルファイル。
			*/
			public UnityEngine.AssetBundle assetbundle_file;

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
				//assetbundle_file
				this.assetbundle_file = null;

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
			Progress_MainStep_1_LoadAssetBundle = 1,

			Max,
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.AssetBundleList.OnAssetBundleCoroutine_CallBackInterface a_callback_interface,string a_id)
		{
			//result
			this.result = new ResultType();

			/** progress
			*/
			Progress t_progress = new Progress(new float[]{
				0.5f,
				0.5f,
			});

			{
				UnityEngine.AssetBundle t_assetbundle = AssetBundleList.GetInstance().GetAssetBundle(a_id);
				if(t_assetbundle != null){
					//成功。
					this.result.assetbundle_file = t_assetbundle;
					yield break;
				}
			}

			AssetBundleList.PathItem t_pathitem = AssetBundleList.GetInstance().GetPathItem(a_id);
			if(t_pathitem == null){
				//失敗。
				this.result.errorstring = "Coroutine_File : Not Found Path : " + a_id;
				yield break;
			}

			//バイナリのロード。
			byte[] t_binary = null;
			{
				Fee.File.Item t_item = null;

				switch(t_pathitem.pathtype){
				#if(UNITY_EDITOR)
				case Fee.AssetBundleList.PathType.AssetsAssetBundle:
					{
						t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadAssetsBinaryFile,t_pathitem.path);
					}break;
				#endif
				case Fee.AssetBundleList.PathType.UrlAssetBundle:
					{
						t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadUrlBinaryFile,t_pathitem.path);
					}break;
				}

				if(t_item != null){
					while(true){
						if(t_item.GetResultType() != File.Item.ResultType.None){
							if(t_item.GetResultAssetType() == Asset.AssetType.Binary){
								//成功。
								t_binary = t_item.GetResultAssetBinary();
								break;
							}else if(t_item.GetResultType() == File.Item.ResultType.Error){
								//失敗。
								this.result.errorstring = "Coroutine_File : " + a_id + " : " + t_item.GetResultErrorString();
								yield break;
							}else{
								//不明。
								this.result.errorstring = "Coroutine_File : " + a_id + " : Unknown";
								yield break;
							}
						}

						//■ステップ０。
						if(a_callback_interface != null){
							t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_0_LoadBinary,(int)Progress_MainStep.Max,0,1);
							a_callback_interface.OnAssetBundleCoroutine(t_progress.CalcProgress(t_item.GetResultProgress()));
						}
						yield return null;
					}
				}
			}

			if(t_binary == null){
				//失敗。
				this.result.errorstring = "Coroutine_File : binary = null";
				yield break;
			}

			//アセットバンドル化。
			UnityEngine.AssetBundleCreateRequest t_request = null;

			try{
				t_request = UnityEngine.AssetBundle.LoadFromMemoryAsync(t_binary);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			if(t_request == null){
				this.result.errorstring = "Coroutine_File : " + a_id + " : request == null";
				yield break;
			}

			//isDone
			while(t_request.isDone == true){
				//■ステップ１。
				if(a_callback_interface != null){
					t_progress.SetStep((int)Progress_MainStep.Progress_MainStep_1_LoadAssetBundle,(int)Progress_MainStep.Max,0,1);
					a_callback_interface.OnAssetBundleCoroutine(t_progress.CalcProgress(t_request.progress));
				}
				yield return null;
			}

			UnityEngine.AssetBundle t_result_assetbundle = t_request.assetBundle;

			if(t_result_assetbundle == null){
				this.result.errorstring = "Coroutine_File : " + a_id + " : assetbundle == null";
				yield break;
			}

			//■アセットバンドル登録。
			Fee.AssetBundleList.AssetBundleList.GetInstance().RegisterAssetBundle(a_id,t_result_assetbundle);

			this.result.assetbundle_file = t_result_assetbundle;
			yield break;
		}
	}
}

