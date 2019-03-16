

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 暗号化。
*/


/** Fee.Crypt
*/
namespace Fee.Crypt
{
	/** MonoBehaviour_Security
	*/
	public class MonoBehaviour_Security : MonoBehaviour_Base , OnCoroutine_CallBack
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

			/** 証明作成。プライベートキー。
			*/
			CreateSignaturePrivateKey,

			/** 署名検証。パブリックキー。
			*/
			VerifySignaturePublicKey,

			/** 暗号化。パス。
			*/
			EncryptPass,

			/** 複合化。パス。
			*/
			DecryptPass,
		};

		/** request_type
		*/
		[UnityEngine.SerializeField]
		private RequestType request_type;

		/** request_binary
		*/
		[UnityEngine.SerializeField]
		private byte[] request_binary;

		/** request_key
		*/
		[UnityEngine.SerializeField]
		private string request_key;

		/** request_signature_binary
		*/
		private byte[] request_signature_binary;

		/** request_pass
		*/
		[UnityEngine.SerializeField]
		private string request_pass;

		/** request_salt
		*/
		[UnityEngine.SerializeField]
		private string request_salt;

		/** [Fee.File.OnCoroutine_CallBack]コルーチン実行中。

		戻り値 == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress)
		{
			if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
				return false;
			}

			this.SetResultProgress(a_progress);
			return true;
		}

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected override void OnInitialize()
		{
			//request_type
			this.request_type = RequestType.None;

			//request_binary
			this.request_binary = null;

			//request_key
			this.request_key = null;

			//request_signature_binary
			this.request_signature_binary = null;

			//request_pass
			this.request_pass = null;

			//request_salt
			this.request_salt = null;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override System.Collections.IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.EncryptPublicKey:
			case RequestType.DecryptPrivateKey:
			case RequestType.CreateSignaturePrivateKey:
			case RequestType.VerifySignaturePublicKey:
			case RequestType.EncryptPass:
			case RequestType.DecryptPass:
				{
					Tool.Log("MonoBehaviour_Security",this.request_type.ToString());
					this.SetModeDo();
				}yield break;
			}

			//不明なリクエスト。
			this.SetResultErrorString("request_type == " + this.request_type.ToString());
			this.SetModeDoError();

			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected override System.Collections.IEnumerator OnDo()
		{
			switch(this.request_type){
			case RequestType.EncryptPublicKey:
				{
					Coroutine_EncryptPublicKey t_coroutine = new Coroutine_EncryptPublicKey();
					yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_key);

					if(t_coroutine.result.binary != null){
						this.SetResultBinary(t_coroutine.result.binary);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.DecryptPrivateKey:
				{
					Coroutine_DecryptPrivateKey t_coroutine = new Coroutine_DecryptPrivateKey();
					yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_key);

					if(t_coroutine.result.binary != null){
						this.SetResultBinary(t_coroutine.result.binary);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.CreateSignaturePrivateKey:
				{
					Coroutine_CreateSignaturePrivateKey t_coroutine = new Coroutine_CreateSignaturePrivateKey();
					yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_key);

					if(t_coroutine.result.binary != null){
						this.SetResultBinary(t_coroutine.result.binary);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.VerifySignaturePublicKey:
				{
					Coroutine_VerifySignaturePublicKey t_coroutine = new Coroutine_VerifySignaturePublicKey();
					yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_signature_binary,this.request_key);

					if(t_coroutine.result.verify == true){
						this.SetResultVerifySuccess();
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.EncryptPass:
				{
					Coroutine_EncryptPass t_coroutine = new Coroutine_EncryptPass();
					yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_pass,this.request_salt);

					if(t_coroutine.result.binary != null){
						this.SetResultBinary(t_coroutine.result.binary);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.DecryptPass:
				{
					Coroutine_DecryptPass t_coroutine = new Coroutine_DecryptPass();
					yield return t_coroutine.CoroutineMain(this,this.request_binary,this.request_pass,this.request_salt);

					if(t_coroutine.result.binary != null){
						this.SetResultBinary(t_coroutine.result.binary);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			}

			this.SetModeDoError();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected override System.Collections.IEnumerator OnDoError()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected override System.Collections.IEnumerator OnDoSuccess()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** リクエスト。暗号化。パブリックキー。
		*/
		public bool RequestEncryptPublicKey(byte[] a_binary,string a_key)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.EncryptPublicKey;
				this.request_binary = a_binary;
				this.request_key = a_key;

				return true;
			}

			return false;
		}

		/** リクエスト。複合化。プライベートキー。
		*/
		public bool RequestDecryptPrivateKey(byte[] a_binary,string a_key)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.DecryptPrivateKey;
				this.request_binary = a_binary;
				this.request_key = a_key;

				return true;
			}

			return false;
		}

		/** リクエスト。署名作成。プライベートキー。
		*/
		public bool RequestCreateSignaturePrivateKey(byte[] a_binary,string a_key)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.CreateSignaturePrivateKey;
				this.request_binary = a_binary;
				this.request_key = a_key;

				return true;
			}

			return false;
		}

		/** リクエスト。署名検証。パブリックキー。
		*/
		public bool RequestVerifySignaturePublicKey(byte[] a_binary,byte[] a_signature_binary,string a_key)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.VerifySignaturePublicKey;
				this.request_binary = a_binary;
				this.request_signature_binary = a_signature_binary;
				this.request_key = a_key;

				return true;
			}

			return false;
		}

		/** リクエスト。暗号化。パス。
		*/
		public bool RequestEncryptPass(byte[] a_binary,string a_pass,string a_salt)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.EncryptPass;
				this.request_binary = a_binary;
				this.request_pass = a_pass;
				this.request_salt = a_salt;

				return true;
			}

			return false;
		}

		/** リクエスト。複合化。パス。
		*/
		public bool RequestDecryptPass(byte[] a_binary,string a_pass,string a_salt)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.DecryptPass;
				this.request_binary = a_binary;
				this.request_pass = a_pass;
				this.request_salt = a_salt;

				return true;
			}

			return false;
		}
	}
}

