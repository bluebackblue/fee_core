

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
	/** ロードリソース。テクスチャファイル。
	*/
	public class Coroutine_LoadResourcesTextureFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** テクスチャファイル。
			*/
			public UnityEngine.Texture2D texture_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType(UnityEngine.Texture2D a_texture_file,string a_errorstring)
			{
				//texture_file
				this.texture_file = a_texture_file;

				//errorstring
				this.errorstring = a_errorstring;
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
			this.result = null;

			//ロード。
			UnityEngine.Object t_result_object = null;
			{
				UnityEngine.ResourceRequest t_request = null;

				try{
					t_request = UnityEngine.Resources.LoadAsync(a_path.GetPath());
					if(t_request == null){
						//エラー。
						this.result = new ResultType(null,"Unknown Error : LoadResourcesTextureFile : " + a_path.GetPath());
						yield break;
					}
				}catch(System.Exception t_exception){
					//エラー。
					this.result = new ResultType(null,"Unknown Error : LoadResourcesPrefabFile : " + a_path.GetPath() + " : " + t_exception.Message);
					yield break;
				}

				do{
					//キャンセルチェック。
					{
						if(a_callback_interface != null){
							if(a_callback_interface.OnFileCoroutine(t_request.progress) == false){
								//t_request.Cancel();
							}
						}
					}

					yield return null;
				}while(t_request.isDone == false);

				if(t_request.asset == null){
					//エラー。
					this.result = new ResultType(null,"Load Error : LoadResourcesTextureFile : " + a_path.GetPath());
					yield break;
				}

				t_result_object = t_request.asset;
			}

			//コンバート。
			UnityEngine.Texture2D t_result_texture = null;
			{
				UnityEngine.Texture2D t_result = t_result_object as UnityEngine.Texture2D;

				if(t_result == null){
					//エラー。
					this.result = new ResultType(null,"Convert Error : LoadResourcesTextureFile : " + a_path.GetPath());
					yield break;
				}

				t_result_texture = t_result;
			}

			//成功。
			this.result = new ResultType(t_result_texture,null);
			yield break;
		}
	}
}

