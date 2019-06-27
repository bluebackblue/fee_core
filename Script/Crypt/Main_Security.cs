

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 暗号化。
*/


/** Fee.Crypt
*/
namespace Fee.Crypt
{
	/** Main_Security
	*/
	public class Main_Security : Fee.Crypt.OnCryptCoroutine_CallBackInterface
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** 暗号化。パブリックキー。
			*/
			EncryptPublicKey,

			/** 複合化。プライベートキー。
			*/
			DecryptPrivateKey,

			/** 証明書作成。プライベートキー。
			*/
			CreateSignaturePrivateKey,

			/** 証明書検証。パブリックキー。
			*/
			VerifySignaturePublicKey,

			/** 暗号化。パス。
			*/
			EncryptPass,

			/** 複合化。パス。
			*/
			DecryptPass,
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

			/** バイナリ。
			*/
			Binary,

			/** 検証成功。
			*/
			VerifySuccess,
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

		/** request_binary
		*/
		private byte[] request_binary;

		/** request_key
		*/
		private string request_key;

		/** request_signature_binary
		*/
		private byte[] request_signature_binary;

		/** request_pass
		*/
		private string request_pass;

		/** request_salt
		*/
		private string request_salt;

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_binary
		*/
		private byte[] result_binary;

		/** constructor
		*/
		public Main_Security()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_type = RequestType.None;
			this.request_binary = null;
			this.request_key = null;
			this.request_signature_binary = null;
			this.request_pass = null;
			this.request_salt = null;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_binary = null;
		}

		/** Delete
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

		/** GetResultBinary
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}

		/** [Fee.File.OnCryptCoroutine_CallBack]コルーチンからのコールバック。

			return == false : キャンセル。

		*/
		public bool OnCryptCoroutine(float a_progress)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress = a_progress;
			return true;
		}

		/** リクエスト。暗号化。パブリックキー。
		*/
		public bool RequestEncryptPublicKey(byte[] a_binary,string a_key)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;

				//request
				this.request_type = RequestType.EncryptPublicKey;
				this.request_binary = a_binary;
				this.request_key = a_key;
				this.request_signature_binary = null;
				this.request_pass = null;
				this.request_salt = null;

				Function.Function.StartCoroutine(this.DoEncryptPublicKey());
				return true;
			}

			return false;
		}

		/** 実行。暗号化。パブリックキー。
		*/
		private System.Collections.IEnumerator DoEncryptPublicKey()
		{
			Tool.Assert(this.request_type == RequestType.EncryptPublicKey);

			Coroutine_EncryptPublicKey t_coroutine = new Coroutine_EncryptPublicKey();
			yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_key);

			if(t_coroutine.result.binary != null){
				this.result_progress = 1.0f;
				this.result_binary = t_coroutine.result.binary;
				this.result_type = ResultType.Binary;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。複合化。プライベートキー。
		*/
		public bool RequestDecryptPrivateKey(byte[] a_binary,string a_key)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;

				//request
				this.request_type = RequestType.DecryptPrivateKey;
				this.request_binary = a_binary;
				this.request_key = a_key;
				this.request_signature_binary = null;
				this.request_pass = null;
				this.request_salt = null;

				Function.Function.StartCoroutine(this.DoDecryptPrivateKey());
				return true;
			}

			return false;
		}

		/** 実行。複合化。プライベートキー。
		*/
		private System.Collections.IEnumerator DoDecryptPrivateKey()
		{
			Tool.Assert(this.request_type == RequestType.DecryptPrivateKey);

			Coroutine_DecryptPrivateKey t_coroutine = new Coroutine_DecryptPrivateKey();
			yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_key);

			if(t_coroutine.result.binary != null){
				this.result_progress = 1.0f;
				this.result_binary = t_coroutine.result.binary;
				this.result_type = ResultType.Binary;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。証明書作成。プライベートキー。
		*/
		public bool RequestCreateSignaturePrivateKey(byte[] a_binary,string a_key)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;

				//request
				this.request_type = RequestType.CreateSignaturePrivateKey;
				this.request_binary = a_binary;
				this.request_key = a_key;
				this.request_signature_binary = null;
				this.request_pass = null;
				this.request_salt = null;

				Function.Function.StartCoroutine(this.DoCreateSignaturePrivateKey());
				return true;
			}

			return false;
		}

		/** 実行。証明書作成。プライベートキー。
		*/
		private System.Collections.IEnumerator DoCreateSignaturePrivateKey()
		{
			Tool.Assert(this.request_type == RequestType.CreateSignaturePrivateKey);

			Coroutine_CreateSignaturePrivateKey t_coroutine = new Coroutine_CreateSignaturePrivateKey();
			yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_key);

			if(t_coroutine.result.binary != null){
				this.result_progress = 1.0f;
				this.result_binary = t_coroutine.result.binary;
				this.result_type = ResultType.Binary;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。証明書検証。パブリックキー。
		*/
		public bool RequestVerifySignaturePublicKey(byte[] a_binary,byte[] a_signature_binary,string a_key)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;

				//request
				this.request_type = RequestType.VerifySignaturePublicKey;
				this.request_binary = a_binary;
				this.request_key = a_key;
				this.request_signature_binary = a_signature_binary;
				this.request_pass = null;
				this.request_salt = null;

				Function.Function.StartCoroutine(this.DoVerifySignaturePublicKey());
				return true;
			}

			return false;
		}

		/** 実行。証明書検証。パブリックキー。
		*/
		private System.Collections.IEnumerator DoVerifySignaturePublicKey()
		{
			Tool.Assert(this.request_type == RequestType.VerifySignaturePublicKey);

			Coroutine_VerifySignaturePublicKey t_coroutine = new Coroutine_VerifySignaturePublicKey();
			yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_signature_binary,this.request_key);

			if(t_coroutine.result.verify == true){
				this.result_progress = 1.0f;
				this.result_type = ResultType.VerifySuccess;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。暗号化。パス。
		*/
		public bool RequestEncryptPass(byte[] a_binary,string a_pass,string a_salt)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;

				//request
				this.request_type = RequestType.EncryptPass;
				this.request_binary = a_binary;
				this.request_key = null;
				this.request_signature_binary = null;
				this.request_pass = a_pass;
				this.request_salt = a_salt;

				Function.Function.StartCoroutine(this.DoEncryptPass());
				return true;
			}

			return false;
		}

		/** 実行。暗号化。パス。
		*/
		private System.Collections.IEnumerator DoEncryptPass()
		{
			Tool.Assert(this.request_type == RequestType.EncryptPass);

			Coroutine_EncryptPass t_coroutine = new Coroutine_EncryptPass();
			yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_pass,this.request_salt);

			if(t_coroutine.result.binary != null){
				this.result_progress = 1.0f;
				this.result_binary = t_coroutine.result.binary;
				this.result_type = ResultType.Binary;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。複合化。パス。
		*/
		public bool RequestDecryptPass(byte[] a_binary,string a_pass,string a_salt)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;

				//request
				this.request_type = RequestType.DecryptPass;
				this.request_binary = a_binary;
				this.request_key = null;
				this.request_signature_binary = null;
				this.request_pass = a_pass;
				this.request_salt = a_salt;

				Function.Function.StartCoroutine(this.DoDecryptPass());
				return true;
			}

			return false;
		}

		/** [実行。複合化。パス。
		*/
		private System.Collections.IEnumerator DoDecryptPass()
		{
			Tool.Assert(this.request_type == RequestType.DecryptPass);

			Coroutine_DecryptPass t_coroutine = new Coroutine_DecryptPass();
			yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_pass,this.request_salt);

			if(t_coroutine.result.binary != null){
				this.result_progress = 1.0f;
				this.result_binary = t_coroutine.result.binary;
				this.result_type = ResultType.Binary;
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

