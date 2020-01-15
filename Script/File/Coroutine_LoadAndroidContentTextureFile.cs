

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
	/** ロードアンドロイドコンテンツ。テクスチャファイル。
	*/
	public class Coroutine_LoadAndroidContentTextureFile
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
										this.result = new ResultType(null,"Cancel : LoadAndroidContentTextureFile : " + a_path.GetPath());
										yield break;
									}
								}
							}
						}catch(System.Exception t_exception){
							//エラー。
							this.result = new ResultType(null,"Unknown Error : LoadAndroidContentTextureFile : " + a_path.GetPath() + " : " + t_exception.Message);
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
									this.result = new ResultType(null,"Load Error : LoadAndroidContentTextureFile : " + a_path.GetPath());
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
							this.result = new ResultType(null,"Load Error : LoadAndroidContentTextureFile : " + a_path.GetPath() + " : " + t_exception.Message);
							yield break;
						}

						yield return null;
					}while(true);

					//終了。
					Fee.Platform.Platform.GetInstance().LoadAndroidContentFile_End();
				}
			}

			//コンバート。
			UnityEngine.Texture2D t_result_texture = null;
			{
				try{
					UnityEngine.Texture2D t_result = BinaryToTexture2D.Convert(t_result_binary);

					if(t_result == null){
						//エラー。
						this.result = new ResultType(null,"Convert Error : LoadAndroidContentTextureFile : " + a_path.GetPath());
						yield break;
					}

					t_result_texture = t_result;
				}catch(System.Exception t_exception){
					//エラー。
					this.result = new ResultType(null,"Convert Error : LoadAndroidContentTextureFile : " + a_path.GetPath() + " : " + t_exception.Message);
					yield break;
				}
			}

			//成功。
			this.result = new ResultType(t_result_texture,null);
			yield break;
		}
	}
}

