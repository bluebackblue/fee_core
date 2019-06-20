

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** ロードリソース。なんでもファイル。
	*/
	public class Coroutine_LoadResourcesAnythingFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** アセットファイル。
			*/
			public UnityEngine.Object anything_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//anything_file
				this.anything_file = null;

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
				this.result.errorstring = "Coroutine_LoadResourcesAnythingFile : " + a_path.GetPath();
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

			UnityEngine.Object t_result_anything = t_resourcerequest.asset;

			if(t_result_anything == null){
				this.result.errorstring = "Coroutine_LoadResourcesAnythingFile : result_anything == null : " + a_path.GetPath();
				yield break;
			}
			
			this.result.anything_file = t_result_anything;
			yield break;
		}
	}
}

