

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
	/** セーブローカル。テクスチャーファイル。
	*/
	public class Coroutine_SaveLocalTextureFile : Fee.File.OnFileTask_CallBackInterface
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** セーブ完了。
			*/
			public bool saveend;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//saveend
				this.saveend = false;

				//errorstring
				this.errorstring = null;
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
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path,UnityEngine.Texture2D a_texture)
		{
			//result
			this.result = new ResultType();

			//taskprogress_
			this.taskprogress = 0.0f;

			//バイナリ化。
			byte[] t_binary_png = null;
			{
				if(a_texture != null){
					try{
						t_binary_png = UnityEngine.ImageConversion.EncodeToPNG(a_texture);
					}catch(System.Exception t_exception){
						this.result.errorstring = "Coroutine_SaveLocalTextureFile : " + t_exception.Message;
						yield break;
					}
				}
				if(t_binary_png == null){
					this.result.errorstring = "Coroutine_SaveLocalTextureFile : binary_png == null";
					yield break;
				}
			}

			//キャンセルトークン。
			Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

			//タスク起動。
			Fee.TaskW.Task<Task_SaveLocalTextureFile.ResultType> t_task = Task_SaveLocalTextureFile.Run(this,a_path,t_binary_png,t_cancel_token);

			//終了待ち。
			do{
				//キャンセル。
				if(a_callback_interface != null){
					if(a_callback_interface.OnFileCoroutine(this.taskprogress,0.0f) == false){
						t_cancel_token.Cancel();
					}
				}
				yield return null;
			}while(t_task.IsEnd() == false);

			//結果。
			Task_SaveLocalTextureFile.ResultType t_result = t_task.GetResult();

			//成功。
			if(t_task.IsSuccess() == true){
				if(t_result.saveend == true){
					this.result.saveend = true;
					yield break;
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "Coroutine_SaveLocalTextureFile : null";
				yield break;
			}
		}
	}
}

