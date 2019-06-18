

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** ロードローカル。テクスチャーファイル。
	*/
	public class Coroutine_LoadLocalTextureFile : OnTask_CallBack
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

		/** taskprogress
		*/
		public float taskprogress;

		/** [Fee.File.OnTask_CallBack]タスク実行中。
		*/
		public void OnTask(float a_progress)
		{
			this.taskprogress = a_progress;
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,Fee.File.Path a_path)
		{
			//result
			this.result = new ResultType();

			//taskprogress_
			this.taskprogress = 0.0f;

			//キャンセルトークン。
			Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

			//タスク起動。
			Fee.TaskW.Task<Task_LoadLocalTextureFile.ResultType> t_task = Task_LoadLocalTextureFile.Run(this,a_path,t_cancel_token);

			//終了待ち。
			do{
				//キャンセル。
				if(a_instance != null){
					if(a_instance.OnCoroutine(1.0f,this.taskprogress) == false){
						t_cancel_token.Cancel();
					}
				}
				yield return null;
			}while(t_task.IsEnd() == false);

			//結果。
			Task_LoadLocalTextureFile.ResultType t_result = t_task.GetResult();

			//成功。
			if(t_task.IsSuccess() == true){
				if(t_result.binary != null){

					//コンバート。
					UnityEngine.Texture2D t_result_texture = null;

					try{
						t_result_texture = BinaryToTexture2D.Convert(t_result.binary);
					}catch(System.Exception t_exception){
						this.result.errorstring = "Coroutine_LoadLocalTextureFile : " + t_exception.Message;
						yield break;
					}

					if(t_result_texture != null){
						this.result.texture_file = t_result_texture;
						yield break;
					}else{
						this.result.errorstring = "Coroutine_LoadLocalTextureFile : result_texture == null";
						yield break;
					}
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "Coroutine_LoadLocalTextureFile : null";
				yield break;
			}
		}
	}
}

