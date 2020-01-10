

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
	/** ロードアンドロイドコンテンツ。テキストファイル。
	*/
	public class Coroutine_LoadAndroidContentTextFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** テキストファイル。
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
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path)
		{
			//result
			this.result = new ResultType();

			//開始。
			bool t_is_open = false;
			do{
				bool t_ret = Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_Start(a_path);
				if(t_ret == true){
					t_is_open = true;
					break;
				}else{
					//キャンセル。
					if(a_callback_interface != null){
						if(a_callback_interface.OnFileCoroutine(0.0f) == false){
							yield break;
						}
					}
					yield return null;
				}
			}while(true);

			byte[] t_result_binary = null;

			//読み込み中。
			if(t_is_open == true){
				do{
					if(Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_IsComplate() == true){
						t_result_binary = Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_GetResult();
						break;
					}else{
						//キャンセル。
						if(a_callback_interface != null){
							if(a_callback_interface.OnFileCoroutine(0.0f) == false){
								Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_Cancel();
							}
						}
						yield return null;
					}
				}while(true);

				//終了。
				Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_End();
			}

			if(t_result_binary == null){
				this.result.errorstring = "Coroutine_LoadAndroidContentBianryFile : result_bianry == null : " + a_path.GetPath();
				yield break;
			}

			string t_result_text = System.Text.Encoding.UTF8.GetString(t_result_binary);

			if(t_result_text == null){
				this.result.errorstring = "Coroutine_LoadAndroidContentBianryFile : result_text == null : " + a_path.GetPath();
				yield break;
			}

			this.result.text_file = t_result_text;
			yield break;
		}
	}
}

