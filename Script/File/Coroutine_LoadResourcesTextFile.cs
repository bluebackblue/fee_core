

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
			public ResultType(string a_text_file,string a_errorstring)
			{
				//text_file
				this.text_file = a_text_file;

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
						this.result = new ResultType(null,"Unknown Error : LoadResourcesTextFile : " + a_path.GetPath());
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
					this.result = new ResultType(null,"Load Error : LoadResourcesTextFile : " + a_path.GetPath());
					yield break;
				}

				t_result_object = t_request.asset;
			}

			//コンバート。
			string t_result_text = null;
			{
				UnityEngine.TextAsset t_textasset = t_result_object as UnityEngine.TextAsset;
				if(t_textasset == null){
					//エラー。
					this.result = new ResultType(null,"Convert Error : LoadResourcesTextFile : " + a_path.GetPath());
					yield break;
				}

				string t_result = null;

				try{
					t_result = t_textasset.text;
				}catch(System.Exception t_exception){
					//エラー。
					this.result = new ResultType(null,"Convert Error : LoadResourcesTextFile : " + a_path.GetPath() + " : " + t_exception.Message);
					yield break;
				}

				if(t_result == null){
					//エラー。
					this.result = new ResultType(null,"Convert Error : LoadResourcesTextFile : " + a_path.GetPath());
					yield break;
				}

				t_result_text = t_result;
			}

			//成功。
			this.result = new ResultType(t_result_text,null);
			yield break;
		}
	}
}

