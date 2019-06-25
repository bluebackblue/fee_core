

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
	/** 暗号化。パブリックキー。
	*/
	public class Task_EncryptPass
	{
		/** ResultType
		*/
		public struct ResultType
		{
			/** binary
			*/
			public byte[] binary;

			/** errorstring
			*/
			public string errorstring;
		}

		/** TaskMain
		*/
		private static async System.Threading.Tasks.Task<ResultType> TaskMain(Fee.Crypt.OnCryptTask_CallBackInterface a_callback_interface,byte[] a_binary,string a_pass,string a_salt,System.Threading.CancellationToken a_cancel)
		{
			ResultType t_ret;
			{
				t_ret.binary = null;
				t_ret.errorstring = null;
			}

			try{
				//RijndaelManaged
				System.Security.Cryptography.RijndaelManaged t_rijndael = new System.Security.Cryptography.RijndaelManaged();
				t_rijndael.KeySize = 256;
				t_rijndael.BlockSize = 128;
				t_rijndael.Mode = System.Security.Cryptography.CipherMode.CBC;

				{
					byte[] t_salt = System.Text.Encoding.UTF8.GetBytes(a_salt);
					System.Security.Cryptography.Rfc2898DeriveBytes t_derivebyte = new System.Security.Cryptography.Rfc2898DeriveBytes(a_pass,t_salt);
					t_derivebyte.IterationCount = 1000;

					t_rijndael.Key = t_derivebyte.GetBytes(t_rijndael.KeySize / 8);
					t_rijndael.IV = t_derivebyte.GetBytes(t_rijndael.BlockSize / 8);

					Tool.Log("Key",System.BitConverter.ToString(t_rijndael.Key));
					Tool.Log("IV",System.BitConverter.ToString(t_rijndael.IV));
				}

				//TransformFinalBlock
				using(System.Security.Cryptography.ICryptoTransform t_encryptor = t_rijndael.CreateEncryptor()){
					t_ret.binary = t_encryptor.TransformFinalBlock(a_binary,0,a_binary.Length);
				}
			}catch(System.Exception t_exception){
				t_ret.binary = null;
				t_ret.errorstring = "Task_EncryptPass : " + t_exception.Message;
			}

			if(a_cancel.IsCancellationRequested == true){
				t_ret.binary = null;
				t_ret.errorstring = "Task_EncryptPass : Cancel";

				a_cancel.ThrowIfCancellationRequested();
			}

			if(t_ret.binary == null){
				if(t_ret.errorstring == null){
					t_ret.errorstring = "Task_EncryptPass : null";
				}
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static Fee.TaskW.Task<ResultType> Run(Fee.Crypt.OnCryptTask_CallBackInterface a_callback_interface,byte[] a_binary,string a_pass,string a_salt,Fee.TaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new Fee.TaskW.Task<ResultType>(() => {
				return Task_EncryptPass.TaskMain(a_callback_interface,a_binary,a_pass,a_salt,t_cancel_token);
			});
		}
	}
}

