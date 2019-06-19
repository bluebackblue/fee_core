

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。タスク。
*/


//Async block lacks await operator and will run synchronously.
#pragma warning disable 1998


/** Fee.Crypt
*/
namespace Fee.Crypt
{
	/** 証明書検証。パブリックキー。
	*/
	public class Task_VerifySignaturePublicKey
	{
		/** ResultType
		*/
		public struct ResultType
		{
			/** verify
			*/
			public bool verify;

			/** errorstring
			*/
			public string errorstring;
		}

		/** TaskMain
		*/
		private static async System.Threading.Tasks.Task<ResultType> TaskMain(byte[] a_binary,byte[] a_signature_binary,string a_key,System.Threading.CancellationToken a_cancel)
		{
			ResultType t_ret;
			{
				t_ret.verify = false;
				t_ret.errorstring = null;
			}

			try{
				//ハッシュの計算。
				byte[] t_hash_binary = null;
				using(System.Security.Cryptography.SHA1Managed t_sha1 = new System.Security.Cryptography.SHA1Managed()){
					t_hash_binary = t_sha1.ComputeHash(a_binary);
				}

				if(t_hash_binary == null){
					t_ret.verify = false;
					t_ret.errorstring = "Task_VerifySignaturePublicKey : hash == null";
				}else{
					using(System.Security.Cryptography.RSACryptoServiceProvider t_rsa = new System.Security.Cryptography.RSACryptoServiceProvider()){
						t_rsa.FromXmlString(a_key);

						//証明書検証。
						System.Security.Cryptography.RSAPKCS1SignatureDeformatter t_deformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(t_rsa);
						t_deformatter.SetHashAlgorithm("SHA1");
						t_ret.verify = t_deformatter.VerifySignature(t_hash_binary,a_signature_binary);
					}
				}
			}catch(System.Exception t_exception){
				t_ret.verify = false;
				t_ret.errorstring = "Task_VerifySignaturePublicKey : " + t_exception.Message;
			}

			if(a_cancel.IsCancellationRequested == true){
				t_ret.verify = false;
				t_ret.errorstring = "Task_VerifySignaturePublicKey : Cancel";

				a_cancel.ThrowIfCancellationRequested();
			}

			if(t_ret.verify == false){
				if(t_ret.errorstring == null){
					t_ret.errorstring = "Task_VerifySignaturePublicKey : null";
				}
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static Fee.TaskW.Task<ResultType> Run(byte[] a_binary,byte[] a_signature_binary,string a_key,Fee.TaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new Fee.TaskW.Task<ResultType>(() => {
				return Task_VerifySignaturePublicKey.TaskMain(a_binary,a_signature_binary,a_key,t_cancel_token);
			});
		}
	}
}

