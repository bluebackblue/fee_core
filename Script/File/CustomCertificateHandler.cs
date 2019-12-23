

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
	/** 証明書。
	*/
	public class CustomCertificateHandler : UnityEngine.Networking.CertificateHandler
	{
		/** publickey_string
		*/
		private string publickey_string;

		/** publickey_binary
		*/
		private byte[] publickey_binary;

		/** constructor
		*/
		public CustomCertificateHandler(string a_publickey_string)
		{
			//publickey_string
			this.publickey_string = a_publickey_string;

			//publickey_binary
			this.publickey_binary = null;
		}

		/** constructor
		*/
		public CustomCertificateHandler(byte[] a_publickey_binary)
		{
			//publickey_string
			this.publickey_string = null;

			//publickey_binary
			this.publickey_binary = a_publickey_binary;
		}

		/** ValidateCertificate
		*/
		protected override bool ValidateCertificate(byte[] a_certificate_data)
		{
			using(System.Security.Cryptography.X509Certificates.X509Certificate2 t_certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(a_certificate_data)){
				if(this.publickey_string != null){
					if(t_certificate.GetPublicKeyString() == this.publickey_string){
						return true;
					}
				}else if(this.publickey_binary != null){

					byte[] t_binary = t_certificate.GetPublicKey();
					if(t_binary.Length == this.publickey_binary.Length){
						bool t_check = true;
						for(int ii=0;ii<t_binary.Length;ii++){
							if(t_binary[ii] != this.publickey_binary[ii]){
								t_check = false;
								break;
							}
						}
						if(t_check == true){
							return true;
						}
					}
				}
			}

			return false;
		}
	}
}

