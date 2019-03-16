

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＶＲＭ。タスク。
*/


//Async block lacks `await' operator and will run synchronously.
#pragma warning disable 1998


/** Fee.UniVrm
*/
namespace Fee.UniVrm
{
	/** [タスク]ＶＲＭパース。
	*/
	public class Task_VrmParse
	{
		#if(USE_DEF_FEE_UNIVRM)

		/** TaskMain
		*/
		private static async System.Threading.Tasks.Task<bool> TaskMain(MonoBehaviour_Vrm a_instance,VRM.VRMImporterContext a_context,byte[] a_binary,System.Threading.CancellationToken a_cancel)
		{
			bool t_ret = true;

			//プログレス。
			//Fee.TaskW.TaskW.GetInstance().Post((a_state) => {a_instance.SetProgressFromTask(0.0f);},null);

			try{
				a_context.ParseGlb(a_binary);
			}catch(System.Exception /*t_exception*/){
				t_ret = false;

				//エラー文字列。
				Fee.TaskW.TaskW.GetInstance().Post((a_state) => {
					a_instance.SetErrorStringFromTask("System.Exception");
				},null);
			}

			if(a_cancel.IsCancellationRequested == true){
				t_ret = false;

				//エラー文字列。
				Fee.TaskW.TaskW.GetInstance().Post((a_state) => {
					a_instance.SetErrorStringFromTask("Cancel");
				},null);

				a_cancel.ThrowIfCancellationRequested();
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static Fee.TaskW.Task<bool> Run(MonoBehaviour_Vrm a_instance,VRM.VRMImporterContext a_context,byte[] a_binary,Fee.TaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new Fee.TaskW.Task<bool>(() => {
				return Task_VrmParse.TaskMain(a_instance,a_context,a_binary,t_cancel_token);
			});
		}

		#endif
	}
}

