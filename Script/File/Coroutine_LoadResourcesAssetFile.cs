

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** ロードリソース。オブジェクトファイル。
	*/
	public class Coroutine_LoadResourcesAssetFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** アセットファイル。
			*/
			public UnityEngine.Object asset_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//asset_file
				this.asset_file = null;

				//errorstring
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,Fee.File.Path a_path)
		{
			//result
			this.result = new ResultType();

			UnityEngine.ResourceRequest t_resourcerequest = null;

			try{
				t_resourcerequest = UnityEngine.Resources.LoadAsync(a_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			if(t_resourcerequest == null){
				this.result.errorstring = "Coroutine_LoadResourcesAssetFile : " + a_path.GetPath();
				yield break;
			}

			//priority
			t_resourcerequest.priority = 100;

			//isDone
			while(t_resourcerequest.isDone == false){
				if(a_instance != null){
					a_instance.OnCoroutine(1.0f,t_resourcerequest.progress);
				}
				yield return null;
			}

			UnityEngine.Object t_result_asset = t_resourcerequest.asset;

			if(t_result_asset == null){
				this.result.errorstring = "Coroutine_LoadResourcesAssetFile : result_asset == null : " + a_path.GetPath();
				yield break;
			}
			
			this.result.asset_file = t_result_asset;
			yield break;
		}
	}
}

