

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。自己証明書。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** CustomCertificateHandler
	*/
	public class CustomCertificateHandler : UnityEngine.Networking.CertificateHandler
	{
		/** certificate_string
		*/
		private string certificate_string;

		/** certificate_binary
		*/
		private byte[] certificate_binary;

		/** errorstring
		*/
		private string errorstring;

		/** receive_certificate_string
		*/
		private string receive_certificate_string;

		/** constructor
		*/
		public CustomCertificateHandler(string a_certificate_string)
		{
			//certificate_string
			this.certificate_string = a_certificate_string;

			//certificate_binary
			this.certificate_binary = null;

			//errorstring
			this.errorstring = null;

			//receive_certificate_string
			this.receive_certificate_string = null;
		}

		/** constructor
		*/
		public CustomCertificateHandler(byte[] a_certificate_binary)
		{
			//certificate_string
			this.certificate_string = null;

			//certificate_binary
			this.certificate_binary = a_certificate_binary;

			//errorstring
			this.errorstring = null;

			//receive_certificate_string
			this.receive_certificate_string = null;
		}

		/** GetErrorString
		*/
		public string GetErrorString()
		{
			return this.errorstring;
		}

		/** GetReceiveCertificateString
		*/
		public string GetReceiveCertificateString()
		{
			return this.receive_certificate_string;
		}

		/** 初期化チェック。
		*/
		public void InitializeCheck()
		{
			Tool.Assert(this.errorstring == null);
			this.errorstring = "Initialize";
		}

		/** ValidateCertificate
		*/
		protected override bool ValidateCertificate(byte[] a_certificate_data)
		{
			this.errorstring = "Call ValidateCertificate";

			try{
				using(System.Security.Cryptography.X509Certificates.X509Certificate2 t_certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(a_certificate_data)){

					this.receive_certificate_string = t_certificate.GetPublicKeyString();

					if(this.certificate_string != null){
						//文字列。

						if(this.receive_certificate_string == this.certificate_string){
							this.errorstring = "Success(String Certificate)";
							return true;
						}else{
							this.errorstring = "Mismatch String Error(String Certificate)";
						}
					}else if(this.certificate_binary != null){
						//バイナリ。

						byte[] t_binary = t_certificate.GetPublicKey();
						if(t_binary.Length == this.certificate_binary.Length){
							bool t_check = true;
							for(int ii=0;ii<t_binary.Length;ii++){
								if(t_binary[ii] != this.certificate_binary[ii]){
									t_check = false;
									break;
								}
							}
							if(t_check == true){
								this.errorstring = "Success(Binary Certificate)";
								return true;
							}else{
								this.errorstring = "Mismatch Binary Error(Binary Certificate)";
							}
						}else{
							this.errorstring = "Mismatch Binary Length Error(Binary Certificate)";
						}
					}else{
						//不明。

						this.errorstring = "Unknown Type Error";
					}
				}
			}catch(System.Exception t_exception){
				this.errorstring = "Exception : " + t_exception.Message;
			}

			return false;
		}
	}
}

