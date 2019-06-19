

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
	/** ロードリソース。テキストファイル。
	*/
	public class Coroutine_LoadResourcesTextFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** アセットファイル。
			*/
			public string text_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//text_file
				this.text_file = null;

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
				this.result.errorstring = "Coroutine_LoadResourcesTextFile : " + a_path.GetPath();
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

			//TextAsset
			UnityEngine.TextAsset t_result_textasset = t_resourcerequest.asset as UnityEngine.TextAsset;

			if(t_result_textasset == null){
				this.result.errorstring = "Coroutine_LoadResourcesTextFile : result_textasset == null : " + a_path.GetPath();
				yield break;
			}

			//string
			string t_result_string = null;
			if(t_result_textasset != null){
				t_result_string = t_result_textasset.text;
			}

			if(t_result_string == null){
				this.result.errorstring = "Coroutine_LoadResourcesTextFile : result_string == null : " + a_path.GetPath();
				yield break;
			}
			
			this.result.text_file = t_result_string;
			yield break;
		}
	}
}

