

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
	/** ロードアンドロイドコンテンツ。バイナリファイル。
	*/
	public class Coroutine_LoadAndroidContentBinaryFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** バイナリファイル。
			*/
			public byte[] binary_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType(byte[] a_binary_file,string a_errorstring)
			{
				//binary_file
				this.binary_file = a_binary_file;

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
			byte[] t_result_binary = null;
			{
				//開始。
				{
					do{
						try{
							bool t_result = Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_Start(a_path);
							if(t_result == true){
								//成功。
								break;
							}else{
								//キャンセルチェック。
								if(a_callback_interface != null){
									if(a_callback_interface.OnFileCoroutine(0.0f) == false){
										//エラー。
										this.result = new ResultType(null,"Cancel : LoadAndroidContentBinaryFile : " + a_path.GetPath());
										yield break;
									}
								}
							}
						}catch(System.Exception t_exception){
							//エラー。
							this.result = new ResultType(null,"Unknown Error : LoadAndroidContentBinaryFile : " + a_path.GetPath() + " : " + t_exception.Message);
							yield break;
						}

						yield return null;
					}while(true);
				}

				//読み込み中。
				{
					do{
						try{
							if(Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_IsComplate() == true){
								byte[] t_result = Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_GetResult();

								if(t_result == null){
									//エラー。
									this.result = new ResultType(null,"Load Error : LoadAndroidContentBinaryFile : " + a_path.GetPath());
									yield break;
								}

								//成功。
								t_result_binary = t_result;
								break;
							}else{
								//キャンセルチェック。
								{
									if(a_callback_interface != null){
										if(a_callback_interface.OnFileCoroutine(0.0f) == false){
											Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_Cancel();
										}
									}
								}
							}
						}catch(System.Exception t_exception){
							//エラー。
							this.result = new ResultType(null,"Load Error : LoadAndroidContentBinaryFile : " + a_path.GetPath() + " : " + t_exception.Message);
							yield break;
						}

						yield return null;
					}while(true);

					//終了。
					Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_End();
				}
			}

			//成功。
			this.result = new ResultType(t_result_binary,null);
			yield break;
		}
	}
}

