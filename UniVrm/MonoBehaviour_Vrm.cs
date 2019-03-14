using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＶＲＭ。ＶＲＭ。
*/


//The private field * is assigned but its value is never used
#pragma warning disable 0414


/** NUniVrm
*/
namespace NUniVrm
{
	/** MonoBehaviour_Vrm
	*/
	public class MonoBehaviour_Vrm : MonoBehaviour_Base
	{
		/** リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ロード。
			*/
			Load,
		};

		/** ProgressStep
		*/
		private enum ProgressStep
		{
			Step0 = 0,
			Step1,

			Max,
		};

		/** Work
		*/
		private class Work
		{
			/** context
			*/
			#if(USE_DEF_UNIVRM)
			public VRM.VRMImporterContext context;
			#endif

			public int progress_step_max;
			public int progress_step;
			public int progress_substep_max;
			public int progress_substep;

			/** constructor
			*/
			public Work()
			{
				#if(USE_DEF_UNIVRM)
				this.context = null;
				#endif

				this.progress_step_max = (int)ProgressStep.Max;
				this.progress_step = (int)ProgressStep.Step0;
				this.progress_substep_max = 1;
				this.progress_substep = 0;
			}
		};

		/** request_type
		*/
		[SerializeField]
		private RequestType request_type;

		/** request_binary
		*/
		[SerializeField]
		private byte[] request_binary;

		/** work
		*/
		[SerializeField]
		private Work work;

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected override void OnInitialize()
		{
			//request_type
			this.request_type = RequestType.None;

			//request_binary
			this.request_binary = null;

			//work
			this.work = null;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.Load:
				{
					Tool.Log("MonoBehaviour_Load",this.request_type.ToString());
					this.work = new Work();
					this.SetModeDo();
				}yield break;
			}

			//不明なリクエスト。
			this.SetResultErrorString("request_type == " + this.request_type.ToString());
			this.SetModeDoError();
			
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected override IEnumerator OnDo()
		{
			{
				#if(USE_DEF_UNIVRM)
				this.work.context = new VRM.VRMImporterContext();
				#endif

				//「ParseGlb」の呼び出し。
				{
					this.work.progress_step = (int)ProgressStep.Step0;
					this.work.progress_substep = 0;
					this.work.progress_substep_max = 1;
					yield return this.Raw_Do_Load_Parse();
					if(this.GetResultType() == ResultType.Error){
						this.SetModeDoError();
						yield break;
					}
				}

				{
					this.work.progress_step = (int)ProgressStep.Step1;
					this.work.progress_substep = 0;
					this.work.progress_substep_max = 1;
					yield return this.Raw_Do_Load_Create();
					if(this.GetResultType() == ResultType.Error){
						this.SetModeDoError();
						yield break;
					}
				}
			}

			#if(USE_DEF_UNIVRM)
			this.SetResultContext(this.work.context);
			#endif

			this.SetModeDoSuccess();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected override IEnumerator OnDoError()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected override IEnumerator OnDoSuccess()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** リクエスト。ロード。
		*/
		public bool RequestLoad(byte[] a_binary)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.Load;
				this.request_binary = a_binary;
				this.work = null;

				return true;
			}

			return false;
		}

		/** プログレス。設定。
		*/
		public void SetProgressFromTask(float a_progress)
		{
			this.SetResultProgress(this.CalcProgress(a_progress));
		}

		/** エラー文字列。設定。
		*/
		public void SetErrorStringFromTask(string a_error_string)
		{
			this.SetResultErrorString(a_error_string);
		}

		/** プログレス計算。
		*/
		private float CalcProgress(float a_progress)
		{
			float t_progress = 0.0f;
			t_progress += ((float)this.work.progress_step) / this.work.progress_step_max;
			t_progress += (a_progress + (float)this.work.progress_substep) / (this.work.progress_step_max * this.work.progress_substep_max);
			return t_progress;
		}

		/** [内部からの呼び出し]ロード。「ParseGlb」の呼び出し。
		*/
		private IEnumerator Raw_Do_Load_Parse()
		{
			#if(USE_DEF_UNIVRM)
			{
				//プログレス。
				this.SetResultProgress(this.CalcProgress(0.0f));

				bool t_result = false;

				{
					//キャンセルトークン。
					NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

					//タスク起動。
					NTaskW.Task<bool> t_task = Task_VrmParse.Run(this,this.work.context,this.request_binary,t_cancel_token);

					//終了待ち。
					do{
						//プログレス。
						this.SetResultProgress(0.5f);

						//キャンセル。
						if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
							t_cancel_token.Cancel();
						}

						yield return null;
					}while(t_task.IsEnd() == false);

					Tool.Log("Raw_Do_Load_Parse","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
					if(t_task.IsSuccess()){
						t_result = t_task.GetResult();
					}else{
						t_result = false;
					}
				}

				//結果。
				if(t_result == true){
					yield break;
				}else{
					if(this.GetResultType() != ResultType.Error){
						this.SetResultErrorString("null");
					}
				}
			}
			#else
			{
				this.SetResultErrorString("null");
			}
			#endif

			yield break;
		}

		/** [内部からの呼び出し]ロード。作成。
		*/
		private IEnumerator Raw_Do_Load_Create()
		{
			#if(USE_DEF_UNIVRM)
			{
				//プログレス。
				this.SetResultProgress(this.CalcProgress(0.0f));

				this.work.context.Load();
			}
			#else
			{
				this.SetResultErrorString("null");
			}
			#endif

			yield break;
		}
	}
}

