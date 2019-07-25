

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
	/** 複合化。プライベートキー。
	*/
	public class Task_DecryptPrivateKey
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
		#if((UNITY_5)||(UNITY_WEBGL))
		private static ResultType TaskMain(Fee.Crypt.OnCryptTask_CallBackInterface a_callback_interface,byte[] a_binary,string a_key,Fee.TaskW.CancelToken a_cancel)
		#else
		private static async System.Threading.Tasks.Task<ResultType> TaskMain(Fee.Crypt.OnCryptTask_CallBackInterface a_callback_interface,byte[] a_binary,string a_key,Fee.TaskW.CancelToken a_cancel)
		#endif
		{
			ResultType t_ret;
			{
				t_ret.binary = null;
				t_ret.errorstring = null;
			}

			try{
				using(System.Security.Cryptography.RSACryptoServiceProvider t_rsa = new System.Security.Cryptography.RSACryptoServiceProvider()){
					t_rsa.FromXmlString(a_key);
					t_ret.binary = t_rsa.Decrypt(a_binary,false);
				}
			}catch(System.Exception t_exception){
				t_ret.binary = null;
				t_ret.errorstring = "Task_DecryptPrivateKey : " + t_exception.Message;
			}

			if(a_cancel.IsCancellationRequested() == true){
				t_ret.binary = null;
				t_ret.errorstring = "Task_DecryptPrivateKey : Cancel";

				a_cancel.ThrowIfCancellationRequested();
			}

			if(t_ret.binary == null){
				if(t_ret.errorstring == null){
					t_ret.errorstring = "Task_DecryptPrivateKey : null";
				}
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static Fee.TaskW.Task<ResultType> Run(Fee.Crypt.OnCryptTask_CallBackInterface a_callback_interface,byte[] a_binary,string a_key,Fee.TaskW.CancelToken a_cancel)
		{
			return new Fee.TaskW.Task<ResultType>(() => {
				return Task_DecryptPrivateKey.TaskMain(a_callback_interface,a_binary,a_key,a_cancel);
			});
		}
	}
}

