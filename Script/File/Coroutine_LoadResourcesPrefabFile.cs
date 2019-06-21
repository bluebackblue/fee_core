

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
	/** ロードリソース。プレハブファイル。
	*/
	public class Coroutine_LoadResourcesPrefabFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** プレハブファイル。
			*/
			public UnityEngine.GameObject prefab_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//prefab_file
				this.prefab_file = null;

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
				this.result.errorstring = "Coroutine_LoadResourcesPrefabFile : " + a_path.GetPath();
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

			UnityEngine.GameObject t_result_prefab = t_resourcerequest.asset as UnityEngine.GameObject;

			if(t_result_prefab == null){
				this.result.errorstring = "Coroutine_LoadResourcesPrefabFile : result_prefab == null : " + a_path.GetPath();
				yield break;
			}
			
			this.result.prefab_file = t_result_prefab;
			yield break;
		}
	}
}

