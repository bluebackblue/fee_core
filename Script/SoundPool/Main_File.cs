

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief サウンドプール。ファイル。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** Main_File
	*/
	public class Main_File : OnCoroutine_CallBack
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ロードローカル。サウンドプール。
			*/
			LoadLocalSoundPool,

			/** セーブローカル。サウンドプール。
			*/
			SaveLocalSoundPool,

			/** ダウンロード。サウンドプール。
			*/
			DownLoadSoundPool,
		};

		/** ResultType
		*/
		public enum ResultType
		{
			/** 未定義。
			*/
			None,

			/** エラー。
			*/
			Error,

			/** セーブ完了。
			*/
			SaveEnd,

			/** アセットバンドル。
			*/
			SoundPool,
		};

		/** is_busy
		*/
		private bool is_busy;

		/** キャンセル。チェック。
		*/
		private bool is_cancel;

		/** シャットダウン。チェック。
		*/
		private bool is_shutdown;

		/** request_type
		*/
		private RequestType request_type;

		/** request_path
		*/
		private File.Path request_path;

		/** request_post_data
		*/
		private UnityEngine.WWWForm request_post_data;

		/** request_soundpool
		*/
		private Fee.Audio.Pack_SoundPool request_soundpool;

		/** request_data_version
		*/
		private uint request_data_version;

		/** result_progress 
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_soundpool
		*/
		private Fee.Audio.Pack_SoundPool result_soundpool;

		/** result_responseheader
		*/
		private System.Collections.Generic.Dictionary<string,string> result_responseheader;

		/** constructor
		*/
		public Main_File()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_type = RequestType.None;
			this.request_path = null;
			this.request_post_data = null;
			this.request_soundpool = null;
			this.request_data_version = 0;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_soundpool = null;
			this.result_responseheader = null;
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.is_shutdown = true;
		}

		/** キャンセル。
		*/
		public void Cancel()
		{
			this.is_cancel = true;
		}

		/** 完了。
		*/
		public void Fix()
		{
			this.is_busy = false;
		}

		/** GetResultProgress
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** GetResultErrorString
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** GetResultType
		*/
		public ResultType GetResultType()
		{
			return this.result_type;
		}

		/** GetResultSoundPool
		*/
		public Fee.Audio.Pack_SoundPool GetResultSoundPool()
		{
			return this.result_soundpool;
		}

		/** GetResultResponseHeader
		*/
		public System.Collections.Generic.Dictionary<string,string> GetResultResponseHeader()
		{
			return this.result_responseheader;
		}

		/** [Fee.File.OnCoroutine_CallBack]コルーチンからのコールバック。

		戻り値 == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress = a_progress;
			return true;
		}

		/** リクエスト。ロードローカル。サウンドプール。
		*/
		public bool RequestLoadLocalSoundPool(File.Path a_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_type = RequestType.LoadLocalSoundPool;
				this.request_path = a_path;
				this.request_post_data = null;
				this.request_soundpool = null;
				this.request_data_version = 0;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_soundpool = null;

				Function.Function.StartCoroutine(this.DoLoadLocalSoundPool());
				return true;
			}

			return false;
		}

		/** 実行。ロードローカル。サウンドプール。
		*/
		private System.Collections.IEnumerator DoLoadLocalSoundPool()
		{
			Tool.Assert(this.request_type == RequestType.LoadLocalSoundPool);

			Coroutine_LoadLocalSoundPool t_coroutine = new Coroutine_LoadLocalSoundPool();
			yield return t_coroutine.CoroutineMain(this,this.request_path);

			if(t_coroutine.result.soundpool != null){
				this.result_progress = 1.0f;
				this.result_soundpool = t_coroutine.result.soundpool;
				this.result_type = ResultType.SoundPool;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。セーブローカル。サウンドプール。
		*/
		public bool RequestSaveLocalSoundPool(Fee.File.Path a_path,Fee.Audio.Pack_SoundPool a_soundpool)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_type = RequestType.SaveLocalSoundPool;
				this.request_path = a_path;
				this.request_post_data = null;
				this.request_soundpool = a_soundpool;
				this.request_data_version = 0;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_soundpool = null;

				Function.Function.StartCoroutine(this.DoSaveLocalSoundPool());
				return true;
			}

			return false;
		}

		/** 実行。セーブローカル。サウンドプール。
		*/
		private System.Collections.IEnumerator DoSaveLocalSoundPool()
		{
			Tool.Assert(this.request_type == RequestType.SaveLocalSoundPool);

			Coroutine_SaveLocalSoundPool t_coroutine = new Coroutine_SaveLocalSoundPool();
			yield return t_coroutine.CoroutineMain(this,this.request_path,this.request_soundpool);

			if(t_coroutine.result.saveend == true){
				this.result_progress = 1.0f;
				this.result_type = ResultType.SaveEnd;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}

			yield break;
		}

		/** リクエスト。ダウンロード。サウンドプール。
		*/
		public bool RequestDownLoadSoundPool(File.Path a_path,UnityEngine.WWWForm a_post_data,uint a_data_version)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_type = RequestType.DownLoadSoundPool;
				this.request_path = a_path;
				this.request_post_data = a_post_data;
				this.request_soundpool = null;
				this.request_data_version = a_data_version;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_soundpool = null;

				this.result_type = ResultType.None;

				Function.Function.StartCoroutine(this.DoDownLoadSoundPool());
				return true;
			}

			return false;
		}

		/** 実行。ダウンロード。サウンドプール。
		*/
		private System.Collections.IEnumerator DoDownLoadSoundPool()
		{
			Tool.Assert(this.request_type == RequestType.DownLoadSoundPool);

			Coroutine_DownLoadSoundPool t_coroutine = new Coroutine_DownLoadSoundPool();
			yield return t_coroutine.CoroutineMain(this,this.request_path,this.request_post_data,this.request_data_version);

			if(t_coroutine.result.soundpool != null){
				this.result_progress = 1.0f;
				this.result_soundpool = t_coroutine.result.soundpool;
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.SoundPool;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}
	}
}

