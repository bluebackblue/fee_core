

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief サウンドプール。ファイル。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** Main_File
	*/
	public class Main_File : Fee.SoundPool.OnSoundPoolCoroutine_CallBackInterface
	{
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

			/** パック。
			*/
			Pack,
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

		/** request_path
		*/
		private File.Path request_path;

		/** request_post_data
		*/
		private System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> request_post_data;

		/** request_certificate_handler
		*/
		private Fee.File.CustomCertificateHandler request_certificate_handler;

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

		/** result_pack
		*/
		private Pack result_pack;

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
			this.request_path = null;
			this.request_post_data = null;
			this.request_certificate_handler = null;
			this.request_data_version = 0;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_pack = null;
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

		/** GetResultPack
		*/
		public Pack GetResultPack()
		{
			return this.result_pack;
		}

		/** GetResultResponseHeader
		*/
		public System.Collections.Generic.Dictionary<string,string> GetResultResponseHeader()
		{
			return this.result_responseheader;
		}

		/** [Fee.SoundPool.OnSoundPoolCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		public bool OnSoundPoolCoroutine(float a_progress)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress = a_progress;
			return true;
		}

		/** リクエスト。ロードローカル。パック。
		*/
		public bool RequestLoadLocalPack(File.Path a_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_path = a_path;
				this.request_post_data = null;
				this.request_certificate_handler = null;
				this.request_data_version = 0;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_pack = null;
				this.result_responseheader = null;

				Function.Function.GetInstance().StartCoroutine(this.DoLoadLocalPack());
				return true;
			}

			return false;
		}

		/** 実行。ロードローカル。パック。
		*/
		private System.Collections.IEnumerator DoLoadLocalPack()
		{
			Coroutine_LoadLocalPack t_coroutine = new Coroutine_LoadLocalPack();
			yield return t_coroutine.CoroutineMain(this,this.request_path);

			if(t_coroutine.result.pack != null){
				this.result_progress = 1.0f;
				this.result_pack = t_coroutine.result.pack;
				this.result_type = ResultType.Pack;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードストリーミングアセット。パック。
		*/
		public bool RequestLoadStreamingAssetsPack(Fee.File.Path a_path,uint a_data_version)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_path = a_path;
				this.request_post_data = null;
				this.request_certificate_handler = null;
				this.request_data_version = a_data_version;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_pack = null;
				this.result_responseheader = null;

				Function.Function.GetInstance().StartCoroutine(this.DoLoadStreamingAssetsPack());
				return true;
			}

			return false;
		}

		/** 実行。ロードストリーミングアセット。パック。
		*/
		private System.Collections.IEnumerator DoLoadStreamingAssetsPack()
		{
			Coroutine_LoadPack t_coroutine = new Coroutine_LoadPack();
			yield return t_coroutine.CoroutineMain(this,this.request_path,null,null,true,this.request_data_version);

			if(t_coroutine.result.pack != null){
				this.result_progress = 1.0f;
				this.result_pack = t_coroutine.result.pack;
				this.result_type = ResultType.Pack;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードＵＲＬ。パック。
		*/
		public bool RequestLoadUrlPack(File.Path a_path,System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler,uint a_data_version)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_path = a_path;
				this.request_post_data = a_post_data;
				this.request_certificate_handler = a_certificate_handler;
				this.request_data_version = a_data_version;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_pack = null;
				this.result_responseheader = null;

				Function.Function.GetInstance().StartCoroutine(this.DoLoadUrlPack());
				return true;
			}

			return false;
		}

		/** 実行。ダウンロード。パック。
		*/
		private System.Collections.IEnumerator DoLoadUrlPack()
		{
			Coroutine_LoadPack t_coroutine = new Coroutine_LoadPack();
			yield return t_coroutine.CoroutineMain(this,this.request_path,this.request_post_data,this.request_certificate_handler,false,this.request_data_version);

			if(t_coroutine.result.pack != null){
				this.result_progress = 1.0f;
				this.result_pack = t_coroutine.result.pack;
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Pack;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Error;
				yield break;
			}
		}
	}
}

