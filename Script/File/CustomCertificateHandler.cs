

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

		/** receive_certificate_string
		*/
		private string receive_certificate_string;

		/** constructor
		*/
		public CustomCertificateHandler(string a_certificate_string)
		{
			Tool.Log("CustomCertificateHandler.constructor",a_certificate_string);

			//certificate_string
			this.certificate_string = a_certificate_string;

			//certificate_binary
			this.certificate_binary = null;

			//receive_certificate_string
			this.receive_certificate_string = null;
		}

		/** constructor
		*/
		public CustomCertificateHandler(byte[] a_certificate_binary)
		{
			Tool.Log("CustomCertificateHandler.constructor","binary");

			//certificate_string
			this.certificate_string = null;

			//certificate_binary
			this.certificate_binary = a_certificate_binary;

			//receive_certificate_string
			this.receive_certificate_string = null;
		}

		/** GetReceiveCertificateString
		*/
		public string GetReceiveCertificateString()
		{
			return this.receive_certificate_string;
		}

		/** ValidateCertificate
		*/
		protected override bool ValidateCertificate(byte[] a_certificate_data)
		{
			Tool.Log("CustomCertificateHandler","ValidateCertificate");

			try{
				using(System.Security.Cryptography.X509Certificates.X509Certificate2 t_certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(a_certificate_data)){

					this.receive_certificate_string = t_certificate.GetPublicKeyString();

					if(this.certificate_string != null){
						//文字列。

						if(this.receive_certificate_string == this.certificate_string){
							return true;
						}else{
							Tool.Log("CustomCertificateHandler","Mismatch String Error(String Certificate)");
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
								return true;
							}else{
								Tool.Log("CustomCertificateHandler","Mismatch Binary Error(Binary Certificate)");
							}
						}else{
							Tool.Log("CustomCertificateHandler","Mismatch Binary Length Error(Binary Certificate)");
						}
					}else{
						//不明。

						Tool.Log("CustomCertificateHandler","Unknown Type Error");
					}
				}
			}catch(System.Exception t_exception){
				Tool.Log("CustomCertificateHandler","Exception : " + t_exception.Message);
			}

			return false;
		}
	}
}

