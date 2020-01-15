

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
	/** ロードローカル。テクスチャファイル。
	*/
	public class Coroutine_LoadLocalTextureFile : Fee.File.OnFileTask_CallBackInterface
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

		/** taskprogress
		*/
		public float taskprogress;

		/** [Fee.File.OnFileTask_CallBackInterface]タスク実行中。
		*/
		public void OnFileTask(float a_progress)
		{
			this.taskprogress = a_progress;
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path)
		{
			//result
			this.result = null;

			//taskprogress_
			this.taskprogress = 0.0f;

			//ロード。
			byte[] t_result_binary = null;
			{
				//キャンセルトークン。
				Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

				//タスク起動。
				using(Fee.TaskW.Task<Task_LoadLocalBinaryFile.ResultType> t_task = Task_LoadLocalBinaryFile.Run(this,a_path,t_cancel_token)){

					//終了待ち。
					do{
						//キャンセルチェック。
						{
							if(a_callback_interface != null){
								if(a_callback_interface.OnFileCoroutine(this.taskprogress) == false){
									t_cancel_token.Cancel();
								}
							}
						}

						yield return null;
					}while(t_task.IsEnd() == false);

					//結果。
					Task_LoadLocalBinaryFile.ResultType t_result = t_task.GetResult();

					if(t_result.errorstring != null){
						//エラー。
						this.result = new ResultType(null,t_result.errorstring);
						yield break;
					}

					if(t_task.IsSuccess() == false){
						//エラー。
						this.result = new ResultType(null,"Task Error : LoadLocalTextureFile : " + a_path.GetPath());
						yield break;
					}

					if(t_result.binary == null){
						//エラー。
						this.result = new ResultType(null,"Unknown Error : LoadLocalTextureFile : " + a_path.GetPath());
						yield break;
					}

					//成功。
					t_result_binary = t_result.binary;
				}
			}

			//コンバート。
			UnityEngine.Texture2D t_result_texture = null;
			{
				t_result_texture = BinaryToTexture2D.Convert(t_result_binary);
				if(t_result_texture == null){
					//エラー。
					this.result = new ResultType(null,"Convert Error : LoadLocalTextureFile : " + a_path.ToString());
					yield break;
				}
			}

			//成功。
			this.result = new ResultType(t_result_texture,null);
			yield break;
		}
	}
}

