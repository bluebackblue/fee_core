

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。ワーク。
*/


/** Fee.Data
*/
namespace Fee.Data
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
			/*
			Do_Security,
			*/

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

			#if(false)
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
			#endif
		};

		/** mode
		*/
		private Mode mode;

		/** request_type
		*/
		private RequestType request_type;

		/** request_binary
		*/
		/*
		private byte[] request_binary;
		*/

		/** request_key
		*/
		/*
		private string request_key;
		*/

		/** request_signature_binary
		*/
		/*
		private byte[] request_signature_binary;
		*/

		/** request_pass
		*/
		/*
		private string request_pass;
		*/

		/** request_salt
		*/
		/*
		private string request_salt;
		*/

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
			/*
			this.request_binary = null;
			*/

			//request_key
			/*
			this.request_key = null;
			*/

			//request_pass
			/*
			this.request_pass = null;
			*/

			//request_salt
			/*
			this.request_salt = null;
			*/

			//item
			this.item = new Item();
		}

		/** リクエスト。暗号化。パブリックキー。
		*/
		/*
		public void RequestEncryptPublicKey(byte[] a_binary,string a_key)
		{
			this.request_type = RequestType.EncryptPublicKey;
			this.request_binary = a_binary;
			this.request_key = a_key;
		}
		*/

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** 更新。

			return == true : 完了。

		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Start:
				{
					switch(this.request_type){

					/*
					case RequestType.EncryptPublicKey:
						{
							if(Fee.Crypt.Crypt.GetInstance().GetMainSecurity().RequestEncryptPublicKey(this.request_binary,this.request_key) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					case RequestType.DecryptPrivateKey:
						{
							if(Fee.Crypt.Crypt.GetInstance().GetMainSecurity().RequestDecryptPrivateKey(this.request_binary,this.request_key) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					case RequestType.CreateSignaturePrivateKey:
						{
							if(Fee.Crypt.Crypt.GetInstance().GetMainSecurity().RequestCreateSignaturePrivateKey(this.request_binary,this.request_key) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					case RequestType.VerifySignaturePublicKey:
						{
							if(Fee.Crypt.Crypt.GetInstance().GetMainSecurity().RequestVerifySignaturePublicKey(this.request_binary,this.request_signature_binary,this.request_key) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					case RequestType.EncryptPass:
						{
							if(Fee.Crypt.Crypt.GetInstance().GetMainSecurity().RequestEncryptPass(this.request_binary,this.request_pass,this.request_salt) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					case RequestType.DecryptPass:
						{
							if(Fee.Crypt.Crypt.GetInstance().GetMainSecurity().RequestDecryptPass(this.request_binary,this.request_pass,this.request_salt) == true){
								this.mode = Mode.Do_Security;
							}
						}break;
					*/

					}
				}break;
			case Mode.End:
				{
				}return true;

				/*
			case Mode.Do_Security:
				{
					Main_Security t_main = Fee.Crypt.Crypt.GetInstance().GetMainSecurity();

					this.item.SetResultProgress(t_main.GetResultProgress());

					if(t_main.GetResultType() != Main_Security.ResultType.None){
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_Security.ResultType.Binary:
							{
								if(t_main.GetResultBinary() != null){
									this.item.SetResultBinary(t_main.GetResultBinary());
									t_success = true;
								}
							}break;
						case Main_Security.ResultType.VerifySuccess:
							{
								this.item.SetResultVerifySuccess();
								t_success = true;
							}break;
						}

						if(t_success == false){
							this.item.SetResultErrorString(t_main.GetResultErrorString());
						}

						//完了。
						t_main.Fix();

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_main.Cancel();
					}
				}break;
				*/
			}

			return false;
		}
	}
}

