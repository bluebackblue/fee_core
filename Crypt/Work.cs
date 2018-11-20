using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 暗号。ワーク。
*/


/** NCrypt
*/
namespace NCrypt
{
	/** Work
	*/
	public class Work
	{
		/** Mode
		*/
		private enum Mode
		{
			/** 開始。
			*/
			Start,

			/** Do_Security
			*/
			Do_Security,

			/** 完了。
			*/
			End
		};

		/** RequestType
		*/
		private enum RequestType
		{
			/** None
			*/
			None,

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

		/** mode
		*/
		private Mode mode;

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

		/** item
		*/
		private Item item;

		/** constructor
		*/
		public Work()
		{
			//mode
			this.mode = Mode.Start;

			//request_type
			this.request_type = RequestType.None;

			//request_binary
			this.request_binary = null;

			//request_key
			this.request_key = null;

			//request_pass
			this.request_pass = null;

			//request_salt
			this.request_salt = null;

			//item
			this.item = new Item();
		}

		/** リクエスト。暗号化。パブリックキー。
		*/
		public void RequestEncryptPublicKey(byte[] a_binary,string a_key)
		{
			this.request_type = RequestType.EncryptPublicKey;
			this.request_binary = a_binary;
			this.request_key = a_key;
		}

		/** リクエスト。複合化。プライベートキー。
		*/
		public void RequestDecryptPrivateKey(byte[] a_binary,string a_key)
		{
			this.request_type = RequestType.DecryptPrivateKey;
			this.request_binary = a_binary;
			this.request_key = a_key;
		}

		/** リクエスト。証明作成。プライベートキー。
		*/
		public void RequestCreateSignaturePrivateKey(byte[] a_binary,string a_key)
		{
			this.request_type = RequestType.CreateSignaturePrivateKey;
			this.request_binary = a_binary;
			this.request_key = a_key;
		}

		/** リクエスト。署名検証。パブリックキー。
		*/
		public void RequestVerifySignaturePublicKey(byte[] a_binary,byte[] a_signature_binary,string a_key)
		{
			this.request_type = RequestType.VerifySignaturePublicKey;
			this.request_binary = a_binary;
			this.request_signature_binary = a_signature_binary;
			this.request_key = a_key;
		}

		/** リクエスト。暗号化。パス。
		*/
		public void RequestEncryptPass(byte[] a_binary,string a_pass,string a_salt)
		{
			this.request_type = RequestType.EncryptPass;
			this.request_binary = a_binary;
			this.request_pass = a_pass;
			this.request_salt = a_salt;
		}

		/** リクエスト。複合化。パス。
		*/
		public void RequestDecryptPass(byte[] a_binary,string a_pass,string a_salt)
		{
			this.request_type = RequestType.DecryptPass;
			this.request_binary = a_binary;
			this.request_pass = a_pass;
			this.request_salt = a_salt;
		}

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** 更新。

		戻り値 = true : 完了。

		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Start:
				{
					switch(this.request_type){
					case RequestType.EncryptPublicKey:
						{
							MonoBehaviour_Security t_security = NCrypt.Crypt.GetInstance().GetMonoIo();
							if(t_security.RequestEncryptPublicKey(this.request_binary,this.request_key) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					case RequestType.DecryptPrivateKey:
						{
							MonoBehaviour_Security t_security = NCrypt.Crypt.GetInstance().GetMonoIo();
							if(t_security.RequestDecryptPrivateKey(this.request_binary,this.request_key) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					case RequestType.CreateSignaturePrivateKey:
						{
							MonoBehaviour_Security t_security = NCrypt.Crypt.GetInstance().GetMonoIo();
							if(t_security.RequestCreateSignaturePrivateKey(this.request_binary,this.request_key) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					case RequestType.VerifySignaturePublicKey:
						{
							MonoBehaviour_Security t_security = NCrypt.Crypt.GetInstance().GetMonoIo();
							if(t_security.RequestVerifySignaturePublicKey(this.request_binary,this.request_signature_binary,this.request_key) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					case RequestType.EncryptPass:
						{
							MonoBehaviour_Security t_security = NCrypt.Crypt.GetInstance().GetMonoIo();
							if(t_security.RequestEncryptPass(this.request_binary,this.request_pass,this.request_salt) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					case RequestType.DecryptPass:
						{
							MonoBehaviour_Security t_security = NCrypt.Crypt.GetInstance().GetMonoIo();
							if(t_security.RequestDecryptPass(this.request_binary,this.request_pass,this.request_salt) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					}
				}break;
			case Mode.End:
				{
				}return true;
			case Mode.Do_Security:
				{
					MonoBehaviour_Security t_security = NCrypt.Crypt.GetInstance().GetMonoIo();

					this.item.SetResultProgress(t_security.GetResultProgress());

					if(t_security.IsFix() == true){
						//結果。
						bool t_success = false;
						switch(t_security.GetResultType()){
						case MonoBehaviour_Base.ResultType.Binary:
							{
								if(t_security.GetResultBinary() != null){
									this.item.SetResultBinary(t_security.GetResultBinary());
									t_success = true;
								}
							}break;
						case MonoBehaviour_Base.ResultType.VerifySuccess:
							{
								this.item.SetResultVerifySuccess();
								t_success = true;
							}break;
						}

						if(t_success == false){
							this.item.SetResultErrorString(t_security.GetResultErrorString());
						}

						//リクエスト待ち開始。
						t_security.WaitRequest();						

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_security.Cancel();
					}
				}break;
			}

			return false;
		}
	}
}

