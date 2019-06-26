

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
	/** ロードリソース。テクスチャーファイル。
	*/
	public class Coroutine_LoadResourcesTextureFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** テクスチャーファイル。
			*/
			public UnityEngine.Texture2D texture_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//texture_file
				this.texture_file = null;

				//errorstring
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path)
		{
			//result
			this.result = new ResultType();

			UnityEngine.ResourceRequest t_request = null;

			try{
				t_request = UnityEngine.Resources.LoadAsync(a_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			if(t_request == null){
				this.result.errorstring = "Coroutine_LoadResourcesTextureFile : " + a_path.GetPath();
				yield break;
			}

			//isDone
			while(t_request.isDone == false){
				if(a_callback_interface != null){
					a_callback_interface.OnFileCoroutine(t_request.progress);
				}
				yield return null;
			}

			//Texture
			UnityEngine.Texture2D t_result_texture = t_request.asset as UnityEngine.Texture2D;

			if(t_result_texture == null){
				this.result.errorstring = "Coroutine_LoadResourcesTextureFile : result_texture == null : " + a_path.GetPath();
				yield break;
			}

			this.result.texture_file = t_result_texture;
			yield break;
		}
	}
}

