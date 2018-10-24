using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。タスク。
*/


//Async block lacks `await' operator and will run synchronously.
#pragma warning disable 1998


/** NCrypt
*/
namespace NCrypt
{
	/** 複合化。プライベートキー。
	*/
	public class Task_DecryptPass
	{
		/** ResultType
		*/
		public struct ResultType
		{
			public byte[] binary;
			public string errorstring;
		}

		/** TaskMain
		*/
		private static async System.Threading.Tasks.Task<ResultType> TaskMain(byte[] a_binary,string a_pass,string a_salt,System.Threading.CancellationToken a_cancel)
		{
			ResultType t_ret;
			{
				t_ret.binary = null;
				t_ret.errorstring = null;
			}

			try{
				//RijndaelManaged
				System.Security.Cryptography.RijndaelManaged t_rijndael = new System.Security.Cryptography.RijndaelManaged();

				{
					byte[] t_salt = System.Text.Encoding.UTF8.GetBytes(a_salt);
					System.Security.Cryptography.Rfc2898DeriveBytes t_derivebyte = new System.Security.Cryptography.Rfc2898DeriveBytes(a_pass,t_salt);
					t_derivebyte.IterationCount = 1000;
					t_rijndael.Key = t_derivebyte.GetBytes(t_rijndael.KeySize / 8);
					t_rijndael.IV = t_derivebyte.GetBytes(t_rijndael.BlockSize / 8);
				}

				//TransformFinalBlock
				using(System.Security.Cryptography.ICryptoTransform t_decryptor = t_rijndael.CreateDecryptor()){
					t_ret.binary = t_decryptor.TransformFinalBlock(a_binary,0,a_binary.Length);
				}
			}catch(System.Exception t_exception){
				t_ret.binary = null;
				t_ret.errorstring = "Task_DecryptPass : " + t_exception.Message;
			}

			if(a_cancel.IsCancellationRequested == true){
				t_ret.binary = null;
				t_ret.errorstring = "Task_DecryptPass : Cancel";

				a_cancel.ThrowIfCancellationRequested();
			}

			if(t_ret.binary == null){
				if(t_ret.errorstring == null){
					t_ret.errorstring = "Task_DecryptPass : null";
				}
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static NTaskW.Task<ResultType> Run(byte[] a_binary,string a_pass,string a_salt,NTaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new NTaskW.Task<ResultType>(() => {
				return Task_DecryptPass.TaskMain(a_binary,a_pass,a_salt,t_cancel_token);
			});
		}
	}
}

